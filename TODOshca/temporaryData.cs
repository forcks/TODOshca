using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TODOshca
{
    class temporaryData
    {
        public static string ActiveNote;
        private data _data = new data();
        private workWithFiles workWithFiles = new workWithFiles();
        private string fileName;
        private string path;

        public temporaryData()
        {
            fileName = "ConfigActiveNotes";

            path = _data.PathConfigFolder + "\\" + "~" + fileName + ".rtf";//txt => rtf
            FileInfo configActiveNote = new FileInfo(path);
            if (!configActiveNote.Exists)
            {
                ActiveNote = "First";
                SaveActiveNote();
            }
            else
            {
                ActiveNote = workWithFiles.ReadFile(_data.PathConfigFolder, "ConfigActiveNotes");
            }
        }

        public void SaveActiveNote() => workWithFiles.SaveFile(_data.PathConfigFolder, ActiveNote, "ConfigActiveNotes");
    }

}
