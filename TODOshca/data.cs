using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

namespace TODOshca
{
    class data
    {
        public string PathFolder { get; private set; }
        public string PathConfigFolder { get; private set; }
        public data()
        {
            string userFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            //MessageBox.Show(userFolder);
            PathFolder = userFolder+@"\TODOshca";
            PathConfigFolder = userFolder+@"\TODOshca\config";

            checkFolder(PathFolder);
            checkFolder(PathConfigFolder);
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
    }
}
