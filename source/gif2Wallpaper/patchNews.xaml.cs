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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for patchNews.xaml
    /// </summary>
    public partial class patchNews : UserControl
    {
        public patchNews()
        {
            InitializeComponent();

            WebClient patchnewsDownloader = new WebClient();
            patchnewsDownloader.DownloadFile("https://raw.githubusercontent.com/pikao233/gif2wallpaper/master/updates/patchNews.txt", @"patchNews.g2w");
            patchNewsContainer.Text = File.ReadAllText("patchNews.g2w");
        }
    }
}
