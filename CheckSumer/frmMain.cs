using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace CheckSumer
{
    public partial class frmMain : Form
    {
        private string file;

        public frmMain()
        {
            InitializeComponent();
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAbout about = new frmAbout();
            about.Show();
        }

        private void lblDrop_DragDrop(object sender, DragEventArgs e)
        {
            if (file != string.Empty)
            {
                int slashPos = file.LastIndexOf('\\');
                string noPath = file.Substring(slashPos + 1, file.Length - slashPos - 1);
                lblFilename.Text = noPath;
                txtMD5.Text = GetMD5(file);
                lblSHA1.Text = GetSHA1(file);
                lblSHA256.Text = GetSHA256(file);
            }
        }

        private void lblDrop_DragEnter(object sender, DragEventArgs e)
        {
            string tempfile = GetFilename(e);
            if (tempfile != string.Empty)
            {
                file = tempfile;
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        } 

        private void lblDrop_DragLeave(object sender, EventArgs e)
        {

        }

        private void lblDrop_DragOver(object sender, DragEventArgs e)
        {

        }

        private string GetFilename(DragEventArgs e)
        {
            string filename = string.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
                
                if (data != null)
                {
                    filename = ((string[])data)[0];
                }
            }

            return filename;
        }

        private string GetMD5(string filename)
        {
            StringBuilder sb = new StringBuilder();
            
            using (MD5 hash = MD5.Create())
            {
                Stream stream = File.OpenRead(filename);
                byte[] data = hash.ComputeHash(stream);

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                hash.Dispose();
            }

            return sb.ToString();
        }

        private string GetSHA1(string filename)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA1 hash = SHA1.Create())
            {
                Stream stream = File.OpenRead(filename);
                byte[] data = hash.ComputeHash(stream);

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                hash.Dispose();
            }

            return sb.ToString();
        }

        private string GetSHA256(string filename)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Stream stream = File.OpenRead(filename);
                byte[] data = hash.ComputeHash(stream);

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                hash.Dispose();
            }

            return sb.ToString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblFilename.Text = string.Empty;
            txtMD5.Text = string.Empty;
            lblSHA1.Text = string.Empty;
            lblSHA256.Text = string.Empty;
        }
    }
}
