using System;
using System.Management;
using System.Collections.Generic;

namespace WMITestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionOptions co = new ConnectionOptions();
            Console.WriteLine("Username :");
            co.Username = Console.ReadLine();
            Console.Write("Password: ");
            co.Password = silientInput();

            List<SystemAssetInfo> sysAssetList = SystemAssetInfo.CreateSAIList(args, co);
            sysAssetList.Sort();
            foreach (var sysAssetObj in sysAssetList)
            {
                Console.WriteLine("{0} {1} {2}", sysAssetObj.Name, sysAssetObj.Serial, sysAssetObj.Asset);
            }
            Console.ReadKey();
        }

        static private string silientInput()
        {
            string temp = "";
            ConsoleKeyInfo ckinfo = Console.ReadKey(true);

            while (ckinfo.Key != ConsoleKey.Enter)
            {
                if (ckinfo.Key == ConsoleKey.Backspace && temp.Length > 0)
                    temp.Remove(temp.Length - 1);
                else if (ckinfo.KeyChar > 31 && ckinfo.KeyChar < 127)
                    temp += ckinfo.KeyChar;

                ckinfo = Console.ReadKey(true);
            }

            return temp;
        }
    }
}
