using System.Management;
using System.Collections.Generic;

namespace WMITestRun // rename namespace at somepoint...
{
    class SystemAssetInfo  // not sure if I want a class or a struct here
    {
        private string _name;
        private string _serial;
        private string _asset;
        private bool _good_connection;

        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public string Serial
        {
            get
            {
                return this._serial;
            }
        }
        public string Asset
        {
            get
            {
                return this._asset;
            }
        }
        public bool GoodConnection
        {
            get
            {
                return this._good_connection;
            }
        }

        //using https://msdn.microsoft.com/en-US/library/ms257337%28v=vs.80%29.aspx for examples
        // on how to connect to WMI remotely.
        public SystemAssetInfo(string computerName)
        {
            this._name = computerName;
            ManagementScope scope = new ManagementScope("\\\\" + this._name + "\\root\\cimv2");

            this._good_connection = true;

            try  // Putting this here because even if an exception happens, I want the object created.
            {
                Get_Asset(scope);
                Get_Serial(scope);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this._good_connection = false;
                this._asset = "Bad Connection";
                this._serial = e.Message;
            }
            catch (System.UnauthorizedAccessException e)
            {
                this._good_connection = false;
                this._asset = "Bad Login";
                this._serial = e.Message;
            }
            catch (System.Exception e)
            {
                this._good_connection = false;
                this._asset = e.ToString();
                this._serial = e.Message; 
            }
        }

        public SystemAssetInfo(string computerName, ConnectionOptions connectionOptions)
        {
            this._name = computerName;
            ManagementScope scope = new ManagementScope("\\\\" + this._name + "\\root\\cimv2", connectionOptions);

            this._good_connection = true;

            try  // Putting this here because even if an exception happens, I want the object created.
            {
                Get_Asset(scope);
                Get_Serial(scope);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this._good_connection = false;
                this._asset = "Bad Connection";
                this._serial = e.Message;
            }
            catch (System.UnauthorizedAccessException e)
            {
                this._good_connection = false;
                this._asset = "Bad Login";
                this._serial = e.Message;
            }
            catch (System.Exception e)
            {
                this._good_connection = false;
                this._asset = e.ToString();
                this._serial = e.Message;
            }
        }

        private void Get_Serial(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject mo in queryCollection)
            {
                this._serial = (mo["SerialNumber"].ToString()).Trim();
            }
        }

        private void Get_Asset(ManagementScope scope)
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SystemEnclosure");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();

            foreach (ManagementObject mo in queryCollection)
            {
                this._asset = (mo["SMBIOSAssetTag"].ToString()).Trim();
            }
        }

        // may want to implement threading with this method at some point...
        static public List<SystemAssetInfo> CreateSAIList(string[] computerNames)
        {
            List<SystemAssetInfo> saiList = new List<SystemAssetInfo>();
            foreach (string computerName in computerNames)
            {
                saiList.Add(new SystemAssetInfo(computerName));
            }
            return saiList;
        }

        static public List<SystemAssetInfo> CreateSAIList(string[] computerNames, ConnectionOptions connectionOptions)
        {
            List<SystemAssetInfo> saiList = new List<SystemAssetInfo>();
            foreach (string computerName in computerNames)
            {
                saiList.Add(new SystemAssetInfo(computerName, connectionOptions));
            }
            return saiList;
        }
    }
}
