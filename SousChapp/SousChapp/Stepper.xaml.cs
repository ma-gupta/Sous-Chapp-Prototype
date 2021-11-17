using System;
using System.Collections;
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
    /// Interaction logic for UserControl4.xaml
    /// </summary>
    public partial class Stepper : UserControl

    {
        public DynamicRecipeView rv;

        private ArrayList steps;
        private Dictionary<int, List<String>> tools_to_step;
        private int current;

        public Stepper()
        {
            InitializeComponent();
        }

        public void initializeStepper(ArrayList steps, Dictionary<int, List<String>> tools_to_step) {
            this.steps = steps;
            this.tools_to_step = tools_to_step;
            reset();

            
            
        }

        public void reset() {
            this.current = 0;
            changeText();

        }

        private void changeText() {
            this.StepDetail.Text = "";
            Run title_steps = new Run();
            title_steps.Text = "Step #" + (current+1).ToString() + "\n";
            title_steps.TextDecorations = TextDecorations.Underline;
            this.StepDetail.Inlines.Add(title_steps);

            if (current == this.steps.Count-1)
            {
                this.FinishButton.Visibility = Visibility.Visible;
                this.NextButton.Visibility = Visibility.Hidden;
                this.ComingUpLabel.Visibility = Visibility.Hidden;

                this.StepDetail.Inlines.Add ((String)this.steps[current]);

                if (current != 0) {
                    this.prevButton.Visibility = Visibility.Visible;
                }

            }
            else if (current == 0) {

                this.FinishButton.Visibility = Visibility.Hidden;

                this.FinishButton.Visibility = Visibility.Hidden;
                this.prevButton.Visibility = Visibility.Hidden;
                this.NextButton.Visibility = Visibility.Visible;

                this.StepDetail.Inlines.Add((String)this.steps[current]);


                this.ComingUpLabel.Visibility = Visibility.Visible;
                this.ComingUpLabel.Content = "Coming up: " + (String)this.steps[current+1];
            }
            else
            {
                this.FinishButton.Visibility = Visibility.Hidden;

                this.NextButton.Visibility = Visibility.Visible;
                this.prevButton.Visibility = Visibility.Visible;

                this.StepDetail.Inlines.Add((String)this.steps[current]);

                this.ComingUpLabel.Visibility = Visibility.Visible;
                this.ComingUpLabel.Content = "Coming up: " + (String)this.steps[current+1];
            }

            //Add the tools for this step
            this.StepTools.Text = "";
            Run title_ts = new Run();
            title_ts.Text = "Tools and Ingredients \n";
            title_ts.TextDecorations = TextDecorations.Underline;
            this.StepTools.Inlines.Add(title_ts);
            if (tools_to_step.ContainsKey(current)) {
                List<String> tools = this.tools_to_step[current];
                foreach (String tool in tools)
                {
                    this.StepTools.Inlines.Add(tool + "\n");
                }
            }
            
        }

        private void nextButton_Click(object sender, RoutedEventArgs e) {
            current++;
            changeText();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e) {

            current--;
            changeText();
        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            rv.recipeGrid.Style = rv.mainGrid.Resources["Default"] as Style;

            rv.contButton.Visibility = Visibility.Hidden;
            rv.cancelButton.Visibility = Visibility.Hidden;
            rv.startButton.Visibility = Visibility.Visible;
            rv.startButton.IsEnabled = true;
        }



        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Visibility = Visibility.Hidden;
            rv.recipeGrid.Style = rv.mainGrid.Resources["Default"] as Style;
            rv.contButton.Visibility = Visibility.Visible;
            rv.cancelButton.Visibility = Visibility.Visible;
            rv.startButton.Visibility = Visibility.Hidden;
            rv.startButton.IsEnabled = true;
            this.rv.contButton.IsEnabled = true;
            this.rv.cancelButton.IsEnabled = true;

        }
    }
}
