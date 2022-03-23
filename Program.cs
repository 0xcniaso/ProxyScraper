using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Security.Principal;

namespace ProxyScraper
{
    //Made by Niaso (@Zpk2)
    //https://github.com/Zpk2

    class Program
    {
        static string ActualVersion = "1.0";

        static string IntroText = @"__________                              _________                                        
\______   \_______  _______  ______.__./   _____/ ________________  ______   ___________ 
 |     ___/\_  __ \/  _ \  \/  <   |  |\_____  \_/ ___\_  __ \__  \ \____ \_/ __ \_  __ \
 |    |     |  | \(  <_> >    < \___  |/        \  \___|  | \// __ \|  |_> >  ___/|  | \/
 |____|     |__|   \____/__/\_ \/ ____/_______  /\___  >__|  (____  /   __/ \___  >__|   
                              \/\/            \/     \/           \/|__|        \/      
               __
    ..=====.. |==|       [--]    Proxy Scraper (HTTP(S), SOCKS4 & 5)  [--]
    ||     || |= |       [--]           Created by : Niaso (@Zpk2)    [--]
 _  ||     || |^*| _     [--]              Version : 1.0              [--]
|=| o=,===,=o |__||=|    [--]    We are not responsible of the data   [--]
|_|  _______)~`)  |_|    [--]           leak trough the proxy's       [--]
    [=======]  ()        [--]                                         [--]
                         [--]         SELECT AN OPTION TO BEGIN:      [--]
                         [--] ._______________________________________[--]
                         \_.---------------------------------------------/


   [1] Download proxys
   [2] Help
   [3] Credits
   [4] Author's Github";

        static void Main(string[] args)
        {
            Console.Title = "ProxyScraper by Niaso";

            Home:

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(IntroText);
            Console.ForegroundColor = ConsoleColor.White;
            CheckAdmin();
            CheckUpdate();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Environment.NewLine + "@ProxyScraper: ");
            Console.ForegroundColor = ConsoleColor.White;
            String SelectedOption = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);

            if (SelectedOption == "1")
            {
                Start:

                Console.Write("Please select a protocol (http(s), socks4, socks5): ");
                String SelectedProtocol = Console.ReadLine();
                Console.WriteLine(Environment.NewLine);
                Console.Write("Please enter the path where to save proxy list: ");
                String SavePath = Console.ReadLine();

                if (SavePath == "")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid path.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to retry");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }

                if (SelectedProtocol == "http(s)")
                {
                    DownloadProxy("http(s)", SavePath);

                    if (File.Exists(SavePath + "\\Http(s)ProxyList.txt"))
                    {
                        Console.WriteLine("Succes ! Proxy list saved at : " + SavePath + @"\Http(s)ProxyList.txt");
                    }

                    Console.ReadLine();
                }
                else if (SelectedProtocol == "socks4")
                {
                    DownloadProxy("socks4", SavePath);

                    if (File.Exists(SavePath + "\\Socks4ProxyList.txt"))
                    {
                        Console.WriteLine("Succes ! Proxy list saved at : " + SavePath + @"\Socks4ProxyList.txt");
                    }

                    Console.ReadLine();
                }
                else if (SelectedProtocol == "socks5")
                {
                    DownloadProxy("socks5", SavePath);

                    if (File.Exists(SavePath + "\\Socks5ProxyList.txt"))
                    {
                        Console.WriteLine("Succes ! Proxy list saved at : " + SavePath + @"\Socks5ProxyList.txt");
                    }

                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid option.");
                    Console.WriteLine(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to retry");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
            }
            else if (SelectedOption == "2")
            {
                Console.WriteLine("Help: Why are you looking for help its so simple to use this ProxyScraper");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press any key to return to home");
                Console.ReadKey();
                Console.Clear();
                goto Home;
            }
            else if (SelectedOption == "3")
            {
                Console.WriteLine("Thanks to me @Zpk2, to @clarketm and his proxy-list repo, @TheSpeedX and his PROXY-List repo, pubproxy.com, proxy-list.download, proxyscrape.com,");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press any key to return to home");
                Console.ReadKey();
                Console.Clear();
                goto Home;
            }   
            else if (SelectedOption == "4")
            {
                Console.WriteLine("@Zpk2 / https://github.com/Zpk2");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Press any key to return to home");
                Console.ReadKey();
                Console.Clear();
                goto Home;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select a valid option." + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to return to home");
                Console.ReadKey();
                Console.Clear();
                goto Home;
            }
        }

        private static void DownloadProxy(string ProxyType , string DownloadPath)
        {
            try
            {
                if (ProxyType == "http(s)")
                {
                    WebClient HttpwebClient = new WebClient();

                    try
                    {
                        string HttpProxySource1 = HttpwebClient.DownloadString("https://raw.githubusercontent.com/clarketm/proxy-list/master/proxy-list-raw.txt");
                        string HttpProxySource2 = HttpwebClient.DownloadString("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/http.txt");
                        string HttpProxySource3 = HttpwebClient.DownloadString("http://pubproxy.com/api/proxy?type=http&format=txt&limit=5");
                        string HttpProxySource4 = HttpwebClient.DownloadString("http://pubproxy.com/api/proxy?type=http&format=txt&limit=5&https=true");
                        string HttpProxySource5 = HttpwebClient.DownloadString("https://www.proxy-list.download/api/v1/get?type=http");
                        string HttpProxySource6 = HttpwebClient.DownloadString("https://www.proxy-list.download/api/v1/get?type=https");
                        string HttpProxySource7 = HttpwebClient.DownloadString("https://api.proxyscrape.com/v2/?request=displayproxies&protocol=http&timeout=10000&country=all&ssl=all&anonymity=all");

                        try
                        {
                            try
                            {
                                if (File.Exists(DownloadPath + "\\Http(s)ProxyList.txt"))
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File already exist, please delete it before download.");
                                    Console.WriteLine(Environment.NewLine);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Press any key to restart");
                                    Console.ReadKey();
                                    Console.Clear();
                                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    using (StreamWriter writer = new StreamWriter(DownloadPath + "\\Http(s)ProxyList.txt"))
                                    {
                                        writer.Write(HttpProxySource1);
                                        writer.Write(HttpProxySource2);
                                        writer.Write(HttpProxySource3);
                                        writer.Write(HttpProxySource4);
                                        writer.Write(HttpProxySource5);
                                        writer.Write(HttpProxySource6);
                                        writer.Write(HttpProxySource7);
                                    }
                                }
                            }
                            catch(UnauthorizedAccessException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Environment.NewLine);
                                Console.WriteLine("Unauthorized to write file please restart app with administrator privileges");
                                Console.WriteLine(Environment.NewLine);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Press any key to exiy");
                                Console.ReadKey();
                                Console.Clear();
                                Environment.Exit(0);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid path.");
                            Console.WriteLine(Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to restart");
                            Console.ReadKey();
                            Console.Clear();
                            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                            Environment.Exit(0);

                        }

                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please check your internet connection.");
                        Console.WriteLine(Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to restart");
                        Console.ReadKey();
                        Console.Clear();
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                        Environment.Exit(0);
                    }
                }
                else if (ProxyType == "socks4")
                {
                    WebClient Socks4webClient = new WebClient();

                    try
                    {
                        string Socks4ProxySource1 = Socks4webClient.DownloadString("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks4.txt");
                        string Socks4ProxySource2 = Socks4webClient.DownloadString("http://pubproxy.com/api/proxy?type=socks4&format=txt&limit=5");
                        string Socks4ProxySource3 = Socks4webClient.DownloadString("https://www.proxy-list.download/api/v1/get?type=socks4");
                        string Socks4ProxySource4 = Socks4webClient.DownloadString("https://api.proxyscrape.com/v2/?request=displayproxies&protocol=socks4&timeout=10000&country=all&anonymity=all");

                        try
                        {
                            try
                            {
                                if (File.Exists(DownloadPath + "\\Socks4ProxyList.txt"))
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File already exist, please delete it before download.");
                                    Console.WriteLine(Environment.NewLine);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Press any key to restart");
                                    Console.ReadKey();
                                    Console.Clear();
                                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    using (StreamWriter writer = new StreamWriter(DownloadPath + "\\Socks4ProxyList.txt"))
                                    {
                                        writer.Write(Socks4ProxySource1);
                                        writer.Write(Socks4ProxySource2);
                                        writer.Write(Socks4ProxySource3);
                                        writer.Write(Socks4ProxySource4);
                                    }
                                }
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Environment.NewLine);
                                Console.WriteLine("Unauthorized to write file please restart app with administrator privileges");
                                Console.WriteLine(Environment.NewLine);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Press any key to exit");
                                Console.ReadKey();
                                Console.Clear();
                                Environment.Exit(0);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid path.");
                            Console.WriteLine(Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to restart");
                            Console.ReadKey();
                            Console.Clear();
                            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                            Environment.Exit(0);
                        }

                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please check your internet connection.");
                        Console.WriteLine(Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to restart");
                        Console.ReadKey();
                        Console.Clear();
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                        Environment.Exit(0);
                    }
                }
                else if (ProxyType == "socks5")
                {
                    WebClient Socks5webClient = new WebClient();

                    try
                    {
                        string Socks5ProxySource1 = Socks5webClient.DownloadString("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks5.txt");
                        string Socks5ProxySource2 = Socks5webClient.DownloadString("http://pubproxy.com/api/proxy?type=socks5&format=txt&limit=5");
                        string Socks5ProxySource3 = Socks5webClient.DownloadString("https://www.proxy-list.download/api/v1/get?type=socks5");
                        string Socks5ProxySource4 = Socks5webClient.DownloadString("https://api.proxyscrape.com/v2/?request=displayproxies&protocol=socks5&timeout=10000&country=all&anonymity=all");

                        try
                        {
                            try
                            {
                                if (File.Exists(DownloadPath + "\\Socks5ProxyList.txt"))
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("File already exist, please delete it before download.");
                                    Console.WriteLine(Environment.NewLine);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Press any key to restart");
                                    Console.ReadKey();
                                    Console.Clear();
                                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    using (StreamWriter writer = new StreamWriter(DownloadPath + "\\Socks5ProxyList.txt"))
                                    {
                                        writer.Write(Socks5ProxySource1);
                                        writer.Write(Socks5ProxySource2);
                                        writer.Write(Socks5ProxySource3);
                                        writer.Write(Socks5ProxySource4);
                                    }
                                }
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Environment.NewLine);
                                Console.WriteLine("Unauthorized to write file please restart app with administrator privileges");
                                Console.WriteLine(Environment.NewLine);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Press any key to exit");
                                Console.ReadKey();
                                Console.Clear();
                                Environment.Exit(0);
                            }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid path.");
                            Console.WriteLine(Environment.NewLine);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press any key to restart");
                            Console.ReadKey();
                            Console.Clear();
                            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                            Environment.Exit(0);
                        }

                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please check your internet connection.");
                        Console.WriteLine(Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to restart");
                        Console.ReadKey();
                        Console.Clear();
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error downloading proxys.");
                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to restart");
                Console.ReadKey();
                Console.Clear();
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(0);
            }
        }

        private static void CheckUpdate()
        {
            try
            {
                WebClient UpdatewebClient = new WebClient();

                string WebCheckVersion = UpdatewebClient.DownloadString("https://raw.githubusercontent.com/Zpk2/ProxyScraper/main/ActualVersion.txt");

                if (WebCheckVersion == ActualVersion)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Environment.NewLine + "Sotware is up to date.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Environment.NewLine + "An update is aviable. Download aviable at https://github.com/Zpk2/ProxyScraper");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Environment.NewLine + "Unable to check for new updates.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void CheckAdmin()
        {
            try
            {
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Environment.NewLine + "Started with admin privileges");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "Started without admin privileges");
                }
            }
            catch(Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Environment.NewLine + "Unable to check privileges.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
