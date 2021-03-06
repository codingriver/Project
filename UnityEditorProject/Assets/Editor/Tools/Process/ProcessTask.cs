﻿//=====================================================
// - FileName:    	ProcessTask 
// - Description:
// - Author:		wangguoqing
// - Email:			wangguoqing@hehemj.com
// - Created:		2017/12/13 17:12:17
// - CLR version: 	4.0.30319.42000
// - UserName:		Wang
// -  (C) Copyright 2008 - 2015, hehehuyu,Inc.
// -  All Rights Reserved.
//======================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Text;
using Debug = UnityEngine.Debug;
 
    class ProcessTask
    {




        public static bool RunProcess(string filePath,string args="")
        {
            bool isSuncc = true;
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = filePath;
            start.Arguments = args;
            start.UseShellExecute = false;//是否使用操作系统shell启动
            start.RedirectStandardOutput = true;//由调用程序获取输出信息
            start.RedirectStandardError = true;//重定向标准错误输出
            start.RedirectStandardInput = true;//接受来自调用程序的输入信息
            //start.CreateNoWindow = true;/不显示程序窗口
            //start.WorkingDirectory = "";
            using (Process process = Process.Start(start))
            {
                process.StandardInput.WriteLine(args);
                process.StandardInput.WriteLine("exit");
                process.EnableRaisingEvents = true;
                process.BeginOutputReadLine();

                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine(e.Data);
                };
                using (StreamReader reader = process.StandardError)
                {
                    string result = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        Debug.LogError(result);
                        isSuncc = false;
                    }

                }
                //using (StreamReader reader = process.StandardOutput)
                //{
                //    string result = reader.ReadToEnd();
                //    if (!string.IsNullOrEmpty(result))
                //    {
                //        UnityEngine.Debug.Log("" + result);
                //    }

                //}
                process.WaitForExit();
            }
            return isSuncc;
        }



        public static string[] RunProcess(string filePath, string args, string workDir)
        {
            bool isSuncc = true;
            List<string> ls = new List<string>();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = filePath;
            start.Arguments = args;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;
            //start.CreateNoWindow = true;
            //start.WorkingDirectory = workDir;
            string CurDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = workDir;
            using (Process process = Process.Start(start))
            {
                process.EnableRaisingEvents = true;
                //process.BeginOutputReadLine();

                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine(e.Data);
                };
                using (StreamReader reader = process.StandardError)
                {
                    string result = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        Debug.LogError(result);
                        isSuncc = false;
                    }

                }
                using (StreamReader reader = process.StandardOutput)
                {
                    string line = string.Empty;
                    while((line=reader.ReadLine())!=null)
                    {
                        ls.Add(line);
                    }

                }
                process.WaitForExit();
            }
            Environment.CurrentDirectory = CurDir;
            if(isSuncc)
            {
                return ls.ToArray();
            }

            return null;
        }


        public static void RunCmdAsync(string arg)
        {
            System.Console.InputEncoding = new System.Text.UTF8Encoding(false);
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            //process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            //process.StartInfo.CreateNoWindow = true;//不显示程序窗口
            //process.StartInfo.WorkingDirectory = "./";
            process.OutputDataReceived += new DataReceivedEventHandler(OutputReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(ErrorReceived);            
            process.EnableRaisingEvents = true;                      // 启用Exited事件  
            process.Exited += new EventHandler(ExitReceived);   // 注册进程结束事件  

            process.Start();
            //foreach (var arg in args)
            //{

            //    process.StandardInput.WriteLine(arg);
            //    process.StandardInput.Flush();
            //    process.WaitForInputIdle();
            //}
            process.StandardInput.WriteLine(arg);
            process.StandardInput.Flush();            
            process.StandardInput.WriteLine("exit");
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            // 如果打开注释，则以同步方式执行命令，此例子中用Exited事件异步执行。  
            // CmdProcess.WaitForExit();    
        }

        private static void OutputReceived(object sender,DataReceivedEventArgs e)
        {
            Debug.Log(e.Data);
        }
        private static void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data!=null&&e.Data!=string.Empty)
            {
                Debug.LogError("Error::" + e.Data);
            }
            
        }
        private static void ExitReceived(object sender, EventArgs e)
        {
            //Debug.Log("Exit::"+e.ToString());
        }        
    }

