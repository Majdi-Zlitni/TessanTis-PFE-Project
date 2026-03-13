using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.Versioning;

[SupportedOSPlatform("windows")]
public static class BrowserUtility
{
    public static List<Browser> GetBrowsers()
    {
        RegistryKey browserKeys;
        //on 64bit the browsers are in a different location
        browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432NODE\Clients\StartMenuInternet");
        if (browserKeys == null)
            browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
        string[] browsersNames = browserKeys.GetSubKeyNames();
        var browsers = new List<Browser>();
        for (int i = 0; i < browsersNames.Length; i++)
        {
            Browser browser = new Browser();
            RegistryKey browserKey = browserKeys.OpenSubKey(browsersNames[i]);
            browser.Name = (string)browserKey.GetValue(null);
            RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
            browser.Path = (string)browserKeyPath.GetValue(null).ToString().StripQuotes();
            RegistryKey browserIconPath = browserKey.OpenSubKey(@"DefaultIcon");
            browser.IconPath = (string)browserIconPath.GetValue(null).ToString().StripQuotes();
            browsers.Add(browser);
            if (browser.Path != null)
                browser.Version = FileVersionInfo.GetVersionInfo(browser.Path).FileVersion;
            else
                browser.Version = "unknown";
        }
        return browsers;
    }
}

internal static class Extensions
{
    //if string begins and ends with quotes, they are removed
    internal static String StripQuotes(this String s)
    {
        if (s.EndsWith("\"") && s.StartsWith("\""))
        {
            return s.Substring(1, s.Length - 2);
        }
        else
        {
            return s;
        }
    }
}

public class Browser
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string IconPath { get; set; }
    public string Version { get; set; }
}
