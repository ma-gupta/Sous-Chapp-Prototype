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
using System.Windows.Shapes;

namespace SousChapp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SplitWindow : Window 
    {
        Boolean menuVisible = false;
        public SplitWindow()
        {
            InitializeComponent();
        }

        private void ShowMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!menuVisible)
            {
                mainMenu.Visibility = Visibility.Visible;
                menuVisible = true;
            }
            else
            {
                mainMenu.Visibility = Visibility.Hidden;
                menuVisible = false;
            }
        }

        private void MenuMouseEnter(object sender, MouseEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            item.Foreground = Brushes.Black;
        }

        private void MenuMouseLeave(object sender, MouseEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            item.Foreground = Brushes.White;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
