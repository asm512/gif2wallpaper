using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gif2Wallpaper
{
    /// <summary>
    /// Interaction logic for settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : System.Windows.Controls.UserControl
    {
        public settingsWindow()
        {
            InitializeComponent();

            var settings = new IniFile(@"settings.ini");

            int value;
            if (int.TryParse(settings.Read("customInterval"), out value))
            {
                customIntervalInput.Visibility = Visibility.Visible;
                customIntervalCheckbox.IsChecked = true;
                customIntervalInput.Text = settings.Read("customInterval").ToString();
            }
            else if (settings.Read("customInterval") == "false")
            {

            }
            else
            {
                System.Windows.MessageBox.Show("Custom Interval value contained in INI file is not valid, resetting to default");
                settings.Write("customInterval", "false");
            }
        }

        private void setCustomFolder_Click(object sender, RoutedEventArgs e)
        {
            var settings = new IniFile(@"settings.ini");

            using (var folderBrowser = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowser.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    settings.Write("customPath", folderBrowser.SelectedPath);
                    System.Windows.MessageBox.Show("Application Data folder set to " + folderBrowser.SelectedPath);
                }
                else if (folderBrowser.SelectedPath == "")
                {

                }
                else
                {
                    System.Windows.MessageBox.Show("Unable to parse folder");
                }
            }
        }

        private void customIntervalCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (customIntervalCheckbox.IsChecked == true)
            {
                customIntervalInput.Visibility = Visibility.Visible;
            }
            else
            {
                customIntervalInput.Visibility = Visibility.Hidden;
            }
        }

        private void customIntervalInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = 1;
            if (x >= 1)
            {
                var settings = new IniFile(@"settings.ini");
                if (customIntervalInput.Text == "")
                {
                    //do nothing
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(customIntervalInput.Text, "[^0-9]"))
                {
                    System.Windows.MessageBox.Show("Please enter only numbers.");
                    customIntervalInput.Text = "";
                    settings.Write("customInterval", "false");
                }
                else
                {
                    settings.Write("customInterval", customIntervalInput.Text);
                }
            }
            x++;
        }
    }
}
