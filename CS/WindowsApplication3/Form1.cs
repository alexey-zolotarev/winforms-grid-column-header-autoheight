using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;


namespace DXSample
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void InitData()
        {
            var dt = new DataTable();
            dt.Columns.Add("CustomerID", typeof(Int32));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Age", typeof(Int32));
            for (int i = 0; i < 5; i++)
                dt.Rows.Add(new object[] { i, string.Format("FirstName {0}", i), string.Format("LastName {0}", i), 20 + i });
            gridControl1.DataSource = dt;
        }
        AutoHeightHelper helper;
        private void OnFormLoad(object sender, EventArgs e)
        {
            InitData();
            gridControl1.ForceInitialize();
            helper = new AutoHeightHelper(gridView1);
            helper.EnableColumnPanelAutoHeight();
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            helper.DisableColumnPanelAutoHeight();
        }
    }
}
