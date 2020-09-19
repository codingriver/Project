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


    }
}
