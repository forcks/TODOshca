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
        }

        private void OpenNote(object sender, RoutedEventArgs e)
        {
            Notes notes = new Notes();
            notes.Show();
            this.Close();
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            addNote addNoteWindow = new addNote();
            addNoteWindow.Show();
            this.Close();
        }



    }
}
