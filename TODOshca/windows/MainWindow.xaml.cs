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

using System.IO;
using TODOshca.windows;

namespace TODOshca
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string pathFolderNote;
        private data _data = new data();
        private temporaryData tempData = new temporaryData();
        public MainWindow()
        {
            InitializeComponent();
            //folder path
            pathFolderNote = _data.PathFolder;
            LoadDataInNote(pathFolderNote,temporaryData.ActiveNote);

            //load other notes buttons 
            checkFolder(_data.PathFolder);
            createButtons(_data.PathFolder);
        }

        #region System saving
        

        private void saveFile(string savingText, string pathSave, string fileName)
        {
            /*
             have to delete file to write data to file repeatedly
             */
            deleteFile(pathSave,fileName);

            //write to file 
            using (FileStream fstream = new FileStream($"{pathSave}\\"+"~"+fileName+".txt", FileMode.OpenOrCreate))
            {
                //преобразовать строку в байты 
                byte[] textInByte = System.Text.Encoding.Default.GetBytes(savingText);

                //write byte array in file
                fstream.Write(textInByte, 0, textInByte.Length);
            }
        }

        private void deleteFile(string pathFile,string fileName)
        {
            FileInfo fileInfo = new FileInfo(pathFile+"\\"+"~"+fileName+".txt");
            if (checkExistsFile(pathFile,fileName))
            {
                fileInfo.Delete();
            }
        }

        private string GetDataInFile(string pathFile,string fileName)
        {
            if (checkExistsFile(pathFile, fileName))
            {
                using (FileStream fstream = File.OpenRead($"{pathFile}\\"+"~"+ fileName + ".txt"))
                {
                    //преобразовать строку в байты 
                    byte[] textInByte = new byte[fstream.Length];

                    //read data 
                    fstream.Read(textInByte, 0, textInByte.Length);

                    //декодирование из байтов в строку 
                    string textFromFile = System.Text.Encoding.Default.GetString(textInByte);

                    return textFromFile;
                }
            }
            else
            {
                return "Hello";
            }
        }

        private bool checkExistsFile(string pathFile,string fileName)
        {
            FileInfo fileInfo = new FileInfo(pathFile + "\\" +"~"+ fileName + ".txt");
            return fileInfo.Exists;
        }

        private void LoadDataInNote(string pathFile,string fileName)
        {
            TextNote.Text = GetDataInFile(pathFile,fileName);
        }

        #endregion

        private void SaveNote(object sender, RoutedEventArgs e)
        {
            string textNote;
            textNote = TextNote.Text;

            saveFile(textNote,pathFolderNote, temporaryData.ActiveNote);

            checkFolder(_data.PathFolder);
            createButtons(_data.PathFolder);
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            addNote addNoteWindow = new addNote();
            addNoteWindow.Show();
            this.Close();
        }

        #region go_to_other_notes
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

                OtherNotes.Children.Add(button);
            }
        }

        private string getNameFile(string fullNameFile)
        {
            string[] files = fullNameFile.Split('~', '.');
            return files[files.Length - 2];
        }

        private void OpenNote(Object sender, EventArgs e)
        {
            string fileName = (sender as Button).Content.ToString();
            temporaryData.ActiveNote = fileName;
            tempData.SaveActiveNote();
            LoadDataInNote(pathFolderNote, temporaryData.ActiveNote);
        }
        #endregion
    }
}
