using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace QDAudit
{
    static class Utilities
    {
        public static string[] GetNamesFromFile(string fileName)
        {
            List<string> computerList = new List<string>();

            using (StreamReader sr = new StreamReader(fileName))
                while (!sr.EndOfStream)
                    computerList.Add(sr.ReadLine());

            return computerList.ToArray();
        }

        static public void ConvertSAIListToDataSource(List<SystemAssetInfo> sysAssetList, DataTable table)
        {
            table.Rows.Clear();
            foreach (SystemAssetInfo devInfo in sysAssetList)
                table.Rows.Add(devInfo.Name, devInfo.Asset, devInfo.Serial);
        }
    }
}
