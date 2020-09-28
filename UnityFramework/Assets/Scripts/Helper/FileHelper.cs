using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codingriver
{
    public static class FileHelper
    {
        /// <summary>
        /// 获取全部文件（包括子目录）
        /// </summary>
        /// <param name="files"></param>
        /// <param name="dir"></param>
		public static void GetAllFiles(List<string> files, string dir)
        {
            try
            {

                string[] fls = Directory.GetFiles(dir);
                foreach (string fl in fls)
                {
                    files.Add(fl);
                }

                string[] subDirs = Directory.GetDirectories(dir);
                foreach (string subDir in subDirs)
                {
                    GetAllFiles(files, subDir);
                }
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string[] GetFiles(string path, bool recursive = true, string searchPattern = "*")
        {
            try
            {
                string[] files = Directory.GetFiles(path, searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                return files;
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void CleanDirectory(string dir)
        {
            try
            {
                foreach (string subdir in Directory.GetDirectories(dir))
                {
                    Directory.Delete(subdir, true);
                }

                foreach (string subFile in Directory.GetFiles(dir))
                {
                    File.Delete(subFile);
                }
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            try
            {
                DirectoryInfo source = new DirectoryInfo(srcDir);
                DirectoryInfo target = new DirectoryInfo(tgtDir);

                if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("父目录不能拷贝到子目录！");
                }

                if (!source.Exists)
                {
                    return;
                }

                if (!target.Exists)
                {
                    target.Create();
                }

                FileInfo[] files = source.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
                }

                DirectoryInfo[] dirs = source.GetDirectories();

                for (int j = 0; j < dirs.Length; j++)
                {
                    CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
                }

            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 拷贝文件夹和文件夹内文件，强制覆盖文件
        /// </summary>
        /// <param name="originDir"></param>
        /// <param name="targetDir"></param>
        /// <param name="searchPattern"></param>
        public static bool CopyDirectory(string originDir, string targetDir, string searchPattern, bool overwrite = true)
        {
            if (!Directory.Exists(originDir))
                return true;
            originDir = originDir.Replace("\\", "/");
            targetDir = targetDir.Replace("\\", "/");
            if (!originDir.EndsWith("/"))
                originDir = originDir + "/";
            if (!targetDir.EndsWith("/"))
                targetDir = targetDir + "/";

            string[] files = Directory.GetFiles(originDir, searchPattern, SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                string originPath = files[i];
                string targetPath = originPath.Replace(originDir, targetDir);
                if (!CopyFile(originPath, targetPath, overwrite))
                {
                    return false;
                }
            }
            string[] dirs = Directory.GetDirectories(originDir, "*", SearchOption.AllDirectories);
            for (int i = 0; i < dirs.Length; i++)
            {
                string _targetDir = dirs[i].Replace(originDir, targetDir);
                try
                {
                    if (!Directory.Exists(_targetDir))
                    {
                        Directory.CreateDirectory(_targetDir);
                    }

                }
                catch (IOException e)
                {
                    UnityEngine.Debug.LogError("FileTools::CopyDirectory: IOException:" + e.ToString());
                    return false;
                }
            }

            return true;
        }

        public static bool CopyFile(string originPath, string targetPath, bool overwrite)
        {
            if (!File.Exists(originPath))
                return true;


            try
            {
                string targetDir = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }
                File.Copy(originPath, targetPath, overwrite);
                return true;
            }
            catch (System.IO.IOException e)
            {
                UnityEngine.Debug.LogErrorFormat("CopyFile::Error,IOException({0})", e.ToString());

            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogErrorFormat("CopyFile::Error,Exception({0})", e.ToString());

            }


            return false;
        }

        public static void CopyFile(string src, string dst)
        {
            try
            {
                FileInfo fi1 = new FileInfo(src);
                FileInfo fi2 = new FileInfo(dst);
                if (!fi1.Exists)
                {
                    return;
                }
                if (!fi2.Directory.Exists)
                {
                    fi2.Directory.Create();
                }
                File.Copy(src, dst, true);

            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 复制文件或者文件夹
        /// 文件：如果目标路径不存在则自动创建(默认强制覆盖)
        /// 文件夹：目标路径不存在则自动创建，复制文件夹，根据通配符复制文件(默认强制覆盖)
        /// </summary>
        /// <param name="originPath">源路径</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="searchPattern">通配符</param>
        /// <param name="overwrite">是否强制覆盖，默认强制覆盖</param>
        /// <returns>true:操作成功；false:操作失败</returns>
        public static bool Copy(string originPath, string targetPath, string searchPattern = "*", bool overwrite = true)
        {
            if (Directory.Exists(originPath))
            {
                return CopyDirectory(originPath, targetPath, searchPattern, overwrite);
            }
            else if (File.Exists(originPath))
            {
                return CopyFile(originPath, targetPath, overwrite);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Copy::Error,originPath({0}) is not find!", originPath);
            }
            return false;
        }

        /// <summary>
        /// 删除文件或者目录（递归的删除目录）
        /// </summary>
        /// <param name="source"></param>
        public static void DeleteFileOrDirectory(string source)
        {
            try
            {
                FileInfo fi = new FileInfo(source);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(source);
                    if (!di.Exists)
                    {
                        return;
                    }
                    string[] files = Directory.GetFiles(source, "*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                    string[] dirs = Directory.GetDirectories(source, "*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        DeleteFileOrDirectory(dirs[i]);
                    }

                }
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// 将文件或目录从给定路径移动到其他路径。
        /// 移动目录时，目标目录存在则先清空
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void MoveFileOrDirectory(string source, string dest)
        {
            try
            {
                FileInfo fi = new FileInfo(source);
                if (fi.Exists)
                {
                    FileInfo fi1 = new FileInfo(dest);
                    if (!fi1.Directory.Exists)
                    {
                        fi1.Directory.Create();
                    }
                    if (fi1.Exists)
                    {
                        File.Delete(dest);
                    }
                    File.Move(source, dest);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(source);
                    DirectoryInfo di1 = new DirectoryInfo(dest);
                    if (!di.Exists)
                    {
                        return;
                    }
                    if (!di1.Exists)
                    {
                        di1.Create();
                    }
                    else
                    {
                        CleanDirectory(dest);
                    }
                    di.MoveTo(dest);
                }
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        /// <summary>
        /// 替换文件
        /// 将位于 dst 的文件替换为位于 src 的文件。 如果 dst 不存在，它将复制文件。如果 dst 存在， 那么它会删除其中的文件并将位于 src 的文件复制到 dst
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void ReplaceFile(string src, string dst)
        {
            try
            {
                FileInfo fi1 = new FileInfo(src);
                FileInfo fi2 = new FileInfo(dst);
                if (!fi1.Exists)
                {
                    return;
                }

                if (fi2.Exists)
                {
                    File.Delete(dst);
                }
                if (!fi2.Directory.Exists)
                {
                    fi2.Directory.Create();
                }
                File.Copy(src, dst, true);

            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// 替换文件夹
        /// 将位于 dst 的目录替换为位于 src 的目录。 如果 dst 不存在，它将复制文件。如果 dst 存在， 那么它会删除其中的目录并将位于 src 的目录复制到 dst
        /// 替换目录时，目标目录存在则先清空
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void ReplaceDirectory(string src, string dst)
        {
            try
            {
                DirectoryInfo di1 = new DirectoryInfo(src);
                DirectoryInfo di2 = new DirectoryInfo(dst);
                if (!di1.Exists)
                {
                    return;
                }
                if (di2.Exists)
                {
                    CleanDirectory(dst);
                }
                else
                {
                    di2.Create();
                }
                CopyDirectory(src, dst);
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 转换成为人可读文件的大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static String ConvertHumanFileSize(double size)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            float mod = 1024.0f;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];

        }
        /// <summary>
        /// 创建目录，检查目录是否存在，如果存在则清空目录
        /// </summary>
        /// <param name="dir"></param>
        public static void CreateNewForder(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    CleanDirectory(dir);
                }
                else
                {
                    DirectoryInfo dirinfo = Directory.CreateDirectory(dir);
                    
                }
            }
            catch (IOException e)
            {

                UnityEngine.Debug.LogError("CreateNewForder::"+e.ToString());
                
            }
        }

    }
}
