using HSyncher3.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HSyncher3.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Properties.Settings _settings = new Properties.Settings();
        private HSyncher3Library _synch = new();

        public MainWindow()
        {
            InitializeComponent();

            txtSourceDirectoryRoot.Text = _settings.SourceRootDirectory;
            txtDestinationDirectoryRoot.Text = _settings.DestinationRootDirectory;
            chkDeleteFilesInDestinationNotInSource.IsChecked = _settings.DeleteFilesInDestinationNotInSource;
            chkBreakOnError.IsChecked = _settings.BreakOnError;
            chkRunAsynchrounously.IsChecked = _settings.RunAsynchronously;
            _synch.FilesToCopyEvent += _synch_FilesToCopyEvent;
            _synch.FileCopiedEvent += _synch_FileCopiedEvent;

        }

        private void _synch_FileCopiedEvent(object sender, FileCopiedEventArgs e) => prg.Value += 1.0D;

        private void _synch_FilesToCopyEvent(object sender, AllFilesEventArgs e)
        {
            prg.Minimum = 0;
            prg.Value = 0;
            prg.Maximum = e.FilesToCopy.Length;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.SourceRootDirectory = txtSourceDirectoryRoot.Text;
            _settings.DestinationRootDirectory = txtDestinationDirectoryRoot.Text;
            _settings.DeleteFilesInDestinationNotInSource = chkDeleteFilesInDestinationNotInSource.IsChecked.Value;
            _settings.BreakOnError = chkBreakOnError.IsChecked.Value;
            _settings.RunAsynchronously = chkRunAsynchrounously.IsChecked.Value;
            _settings.Save();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            _synch.Go(txtSourceDirectoryRoot.Text, txtDestinationDirectoryRoot.Text, chkDeleteFilesInDestinationNotInSource.IsChecked.Value, chkBreakOnError.IsChecked.Value);

        }
    }
}
