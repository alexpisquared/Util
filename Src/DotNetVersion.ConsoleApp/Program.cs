﻿using System;
using System.Diagnostics;
using System.IO;

class Program
{
  static void Main(string[] args)
  {
    try
    {
      //Wcl(ConsoleColor.DarkYellow, $"\r\n\n{xArcGageWpfApp.GetDotNetVersion.Get1()}\r\n\n");

      var v3 = xArcGageWpfApp.GetDotNetVersion.Get3();
      Wcl(ConsoleColor.DarkCyan, "\n  **** .NET Framework: ");
      Wcl(ConsoleColor.DarkGray, "\n\t         Version: ");
      Wcl(ConsoleColor.Cyan, v3[0]);
      Wcl(ConsoleColor.DarkGray, "\n\t     Release Key: ");
      Wcl(ConsoleColor.Yellow, v3[1]);
      Wcl(ConsoleColor.DarkGray, "\n\t            ==> : ");
      Wcl(ConsoleColor.Green, v3[2]);

      Wcl(ConsoleColor.DarkCyan, "\n\n  **** .NET Core: \n");

      //Process.Start("cmd", "/K dotnet --version"); ===

      var cmd = new Process();
      cmd.StartInfo.FileName = "cmd.exe";
      cmd.StartInfo.RedirectStandardInput = true;
      cmd.StartInfo.RedirectStandardOutput = true;
      cmd.StartInfo.CreateNoWindow = true;
      cmd.StartInfo.UseShellExecute = false;
      cmd.Start();
      cmd.StandardInput.WriteLine("echo OFF");
      //cmd.StandardInput.WriteLine("dotnet --version");
      cmd.StandardInput.WriteLine("dotnet --info");
      cmd.StandardInput.Flush();
      cmd.StandardInput.Close();
      Console.WriteLine(cmd.StandardOutput.ReadToEnd());

      Wcl(ConsoleColor.DarkGray, " App's target version: ");
      Wcl(ConsoleColor.Cyan, DotNetCoreVersion);
      Wcl(ConsoleColor.DarkGray, "     Get .NET from ");
      Wcl(ConsoleColor.Yellow, "https://dotnet.microsoft.com/download/dotnet-core");

#if flase // ~4.5 or above: server MKEODBATCH has c:\Win...\.NET\v4.0.30319 folder but does not even run 4.8 (2020-04) 
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName);
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString());
#endif

      Wcl(ConsoleColor.DarkYellow, "\r\n\n\tPress any key to continue ...\r\n\t\t...or any other key to quit ");

      File.AppendAllText("DotNetVersion.log", $"{DateTimeOffset.Now:y-MM-dd HH:mm} \t {Environment.MachineName} \t {xArcGageWpfApp.GetDotNetVersion.Get1()} \r\n");
    }
    catch (Exception ex) { Wcl(ConsoleColor.Red, ex.Message); }
    Console.ReadKey(true); // System.Threading.Thread.Sleep(2500);
  }

  static string DotNetCoreVersion { get { try { return Environment.Version.ToString(); } catch (Exception ex) { return ex.Message; } } }

  public static void Wcl(ConsoleColor clr, string msg)
  {
    Console.ForegroundColor = clr;
    Console.Write(msg);
    Console.ResetColor();
  }
}