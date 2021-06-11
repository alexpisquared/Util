using Microsoft.Win32;
using System;

namespace xArcGageWpfApp
{
  public class GetDotNetVersion // new 2021-06
  {
    public static string[] Get3()
    {
      var rv = GetPost45VersionFromRegistry3();
      if (rv != null && rv[1] != "Excepted!")
        return rv;

      return new[] { GetPre_45VersionFromRegistry(), "", "GetPost45VersionFromRegistry3() failed!!!" };
    }
    public static string Get1() { var rv = GetPost45VersionFromRegistry1(); if (rv != null) return rv; return GetPre_45VersionFromRegistry(); }

    public static string[] GetPost45VersionFromRegistry3()
    {
      try
      {
        using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
        {
          var releaseKey = (int)ndpKey.GetValue("Release");
          if (ndpKey?.GetValue("Release") != null)
            return new[] { ndpKey.GetValue("Version")?.ToString(), releaseKey.ToString(), CheckFor45PlusVersion(releaseKey) };
          else
            return null; // (".NET Framework Version 4.5 or later is not detected.");
        }
      }
      catch (Exception ex) { return new[] { "GetPost45VersionFromRegistry3()", "Excepted!", ex.Message }; }
    }
    public static string GetPost45VersionFromRegistry1()
    {
      using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
      {
        var releaseKey = (int)ndpKey.GetValue("Release");
        if (ndpKey?.GetValue("Release") != null)
          return $"{ndpKey.GetValue("Version")}  +  Release Key: {releaseKey:N0}  ►  {CheckFor45PlusVersion(releaseKey)}";
        else
          return null; // (".NET Framework Version 4.5 or later is not detected.");
      }
    }

    static string CheckFor45PlusVersion(int releaseKey)
    {
      if (releaseKey >= 528040) return "4.8 or later";
      if (releaseKey >= 461808) return "4.7.2";
      if (releaseKey >= 461308) return "4.7.1";
      if (releaseKey >= 460798) return "4.7";
      if (releaseKey >= 394802) return "4.6.2";
      if (releaseKey >= 394254) return "4.6.1";
      if (releaseKey >= 393295) return "4.6";
      if (releaseKey >= 379893) return "4.5.2";
      if (releaseKey >= 378675) return "4.5.1";
      if (releaseKey >= 378389) return "4.5";
      return "No 4.5 or later version detected"; // This code should never execute. A non-null release key should mean that 4.5 or later is installed.
    }

    public static string GetPre_45VersionFromRegistry()
    {
      try
      {
        var rv = "";
        // Opens the registry key for the .NET Framework entry.
        using (var ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        {
          // As an alternative, if you know the computers you will query are running .NET Framework 4.5 
          // or later, you can use:
          // using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, 
          // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
          foreach (string versionKeyName in ndpKey.GetSubKeyNames())
          {
            if (versionKeyName.StartsWith("v"))
            {
              var versionKey = ndpKey.OpenSubKey(versionKeyName);
              var name = (string)versionKey.GetValue("Version", "");
              var sp = versionKey.GetValue("SP", "").ToString();
              var install = versionKey.GetValue("Install", "").ToString();
              if (install == "") //no install info, must be later.
                rv += (versionKeyName + "  " + name);
              else
              {
                if (sp != "" && install == "1")
                {
                  rv += (versionKeyName + "  " + name + "  SP" + sp);
                }
              }

              if (name != "") continue;
              foreach (string subKeyName in versionKey.GetSubKeyNames())
              {
                var subKey = versionKey.OpenSubKey(subKeyName);
                name = (string)subKey.GetValue("Version", "");
                if (name != "")
                  sp = subKey.GetValue("SP", "").ToString();
                install = subKey.GetValue("Install", "").ToString();
                if (install == "") //no install info, must be later.
                  rv += (versionKeyName + "  " + name);
                else
                {
                  if (sp != "" && install == "1")
                  {
                    rv += ("  " + subKeyName + "  " + name + "  SP" + sp);
                  }
                  else if (install == "1")
                  {
                    rv += ("  " + subKeyName + "  " + name);
                  }
                }
              }
            }
          }
        }

        return rv;
      }
      catch (Exception ex) { return ex.Message; }
    }
  }
}