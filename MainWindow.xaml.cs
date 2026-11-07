using com.github.mreutegg.laszip4j.laslib;
using Microsoft.Win32;
using sun.swing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LAZ_to_LAS_File_Converter.MainWindow;
using Application = System.Windows.Forms.Application;

namespace LAZ_to_LAS_File_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] selectedFiles = null;
        private readonly ViewModel _viewModel = new ViewModel();
        public ObservableCollection<report> Reports { get; set; } = new ObservableCollection<report>();
        string folder = "";
        public MainWindow()
        {
            InitializeComponent();


            dg.ItemsSource = Reports;
            DataContext = this;
            DataContext = _viewModel;
        }
        #region ToolBar
        private void quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void restart_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            Application.Restart();
        }
        private void aboutus_Click(object sender, RoutedEventArgs e)
        {
           new AboutUs().Show();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.toolB.Width = e.NewSize.Width - 15;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.agrimetsoft.com/");
        }
        private void linkProducts_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.agrimetsoft.com/products");
        }
        #endregion
        private void selectLAZ_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "LAZ files (*.laz)|*.laz|All files (*.*)|*.*",
                Title = "Select LAZ/LAS Files",
                Multiselect = true,
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    selectedFiles = openFileDialog.FileNames;
                    Reports = new ObservableCollection<report>();
                    for (int i = 0; i < selectedFiles.Length; i++)
                    {
                        var fn = System.IO.Path.GetFileName(selectedFiles[i]);
                        var ext = System.IO.Path.GetExtension(selectedFiles[i]);
                        var fileName = fn.Replace(ext, "");
                        Reports.Add(new report { FileName = fileName, Done = false, });
                    }
                    dg.ItemsSource = Reports;
                    browseFolder.IsEnabled = true;
                }
                catch (Exception ex)
                {
                  System.Windows.Forms.MessageBox.Show($"Error processing files: {ex.Message}", "Error",
                      System.Windows.Forms.MessageBoxButtons.OK,
                      System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        private void browseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folder = fbd.SelectedPath;
                    Convert.IsEnabled = true;
                }
            }
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < selectedFiles.Length; i++)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        _viewModel.ProgressValue =  100.0 * ((double)i / selectedFiles.Length);

                        LASreader lasReader = null;
                        LASwriter lasWriter = null;
                        
                        try
                        {
                            // Open the input .laz file
                            var lsro = new LASreadOpener();
                            lasReader = lsro.open(selectedFiles[i]);

                            if (lasReader == null)
                            {
                                throw new Exception("Failed to open the input .laz file.");
                            }
                            var lhd = lasReader.header.ConvertLASHeader();
                            var str1 = lhd.ToFormattedString();
                            // Set up the output .las file
                            var outputFile = System.IO.Path.Combine(folder, $"{Reports[i].FileName}.las");
                            var lswo = new com.github.mreutegg.laszip4j.laslib.LASwriteOpener();
                            lswo.set_file_name(outputFile);
                            
                             // Open the LASwriter
                             lasWriter = lswo.open(lasReader.header);

                            if (lasWriter == null)
                            {
                                throw new Exception("Failed to initialize the LASwriter.");
                            }

                            // Read and write points
                            while (lasReader.read_point())
                            {
                                lasWriter.write_point(lasReader.point);
                            }

                            // Mark as done and no error
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                Reports[i].Done = true;
                                Reports[i].Error = "No error.";
                                Reports[i].NumPoint = lasReader.header.extended_number_of_point_records.ToString();
                                Reports[i].Domain = $"X[{lasReader.header.min_x},{lasReader.header.max_x}], Y[{lasReader.header.min_x},{lasReader.header.max_x}]";
                            });
                        }
                        catch (Exception ex)
                        {
                            // Mark as not done and log the error
                            Reports[i].Done = false;
                            Reports[i].Error = ex.Message;
                        }
                        finally
                        {
                            // Ensure resources are closed
                            if (lasReader != null)
                            {
                                lasReader.close();
                            }
                            if (lasWriter != null)
                            {
                                lasWriter.close();
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Handle any unexpected errors
                    Reports[i].Done = false;
                    Reports[i].Error = $"Unexpected error: {ex.Message}";
                }

            }
            progress.Value = 100;
            dg.ItemsSource = Reports;
           System.Windows.MessageBox.Show("Finished");
        }
        class ViewModel : INotifyPropertyChanged
        {
            private double _progressValue;

            public double ProgressValue
            {
                get { return _progressValue; }
                set { _UpdatePropertyField(ref _progressValue, value); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void _UpdatePropertyField<T>(
                ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (!EqualityComparer<T>.Default.Equals(field, value))
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        public class report : INotifyPropertyChanged
        {
            private string _fileName;
            private bool _done = false;
            private string _error;
            private string _numPoint;
            private string _domain;

            public string FileName
            {
                get => _fileName;
                set
                {
                    if (_fileName != value)
                    {
                        _fileName = value;
                        OnPropertyChanged(nameof(FileName));
                    }
                }
            }
            public string NumPoint 
            {
                get => _numPoint;
                set
                {
                    if (_numPoint != value)
                    {
                        _numPoint = value;
                        OnPropertyChanged(nameof(NumPoint));
                    }
                }
            }
            public string Domain {
                get => _domain;
                set
                {
                    if (_domain != value)
                    {
                        _domain = value;
                        OnPropertyChanged(nameof(Domain));
                    }
                }
            }
            public bool Done
            {
                get => _done;
                set
                {
                    if (_done != value)
                    {
                        _done = value;
                        OnPropertyChanged(nameof(Done));
                    }
                }
            }
            public string Error
            {
                get => _error;
                set
                {
                    if (_error != value)
                    {
                        _error = value;
                        OnPropertyChanged(nameof(Error));
                    }
                }
            }
            // Implement INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
