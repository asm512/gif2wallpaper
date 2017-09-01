using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using System.Windows.Documents;
using System.Drawing.Imaging;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
using System.Drawing;
using System.Windows.Forms;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for gifConfirmer.xaml
    /// </summary>
    public partial class gifConfirmer : Window
    {

        public gifConfirmer()
        {
            InitializeComponent();


            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(File.ReadAllText("gifPath.g2w"));
            image.EndInit();
            ImageBehavior.SetAnimatedSource(gifContainer, image);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var settings = new IniFile(@"settings.ini");
            //Extract frames here

            string dir = @"extractedFrames\";
            var gifPath = File.ReadAllText("gifPath.g2w");
            System.Drawing.Image gifImg = System.Drawing.Image.FromFile(gifPath);
            int frames = gifImg.GetFrameCount(FrameDimension.Time);
            if (frames <= 1) System.Windows.Forms.MessageBox.Show("Image is not animated (contains 1 or less frames)");
            for (int i = 0; i < frames; i++)
            {
                gifImg.SelectActiveFrame(FrameDimension.Time, i);
                var outputFile = dir + i + ".png";
                gifImg.Save(outputFile, ImageFormat.Png);
            }


            string[] wallpapers = Directory.GetFiles(@"extractedFrames");
            int countedWallpapers = wallpapers.Length;
            int x = 0;

            if(settings.Read("customInterval")=="false")
            {
                //Get correct interval from gif frames
                PropertyItem item = gifImg.GetPropertyItem(0x5100);
                int interval = (item.Value[0] + item.Value[1] * 256) * 13;
                settings.Write("customInterval", interval.ToString());
            }



            var wallpaperTimer = new Timer { Interval = Convert.ToInt32(settings.Read("customInterval")) };
            wallpaperTimer.Tick += (o, args) =>
            {
                foreach (string wallpaper in wallpapers)
                {
                    if (x >= countedWallpapers)
                    {
                        x = 0;
                    }
                    Wallpaper.Set(wallpapers[x], Wallpaper.Style.Stretched);
                    x++;
                }
            };
            wallpaperTimer.Start();





























        }
    }
}
