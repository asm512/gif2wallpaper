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
using System.Net;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for splashScreen.xaml
    /// </summary>
    public partial class splashScreen : Window
    {
        public splashScreen()
        {
            InitializeComponent();




            var progressbarTimer = new Timer { Interval = 2 };
            progressbarTimer.Tick += (o, args) =>
            {
                if (progressBar.Value < 100)
                {
                    progressBar.Value++;
                }
                else if (progressBar.Value == 100)
                {
                    //note to self : put something here to prevent window from closing whilst downloads are ongoing , e.g. if !file exists && file size < proper size
                    progressbarTimer.Stop();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            };
            progressbarTimer.Start();


            //Setup stuff here

            if(!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\data"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\data");
            }



            string splashScreenGIF = AppDomain.CurrentDomain.BaseDirectory + @"\data\splashScreen.gif";

            WebClient gifDownloader = new WebClient();

            if (!File.Exists(splashScreenGIF))
            {
                System.Windows.MessageBox.Show("Missing reference file found, will be downloaded from Github");
                gifDownloader.DownloadFile("https://raw.githubusercontent.com/pikao233/gif2wallpaper/master/media/splashScreen.gif", splashScreenGIF);
            }
            else
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(splashScreenGIF);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(gifContainer, image);
            }

            if(!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\data\searching.gif"))
            {
                System.Windows.MessageBox.Show("Missing reference file found, will be downloaded from Github");
                gifDownloader.DownloadFile("https://raw.githubusercontent.com/pikao233/gif2wallpaper/master/media/searching.gif", AppDomain.CurrentDomain.BaseDirectory + @"\data\searching.gif");
            }



            File.Delete("gifPath.g2w");
            string extractedFrames = AppDomain.CurrentDomain.BaseDirectory + @"\extractedFrames";

            if (Directory.Exists(extractedFrames))
            {
                foreach (string file in Directory.GetFiles(@"C:\Users\f6run\source\repos\gif2Wallpaper\gif2Wallpaper\bin\Debug\extractedFrames"))
                {
                    File.Delete(file);
                }
            }
            else
            {
                Directory.CreateDirectory(extractedFrames);
            }

            var settings = new IniFile(@"settings.ini");

            string[] keys = { "customPath", "customInterval" };

            List<string> INIValues = new List<string>();
 

            foreach (string value in keys)
            {
                if (!settings.KeyExists(value))
                {
                    if (value == "customPath")
                    {
                        settings.Write("customPath", "false");
                    }
                    else if (value == "customInterval")
                    {
                        settings.Write("customInterval", "false");
                    }
                }
            }





        }
    }
}
