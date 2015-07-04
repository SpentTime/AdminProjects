using System;
using System.Management;
using System.Threading;
using System.Collections.Generic;
using System.Data;

namespace QDAudit
{
    //This contains info directly related to class SystemAssetInfo
    partial class SystemAssetInfo : IComparable<SystemAssetInfo>
    {
        public string Name { get; private set; }
        public string Serial { get; private set; }
        public string Asset { get; private set; }
        public bool GoodConnection{ get; private set; }

        //using https://msdn.microsoft.com/en-US/library/ms257337%28v=vs.80%29.aspx for examples
        // on how to connect to WMI remotely.
        public SystemAssetInfo(string computerName)
        {
            this.Name = computerName;
            ManagementScope scope = new ManagementScope("\\\\" + this.Name + "\\root\\cimv2");

            this.GoodConnection = true;

            try  // Putting this here because even if an exception happens, I want the object created.
            {
                Get_Asset(scope);
                Get_Serial(scope);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.GoodConnection = false;
                this.Asset = "Bad Connection";
                this.Serial = e.Message;
            }
            catch (System.UnauthorizedAccessException e)
            {
                this.GoodConnection = false;
                this.Asset = "Bad Login";
                this.Serial = e.Message;
            }
            catch (System.Exception e)
            {
                this.GoodConnection = false;
                this.Asset = e.ToString();
                this.Serial = e.Message; 
            }
        }

        public SystemAssetInfo(string computerName, ConnectionOptions connectionOptions)
        {
            this.Name = computerName;
            ManagementScope scope = new ManagementScope("\\\\" + this.Name + "\\root\\cimv2", connectionOptions);

            this.GoodConnection = true;

            try  // Putting this here because even if an exception happens, I want the object created.
            {
                Get_Asset(scope);
                Get_Serial(scope);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.GoodConnection = false;
                this.Asset = "Bad Connection";
                this.Serial = e.Message;
            }
            catch (System.UnauthorizedAccessException e)
            {
                this.GoodConnection = false;
                this.Asset = "Bad Login";
                this.Serial = e.Message;
            }
            catch (System.Exception e)
            {
                this.GoodConnection = false;
                this.Asset = e.ToString();
                this.Serial = e.Message;
            }
        }

        private void Get_Serial(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject mo in queryCollection)
            {
                this.Serial = (mo["SerialNumber"].ToString()).Trim();
            }
        }

        private void Get_Asset(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemEnclosure");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject mo in queryCollection)
            {
                this.Asset = (mo["SMBIOSAssetTag"].ToString()).Trim();
            }
        }

        int IComparable<SystemAssetInfo>.CompareTo(SystemAssetInfo other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
