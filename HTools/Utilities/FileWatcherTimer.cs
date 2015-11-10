using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace HTools.Utilities
{
    /// <summary>
    /// 文件变化监控实时处理类（此类代码来自万能的互联网）
    /// </summary>
    public class FileWatcherTimer
    {
        private int timeout = 2000;
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private Timer timer = null;
        private List<string> files = new List<string>();
        private FileSystemEventHandler watcherHandler = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="watchHandler"></param>
        public FileWatcherTimer(FileSystemEventHandler watchHandler)
        {
            timer = new Timer(new TimerCallback(OnTimer), null, Timeout.Infinite, Timeout.Infinite);
            watcherHandler = watchHandler;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="watchHandler"></param>
        /// <param name="timerInterval"></param>
        public FileWatcherTimer(FileSystemEventHandler watchHandler, int timerInterval)
        {
            timer = new Timer(new TimerCallback(OnTimer), null, Timeout.Infinite, Timeout.Infinite);
            timeout = timerInterval;
            watcherHandler = watchHandler;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            if (!files.Contains(e.Name))
            {
                files.Add(e.Name);
            }
            mutex.ReleaseMutex();

            timer.Change(timeout, Timeout.Infinite);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void OnTimer(object state)
        {
            List<string> backup = new List<string>();

            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            backup.AddRange(files);
            files.Clear();
            mutex.ReleaseMutex();

            foreach (string file in backup)
            {
                watcherHandler(this, new FileSystemEventArgs(WatcherChangeTypes.Changed, string.Empty, file));
            }
        }
    }
}