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
    public partial class RecipeView : Window
    {
        public RecipeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.recipeGrid.Style = this.mainGrid.Resources["Blurred"] as Style;
            this.step.Visibility = Visibility.Visible;
            //this.step.rv = this;
        }
    }
}
