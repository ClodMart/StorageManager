using DBManagerTester.Classes;
using DBManagerTester.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DBManagerTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).ImportCSV(FileUri.Text);
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (Directory.Exists(Directory.GetParent(FileUri.Text).FullName))
            {
                openFileDialog.InitialDirectory = Directory.GetParent(FileUri.Text).FullName;
            }
            else
            {
                openFileDialog.InitialDirectory = "C:\\";
            }
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;

            if ((bool)openFileDialog.ShowDialog())
            {
                //Get the path of specified file
                FileUri.Text = openFileDialog.FileName;
            }
        }

        private void dbList_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).UpdateData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTypeSelector.ItemsSource = Enum.GetValues(typeof(TypeOfData));
            DataTypeOutput.ItemsSource = Enum.GetValues(typeof(TypeOfData));
        }
    }
}
