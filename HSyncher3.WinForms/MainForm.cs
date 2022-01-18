using HSyncher3.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSyncher3.WinForms
{
    public partial class MainForm : Form
    {
        private HSyncher3Library _synch = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            var ps = new Properties.Settings();

            lblSourceDirectoryRoot.Text = ps.SourceDirectoryRoot;
            lblDestinationDirectoryRoot.Text = ps.DestinationDirectoryRoot;
            chkDeleteDestinationFilesNotInSource.Checked = ps.DeleteFilesInDestinationNotInSource;
            chkRunAsynchronously.Checked = ps.RunAsynchronously;
        }

        private void SetSelectedPath(Label lblPath)
        {
            var fbd = new FolderBrowserDialog();

            fbd.ShowDialog();
            if (string.IsNullOrWhiteSpace(fbd.SelectedPath) == false)
                lblPath.Text = fbd.SelectedPath;
        }

        private void SetupEvents()
        {
            _synch.FilesToCopyEvent += _synch_FilesToCopyEvent;
            _synch.FileCopiedEvent += _synch_FileCopiedEvent;
            _synch.FilesCopiedDoneEvent += _synch_FilesCopiedDoneEvent;
        }

        private void _synch_FilesCopiedDoneEvent(object sender, FilesCopiedDoneEventArgs e)
        {
            if (chkRunAsynchronously.Checked == false)
                this.Cursor = Cursors.WaitCursor;

            MessageBox.Show("Done", "HSyncher3");
        }
        private void _synch_FileCopiedEvent(object sender, FileCopiedEventArgs e) => pbCopy.Value++;

        private void _synch_FilesToCopyEvent(object sender, AllFilesEventArgs e)
        {
            pbCopy.Minimum = 0;
            pbCopy.Value = 0;
            pbCopy.Maximum = e.FilesToCopy.Length;
        }

        private void btnSourceDirectory_Click(object sender, EventArgs e) => SetSelectedPath(lblSourceDirectoryRoot);

        private void btnDestinationDirectory_Click(object sender, EventArgs e) => SetSelectedPath(lblDestinationDirectoryRoot);

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            SetupEvents();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var ps = new Properties.Settings();

            ps.SourceDirectoryRoot = lblSourceDirectoryRoot.Text;
            ps.DestinationDirectoryRoot = lblDestinationDirectoryRoot.Text;
            ps.DeleteFilesInDestinationNotInSource = chkDeleteDestinationFilesNotInSource.Checked;
            ps.RunAsynchronously = chkRunAsynchronously.Checked;
            ps.Save();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (chkRunAsynchronously.Checked == false)
            {
                this.Cursor = Cursors.WaitCursor;
                _synch.Go(lblSourceDirectoryRoot.Text, lblDestinationDirectoryRoot.Text, chkDeleteDestinationFilesNotInSource.Checked, chkBreakOnError.Checked);
            }
            else
                Task.Run(() => _synch.Go(lblSourceDirectoryRoot.Text, lblDestinationDirectoryRoot.Text, chkDeleteDestinationFilesNotInSource.Checked, chkBreakOnError.Checked));
        }
    }
}
