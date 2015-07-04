using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using System.Data;

namespace QDAudit
{
    // this is for utilities for working with type SystemAssetInfo.  Hence the use of static methods.
    partial class SystemAssetInfo : IComparable<SystemAssetInfo>
    {
        static public List<SystemAssetInfo> CreateSAIList(string[] computerNames)
        {
            List<SystemAssetInfo> saiList = new List<SystemAssetInfo>();
            List<Thread> tList = new List<Thread>();
            foreach (string computerName in computerNames)
            {
                tList.Add(new Thread(() => saiList.Add(new SystemAssetInfo(computerName))));
                tList[tList.Count - 1].Start();
            }
            foreach (var thread in tList) { thread.Join(); }
            return saiList;
        }

        static public List<SystemAssetInfo> CreateSAIList(string[] computerNames, ConnectionOptions connectionOptions)
        {
            List<SystemAssetInfo> saiList = new List<SystemAssetInfo>();
            List<Thread> tList = new List<Thread>();
            foreach (string computerName in computerNames)
            {
                tList.Add(new Thread(() => saiList.Add(new SystemAssetInfo(computerName, connectionOptions))));
                tList[tList.Count - 1].Start();
            }
            foreach (var thread in tList) { thread.Join(); }
            return saiList;
        }
    }
}
