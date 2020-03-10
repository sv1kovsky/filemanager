using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace filemen
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string way="";
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(metroTextBox1_KeyDown);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            showdrives();
        }
        private void showdrives()
        {
            treeView1.Nodes.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
                treeView1.Nodes.Add(drive.Name);
        }
        private void showdirs()
        {
            treeView1.Nodes.Clear();
            string[] dirs = Directory.GetDirectories(@way);
            foreach (string dir in dirs)
                treeView1.Nodes.Add(dir);

        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            way = treeView1.SelectedNode.Text.ToString();
            treeView1.Nodes.Clear();
            showdirs();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (way.Length == 3)
            {
                way = "";
                showdrives();
            }
            if (way.Length != 0)
            {
                int i = way.LastIndexOf('\\');
                way = way.Substring(0, i);
                if (way.Length == 2) way += '\\';
                showdirs();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (way.Length > 3)
            {
                metroButton1.Enabled = false;
                metroButton2.Enabled = false;
                treeView1.Enabled = false;
                metroTextBox1.Text = "Введите название папки и нажмите enter";
                metroTextBox1.ReadOnly = false;
            }
        }

        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && metroTextBox1.Text.Length != 0)
            {
                Directory.CreateDirectory(way + '\\' + metroTextBox1.Text);
                metroButton1.Enabled = true;
                metroButton2.Enabled = true;
                treeView1.Enabled = true;
                metroTextBox1.ReadOnly = true;
                metroTextBox1.Text = "";
                showdirs();
            }
        }
    }
}
