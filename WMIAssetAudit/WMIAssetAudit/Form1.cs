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
        public Form1()
        {
            InitializeComponent();

            //Code to be removed later.
            
            //end test code
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string [] computerNames = Utilities.GetNamesFromFile(openFileDialog1.FileName);
            List<SystemAssetInfo> saiList = SystemAssetInfo.CreateSAIList(computerNames);
            DataTable table = SystemAssetInfo.ConvertSAIListToDataSource(saiList);
            dataGridView1.DataSource = table;
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the program.
        }
    }
}
