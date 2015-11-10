using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using HTools.Entities;
using HTools.Utilities;

namespace HTools
{
    /// <summary>
    /// 主窗口类
    /// </summary>
    public partial class MainForm : Form
    {
        private const string JsTextBoxDefaultContent = "此处可以直接复制粘贴（写入）JS 代码进行压缩，也可以通过选择JS文件（可多选）进行压缩！\r\n\r\n特别提醒：待压缩的代码或者文件内容中，不能包含已被相同方式（UglifyJs）压缩过的代码，否则可能会导致压缩结果异常！";
        private const string CssTextBoxDefaultContent = "此处可以直接复制粘贴（写入）CSS 代码进行压缩，也可以通过选择CSS文件（可多选）进行压缩！";
        private const string LastTaskConfigFileName = "LastTask.config";
        private static string LastTaskConfigFileNamePath;
        private FileWatcherTimer jsWatcher = null;
        private FileSystemWatcher jsFileWatcher = null;
        private FileWatcherTimer cssWatcher = null;
        private FileSystemWatcher cssFileWatcher = null;
        private delegate void AppendTaskResultText(Control render, string resultText);

        /// <summary>
        /// 主窗口类构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            ThreadPool.RegisterWaitForSingleObject(Program.ProgramStarted, OnProgramStarted, null, -1, false);

            LastTaskConfigFileNamePath = Path.Combine(Application.StartupPath, LastTaskConfigFileName);

            BindWindowStatusEvent();
            BindTextBoxSelectAllEvent(this.Controls);
            BindControlsEnvet();

            LoadLastTaskConfig();
        }

        #region 重写窗口关闭事件 

        /// <summary>
        /// 重写窗口关闭事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            const long SC_MINIMIZE = 0xF020;
            if (m.Msg == WM_SYSCOMMAND && ((int)m.WParam == SC_CLOSE || (long)m.WParam == SC_MINIMIZE))
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                return;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Common

        /// <summary>
        /// 绑定窗口隐藏、显示事件
        /// </summary>
        private void BindWindowStatusEvent()
        {
            this.notifyIcon_Toolbar.Click += new EventHandler((sender, e) =>
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                }
            });

            this.toolStripMenuItem_Exit.Click += new EventHandler((sender, e) =>
            {
                this.Close();
            });

            this.notifyIcon_Toolbar.DoubleClick += new EventHandler((sender, e) =>
            {
                this.Close();
            });
        }

        /// <summary>
        /// 显示系统托盘通知
        /// </summary>
        protected void OnProgramStarted(object state, bool timeout)
        {
            this.notifyIcon_Toolbar.ShowBalloonTip(3000, "HI", "我已经在这里运行啦！表点啦。。", ToolTipIcon.Warning);
        }

        /// <summary>
        /// 遍历并绑定界面所有TextBox的全选（Ctrl + A）事件
        /// </summary>
        /// <param name="controls"></param>
        private void BindTextBoxSelectAllEvent(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    TextBox tc = control as TextBox;
                    tc.KeyDown += new KeyEventHandler((sender, e) =>
                    {
                        if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
                        {
                            ((TextBox)sender).SelectAll();
                        }
                    });

                    if (tc.AllowDrop)
                    {
                        tc.DragEnter += new DragEventHandler((sender, e) =>
                        {
                            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                            {
                                e.Effect = DragDropEffects.Copy;
                            }
                        });
                    }
                }
                else
                {
                    if (control.Controls.Count > 0)
                    {
                        BindTextBoxSelectAllEvent(control.Controls);
                    }
                }
            }
        }

        /// <summary>
        /// 绑定界面控件事件
        /// </summary>
        private void BindControlsEnvet()
        {
            //Image Base64
            this.textBox_ImageFilePath.DragDrop += TextBox_ImageFilePath_DragDrop;
            this.button_ImageFileSelection.Click += Button_ImageFileSelection_Click;

            //Js Compressor
            this.textBox_JsFilePath.DragDrop += TextBox_JsFilePath_DragDrop;
            this.button_JsFileSelection.Click += Button_JsFileSelectio_Click;
            this.button_Js_Do.Click += Button_Js_Do_Click;
            this.textBox_JsResult.Text = JsTextBoxDefaultContent;
            this.textBox_JsResult.Enter += new EventHandler((sender, e) =>
            {
                TextBox tc = (TextBox)sender;
                if (tc.Text.Equals(JsTextBoxDefaultContent))
                {
                    tc.Text = string.Empty;
                }
            });

            //Css Compressor
            this.textBox_cssFilePath.DragDrop += TextBox_CssFilePath_DragDrop;
            this.button_CssFileSelection.Click += Button_CssFileSelectio_Click;
            this.button_Css_Do.Click += Button_Css_Do_Click;
            this.textBox_CssResult.Text = CssTextBoxDefaultContent;
            this.textBox_CssResult.Enter += new EventHandler((sender, e) =>
            {
                TextBox tc = (TextBox)sender;
                if (tc.Text.Equals(CssTextBoxDefaultContent))
                {
                    tc.Text = string.Empty;
                }
            });

            //Folder
            this.textBox_FolderJs_SourcePath.DragDrop += TextBox_FolderJs_SourcePath_DragDrop;            
            this.button_FolderJs_SourceSelection.Click += Button_FolderJs_SourceSelection_Click;
            this.textBox_FolderJs_SourcePath.TextChanged += new EventHandler((sender, e) => { FolderJsPathChanged(); });
            this.textBox_FolderJs_TargetPath.DragDrop += TextBox_FolderJs_TargetPath_DragDrop;
            this.button_FolderJs_TargetSelection.Click += Button_FolderJs_TargetSelection_Click;
            this.textBox_FolderJs_TargetPath.TextChanged += new EventHandler((sender, e) => { FolderJsPathChanged(); });

            this.textBox_FolderCss_SourcePath.DragDrop += TextBox_FolderCss_SourcePath_DragDrop;
            this.button_FolderCss_SourceSelection.Click += Button_FolderCss_SourceSelection_Click;
            this.textBox_FolderCss_SourcePath.TextChanged += new EventHandler((sender, e) => { FolderCssPathChanged(); });
            this.textBox_FolderCss_TargetPath.DragDrop += TextBox_FolderCss_TargetPath_DragDrop;
            this.button_FolderCss_TargetSelection.Click += Button_FolderCss_TargetSelection_Click;
            this.textBox_FolderCss_TargetPath.TextChanged += new EventHandler((sender, e) => { FolderCssPathChanged(); });

            this.button_BeginTask.Click += new EventHandler((sender, e) =>
            {
                FolderTaskBegin();
            });

            this.button_ClearLogs.Click += new EventHandler((sender, e) =>
            {
                this.textBox_TaskResult.Text = string.Empty;
            });
        }

        /// <summary>
        /// 筛选指定的文件路径
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private static IEnumerable<string> FilePathFilter(IEnumerable<string> sourcePath, FileType fileType)
        {
            return sourcePath.Where(p => p.FileExists() && p.IsAllowFile(fileType));
        }

        #endregion

        #region  IMAGE BASE64

        /// <summary>
        /// 文件路径拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_ImageFilePath_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length == 1)
                ImagePathChanged(paths[0]);
            else
                SetImageBase64Content("只允许拖放一个文件！");
        }

        /// <summary>
        /// 浏览文件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ImageFileSelection_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_Image.ShowDialog())
            {
                ImagePathChanged(openFileDialog_Image.FileName);
            }
        }

        /// <summary>
        /// 图片文件路径变化
        /// </summary>
        /// <param name="imageFilePath"></param>
        private async void ImagePathChanged(string imageFilePath)
        {
            if (imageFilePath.FileExists() && imageFilePath.IsAllowFile(FileType.Image))
            {
                this.textBox_ImageFilePath.Text = imageFilePath;
                SetImageBase64Content("正在编码中，请稍候……");
                SetImageBase64Content(await imageFilePath.ImageToBase64());
            }
            else
            {
                SetImageBase64Content("所选文件不存在或者不是有效的图片文件！");
            }
        }

        /// <summary>
        /// 输出图片BASE64编码结果
        /// </summary>
        /// <param name="content"></param>
        private void SetImageBase64Content(string content)
        {
            this.textBox_ImageBase64.Text = content;
        }

        #endregion

        #region Js Compressor

        /// <summary>
        /// JS文件拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_JsFilePath_DragDrop(object sender, DragEventArgs e)
        {
            JsFilesPathChanged(FilePathFilter((string[])e.Data.GetData(DataFormats.FileDrop), FileType.Js));
        }

        /// <summary>
        /// 选择JS文件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_JsFileSelectio_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_Js.ShowDialog())
            {
                JsFilesPathChanged(FilePathFilter(openFileDialog_Js.FileNames, FileType.Js));
            }
        }

        /// <summary>
        /// JS文件路径变化事件
        /// </summary>
        /// <param name="jsFilesPath"></param>
        private void JsFilesPathChanged(IEnumerable<string> jsFilesPath)
        {
            TextBox jsResult = this.textBox_JsResult;
            jsResult.Text = string.Empty;
            int jsFileCount = jsFilesPath.Count();
            if (jsFileCount > 0)
            {
                this.textBox_JsFilePath.Text = string.Join(";", jsFilesPath);
                this.js_radioButton_1.Enabled = true;
                this.js_radioButton_2.Enabled = true;
                this.js_radioButton_3.Enabled = true;

                jsResult.Text = string.Format("共选择 {0} 个JS文件：\r\n", jsFileCount);
                jsResult.Text += string.Join("\r\n", jsFilesPath);
                jsResult.Text += "\r\n开始压缩，请稍候：\r\n\r\n";
            }
            else
            {
                this.textBox_JsFilePath.Text = string.Empty;
                this.js_radioButton_1.Enabled = false;
                this.js_radioButton_2.Enabled = false;
                this.js_radioButton_3.Enabled = false;
                jsResult.Text = "所选文件不存在或者不是有效的JS文件";
            }
        }

        /// <summary>
        /// JS开始压缩按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Js_Do_Click(object sender, EventArgs e)
        {
            TextBox jsResult = this.textBox_JsResult;
            string jsFilePath = this.textBox_JsFilePath.Text.Trim();
            if (!string.IsNullOrEmpty(jsFilePath))
            {
                CompressType ct = this.js_radioButton_1.Checked ? CompressType.FileSingle : (this.js_radioButton_2.Checked ? CompressType.MergeAndCompressed : CompressType.CompressedAndMerge);
                CompressjsCodeFromFiles(jsFilePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), jsResult, ct);
            }
            else
            {
                string sourceJsCode = jsResult.Text.Trim();
                if (!string.IsNullOrEmpty(sourceJsCode) && !sourceJsCode.Equals(JsTextBoxDefaultContent))
                    CompressJsCodeFromString(sourceJsCode, jsResult);
                else
                    jsResult.Text = "没有要压缩的内容！";
            }
        }

        /// <summary>
        /// 从输入的源码压缩JS脚本
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="resultRender"></param>
        private async void CompressJsCodeFromString(string sourceCode, TextBox resultRender)
        {
            resultRender.Text = "正在压缩，请稍候……";
            string resultCode = await sourceCode.ComoressJsCode();
            resultRender.Text = !string.IsNullOrEmpty(resultCode) ? resultCode : "压缩失败！请检查被压缩代码！";
        }

        /// <summary>
        /// 从选择的文件压缩JS脚本
        /// </summary>
        /// <param name="jsFilesPath"></param>
        /// <param name="resultRender"></param>
        /// <param name="ct"></param>
        private async void CompressjsCodeFromFiles(IEnumerable<string> jsFilesPath, TextBox resultRender, CompressType ct)
        {
            #region CompressType.FileSingle

            if (ct == CompressType.FileSingle)
            {
                foreach (var jsFile in jsFilesPath)
                {
                    resultRender.Text += string.Format("正在处理文件 {0} \r\n", jsFile);
                    string sourceCode = await jsFile.ReadFileText();
                    if (!string.IsNullOrEmpty(sourceCode))
                    {
                        string resultCode = await sourceCode.ComoressJsCode();
                        if (!string.IsNullOrEmpty(resultCode) && !sourceCode.Equals(resultCode))
                        {
                            string saveFilePath = new List<string> { jsFile }.GetCompressFileName();
                            if (!string.IsNullOrEmpty(saveFilePath))
                            {
                                await saveFilePath.WriteTxtFile(resultCode);
                                resultRender.Text += string.Format("压缩完成，已经保存至文件 {0} \r\n", saveFilePath);
                            }
                            else
                            {
                                resultRender.Text += string.Format("*** 压缩完成，保存文件失败！ \r\n");
                            }
                        }
                        else
                        {
                            resultRender.Text += string.Format("*** 压缩失败，请检查文件及内容！ \r\n");
                        }
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 读取失败，请检查文件及内容！ \r\n");
                    }
                    resultRender.Text += string.Format("====================================================================\r\n\r\n");
                }
            }

            #endregion 

            #region CompressType.MergeAndCompressed

            if (ct == CompressType.MergeAndCompressed)
            {
                StringBuilder jsBuilder = new StringBuilder();
                string sourceCode;
                foreach (var jsFile in jsFilesPath)
                {
                    resultRender.Text += string.Format("正在读取文件 {0} \r\n", jsFile);
                    sourceCode = await jsFile.ReadFileText();
                    if (!string.IsNullOrEmpty(sourceCode))
                    {
                        jsBuilder.AppendLine(sourceCode);
                        resultRender.Text += "读取完成！\r\n";
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 读取失败，请检查文件及内容！ \r\n");
                    }
                    resultRender.Text += string.Format("-------------------------------\r\n");
                }

                resultRender.Text += "\r\n============== 文件读取完成！开始压缩 ==============\r\n";
                sourceCode = jsBuilder.ToString();

                jsBuilder.Clear();
                jsBuilder = null;

                string resultCode = await sourceCode.ComoressJsCode();
                if (!string.IsNullOrEmpty(resultCode) && !sourceCode.Equals(resultCode))
                {
                    string saveFilePath = jsFilesPath.GetCompressFileName();
                    if (!string.IsNullOrEmpty(saveFilePath))
                    {
                        await saveFilePath.WriteTxtFile(resultCode);
                        resultRender.Text += string.Format("压缩完成，已经保存至文件 {0} \r\n", saveFilePath);
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 压缩完成，保存文件失败！ \r\n");
                    }
                }
                else
                {
                    resultRender.Text += string.Format("*** 压缩失败，请检查文件及内容！ \r\n");
                }
            }

            #endregion 

            #region CompressType.CompressedAndMerge

            if (ct == CompressType.CompressedAndMerge)
            {
                StringBuilder jsBuilder = new StringBuilder();
                string sourceCode, resultCode;

                foreach (var jsFile in jsFilesPath)
                {
                    resultRender.Text += string.Format("正在处理文件 {0} \r\n", jsFile);
                    sourceCode = await jsFile.ReadFileText();
                    if (!string.IsNullOrEmpty(sourceCode))
                    {
                        resultCode = await sourceCode.ComoressJsCode();
                        if (!string.IsNullOrEmpty(resultCode) && !sourceCode.Equals(resultCode))
                        {
                            resultRender.Text += "压缩完成 \r\n";
                            jsBuilder.AppendLine("//" + Path.GetFileName(jsFile));
                            jsBuilder.AppendLine(resultCode);
                        }
                        else
                        {
                            resultRender.Text += string.Format("*** 压缩失败，请检查文件及内容！ \r\n");
                        }
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 读取失败，请检查文件及内容！ \r\n");
                    }
                    resultRender.Text += string.Format("-------------------------------\r\n");
                }

                resultRender.Text += "\r\n============== 文件读取并压缩完成！开始合并及保存 ==============\r\n";
                resultCode = jsBuilder.ToString();

                jsBuilder.Clear();
                jsBuilder = null;

                string saveFilePath = jsFilesPath.GetCompressFileName();
                if (!string.IsNullOrEmpty(saveFilePath))
                {
                    await saveFilePath.WriteTxtFile(resultCode);
                    resultRender.Text += string.Format("合并完成，已经保存至文件 {0} \r\n", saveFilePath);
                }
                else
                {
                    resultRender.Text += string.Format("*** 保存文件失败！ \r\n");
                }
            }

            #endregion 
        }

        #endregion

        #region CSS Compressor

        /// <summary>
        /// Css文件拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_CssFilePath_DragDrop(object sender, DragEventArgs e)
        {
            CssFilesPathChanged(FilePathFilter((string[])e.Data.GetData(DataFormats.FileDrop), FileType.Css));
        }

        /// <summary>
        /// 选择Css文件按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CssFileSelectio_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog_Css.ShowDialog())
            {
                CssFilesPathChanged(FilePathFilter(openFileDialog_Css.FileNames, FileType.Css));
            }
        }

        /// <summary>
        /// Css文件路径变化事件
        /// </summary>
        /// <param name="cssFilesPath"></param>
        private void CssFilesPathChanged(IEnumerable<string> cssFilesPath)
        {
            TextBox cssResult = this.textBox_CssResult;
            cssResult.Text = string.Empty;
            int cssFileCount = cssFilesPath.Count();
            if (cssFileCount > 0)
            {
                this.textBox_cssFilePath.Text = string.Join(";", cssFilesPath);
                this.css_radioButton_1.Enabled = true;
                this.css_radioButton_2.Enabled = true;
                this.css_radioButton_3.Enabled = true;
                this.css_checkBox_1.Enabled = true;
                this.css_numericUpDown_1.Enabled = true;

                cssResult.Text = string.Format("共选择 {0} 个CSS文件：\r\n", cssFileCount);
                cssResult.Text += string.Join("\r\n", cssFilesPath);
                cssResult.Text += "\r\n开始压缩，请稍候：\r\n\r\n";
            }
            else
            {
                this.textBox_cssFilePath.Text = string.Empty;
                this.css_radioButton_1.Enabled = false;
                this.css_radioButton_2.Enabled = false;
                this.css_radioButton_3.Enabled = false;
                this.css_checkBox_1.Enabled = false;
                this.css_numericUpDown_1.Enabled = false;

                cssResult.Text = "所选文件不存在或者不是有效的CSS文件";
            }
        }

        /// <summary>
        /// Css开始压缩按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Css_Do_Click(object sender, EventArgs e)
        {
            TextBox cssResult = this.textBox_CssResult;
            string cssFilePath = this.textBox_cssFilePath.Text.Trim();
            if (!string.IsNullOrEmpty(cssFilePath))
            {
                CompressType ct = this.css_radioButton_1.Checked ? CompressType.FileSingle : (this.css_radioButton_2.Checked ? CompressType.MergeAndCompressed : CompressType.CompressedAndMerge);
                CompressCssCodeFromFiles(cssFilePath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), cssResult, ct);
            }
            else
            {
                string sourceCssCode = cssResult.Text.Trim();
                if (!string.IsNullOrEmpty(sourceCssCode) && !sourceCssCode.Equals(CssTextBoxDefaultContent))
                    CompressCssCodeFromString(sourceCssCode, cssResult);
                else
                    cssResult.Text = "没有要压缩的内容！";
            }
        }

        /// <summary>
        /// 从输入的源码压缩Css脚本
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="resultRender"></param>
        private async void CompressCssCodeFromString(string sourceCode, TextBox resultRender)
        {
            resultRender.Text = "正在压缩，请稍候……";
            string resultCode = await sourceCode.CompressCssFromCode();
            resultRender.Text = !string.IsNullOrEmpty(resultCode) ? resultCode : "压缩失败！请检查被压缩代码！";
        }

        /// <summary>
        /// 从选择的文件压缩Css脚本
        /// </summary>
        /// <param name="cssFilesPath"></param>
        /// <param name="resultRender"></param>
        /// <param name="ct"></param>
        private async void CompressCssCodeFromFiles(IEnumerable<string> cssFilesPath, TextBox resultRender, CompressType ct)
        {
            int toBase64ImgMaxSize = this.css_checkBox_1.Checked ? (int)this.css_numericUpDown_1.Value * 1024 : 0;

            if (ct == CompressType.FileSingle)
            {
                foreach (var cssFile in cssFilesPath)
                {
                    resultRender.Text += string.Format("正在处理文件 {0} \r\n", cssFile);
                    string resultCode = await cssFile.CompressCssFromFile(string.Empty, toBase64ImgMaxSize);
                    if (!string.IsNullOrEmpty(resultCode))
                    {
                        string saveFilePath = new List<string> { cssFile }.GetCompressFileName();
                        if (!string.IsNullOrEmpty(saveFilePath))
                        {
                            await saveFilePath.WriteTxtFile(resultCode);
                            resultRender.Text += string.Format("压缩完成，已经保存至文件 {0} \r\n", saveFilePath);
                        }
                        else
                        {
                            resultRender.Text += string.Format("*** 压缩完成，保存文件失败！ \r\n");
                        }
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 压缩失败，请检查文件及内容！ \r\n");
                    }
                    resultRender.Text += string.Format("====================================================================\r\n\r\n");
                }
            }
            else
            {
                StringBuilder jsBuilder = new StringBuilder();
                string resultCode;

                foreach (var cssFile in cssFilesPath)
                {
                    resultRender.Text += string.Format("正在处理文件 {0} \r\n", cssFile);
                    resultCode = await cssFile.CompressCssFromFile(string.Empty, toBase64ImgMaxSize);
                    if (!string.IsNullOrEmpty(resultCode))
                    {
                        resultRender.Text += "压缩完成 \r\n";
                        if (ct == CompressType.MergeAndCompressed)
                        {
                            jsBuilder.Append(resultCode);
                        }
                        else
                        {
                            jsBuilder.AppendLine("/* " + Path.GetFileName(cssFile) + " */");
                            jsBuilder.AppendLine(resultCode);
                        }
                    }
                    else
                    {
                        resultRender.Text += string.Format("*** 压缩失败，请检查文件及内容！ \r\n");
                    }
                    resultRender.Text += string.Format("-------------------------------\r\n");
                }

                resultRender.Text += "\r\n============== 文件读取并压缩完成！开始合并及保存 ==============\r\n";
                resultCode = jsBuilder.ToString();

                jsBuilder.Clear();
                jsBuilder = null;

                string saveFilePath = cssFilesPath.GetCompressFileName();
                if (!string.IsNullOrEmpty(saveFilePath))
                {
                    await saveFilePath.WriteTxtFile(resultCode);
                    resultRender.Text += string.Format("任务完成，已经保存至文件 {0} \r\n", saveFilePath);
                }
                else
                {
                    resultRender.Text += string.Format("*** 保存文件失败！ \r\n");
                }
            }
        }

        #endregion

        #region 目录批量压缩

        /// <summary>
        /// 读取最后一次任务配置
        /// </summary>
        private async void LoadLastTaskConfig()
        {
            LastTaskConfig config = await LastTaskConfigFileNamePath.ReadLastTaskConfig();
            if (null != config)
            {
                if (this.tabControl.SelectedTab != this.tabPage_Watch)
                {
                    this.tabControl.SelectedTab = this.tabPage_Watch;
                }

                this.textBox_FolderJs_SourcePath.Text = config.JsSourceFolder ?? string.Empty;
                this.textBox_FolderJs_TargetPath.Text = config.JsTargetFolder ?? string.Empty;
                if ((config.JsRenameFixed ?? string.Empty).Equals(string.Empty))
                {
                    this.radioButton_FolderJs_ReName_1.Checked = true;
                    this.radioButton_FolderJs_ReName_2.Checked = false;
                }
                else
                {
                    this.radioButton_FolderJs_ReName_1.Checked = true;
                    this.radioButton_FolderJs_ReName_2.Checked = false;
                }
                FolderJsPathChanged();

                this.textBox_FolderCss_SourcePath.Text = config.CssSourceFolder ?? string.Empty;
                this.textBox_FolderCss_TargetPath.Text = config.CssTargetFolder ?? string.Empty;
                if ((config.CssRenameFixed ?? string.Empty).Equals(string.Empty))
                {
                    this.radioButton_FolderCss_ReName_1.Checked = true;
                    this.radioButton_FolderCss_ReName_2.Checked = false;
                }
                else
                {
                    this.radioButton_FolderCss_ReName_1.Checked = true;
                    this.radioButton_FolderCss_ReName_2.Checked = false;
                }

                int cssImageToBase64MaxSize = config.CssImageToBase64MaxSize == int.MinValue ? 0 : config.CssImageToBase64MaxSize;
                if (cssImageToBase64MaxSize > 0)
                {
                    this.checkBox_FolderCss_ImageToBase64.Enabled = true;
                    this.numericUpDown_FolderCss_ImageToBase64MaxSize.Enabled = true;
                    this.numericUpDown_FolderCss_ImageToBase64MaxSize.Value = (decimal)cssImageToBase64MaxSize;
                }
                else
                {
                    this.checkBox_FolderCss_ImageToBase64.Enabled = false;
                    this.numericUpDown_FolderCss_ImageToBase64MaxSize.Enabled = false;
                    this.numericUpDown_FolderCss_ImageToBase64MaxSize.Value = (decimal)1;
                }

                FolderCssPathChanged();

                if (config.FolderWatched)
                {
                    this.radioButton_TaskType_Watch.Checked = true;
                    this.radioButton_TaskType_OnlyOnce.Checked = false;

                    BeginFolderTask(config);
                }
                else
                {
                    this.radioButton_TaskType_Watch.Checked = false;
                    this.radioButton_TaskType_OnlyOnce.Checked = true;
                }
            }
        }

        /// <summary>
        /// JS文件源目录路径选择拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_FolderJs_SourcePath_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (path.Length > 0)
            {
                this.textBox_FolderJs_SourcePath.Text = path[0];
                FolderJsPathChanged();
            }
        }

        /// <summary>
        /// 选择JS文件源目录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FolderJs_SourceSelection_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.folderBrowserDialog_FolderJs_Source.ShowDialog())
            {
                this.textBox_FolderJs_SourcePath.Text = this.folderBrowserDialog_FolderJs_Source.SelectedPath;
                FolderJsPathChanged();
            }
        }

        /// <summary>
        /// JS文件发布目录路径选择拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_FolderJs_TargetPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (path.Length > 0)
            {
                this.textBox_FolderJs_TargetPath.Text = path[0];
                FolderJsPathChanged();
            }
        }

        /// <summary>
        /// 选择JS文件发布目录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FolderJs_TargetSelection_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.folderBrowserDialog_FolderJs_Target.ShowDialog())
            {
                this.textBox_FolderJs_TargetPath.Text = this.folderBrowserDialog_FolderJs_Target.SelectedPath;
                FolderJsPathChanged();
            }
        }

        /// <summary>
        /// JS目录变化事件
        /// </summary>
        private void FolderJsPathChanged()
        {
            string sourceFolder = this.textBox_FolderJs_SourcePath.Text.Trim();
            string targetFolder = this.textBox_FolderJs_TargetPath.Text.Trim();

            if (!string.IsNullOrEmpty(sourceFolder) && !string.IsNullOrEmpty(targetFolder))
            {
                this.radioButton_FolderJs_ReName_2.Enabled = true;
                if (sourceFolder.Equals(targetFolder))
                {
                    this.radioButton_FolderJs_ReName_1.Enabled = false;
                    this.radioButton_FolderJs_ReName_1.Checked = false;
                    this.radioButton_FolderJs_ReName_2.Checked = true;
                }
                else
                {
                    this.radioButton_FolderJs_ReName_1.Enabled = true;
                }
            }
            else
            {
                this.radioButton_FolderJs_ReName_1.Enabled = false;
                this.radioButton_FolderJs_ReName_2.Enabled = false;
            }
        }

        /// <summary>
        /// Css文件源目录路径选择拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_FolderCss_SourcePath_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (path.Length > 0)
            {
                this.textBox_FolderCss_SourcePath.Text = path[0];
                FolderCssPathChanged();
            }
        }

        /// <summary>
        /// 选择Css文件源目录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FolderCss_SourceSelection_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.folderBrowserDialog_FolderCss_Source.ShowDialog())
            {
                this.textBox_FolderCss_SourcePath.Text = this.folderBrowserDialog_FolderCss_Source.SelectedPath;
                FolderCssPathChanged();
            }
        }

        /// <summary>
        /// Css文件发布目录路径选择拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_FolderCss_TargetPath_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (path.Length > 0)
            {
                this.textBox_FolderCss_TargetPath.Text = path[0];
                FolderCssPathChanged();
            }
        }

        /// <summary>
        /// 选择Css文件发布目录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_FolderCss_TargetSelection_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.folderBrowserDialog_FolderCss_Target.ShowDialog())
            {
                this.textBox_FolderCss_TargetPath.Text = this.folderBrowserDialog_FolderCss_Target.SelectedPath;
                FolderCssPathChanged();
            }
        }

        /// <summary>
        /// Css目录变化事件
        /// </summary>
        private void FolderCssPathChanged()
        {
            string sourceFolder = this.textBox_FolderCss_SourcePath.Text.Trim();
            string targetFolder = this.textBox_FolderCss_TargetPath.Text.Trim();

            if (!string.IsNullOrEmpty(sourceFolder) && !string.IsNullOrEmpty(targetFolder))
            {
                this.radioButton_FolderCss_ReName_2.Enabled = true;
                if (sourceFolder.Equals(targetFolder))
                {
                    this.radioButton_FolderCss_ReName_1.Enabled = false;
                    this.radioButton_FolderCss_ReName_1.Checked = false;
                    this.radioButton_FolderCss_ReName_2.Checked = true;
                }
                else
                {
                    this.radioButton_FolderCss_ReName_1.Enabled = true;
                }

                this.checkBox_FolderCss_ImageToBase64.Enabled = true;
                this.numericUpDown_FolderCss_ImageToBase64MaxSize.Enabled = true;
            }
            else
            {
                this.radioButton_FolderCss_ReName_1.Enabled = false;
                this.radioButton_FolderCss_ReName_2.Enabled = false;
                this.checkBox_FolderCss_ImageToBase64.Enabled = false;
                this.numericUpDown_FolderCss_ImageToBase64MaxSize.Enabled = false;
            }
        }

        /// <summary>
        /// 从目录开始压缩任务开始按钮点击事件
        /// </summary>
        private void FolderTaskBegin()
        {
            LastTaskConfig config = new LastTaskConfig
            {
                JsSourceFolder = this.textBox_FolderJs_SourcePath.Text.Trim(),
                JsTargetFolder = this.textBox_FolderJs_TargetPath.Text.Trim(),
                JsRenameFixed = this.radioButton_FolderJs_ReName_1.Checked ? string.Empty : ".min",
                CssSourceFolder = this.textBox_FolderCss_SourcePath.Text.Trim(),
                CssTargetFolder = this.textBox_FolderCss_TargetPath.Text.Trim(),
                CssRenameFixed = this.radioButton_FolderCss_ReName_1.Checked ? string.Empty : ".min",
                CssImageToBase64MaxSize = this.checkBox_FolderCss_ImageToBase64.Checked ? (int)this.numericUpDown_FolderCss_ImageToBase64MaxSize.Value : 0,
                FolderWatched = this.radioButton_TaskType_Watch.Checked
            };

            if (config.FolderWatched)
            {
                config.SaveLastTaskConfig(LastTaskConfigFileNamePath);
            }
            else
            {
                LastTaskConfigFileNamePath.RemoveLastTaskConfig();
            }
            BeginFolderTask(config);
        }

        /// <summary>
        /// 开始目录任务
        /// </summary>
        /// <param name="config"></param>
        private void BeginFolderTask(LastTaskConfig config)
        {
            TextBox resultRender = this.textBox_TaskResult;
            resultRender.Text = string.Empty;

            DoCompressByFolder(FileType.Css, config.CssSourceFolder, config.CssTargetFolder, resultRender, config.CssImageToBase64MaxSize * 1024, config.CssRenameFixed, config.FolderWatched);
            DoCompressByFolder(FileType.Js, config.JsSourceFolder, config.JsTargetFolder, resultRender, 0, config.JsRenameFixed, config.FolderWatched);
        }

        /// <summary>
        /// 执行目录压缩
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="sourceFolder"></param>
        /// <param name="targetFolder"></param>
        /// <param name="resultRender"></param>
        /// <param name="toBase64ImgMaxSize"></param>
        /// <param name="renameFixed"></param>
        /// <param name="folderWatched"></param>
        private void DoCompressByFolder(FileType fileType, string sourceFolder, string targetFolder, TextBox resultRender, int toBase64ImgMaxSize, string renameFixed, bool folderWatched)
        {
            string folderPrev = "JS", filePattern = "*.js";
            if (fileType == FileType.Css)
            {
                folderPrev = "CSS";
                filePattern = "*.css";
            }

            if (!string.IsNullOrEmpty(sourceFolder) && !string.IsNullOrEmpty(targetFolder) && Directory.Exists(sourceFolder) && Directory.Exists(targetFolder))
            {
                string[] sourceFiles = Directory.GetFiles(sourceFolder, filePattern, SearchOption.TopDirectoryOnly);
                int fileCount = sourceFiles.Length;
                if (sourceFiles.Length == 0)
                {
                    resultRender.Text += folderPrev + "源目录下无符合条件的文件，" + folderPrev + "任务终止！\r\n";
                    resultRender.Text += "=======================================================\r\n\r\n";
                }
                else
                {
                    resultRender.Text += folderPrev + "源目录下共找到文件" + fileCount + "个：\r\n";
                    resultRender.Text += string.Join("\r\n", sourceFiles) + "\r\n";
                    resultRender.Text += "任务开始：\r\n";

                    if (fileType == FileType.Js)
                    {
                        DoJsCompressByFolder(sourceFiles, targetFolder, resultRender, renameFixed);

                        if (folderWatched)
                        {
                            if (null != jsWatcher)
                            {
                                jsWatcher = null;
                            }

                            if (null != jsFileWatcher)
                            {
                                jsFileWatcher.Dispose();
                                jsFileWatcher = null;
                            }

                            jsWatcher = new FileWatcherTimer((sender, e) => { DoJsCompressByFolder(new List<string> { Path.Combine(sourceFolder, e.Name) }, targetFolder, resultRender, renameFixed); }, 200);
                            jsFileWatcher = new FileSystemWatcher(sourceFolder, "*.js");
                            jsFileWatcher.IncludeSubdirectories = false;
                            jsFileWatcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security;
                            jsFileWatcher.EnableRaisingEvents = true;
                            jsFileWatcher.Changed += new FileSystemEventHandler(jsWatcher.OnFileChanged);
                        }
                        else
                        {
                            if (null != jsWatcher)
                            {
                                jsWatcher = null;
                            }

                            if (null != jsFileWatcher)
                            {
                                jsFileWatcher.Dispose();
                                jsFileWatcher = null;
                            }
                        }
                    }

                    if (fileType == FileType.Css)
                    {
                        DoCssCompressByFolder(sourceFiles, targetFolder, resultRender, toBase64ImgMaxSize, renameFixed);
                        if (folderWatched)
                        {
                            if (null != cssWatcher)
                            {
                                cssWatcher = null;
                            }

                            if (null != cssFileWatcher)
                            {
                                cssFileWatcher.Dispose();
                                cssFileWatcher = null;
                            }
                            cssWatcher = new FileWatcherTimer((sender, e) => { DoCssCompressByFolder(new List<string> { Path.Combine(sourceFolder, e.Name) }, targetFolder, resultRender, toBase64ImgMaxSize, renameFixed); }, 200);
                            cssFileWatcher = new FileSystemWatcher(sourceFolder, "*.css");
                            cssFileWatcher.IncludeSubdirectories = false;
                            cssFileWatcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Security;
                            cssFileWatcher.EnableRaisingEvents = true;
                            cssFileWatcher.Changed += new FileSystemEventHandler(cssWatcher.OnFileChanged);
                        }
                        else
                        {
                            if (null != cssWatcher)
                            {
                                cssWatcher = null;
                            }

                            if (null != cssFileWatcher)
                            {
                                cssFileWatcher.Dispose();
                                cssFileWatcher = null;
                            }
                        }
                    }

                    resultRender.Text += "\r\n任务完成！\r\n";
                }
            }
            else
            {
                resultRender.Text += folderPrev + "目录选择不完整或者所选目录不存在！" + folderPrev + "任务终止！\r\n";
                resultRender.Text += "=======================================================\r\n\r\n";
            }
        }

        /// <summary>
        /// 根据目录批量压缩JS文件
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="targetFolder"></param>
        /// <param name="resultRender"></param>
        /// <param name="renameFixed"></param>
        private async void DoJsCompressByFolder(IEnumerable<string> sourceFiles, string targetFolder, TextBox resultRender, string renameFixed)
        {
            string sourceCode, resultCode, saveFilePath;
            foreach (var file in sourceFiles)
            {
                AppendTaskResult(resultRender, string.Format("正在处理文件 {0} \r\n", file));
                sourceCode = await file.ReadFileText();
                if (!string.IsNullOrEmpty(sourceCode))
                {
                    resultCode = await sourceCode.ComoressJsCode();
                    if (!string.IsNullOrEmpty(resultCode) && !sourceCode.Equals(resultCode))
                    {
                        saveFilePath = file.FileRename(targetFolder, renameFixed);
                        await saveFilePath.WriteTxtFile(resultCode);
                        AppendTaskResult(resultRender, string.Format("压缩完成，已经保存至文件 {0} \r\n", saveFilePath));
                    }
                    else
                    {
                        AppendTaskResult(resultRender, " *** 压缩失败，请检查文件及内容！ \r\n");
                    }
                }
                else
                {
                    AppendTaskResult(resultRender, " *** 读取失败，请检查文件及内容！ \r\n");
                }
                AppendTaskResult(resultRender, "--------------------------------------------------------\r\n\r\n");
            }
        }

        /// <summary>
        /// 根据目录批量压缩CSS文件
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="targetFolder"></param>
        /// <param name="resultRender"></param>
        /// <param name="toBase64ImgMaxSize"></param>
        /// <param name="renameFixed"></param>
        private async void DoCssCompressByFolder(IEnumerable<string> sourceFiles, string targetFolder, TextBox resultRender, int toBase64ImgMaxSize, string renameFixed)
        {
            string resultCode, saveFilePath;
            foreach (var file in sourceFiles)
            {
                AppendTaskResult(resultRender, string.Format("正在处理文件 {0} \r\n", file));
                resultCode = await file.CompressCssFromFile(string.Empty, toBase64ImgMaxSize);
                if (!string.IsNullOrEmpty(resultCode))
                {
                    saveFilePath = file.FileRename(targetFolder, renameFixed);
                    await saveFilePath.WriteTxtFile(resultCode);
                    AppendTaskResult(resultRender, string.Format("压缩完成，已经保存至文件 {0} \r\n", saveFilePath));
                }
                else
                {
                    AppendTaskResult(resultRender, " *** 压缩失败，请检查文件及内容！ \r\n");
                }
                AppendTaskResult(resultRender, "--------------------------------------------------------\r\n\r\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render"></param>
        /// <param name="result"></param>
        private void AppendTaskResult(Control render, string result)
        {
            if (this.InvokeRequired)
                this.Invoke(new AppendTaskResultText(AppendTaskResult), render, result);
            else
                ((TextBox)render).Text += result;
        }

        #endregion
    }
}