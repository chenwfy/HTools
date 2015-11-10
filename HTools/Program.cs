using System;
using System.Threading;
using System.Windows.Forms;

namespace HTools
{
    static class Program
    {
        public static EventWaitHandle ProgramStarted;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 判断程序是否已在运行，是则阻止并发出通知
            //此段代码来自万能的互联网

            bool createNew;
            ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, Application.ProductName, out createNew);
            if (!createNew)
            {
                ProgramStarted.Set();
                return;
            }

            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
