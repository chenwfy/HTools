using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using EdgeJs;
using HTools.Entities;

namespace HTools.Utilities
{
    /// <summary>
    /// 辅助工具类
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IDictionary<FileType, IEnumerable<string>> AllowFileExtDict = new Dictionary<FileType, IEnumerable<string>>()
        {
            { FileType.Hhtml, new List<string>() { ".htm", ".html", ".shtml" } },
            { FileType.Image, new List<string>() { ".jpg", ".jpeg", ".png", ".gif", ".bmp" } },
            { FileType.Js, new List<string>() { ".js" } },
            { FileType.Css, new List<string>() { ".css" } }
        };

        #region XML序列化

        /// <summary>
        /// 将对象序列化为XML格式字串
        /// </summary>
        /// <param name="value">待序列化的对象</param>
        /// <returns>XML格式字串</returns>
        public static string XmlSerializer<T>(this T value)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                TextWriter writer = new StreamWriter(ms, Encoding.UTF8);
                XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                xmlns.Add(String.Empty, String.Empty);
                xmlSerializer.Serialize(writer, value, xmlns);
                ms.Position = 0;
                byte[] buffer = new byte[ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                xmlString = Encoding.UTF8.GetString(buffer);
            }
            return ReplaceXmlDate(xmlString);
        }

        /// <summary>
        /// 替换XML字串中的日期字串
        /// </summary>
        /// <param name="containDateTimeSource"></param>
        /// <returns></returns>
        private static string ReplaceXmlDate(string containDateTimeSource)
        {
            string pattern = @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(XmlDateFormat);
            Regex reg = new Regex(pattern);
            string result = containDateTimeSource;
            result = reg.Replace(result, matchEvaluator);
            return result;
        }

        /// <summary>
        /// 格式化XML格式时间字串
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static string XmlDateFormat(Match match)
        {
            return Convert.ToDateTime(match.Value).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// XML字串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象数据类型</typeparam>
        /// <param name="xml">XML字串</param>
        /// <returns>对象</returns>
        public static T XmlDeserialize<T>(this string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            return (T)xs.Deserialize(new StringReader(xml));
        }

        /// <summary>
        /// 写入不带BOM头的XML文件
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="content"></param>
        public static void WriteXmlNoBom(this string xmlPath, string content)
        {
            using (FileStream fs = new FileStream(xmlPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false)))
                {
                    sw.Write(content);
                    sw.Close();
                }
                fs.Close();
            }
        }

        #endregion 

        #region 最后一次任务配置

        /// <summary>
        /// 读取配置
        /// </summary>
        public static async Task<LastTaskConfig> ReadLastTaskConfig(this string configFilePath)
        {
            if (File.Exists(configFilePath))
            {
                string content = await configFilePath.ReadFileText();
                if (!string.IsNullOrEmpty(content)) {
                    return content.XmlDeserialize<LastTaskConfig>();
                }
            }
            return null;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configFilePath"></param>
        public static void SaveLastTaskConfig(this LastTaskConfig config, string configFilePath)
        {
            string content = config.XmlSerializer<LastTaskConfig>();
            if (!string.IsNullOrEmpty(content))
            {
                configFilePath.WriteXmlNoBom(content);
            }
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configFilePath"></param>
        public static void RemoveLastTaskConfig(this string configFilePath)
        {
            if (File.Exists(configFilePath)) {
                File.Delete(configFilePath);
            }
        }

        #endregion

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool FileExists(this string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static long GetFileSize(this string filePath)
        {
            if (!filePath.FileExists())
                return 0;

            return new FileInfo(filePath).Length;
        }

        /// <summary>
        /// 判断路径是否为可允许的文件类型
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static bool IsAllowFile(this string filePath, FileType fileType)
        {
            string ext = (Path.GetExtension(filePath) ?? string.Empty).Trim().ToLower();
            return AllowFileExtDict[fileType].Contains(ext);
        }

        /// <summary>
        /// 获取被压缩后的文件存放路径
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <returns></returns>
        public static string GetCompressFileName(this IEnumerable<string> sourceFiles)
        {
            if (sourceFiles.Count() > 0)
            {
                string tPath = sourceFiles.First();
                string folder = Path.GetDirectoryName(tPath);
                string extName = Path.GetExtension(tPath);
                string fileName = string.Empty;
                foreach (var item in sourceFiles)
                {
                    fileName += Path.GetFileNameWithoutExtension(item);
                }

                fileName += ".min";

                return Path.Combine(folder, fileName + extName);
            }

            return string.Empty;
        }

        /// <summary>
        /// 图片编码为BASE64编码字串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Task<string> ImageToBase64(this string filePath)
        {
            return Task.Run(() =>
            {
                string base64 = "编码失败！";
                if (File.Exists(filePath))
                {
                    string extName = Path.GetExtension(filePath).Substring(1);
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            byte[] buffer = reader.ReadBytes((int)fs.Length);
                            base64 = Convert.ToBase64String(buffer);
                            base64 = string.Format("data:image/{0};base64,{1}", extName, base64);

                            reader.Close();
                        }
                        fs.Close();
                    }
                }
                return base64;
            });
        }

        /// <summary>
        /// 读取文本文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encodName"></param>
        /// <returns></returns>
        public static Task<string> ReadFileText(this string filePath, string encodName = "utf-8")
        {
            return Task.Run(() =>
            {
                string content = string.Empty;
                Encoding encoding = Encoding.GetEncoding(encodName);
                if (File.Exists(filePath))
                {
                    content = File.ReadAllText(filePath, encoding);
                }
                return content;
            });
        }

        /// <summary>
        /// 写入并保存文本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="txt"></param>
        /// <param name="deleteSourceFile">如果存在同名文件，是否删除</param>
        public static Task WriteTxtFile(this string filePath, string txt, bool deleteSourceFile = true)
        {
            return Task.Run(() =>
            {
                if (File.Exists(filePath) && deleteSourceFile)
                {
                    File.Delete(filePath);
                }

                File.WriteAllText(filePath, txt, Encoding.UTF8);
            });
        }

        /// <summary>
        /// 从源码压缩JS脚本
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public static Task<string> ComoressJsCode(this string sourceCode)
        {
            return Task.Run(() =>
            {
                try
                {
                    var result = CompressJavascriptCode(sourceCode);
                    return result.Result.ToString();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            });
        }

        /// <summary>
        /// 从源码压缩JS脚本
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        private static async Task<object> CompressJavascriptCode(string sourceCode)
        {
            var func = Edge.Func(@"
                var fs = require('fs');
                var uglifyJs = require('uglify-js');

                return function (sourceCode, callback) {
                    var result = uglifyJs.minify(sourceCode, {
                        fromString: true,
                        mangle: true,
                        compress: { comparisons: true, booleans: true, conditionals: true, dead_code: true, evaluate: true, hoist_funs: true, hoist_vars: true, if_return: true, join_vars: true, loops: true, sequences: true, unused: true },
                        output: { beautify: false, ascii_only: true }
                    });
    
                    callback(null, result.code);
                };
            ");

            return await func(sourceCode);
        }

        /// <summary>
        /// 从输入的源码压缩CSS代码，不需要做图片转BASE64处理
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public static Task<string> CompressCssFromCode(this string sourceCode)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(sourceCode.Trim()))
                {
                    sourceCode = Regex.Replace(sourceCode, @"/\*([^*]|(\*[^/]))*\*/", string.Empty).Trim();
                    if (!string.IsNullOrEmpty(sourceCode) && sourceCode.Contains("\n"))
                    {
                        StringBuilder sBulder = new StringBuilder();
                        string line;
                        foreach (var item in sourceCode.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            line = item.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();
                            if (!string.IsNullOrEmpty(line))
                            {
                                sBulder.Append(line);
                            }
                        }

                        return sBulder.ToString();
                    }
                }
                return sourceCode;
            });
        }

        /// <summary>
        /// 从文件压缩CSS代码
        /// </summary>
        /// <param name="filePath">CSS文件路径</param>
        /// <param name="imageFileRoot">图片等相关资源根目录</param>
        /// <param name="toBaseImageMaxSize">需要转换为BASE64编码的图片的最大尺寸，0为不转换</param>
        /// <returns></returns>
        public static Task<string> CompressCssFromFile(this string filePath, string imageFileRoot, int toBaseImageMaxSize = 0)
        {
            return Task.Run(() =>
            {
                return CompressCssFromFileAsync(filePath, imageFileRoot, toBaseImageMaxSize);
            });
        }

        /// <summary>
        /// 从文件压缩CSS代码
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageFileRoot"></param>
        /// <param name="toBaseImageMaxSize"></param>
        /// <returns></returns>
        private static async Task<string> CompressCssFromFileAsync(string filePath, string imageFileRoot, int toBaseImageMaxSize)
        {
            string sourceCode = await filePath.ReadFileText();
            if (!string.IsNullOrEmpty(sourceCode.Trim()))
            {
                sourceCode = Regex.Replace(sourceCode, @"/\*([^*]|(\*[^/]))*\*/", string.Empty).Trim();
                if (!string.IsNullOrEmpty(sourceCode) && sourceCode.Contains("\n"))
                {
                    StringBuilder sBulder = new StringBuilder();
                    string line, newLine, matchUrl, imgUrl, imgPath;
                    long imgFileSize = 0;
                    Regex regex = new Regex(@"url\((\S+?)\)", RegexOptions.IgnoreCase);
                    Match match;
                    foreach (var item in sourceCode.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        line = item.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim();
                        if (!string.IsNullOrEmpty(line))
                        {
                            newLine = line;
                            if (toBaseImageMaxSize > 0)
                            {
                                match = regex.Match(line);
                                if (match.Success)
                                {
                                    matchUrl = match.Groups[1].Value;
                                    imgUrl = matchUrl.TrimStart(new char[] { '"', '\'' }).TrimEnd(new char[] { '"', '\'' }).Trim().ToLower();
                                    if (!string.IsNullOrEmpty(imgUrl) && !imgUrl.StartsWith("http://") && !imgUrl.StartsWith("https://") && !imgUrl.StartsWith("ftp://"))
                                    {
                                        imgPath = GetCssImageFilePath(imgUrl, filePath, imageFileRoot);
                                        if (!string.IsNullOrEmpty(imgPath))
                                        {
                                            imgFileSize = (int)imgPath.GetFileSize();
                                            if (imgFileSize > 0 && imgFileSize <= toBaseImageMaxSize)
                                            {
                                                newLine = newLine.Replace(matchUrl, await imgPath.ImageToBase64());
                                                newLine += "*" + line;
                                            }
                                        }
                                    }
                                }
                            }
                            sBulder.Append(newLine);
                        }
                    }

                    return sBulder.ToString();
                }
            }
            return sourceCode;
        }

        /// <summary>
        /// 获取CSS文件中的图片文件物理路径
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="cssFilePath"></param>
        /// <param name="imgFileRoot"></param>
        /// <returns></returns>
        private static string GetCssImageFilePath(string imgUrl, string cssFilePath, string imgFileRoot)
        {
            string cssFileFolder = Path.GetDirectoryName(cssFilePath);

            if (imgUrl.StartsWith("/"))
            {
                imgUrl = imgUrl.Substring(1).Replace("/", "\\");
                if (!string.IsNullOrEmpty(imgFileRoot))
                    return Path.Combine(imgFileRoot, imgUrl);

                return GetFilePathByFolderAndAbsoluteUrl(cssFileFolder, imgUrl);
            }

            if (imgUrl.StartsWith("../"))
                return GetFilePathBFolderAndRelativeUrl(cssFileFolder, imgUrl);

            imgUrl = imgUrl.Replace("/", "\\");
            return Path.Combine(cssFileFolder, imgUrl);
        }

        /// <summary>
        /// 根据CSS文件所在目录和图片相对网站根目录的绝对路径寻找图片对应的物理路径
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="absoluteUrl"></param>
        /// <returns></returns>
        private static string GetFilePathByFolderAndAbsoluteUrl(string folder, string absoluteUrl)
        {
            if (!Directory.Exists(folder))
                return string.Empty;

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            string filePath = Path.Combine(dirInfo.FullName, absoluteUrl);
            while (!File.Exists(filePath))
            {
                dirInfo = dirInfo.Parent;
                if (null == dirInfo)
                {
                    filePath = string.Empty;
                    break;
                }

                filePath = Path.Combine(dirInfo.FullName, absoluteUrl);
            }

            return filePath;
        }

        /// <summary>
        /// 根据CSS文件所在目录和图片相对路径寻找图片对应的物理路径
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        private static string GetFilePathBFolderAndRelativeUrl(string folder, string relativeUrl)
        {
            if (!Directory.Exists(folder))
                return string.Empty;

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            while (relativeUrl.StartsWith("../"))
            {
                relativeUrl = relativeUrl.Substring(3);
                dirInfo = dirInfo.Parent;
                if (null == dirInfo)
                    break;
            }

            if (relativeUrl.StartsWith("../") || null == dirInfo)
                return string.Empty;

            return Path.Combine(dirInfo.FullName, relativeUrl.Replace("/", "\\"));
        }

        /// <summary>
        /// 重命名文件，并返回新的文件路径
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="saveRoot"></param>
        /// <param name="renameFixed"></param>
        /// <returns></returns>
        public static string FileRename(this string sourceFileName, string saveRoot, string renameFixed)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourceFileName);
            string extName = Path.GetExtension(sourceFileName);
            return Path.Combine(saveRoot, fileName + renameFixed + extName);
        }
    }
}