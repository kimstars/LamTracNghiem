using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LamTracNghiem
{
    public partial class Report : DevExpress.XtraEditors.XtraForm
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            txtformat.Text = "Câu <STT>:<Câu hỏi><Đáp án>\nA. < Bắt đầu của đáp án là chữ cái A, B, C, D, E, F và dấu chấm >\nB. ...\nC. ...";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel1.Text);
        }
    }
}