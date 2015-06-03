using System;
using System.Management;
using System.Collections.Generic;

namespace WMITestRun
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SystemAssetInfo> sysAssetList = SystemAssetInfo.CreateSAIList(args);
            foreach (var sysAssetObj in sysAssetList)
            {
                Console.WriteLine("{0} {1} {2}", sysAssetObj.Name, sysAssetObj.Serial, sysAssetObj.Asset);
            }
            Console.ReadKey();
        }
    }
}
