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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfAnimatedGif;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for browseGifs.xaml
    /// </summary>
    public partial class browseGifs : UserControl
    {
        public browseGifs()
        {
            InitializeComponent();

            WebClient Client = new WebClient();
            Client.DownloadFile("https://raw.githubusercontent.com/pikao233/gif2wallpaper/master/reference/references.txt", @"references.txt");

            foreach(string reference in File.ReadAllLines("references.txt"))
            {
                var gifEmbed = new Image();
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(reference);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(gifEmbed, image);
                onlineGIFcontainer.Children.Add(gifEmbed);
            }







        }
    }
}
