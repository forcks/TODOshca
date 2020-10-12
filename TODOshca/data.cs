using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace TODOshca
{
    class data
    {
        public string PathFolder { get; private set; }
        public string PathConfigFolder { get; private set; }
        public data()
        {
            PathFolder = @"C:\Users\kuzme\TODOshca";
            PathConfigFolder = @"C:\Users\kuzme\TODOshca\config";

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
