using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;

class Program
{
  static void Main(string[] args)
  {
    try
    {
      //Wcl(ConsoleColor.DarkYellow, $"\r\n\n{xArcGageWpfApp.GetDotNetVersion.Get1()}\r\n\n");

      var v3 = xArcGageWpfApp.GetDotNetVersion.Get3();
      Wcl(ConsoleColor.DarkGray, "\n\n\t         Version: ");
      Wcl(ConsoleColor.Cyan, v3[0]);
      Wcl(ConsoleColor.DarkGray, "\n\t     Release Key: ");
      Wcl(ConsoleColor.Yellow, v3[1]);
      Wcl(ConsoleColor.DarkGray, "\n\t            ==> : ");
      Wcl(ConsoleColor.Green, v3[2]);

      Process.Start("cmd", "/K dotnet --version");

#if flase // ~4.5 or above: server MKEODBATCH has c:\Win...\.NET\v4.0.30319 folder but does not even run 4.8 (2020-04) 
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName);
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
    Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : "); Wcl(ConsoleColor.Gray, typeof(object).GetTypeInfo().Assembly.GetName().Version.ToString());
#endif

      Wcl(ConsoleColor.DarkGray, "\n\t           Core ? : https://dotnet.microsoft.com/download/dotnet-core");

      Wcl(ConsoleColor.DarkYellow, "\r\n\n\n\tPress any key to continue ...\r\n\n\t\t...or any other key to quit >\r\n\n");
    }    catch (Exception ex) { Wcl(ConsoleColor.Red, ex.Message); }
    Console.ReadKey(true); // System.Threading.Thread.Sleep(2500);
  }
  public static void Wcl(ConsoleColor clr, string msg)
  {
    Console.ForegroundColor = clr;
    Console.Write(msg);
    Console.ResetColor();
  }
}