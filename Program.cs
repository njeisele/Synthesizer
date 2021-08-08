using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Synthesizer
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var d = Directory.GetCurrentDirectory();
            Debug.WriteLine("Here");
            Debug.WriteLine(d);
            //var root = ""
            string[] dirs = Directory.GetDirectories(d);
            foreach (string dir in dirs)
            {
                Debug.WriteLine(dir);
            }
            // TODO: Fix this, should not need to do all this to read a file
            var rootPath = "../../../../../../../../Users/neasd/source/repos/Synthesizer/Music";
            string[] files = Directory.GetFiles(rootPath);
            foreach (string f in files)
            {
                Debug.WriteLine(f);
            }
            var filePath = rootPath + "/MusicXmlWithChords.xml";
            Score score = XmlParser.GetScoreFromFile(filePath);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Synth(score));
            /*string[] files = d.GetFiles("/");
            foreach (string f in files)
            {
                Debug.WriteLine(f);
            }*/
        }

    }
}
