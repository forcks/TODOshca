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
using System.Windows.Shapes;

namespace TODOshca.windows
{
    /// <summary>
    /// Логика взаимодействия для addNote.xaml
    /// </summary>
    public partial class addNote : Window
    {
        private data _data = new data();
        private temporaryData tempData = new temporaryData();
        private workWithFiles workWithFiles = new workWithFiles();
        public addNote()
        {
            InitializeComponent();
        }

        private void CreateNote(object sender, RoutedEventArgs e)
        {
            string _fileName = fileName.Text;
            if (_fileName != "" && _fileName != " " && _fileName != null)
            {
                workWithFiles.SaveFile(_data.PathFolder, "", _fileName);
                temporaryData.ActiveNote = _fileName;
                tempData.SaveActiveNote();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
