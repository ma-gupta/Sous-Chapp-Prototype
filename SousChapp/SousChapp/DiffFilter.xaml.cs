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

namespace SousChapp
{
    /// <summary>
    /// Interaction logic for UserControl3.xaml
    /// </summary>
    public partial class DiffFilter : UserControl
    {

        private HashSet<String> all_options;
        private MainWindow mw;

        public DiffFilter()
        {
            InitializeComponent();
            all_options = new HashSet<String>();
            findChecked();
        }

        /// <summary>
        /// Finds out which checkboxes are currently checked, and adds them to the list
        /// </summary>
        private void findChecked()
        {
            all_options.Clear();
            foreach (var checkBox in Checkbox_grid.Children.OfType<CheckBox>().Where(cb => (bool)cb.IsChecked))
            {
                var name = checkBox.Content;
                all_options.Add((String)name);

            }
        }

        /// <summary>
        /// Sets the main window
        /// </summary>
        /// <param name="mw"></param>
        public void setMainWindow(MainWindow mw)
        {
            this.mw = mw;
        }

        /// <summary>
        /// The cancel button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            //Check those who we have picked before
            foreach (var checkBox in Checkbox_grid.Children.OfType<CheckBox>().Where(cb => (bool)cb.IsChecked))
            {

                var name = checkBox.Content;
                if (!all_options.Contains(name))
                { //Check those we have picked right now, and if they are new, because we cancel, mark them as false
                    checkBox.IsChecked = false;
                }


            }
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// The submit button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            findChecked();

            this.mw.setDifFilter(all_options);
            this.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// The reset button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in Checkbox_grid.Children.OfType<CheckBox>())
            {
                checkBox.IsChecked = false;
            }
            all_options.Clear();
        }
    }
}
