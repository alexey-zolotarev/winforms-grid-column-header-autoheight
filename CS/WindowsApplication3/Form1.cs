using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;


namespace DXSample {
    public partial class Form1: XtraForm {
        public Form1() {
            InitializeComponent();
        }
        public void InitData() {
            for(int i = 0;i < 5;i++) 
                dataSet11.Tables[0].Rows.Add(new object[] { i, string.Format("FirstName {0}", i), string.Format("LastName {0}", i), 20 + i });
        }
        AutoHeightHelper helper;
        private void OnFormLoad(object sender, EventArgs e) {
            InitData();
            gridControl1.ForceInitialize();
            helper = new AutoHeightHelper(gridView1);
            helper.EnableColumnPanelAutoHeight();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            helper.DisableColumnPanelAutoHeight();
        }
    }
}
