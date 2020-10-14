using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace TODOshca
{
    class workWithFiles
    {
        public string path { get; private set; }

        public void SaveFile(string pathfolder, string textSave,string fileName)
        {
            path = pathfolder + "\\" + "~" + fileName + ".rtf";//txt => rtf
            deleteFile(pathfolder, fileName);
            //write to file
            using (FileStream fstream = new FileStream($"{path}", FileMode.OpenOrCreate))
            {
                // преобразование строки в байты
                byte[] text = System.Text.Encoding.Default.GetBytes(textSave);
                // запись массива байтов в файл
                fstream.Write(text, 0, text.Length);
            }
        }

        public string ReadFile(string pathfolder, string fileName)
        {
            path = pathfolder + "\\" + "~" + fileName + ".rtf";//txt => rtf

            using (FileStream fstream = File.OpenRead($"{path}"))
            {
                // преобразование строки в байты
                byte[] text = new byte[fstream.Length];
                // считывание данных
                fstream.Read(text, 0, text.Length);
                // декодирование байт в строку
                string textFromFile = System.Text.Encoding.Default.GetString(text);
                return textFromFile;
            }
        }


        //TODO:в MainWindow.xaml.cs точно такие же функции ,их удалить ,а заместо их поставить эти 
        private void deleteFile(string pathFile, string fileName)
        {
            FileInfo fileInfo = new FileInfo(pathFile + "\\" + "~" + fileName + ".rtf");//txt => rtf
            if (CheckExistsFile(pathFile, fileName))
            {
                fileInfo.Delete();
            }
        }

        public bool CheckExistsFile(string pathFile, string fileName)
        {
            FileInfo fileInfo = new FileInfo(pathFile + "\\" + "~" + fileName + ".rtf");//txt => rtf
            return fileInfo.Exists;
        }
    }
}

