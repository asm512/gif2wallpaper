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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for embeddedGif.xaml
    /// </summary>
    public partial class embeddedGif : UserControl
    {

        public embeddedGif()
        {
            InitializeComponent();

            if (File.Exists("gifPath.g2w"))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(File.ReadAllText("gifPath.g2w"));
                image.EndInit();
                ImageBehavior.SetAnimatedSource(gifContainer, image);
                confirmGifChoice.Visibility = Visibility.Visible;
            }
            else
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\data\searching.gif");
                image.EndInit();
                ImageBehavior.SetAnimatedSource(gifContainer, image);
                MessageBox.Show("No Gif Selected");
            }

        }

        private void confirmGifChoice_Click(object sender, RoutedEventArgs e)
        {
            gifConfirmer gifConfirmer = new gifConfirmer();
            gifConfirmer.ShowDialog();
        }
    }
}
