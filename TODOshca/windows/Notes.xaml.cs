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

using System.IO;

namespace TODOshca.windows
{
    /// <summary>
    /// Логика взаимодействия для Notes.xaml
    /// </summary>
    public partial class Notes : Window
    {
        private data _data = new data();
        private temporaryData tempData = new temporaryData();
        public Notes()
        {
            InitializeComponent();
            checkFolder(_data.PathFolder);
            createButtons(_data.PathFolder);
        }

        private int checkNotes(string pathFiles)
        {
            string numbersAllFiles = new DirectoryInfo(pathFiles).GetFiles().Length.ToString();
            return Convert.ToInt32(numbersAllFiles);
        }

        private void checkFolder(string path)
        {
            //folder for save files
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        private void createButtons(string pathFiles)
        {
            List<string> filesname = Directory.GetFiles(pathFiles, "*.txt").ToList<string>();
            for (int i = 0; i < checkNotes(pathFiles); i++)
            {

                var button = new Button();

                button.Content = getNameFile(filesname[i]);
                button.FontSize = 15;
                button.FontFamily = new FontFamily("Comic Sans MS");

                button.Background = System.Windows.Media.Brushes.LightYellow;

                //button.Name = getNameFile(filesname[i]);

                button.Click += OpenNote;

                StPanel.Children.Add(button);
            }
        }

        private string getNameFile(string fullNameFile)
        {
            string[] files = fullNameFile.Split('~','.');
            return files[files.Length - 2];
        }

        private void OpenNote(Object sender, EventArgs e){
            string fileName = (sender as Button).Content.ToString();
            temporaryData.ActiveNote = fileName;
            tempData.SaveActiveNote();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
