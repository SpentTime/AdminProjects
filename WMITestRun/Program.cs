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
            co.Password = Console.ReadLine(); /*not the most secure way to get a password, 
                                                 but I don't plan on keeping this a console application */

            List<SystemAssetInfo> sysAssetList = SystemAssetInfo.CreateSAIList(args, co);
            sysAssetList.Sort();
            foreach (var sysAssetObj in sysAssetList)
            {
                Console.WriteLine("{0} {1} {2}", sysAssetObj.Name, sysAssetObj.Serial, sysAssetObj.Asset);
            }
            Console.ReadKey();
        }
    }
}
