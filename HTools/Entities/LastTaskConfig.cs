using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTools.Entities
{
    /// <summary>
    /// 最后一次任务配置
    /// </summary>
    public class LastTaskConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public string JsSourceFolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsTargetFolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JsRenameFixed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CssSourceFolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CssTargetFolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CssRenameFixed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CssImageToBase64MaxSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FolderWatched { get; set; }
    }
}