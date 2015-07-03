using System;
using System.Management;
using System.Collections.Generic;
using System.IO;

namespace WMITestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionOptions co = new ConnectionOptions();
            Console.Write("Username: ");
            co.Username = Console.ReadLine();
            Console.Write("Password: ");
            co.Password = silientInput();
            Console.WriteLine();



            List<SystemAssetInfo> sysAssetList = SystemAssetInfo.CreateSAIList(GetNamesFromFile("testFile.txt"), co);
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
                else
                    temp += ckinfo.KeyChar;

                ckinfo = Console.ReadKey(true);
            }

            return temp;
        }

        static string [] GetNamesFromFile(string fileName)
        {
            List<string> computerList = new List<string>();
            
            using (StreamReader sr = new StreamReader(fileName))
                while (!sr.EndOfStream)
                    computerList.Add(sr.ReadLine());
            
            return computerList.ToArray();
        }
    }
}
