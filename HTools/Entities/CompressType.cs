namespace HTools.Entities
{
    /// <summary>
    /// 多文件压缩方式枚举
    /// </summary>
    public enum CompressType
    {
        /// <summary>
        /// 各文件独立处理
        /// </summary>
        FileSingle = 1,

        /// <summary>
        /// 多文件内容先合并后压缩
        /// </summary>
        MergeAndCompressed = 2,

        /// <summary>
        /// 多文件内容先压缩后合并
        /// </summary>
        CompressedAndMerge = 4
    }
}