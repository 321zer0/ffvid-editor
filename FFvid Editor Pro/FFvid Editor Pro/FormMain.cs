using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFvid_Editor_Pro
{
    public partial class FormMain : Form
    {
        List<MediaFile> InputFiles = new List<MediaFile>();

        public FormMain()
        {
            InitializeComponent();
        }

 
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.ShowNewFolderButton = false;
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            
            if (diag.ShowDialog() == DialogResult.OK)
            {
                TextBoxPath.Text = diag.SelectedPath;
            }
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            
            if (diag.ShowDialog() == DialogResult.OK)
            {
                InputFiles.Clear();

                foreach(string filename in diag.FileNames)
                {
                    MediaFile info = new MediaFile(filename);
                    InputFiles.Add(info);

                    // MessageBox.Show(info.ToString());
                }
            }
        }
    }
}