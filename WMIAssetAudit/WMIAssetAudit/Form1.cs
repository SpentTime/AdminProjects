using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QDAudit
{
    public partial class Form1 : Form
    {
        private DataTable _table;

        public Form1()
        {
            InitializeComponent();

            this._table = new DataTable();
            this._table.Columns.Add("Name", typeof(string));
            this._table.Columns.Add("Asset", typeof(string));
            this._table.Columns.Add("Serial", typeof(string));
            this.dataGridView1.DataSource = this._table;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] computerNames = Utilities.GetNamesFromFile(openFileDialog1.FileName);
                List<SystemAssetInfo> saiList = SystemAssetInfo.CreateSAIList(computerNames);
                Utilities.ConvertSAIListToDataSource(saiList, this._table);
            }

        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gatherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> computerNames = new List<string>();
            foreach (DataRow row in this._table.Rows)
            {
                computerNames.Add(row.Field<string>(0));
            }
            List<SystemAssetInfo> saiList = SystemAssetInfo.CreateSAIList(computerNames.ToArray());
            this._table.Rows.Clear();
            Utilities.ConvertSAIListToDataSource(saiList, this._table);
        }
    }
}
