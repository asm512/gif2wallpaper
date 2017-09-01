using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainWindowContainer.Children.Clear();
            patchNews patchNews = new patchNews();
            mainWindowContainer.Children.Add(patchNews);
        }

        private void githubLink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pikao233/gif2wallpaper");
        }

        private void selectGif_Click(object sender, RoutedEventArgs e)
        {
            File.Delete("gifPath.g2w");
            Microsoft.Win32.OpenFileDialog openGif = new Microsoft.Win32.OpenFileDialog();
            if (openGif.ShowDialog() == true)
            {
                Int64 fileSizeInBytes = new FileInfo(openGif.FileName).Length;
                if(fileSizeInBytes >= 10485760)
                {
                    System.Windows.MessageBox.Show("File size or larger than 10MB, this may cause performance issues until program is optimized");
                }
                File.WriteAllText("gifPath.g2w", openGif.FileName);
            }

            mainWindowContainer.Children.Clear();
            embeddedGif embeddedGifObj = new embeddedGif();
            mainWindowContainer.Children.Add(embeddedGifObj);

        }

        private void browseGifs_Click(object sender, RoutedEventArgs e)
        {
            mainWindowContainer.Children.Clear();
            browseGifs embeddedGifObj = new browseGifs();
            mainWindowContainer.Children.Add(embeddedGifObj);
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            mainWindowContainer.Children.Clear();
            patchNews patchNews = new patchNews();
            mainWindowContainer.Children.Add(patchNews);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            mainWindowContainer.Children.Clear();
            settingsWindow settingsWindow = new settingsWindow();
            mainWindowContainer.Children.Add(settingsWindow);
        }
    }
}
