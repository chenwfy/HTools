namespace HTools
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_ImgBase64 = new System.Windows.Forms.TabPage();
            this.textBox_ImageBase64 = new System.Windows.Forms.TextBox();
            this.button_ImageFileSelection = new System.Windows.Forms.Button();
            this.textBox_ImageFilePath = new System.Windows.Forms.TextBox();
            this.tabPage_Js = new System.Windows.Forms.TabPage();
            this.button_Js_Do = new System.Windows.Forms.Button();
            this.textBox_JsResult = new System.Windows.Forms.TextBox();
            this.js_radioButton_3 = new System.Windows.Forms.RadioButton();
            this.button_JsFileSelection = new System.Windows.Forms.Button();
            this.textBox_JsFilePath = new System.Windows.Forms.TextBox();
            this.js_radioButton_2 = new System.Windows.Forms.RadioButton();
            this.js_radioButton_1 = new System.Windows.Forms.RadioButton();
            this.js_label_1 = new System.Windows.Forms.Label();
            this.tabPage_Css = new System.Windows.Forms.TabPage();
            this.button_Css_Do = new System.Windows.Forms.Button();
            this.css_label_3 = new System.Windows.Forms.Label();
            this.css_numericUpDown_1 = new System.Windows.Forms.NumericUpDown();
            this.css_checkBox_1 = new System.Windows.Forms.CheckBox();
            this.textBox_CssResult = new System.Windows.Forms.TextBox();
            this.css_radioButton_3 = new System.Windows.Forms.RadioButton();
            this.button_CssFileSelection = new System.Windows.Forms.Button();
            this.textBox_cssFilePath = new System.Windows.Forms.TextBox();
            this.css_radioButton_2 = new System.Windows.Forms.RadioButton();
            this.css_radioButton_1 = new System.Windows.Forms.RadioButton();
            this.css_label_1 = new System.Windows.Forms.Label();
            this.tabPage_Watch = new System.Windows.Forms.TabPage();
            this.radioButton_TaskType_Watch = new System.Windows.Forms.RadioButton();
            this.radioButton_TaskType_OnlyOnce = new System.Windows.Forms.RadioButton();
            this.textBox_TaskResult = new System.Windows.Forms.TextBox();
            this.button_ClearLogs = new System.Windows.Forms.Button();
            this.button_BeginTask = new System.Windows.Forms.Button();
            this.groupBox_Folder_Css = new System.Windows.Forms.GroupBox();
            this.label_FolderCss_TxtToBase64 = new System.Windows.Forms.Label();
            this.numericUpDown_FolderCss_ImageToBase64MaxSize = new System.Windows.Forms.NumericUpDown();
            this.checkBox_FolderCss_ImageToBase64 = new System.Windows.Forms.CheckBox();
            this.radioButton_FolderCss_ReName_2 = new System.Windows.Forms.RadioButton();
            this.radioButton_FolderCss_ReName_1 = new System.Windows.Forms.RadioButton();
            this.label_FolderCss_TxtReName = new System.Windows.Forms.Label();
            this.button_FolderCss_TargetSelection = new System.Windows.Forms.Button();
            this.textBox_FolderCss_TargetPath = new System.Windows.Forms.TextBox();
            this.label_FolderCss_TxtTarget = new System.Windows.Forms.Label();
            this.button_FolderCss_SourceSelection = new System.Windows.Forms.Button();
            this.textBox_FolderCss_SourcePath = new System.Windows.Forms.TextBox();
            this.label_FolderCss_TxtSource = new System.Windows.Forms.Label();
            this.groupBox_Folder_Js = new System.Windows.Forms.GroupBox();
            this.radioButton_FolderJs_ReName_2 = new System.Windows.Forms.RadioButton();
            this.radioButton_FolderJs_ReName_1 = new System.Windows.Forms.RadioButton();
            this.label_FolderJs_TxtReName = new System.Windows.Forms.Label();
            this.button_FolderJs_TargetSelection = new System.Windows.Forms.Button();
            this.textBox_FolderJs_TargetPath = new System.Windows.Forms.TextBox();
            this.label_FolderJs_TxtTarget = new System.Windows.Forms.Label();
            this.button_FolderJs_SourceSelection = new System.Windows.Forms.Button();
            this.textBox_FolderJs_SourcePath = new System.Windows.Forms.TextBox();
            this.label_FolderJs_TxtSource = new System.Windows.Forms.Label();
            this.openFileDialog_Image = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_Js = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog_Css = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon_Toolbar = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog_FolderJs_Source = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog_FolderJs_Target = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog_FolderCss_Source = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog_FolderCss_Target = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl.SuspendLayout();
            this.tabPage_ImgBase64.SuspendLayout();
            this.tabPage_Js.SuspendLayout();
            this.tabPage_Css.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.css_numericUpDown_1)).BeginInit();
            this.tabPage_Watch.SuspendLayout();
            this.groupBox_Folder_Css.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FolderCss_ImageToBase64MaxSize)).BeginInit();
            this.groupBox_Folder_Js.SuspendLayout();
            this.contextMenuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_ImgBase64);
            this.tabControl.Controls.Add(this.tabPage_Js);
            this.tabControl.Controls.Add(this.tabPage_Css);
            this.tabControl.Controls.Add(this.tabPage_Watch);
            this.tabControl.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage_ImgBase64
            // 
            this.tabPage_ImgBase64.Controls.Add(this.textBox_ImageBase64);
            this.tabPage_ImgBase64.Controls.Add(this.button_ImageFileSelection);
            this.tabPage_ImgBase64.Controls.Add(this.textBox_ImageFilePath);
            resources.ApplyResources(this.tabPage_ImgBase64, "tabPage_ImgBase64");
            this.tabPage_ImgBase64.Name = "tabPage_ImgBase64";
            this.tabPage_ImgBase64.UseVisualStyleBackColor = true;
            // 
            // textBox_ImageBase64
            // 
            this.textBox_ImageBase64.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_ImageBase64, "textBox_ImageBase64");
            this.textBox_ImageBase64.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_ImageBase64.Name = "textBox_ImageBase64";
            this.textBox_ImageBase64.ReadOnly = true;
            // 
            // button_ImageFileSelection
            // 
            resources.ApplyResources(this.button_ImageFileSelection, "button_ImageFileSelection");
            this.button_ImageFileSelection.Name = "button_ImageFileSelection";
            this.button_ImageFileSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_ImageFilePath
            // 
            this.textBox_ImageFilePath.AllowDrop = true;
            this.textBox_ImageFilePath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_ImageFilePath, "textBox_ImageFilePath");
            this.textBox_ImageFilePath.Name = "textBox_ImageFilePath";
            this.textBox_ImageFilePath.ReadOnly = true;
            // 
            // tabPage_Js
            // 
            this.tabPage_Js.Controls.Add(this.button_Js_Do);
            this.tabPage_Js.Controls.Add(this.textBox_JsResult);
            this.tabPage_Js.Controls.Add(this.js_radioButton_3);
            this.tabPage_Js.Controls.Add(this.button_JsFileSelection);
            this.tabPage_Js.Controls.Add(this.textBox_JsFilePath);
            this.tabPage_Js.Controls.Add(this.js_radioButton_2);
            this.tabPage_Js.Controls.Add(this.js_radioButton_1);
            this.tabPage_Js.Controls.Add(this.js_label_1);
            resources.ApplyResources(this.tabPage_Js, "tabPage_Js");
            this.tabPage_Js.Name = "tabPage_Js";
            this.tabPage_Js.UseVisualStyleBackColor = true;
            // 
            // button_Js_Do
            // 
            resources.ApplyResources(this.button_Js_Do, "button_Js_Do");
            this.button_Js_Do.Name = "button_Js_Do";
            this.button_Js_Do.UseVisualStyleBackColor = true;
            // 
            // textBox_JsResult
            // 
            this.textBox_JsResult.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_JsResult, "textBox_JsResult");
            this.textBox_JsResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_JsResult.Name = "textBox_JsResult";
            // 
            // js_radioButton_3
            // 
            resources.ApplyResources(this.js_radioButton_3, "js_radioButton_3");
            this.js_radioButton_3.Name = "js_radioButton_3";
            this.js_radioButton_3.UseVisualStyleBackColor = true;
            // 
            // button_JsFileSelection
            // 
            resources.ApplyResources(this.button_JsFileSelection, "button_JsFileSelection");
            this.button_JsFileSelection.Name = "button_JsFileSelection";
            this.button_JsFileSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_JsFilePath
            // 
            this.textBox_JsFilePath.AllowDrop = true;
            this.textBox_JsFilePath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_JsFilePath, "textBox_JsFilePath");
            this.textBox_JsFilePath.Name = "textBox_JsFilePath";
            this.textBox_JsFilePath.ReadOnly = true;
            // 
            // js_radioButton_2
            // 
            resources.ApplyResources(this.js_radioButton_2, "js_radioButton_2");
            this.js_radioButton_2.Name = "js_radioButton_2";
            this.js_radioButton_2.UseVisualStyleBackColor = true;
            // 
            // js_radioButton_1
            // 
            resources.ApplyResources(this.js_radioButton_1, "js_radioButton_1");
            this.js_radioButton_1.Checked = true;
            this.js_radioButton_1.Name = "js_radioButton_1";
            this.js_radioButton_1.TabStop = true;
            this.js_radioButton_1.UseVisualStyleBackColor = true;
            // 
            // js_label_1
            // 
            resources.ApplyResources(this.js_label_1, "js_label_1");
            this.js_label_1.Name = "js_label_1";
            // 
            // tabPage_Css
            // 
            this.tabPage_Css.Controls.Add(this.button_Css_Do);
            this.tabPage_Css.Controls.Add(this.css_label_3);
            this.tabPage_Css.Controls.Add(this.css_numericUpDown_1);
            this.tabPage_Css.Controls.Add(this.css_checkBox_1);
            this.tabPage_Css.Controls.Add(this.textBox_CssResult);
            this.tabPage_Css.Controls.Add(this.css_radioButton_3);
            this.tabPage_Css.Controls.Add(this.button_CssFileSelection);
            this.tabPage_Css.Controls.Add(this.textBox_cssFilePath);
            this.tabPage_Css.Controls.Add(this.css_radioButton_2);
            this.tabPage_Css.Controls.Add(this.css_radioButton_1);
            this.tabPage_Css.Controls.Add(this.css_label_1);
            resources.ApplyResources(this.tabPage_Css, "tabPage_Css");
            this.tabPage_Css.Name = "tabPage_Css";
            this.tabPage_Css.UseVisualStyleBackColor = true;
            // 
            // button_Css_Do
            // 
            resources.ApplyResources(this.button_Css_Do, "button_Css_Do");
            this.button_Css_Do.Name = "button_Css_Do";
            this.button_Css_Do.UseVisualStyleBackColor = true;
            // 
            // css_label_3
            // 
            resources.ApplyResources(this.css_label_3, "css_label_3");
            this.css_label_3.Name = "css_label_3";
            // 
            // css_numericUpDown_1
            // 
            resources.ApplyResources(this.css_numericUpDown_1, "css_numericUpDown_1");
            this.css_numericUpDown_1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.css_numericUpDown_1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.css_numericUpDown_1.Name = "css_numericUpDown_1";
            this.css_numericUpDown_1.ReadOnly = true;
            this.css_numericUpDown_1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // css_checkBox_1
            // 
            resources.ApplyResources(this.css_checkBox_1, "css_checkBox_1");
            this.css_checkBox_1.Checked = true;
            this.css_checkBox_1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.css_checkBox_1.Name = "css_checkBox_1";
            this.css_checkBox_1.UseVisualStyleBackColor = true;
            // 
            // textBox_CssResult
            // 
            this.textBox_CssResult.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_CssResult, "textBox_CssResult");
            this.textBox_CssResult.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_CssResult.Name = "textBox_CssResult";
            // 
            // css_radioButton_3
            // 
            resources.ApplyResources(this.css_radioButton_3, "css_radioButton_3");
            this.css_radioButton_3.Name = "css_radioButton_3";
            this.css_radioButton_3.UseVisualStyleBackColor = true;
            // 
            // button_CssFileSelection
            // 
            resources.ApplyResources(this.button_CssFileSelection, "button_CssFileSelection");
            this.button_CssFileSelection.Name = "button_CssFileSelection";
            this.button_CssFileSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_cssFilePath
            // 
            this.textBox_cssFilePath.AllowDrop = true;
            this.textBox_cssFilePath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_cssFilePath, "textBox_cssFilePath");
            this.textBox_cssFilePath.Name = "textBox_cssFilePath";
            this.textBox_cssFilePath.ReadOnly = true;
            // 
            // css_radioButton_2
            // 
            resources.ApplyResources(this.css_radioButton_2, "css_radioButton_2");
            this.css_radioButton_2.Name = "css_radioButton_2";
            this.css_radioButton_2.UseVisualStyleBackColor = true;
            // 
            // css_radioButton_1
            // 
            resources.ApplyResources(this.css_radioButton_1, "css_radioButton_1");
            this.css_radioButton_1.Checked = true;
            this.css_radioButton_1.Name = "css_radioButton_1";
            this.css_radioButton_1.TabStop = true;
            this.css_radioButton_1.UseVisualStyleBackColor = true;
            // 
            // css_label_1
            // 
            resources.ApplyResources(this.css_label_1, "css_label_1");
            this.css_label_1.Name = "css_label_1";
            // 
            // tabPage_Watch
            // 
            this.tabPage_Watch.Controls.Add(this.radioButton_TaskType_Watch);
            this.tabPage_Watch.Controls.Add(this.radioButton_TaskType_OnlyOnce);
            this.tabPage_Watch.Controls.Add(this.textBox_TaskResult);
            this.tabPage_Watch.Controls.Add(this.button_ClearLogs);
            this.tabPage_Watch.Controls.Add(this.button_BeginTask);
            this.tabPage_Watch.Controls.Add(this.groupBox_Folder_Css);
            this.tabPage_Watch.Controls.Add(this.groupBox_Folder_Js);
            resources.ApplyResources(this.tabPage_Watch, "tabPage_Watch");
            this.tabPage_Watch.Name = "tabPage_Watch";
            this.tabPage_Watch.UseVisualStyleBackColor = true;
            // 
            // radioButton_TaskType_Watch
            // 
            resources.ApplyResources(this.radioButton_TaskType_Watch, "radioButton_TaskType_Watch");
            this.radioButton_TaskType_Watch.Name = "radioButton_TaskType_Watch";
            this.radioButton_TaskType_Watch.UseVisualStyleBackColor = true;
            // 
            // radioButton_TaskType_OnlyOnce
            // 
            resources.ApplyResources(this.radioButton_TaskType_OnlyOnce, "radioButton_TaskType_OnlyOnce");
            this.radioButton_TaskType_OnlyOnce.Checked = true;
            this.radioButton_TaskType_OnlyOnce.Name = "radioButton_TaskType_OnlyOnce";
            this.radioButton_TaskType_OnlyOnce.TabStop = true;
            this.radioButton_TaskType_OnlyOnce.UseVisualStyleBackColor = true;
            // 
            // textBox_TaskResult
            // 
            this.textBox_TaskResult.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_TaskResult, "textBox_TaskResult");
            this.textBox_TaskResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_TaskResult.Name = "textBox_TaskResult";
            this.textBox_TaskResult.ReadOnly = true;
            // 
            // button_ClearLogs
            // 
            resources.ApplyResources(this.button_ClearLogs, "button_ClearLogs");
            this.button_ClearLogs.Name = "button_ClearLogs";
            this.button_ClearLogs.UseVisualStyleBackColor = true;
            // 
            // button_BeginTask
            // 
            resources.ApplyResources(this.button_BeginTask, "button_BeginTask");
            this.button_BeginTask.Name = "button_BeginTask";
            this.button_BeginTask.UseVisualStyleBackColor = true;
            // 
            // groupBox_Folder_Css
            // 
            this.groupBox_Folder_Css.Controls.Add(this.label_FolderCss_TxtToBase64);
            this.groupBox_Folder_Css.Controls.Add(this.numericUpDown_FolderCss_ImageToBase64MaxSize);
            this.groupBox_Folder_Css.Controls.Add(this.checkBox_FolderCss_ImageToBase64);
            this.groupBox_Folder_Css.Controls.Add(this.radioButton_FolderCss_ReName_2);
            this.groupBox_Folder_Css.Controls.Add(this.radioButton_FolderCss_ReName_1);
            this.groupBox_Folder_Css.Controls.Add(this.label_FolderCss_TxtReName);
            this.groupBox_Folder_Css.Controls.Add(this.button_FolderCss_TargetSelection);
            this.groupBox_Folder_Css.Controls.Add(this.textBox_FolderCss_TargetPath);
            this.groupBox_Folder_Css.Controls.Add(this.label_FolderCss_TxtTarget);
            this.groupBox_Folder_Css.Controls.Add(this.button_FolderCss_SourceSelection);
            this.groupBox_Folder_Css.Controls.Add(this.textBox_FolderCss_SourcePath);
            this.groupBox_Folder_Css.Controls.Add(this.label_FolderCss_TxtSource);
            resources.ApplyResources(this.groupBox_Folder_Css, "groupBox_Folder_Css");
            this.groupBox_Folder_Css.Name = "groupBox_Folder_Css";
            this.groupBox_Folder_Css.TabStop = false;
            // 
            // label_FolderCss_TxtToBase64
            // 
            resources.ApplyResources(this.label_FolderCss_TxtToBase64, "label_FolderCss_TxtToBase64");
            this.label_FolderCss_TxtToBase64.Name = "label_FolderCss_TxtToBase64";
            // 
            // numericUpDown_FolderCss_ImageToBase64MaxSize
            // 
            resources.ApplyResources(this.numericUpDown_FolderCss_ImageToBase64MaxSize, "numericUpDown_FolderCss_ImageToBase64MaxSize");
            this.numericUpDown_FolderCss_ImageToBase64MaxSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_FolderCss_ImageToBase64MaxSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_FolderCss_ImageToBase64MaxSize.Name = "numericUpDown_FolderCss_ImageToBase64MaxSize";
            this.numericUpDown_FolderCss_ImageToBase64MaxSize.ReadOnly = true;
            this.numericUpDown_FolderCss_ImageToBase64MaxSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox_FolderCss_ImageToBase64
            // 
            resources.ApplyResources(this.checkBox_FolderCss_ImageToBase64, "checkBox_FolderCss_ImageToBase64");
            this.checkBox_FolderCss_ImageToBase64.Checked = true;
            this.checkBox_FolderCss_ImageToBase64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_FolderCss_ImageToBase64.Name = "checkBox_FolderCss_ImageToBase64";
            this.checkBox_FolderCss_ImageToBase64.UseVisualStyleBackColor = true;
            // 
            // radioButton_FolderCss_ReName_2
            // 
            resources.ApplyResources(this.radioButton_FolderCss_ReName_2, "radioButton_FolderCss_ReName_2");
            this.radioButton_FolderCss_ReName_2.Checked = true;
            this.radioButton_FolderCss_ReName_2.Name = "radioButton_FolderCss_ReName_2";
            this.radioButton_FolderCss_ReName_2.TabStop = true;
            this.radioButton_FolderCss_ReName_2.UseVisualStyleBackColor = true;
            // 
            // radioButton_FolderCss_ReName_1
            // 
            resources.ApplyResources(this.radioButton_FolderCss_ReName_1, "radioButton_FolderCss_ReName_1");
            this.radioButton_FolderCss_ReName_1.Name = "radioButton_FolderCss_ReName_1";
            this.radioButton_FolderCss_ReName_1.UseVisualStyleBackColor = true;
            // 
            // label_FolderCss_TxtReName
            // 
            resources.ApplyResources(this.label_FolderCss_TxtReName, "label_FolderCss_TxtReName");
            this.label_FolderCss_TxtReName.Name = "label_FolderCss_TxtReName";
            // 
            // button_FolderCss_TargetSelection
            // 
            this.button_FolderCss_TargetSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button_FolderCss_TargetSelection, "button_FolderCss_TargetSelection");
            this.button_FolderCss_TargetSelection.Name = "button_FolderCss_TargetSelection";
            this.button_FolderCss_TargetSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_FolderCss_TargetPath
            // 
            this.textBox_FolderCss_TargetPath.AllowDrop = true;
            this.textBox_FolderCss_TargetPath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_FolderCss_TargetPath, "textBox_FolderCss_TargetPath");
            this.textBox_FolderCss_TargetPath.Name = "textBox_FolderCss_TargetPath";
            // 
            // label_FolderCss_TxtTarget
            // 
            resources.ApplyResources(this.label_FolderCss_TxtTarget, "label_FolderCss_TxtTarget");
            this.label_FolderCss_TxtTarget.Name = "label_FolderCss_TxtTarget";
            // 
            // button_FolderCss_SourceSelection
            // 
            resources.ApplyResources(this.button_FolderCss_SourceSelection, "button_FolderCss_SourceSelection");
            this.button_FolderCss_SourceSelection.Name = "button_FolderCss_SourceSelection";
            this.button_FolderCss_SourceSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_FolderCss_SourcePath
            // 
            this.textBox_FolderCss_SourcePath.AllowDrop = true;
            this.textBox_FolderCss_SourcePath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_FolderCss_SourcePath, "textBox_FolderCss_SourcePath");
            this.textBox_FolderCss_SourcePath.Name = "textBox_FolderCss_SourcePath";
            // 
            // label_FolderCss_TxtSource
            // 
            resources.ApplyResources(this.label_FolderCss_TxtSource, "label_FolderCss_TxtSource");
            this.label_FolderCss_TxtSource.Name = "label_FolderCss_TxtSource";
            // 
            // groupBox_Folder_Js
            // 
            this.groupBox_Folder_Js.Controls.Add(this.radioButton_FolderJs_ReName_2);
            this.groupBox_Folder_Js.Controls.Add(this.radioButton_FolderJs_ReName_1);
            this.groupBox_Folder_Js.Controls.Add(this.label_FolderJs_TxtReName);
            this.groupBox_Folder_Js.Controls.Add(this.button_FolderJs_TargetSelection);
            this.groupBox_Folder_Js.Controls.Add(this.textBox_FolderJs_TargetPath);
            this.groupBox_Folder_Js.Controls.Add(this.label_FolderJs_TxtTarget);
            this.groupBox_Folder_Js.Controls.Add(this.button_FolderJs_SourceSelection);
            this.groupBox_Folder_Js.Controls.Add(this.textBox_FolderJs_SourcePath);
            this.groupBox_Folder_Js.Controls.Add(this.label_FolderJs_TxtSource);
            resources.ApplyResources(this.groupBox_Folder_Js, "groupBox_Folder_Js");
            this.groupBox_Folder_Js.Name = "groupBox_Folder_Js";
            this.groupBox_Folder_Js.TabStop = false;
            // 
            // radioButton_FolderJs_ReName_2
            // 
            resources.ApplyResources(this.radioButton_FolderJs_ReName_2, "radioButton_FolderJs_ReName_2");
            this.radioButton_FolderJs_ReName_2.Checked = true;
            this.radioButton_FolderJs_ReName_2.Name = "radioButton_FolderJs_ReName_2";
            this.radioButton_FolderJs_ReName_2.TabStop = true;
            this.radioButton_FolderJs_ReName_2.UseVisualStyleBackColor = true;
            // 
            // radioButton_FolderJs_ReName_1
            // 
            resources.ApplyResources(this.radioButton_FolderJs_ReName_1, "radioButton_FolderJs_ReName_1");
            this.radioButton_FolderJs_ReName_1.Name = "radioButton_FolderJs_ReName_1";
            this.radioButton_FolderJs_ReName_1.UseVisualStyleBackColor = true;
            // 
            // label_FolderJs_TxtReName
            // 
            resources.ApplyResources(this.label_FolderJs_TxtReName, "label_FolderJs_TxtReName");
            this.label_FolderJs_TxtReName.Name = "label_FolderJs_TxtReName";
            // 
            // button_FolderJs_TargetSelection
            // 
            this.button_FolderJs_TargetSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.button_FolderJs_TargetSelection, "button_FolderJs_TargetSelection");
            this.button_FolderJs_TargetSelection.Name = "button_FolderJs_TargetSelection";
            this.button_FolderJs_TargetSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_FolderJs_TargetPath
            // 
            this.textBox_FolderJs_TargetPath.AllowDrop = true;
            this.textBox_FolderJs_TargetPath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_FolderJs_TargetPath, "textBox_FolderJs_TargetPath");
            this.textBox_FolderJs_TargetPath.Name = "textBox_FolderJs_TargetPath";
            // 
            // label_FolderJs_TxtTarget
            // 
            resources.ApplyResources(this.label_FolderJs_TxtTarget, "label_FolderJs_TxtTarget");
            this.label_FolderJs_TxtTarget.Name = "label_FolderJs_TxtTarget";
            // 
            // button_FolderJs_SourceSelection
            // 
            resources.ApplyResources(this.button_FolderJs_SourceSelection, "button_FolderJs_SourceSelection");
            this.button_FolderJs_SourceSelection.Name = "button_FolderJs_SourceSelection";
            this.button_FolderJs_SourceSelection.UseVisualStyleBackColor = true;
            // 
            // textBox_FolderJs_SourcePath
            // 
            this.textBox_FolderJs_SourcePath.AllowDrop = true;
            this.textBox_FolderJs_SourcePath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBox_FolderJs_SourcePath, "textBox_FolderJs_SourcePath");
            this.textBox_FolderJs_SourcePath.Name = "textBox_FolderJs_SourcePath";
            // 
            // label_FolderJs_TxtSource
            // 
            resources.ApplyResources(this.label_FolderJs_TxtSource, "label_FolderJs_TxtSource");
            this.label_FolderJs_TxtSource.Name = "label_FolderJs_TxtSource";
            // 
            // openFileDialog_Image
            // 
            resources.ApplyResources(this.openFileDialog_Image, "openFileDialog_Image");
            // 
            // openFileDialog_Js
            // 
            resources.ApplyResources(this.openFileDialog_Js, "openFileDialog_Js");
            this.openFileDialog_Js.Multiselect = true;
            // 
            // openFileDialog_Css
            // 
            resources.ApplyResources(this.openFileDialog_Css, "openFileDialog_Css");
            this.openFileDialog_Css.Multiselect = true;
            // 
            // notifyIcon_Toolbar
            // 
            this.notifyIcon_Toolbar.ContextMenuStrip = this.contextMenuStrip_Main;
            resources.ApplyResources(this.notifyIcon_Toolbar, "notifyIcon_Toolbar");
            // 
            // contextMenuStrip_Main
            // 
            this.contextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Exit});
            this.contextMenuStrip_Main.Name = "contextMenuStrip_Main";
            resources.ApplyResources(this.contextMenuStrip_Main, "contextMenuStrip_Main");
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            resources.ApplyResources(this.toolStripMenuItem_Exit, "toolStripMenuItem_Exit");
            // 
            // folderBrowserDialog_FolderJs_Source
            // 
            resources.ApplyResources(this.folderBrowserDialog_FolderJs_Source, "folderBrowserDialog_FolderJs_Source");
            this.folderBrowserDialog_FolderJs_Source.ShowNewFolderButton = false;
            // 
            // folderBrowserDialog_FolderJs_Target
            // 
            resources.ApplyResources(this.folderBrowserDialog_FolderJs_Target, "folderBrowserDialog_FolderJs_Target");
            this.folderBrowserDialog_FolderJs_Target.ShowNewFolderButton = false;
            // 
            // folderBrowserDialog_FolderCss_Source
            // 
            resources.ApplyResources(this.folderBrowserDialog_FolderCss_Source, "folderBrowserDialog_FolderCss_Source");
            this.folderBrowserDialog_FolderCss_Source.ShowNewFolderButton = false;
            // 
            // folderBrowserDialog_FolderCss_Target
            // 
            resources.ApplyResources(this.folderBrowserDialog_FolderCss_Target, "folderBrowserDialog_FolderCss_Target");
            this.folderBrowserDialog_FolderCss_Target.ShowNewFolderButton = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.tabControl.ResumeLayout(false);
            this.tabPage_ImgBase64.ResumeLayout(false);
            this.tabPage_ImgBase64.PerformLayout();
            this.tabPage_Js.ResumeLayout(false);
            this.tabPage_Js.PerformLayout();
            this.tabPage_Css.ResumeLayout(false);
            this.tabPage_Css.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.css_numericUpDown_1)).EndInit();
            this.tabPage_Watch.ResumeLayout(false);
            this.tabPage_Watch.PerformLayout();
            this.groupBox_Folder_Css.ResumeLayout(false);
            this.groupBox_Folder_Css.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_FolderCss_ImageToBase64MaxSize)).EndInit();
            this.groupBox_Folder_Js.ResumeLayout(false);
            this.groupBox_Folder_Js.PerformLayout();
            this.contextMenuStrip_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_Js;
        private System.Windows.Forms.TabPage tabPage_Css;
        private System.Windows.Forms.TabPage tabPage_Watch;
        private System.Windows.Forms.RadioButton js_radioButton_2;
        private System.Windows.Forms.RadioButton js_radioButton_1;
        private System.Windows.Forms.Label js_label_1;
        private System.Windows.Forms.TabPage tabPage_ImgBase64;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Image;
        private System.Windows.Forms.TextBox textBox_ImageBase64;
        private System.Windows.Forms.Button button_ImageFileSelection;
        private System.Windows.Forms.TextBox textBox_ImageFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Js;
        private System.Windows.Forms.Button button_JsFileSelection;
        private System.Windows.Forms.TextBox textBox_JsFilePath;
        private System.Windows.Forms.RadioButton js_radioButton_3;
        private System.Windows.Forms.TextBox textBox_JsResult;
        private System.Windows.Forms.TextBox textBox_CssResult;
        private System.Windows.Forms.RadioButton css_radioButton_3;
        private System.Windows.Forms.Button button_CssFileSelection;
        private System.Windows.Forms.TextBox textBox_cssFilePath;
        private System.Windows.Forms.RadioButton css_radioButton_2;
        private System.Windows.Forms.RadioButton css_radioButton_1;
        private System.Windows.Forms.Label css_label_1;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Css;
        private System.Windows.Forms.Label css_label_3;
        private System.Windows.Forms.NumericUpDown css_numericUpDown_1;
        private System.Windows.Forms.CheckBox css_checkBox_1;
        private System.Windows.Forms.Button button_Js_Do;
        private System.Windows.Forms.Button button_Css_Do;
        private System.Windows.Forms.GroupBox groupBox_Folder_Css;
        private System.Windows.Forms.GroupBox groupBox_Folder_Js;
        private System.Windows.Forms.NotifyIcon notifyIcon_Toolbar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.Button button_FolderJs_TargetSelection;
        private System.Windows.Forms.TextBox textBox_FolderJs_TargetPath;
        private System.Windows.Forms.Label label_FolderJs_TxtTarget;
        private System.Windows.Forms.Button button_FolderJs_SourceSelection;
        private System.Windows.Forms.TextBox textBox_FolderJs_SourcePath;
        private System.Windows.Forms.Label label_FolderJs_TxtSource;
        private System.Windows.Forms.Label label_FolderCss_TxtToBase64;
        private System.Windows.Forms.NumericUpDown numericUpDown_FolderCss_ImageToBase64MaxSize;
        private System.Windows.Forms.CheckBox checkBox_FolderCss_ImageToBase64;
        private System.Windows.Forms.RadioButton radioButton_FolderCss_ReName_2;
        private System.Windows.Forms.RadioButton radioButton_FolderCss_ReName_1;
        private System.Windows.Forms.Label label_FolderCss_TxtReName;
        private System.Windows.Forms.Button button_FolderCss_TargetSelection;
        private System.Windows.Forms.TextBox textBox_FolderCss_TargetPath;
        private System.Windows.Forms.Label label_FolderCss_TxtTarget;
        private System.Windows.Forms.Button button_FolderCss_SourceSelection;
        private System.Windows.Forms.TextBox textBox_FolderCss_SourcePath;
        private System.Windows.Forms.Label label_FolderCss_TxtSource;
        private System.Windows.Forms.RadioButton radioButton_FolderJs_ReName_2;
        private System.Windows.Forms.RadioButton radioButton_FolderJs_ReName_1;
        private System.Windows.Forms.Label label_FolderJs_TxtReName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FolderJs_Source;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FolderJs_Target;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FolderCss_Source;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FolderCss_Target;
        private System.Windows.Forms.TextBox textBox_TaskResult;
        private System.Windows.Forms.Button button_ClearLogs;
        private System.Windows.Forms.Button button_BeginTask;
        private System.Windows.Forms.RadioButton radioButton_TaskType_Watch;
        private System.Windows.Forms.RadioButton radioButton_TaskType_OnlyOnce;
    }
}