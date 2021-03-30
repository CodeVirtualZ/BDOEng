using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace BDOEng
{
    class Program
    {
        public static string bdo_reg = @"SOFTWARE\WOW6432Node\PearlAbyss Corp.\BlackDesert_TH";
        public static bool checklacation()
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(bdo_reg, true);
            return (reg.GetValueNames().Contains("InstallPath"));
        }

        public static void editpatch(string loc, string oldstr, string newstr)
        {
            StreamReader reader = new StreamReader(loc);
            string input = reader.ReadToEnd();
            reader.Dispose();
            using (StreamWriter writer = new StreamWriter(loc, false))
            {
                string output = input.Replace(oldstr, newstr);
                writer.Write(output);
                writer.Flush();
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "BDOEng By TA";
            Console.ForegroundColor = ConsoleColor.White;
            string s = "BlackDesert Online (TH Server) Language Changer v1.0";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop + 1);
            Console.WriteLine(s);
            Console.WriteLine("1.Change language to English");
            Console.WriteLine("2.Change language to Thai");
            Console.WriteLine("3.Exit");
            Console.WriteLine("Enter menu (1-3): ");
            string menu_selected = Console.ReadLine();

            if (checklacation() == true)
            {
                switch (Convert.ToInt32(menu_selected))
                {
                    case 1:
                        Console.WriteLine(Environment.NewLine + "Finding the installation location of BDO...");
                        try
                        {
                            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(bdo_reg))
                            {
                                if (key != null)
                                {
                                    Object o = key.GetValue("InstallPath");
                                    if (o != null)
                                    {
                                        Console.WriteLine("Game location: " + o.ToString());
                                        Console.WriteLine("Downloading English Patch...");
                                        using (var patch = new System.Net.WebClient())
                                        {
                                            patch.DownloadFile("http://dn.sea.playblackdesert.com/UploadData/ads/languagedata_en/285/languagedata_en.loc", (string)o + @"\ads\languagedata_id.loc");
                                        }
                                        System.Threading.Thread.Sleep(2500);
                                        Console.WriteLine("Changing Resource...");
                                        editpatch((string)o + @"\Resource.ini", "TH", "ID");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Done.");
                                        Console.WriteLine(Environment.NewLine + "Developer by TA");
                                        Console.WriteLine("SourceCode: https://github.com/CodeVitual");
                                        Console.WriteLine("Thanks: https://www.reddit.com/r/blackdesertonline/comments/lrid4g/guide_update_new_english_patch_links/");
                                        System.Diagnostics.Process.Start((string)o + @"\BlackDesertLauncher.exe");
                                        Environment.Exit(0);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            Console.Read();
                        }

                        break;
                    case 2:
                        Console.WriteLine(Environment.NewLine + "Finding the installation location of BDO...");
                        try
                        {
                            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(bdo_reg))
                            {
                                if (key != null)
                                {
                                    Object o = key.GetValue("InstallPath");
                                    if (o != null)
                                    {
                                        Console.WriteLine("Game location: " + o.ToString());
                                        Console.WriteLine("Restoring...");
                                        System.Threading.Thread.Sleep(2500);
                                        Console.WriteLine("Saving resource...");
                                        editpatch((string)o + @"\Resource.ini", "ID", "TH");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Done.");
                                        Console.WriteLine(Environment.NewLine + "Developer by TA");
                                        Console.WriteLine("SourceCode: https://github.com/CodeVitual");
                                        Console.WriteLine("Thanks: https://www.reddit.com/r/blackdesertonline/comments/lrid4g/guide_update_new_english_patch_links/");
                                        System.Diagnostics.Process.Start((string)o + @"\BlackDesertLauncher.exe");
                                        Environment.Exit(0);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            Console.Read();
                        }

                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error: please select functions agamin!..");
                        break;
                }
            }
            else
            {
                Console.WriteLine("This application is not working on your PC");
                Console.Read();
            }
        }
    }
}
