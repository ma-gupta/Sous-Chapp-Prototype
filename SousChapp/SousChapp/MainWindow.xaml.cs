using System;
using System.IO;
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
using System.Diagnostics;

namespace SousChapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean menuVisible = false;

        //Currently highlighted object
        object highlighted;
        //Filter options
        private HashSet<String> cui_opt;
        private HashSet<String> dif_opt;
        private HashSet<String> ing_opt;
        private Boolean splitMode = false;
        private MainWindow otherWindow;

        public Boolean viewingRecipe;
        private DynamicRecipeView activeRecipe;

        private String search_word;

        private List<Grid> all_recipes;

        private RecipeDetails current_recipe;
        public MainWindow()
        {
            InitializeComponent();
            mainMenu.Visibility = Visibility.Hidden;

            cui_opt = new HashSet<string>();
            dif_opt = new HashSet<string>();
            ing_opt = new HashSet<string>();

            this.all_recipes = new List<Grid>();

            this.CuiFilterPopup.setMainWindow(this);
            this.DiffFilterPopup.setMainWindow(this);
            this.IngFilterPopup.setMainWindow(this);

            this.search_word = "";


            initializeRecipes(); //Set the all recipes arraylist
            drawRecipes();

        }

        public MainWindow(double top, double left) : this()
        {
            this.Top = top;
            this.Left = left;
            
        }

        //Add all the recipes to the array list
        private void initializeRecipes() {
            this.all_recipes.Add(this.chocolatechipcookies);
            this.all_recipes.Add(this.frenchomelet);
            this.all_recipes.Add(this.padthai);
            this.all_recipes.Add(this.aglioeolio);
            this.all_recipes.Add(this.beansalad);
            this.all_recipes.Add(this.overnightoats);
            this.all_recipes.Add(this.honeysalmon);
            this.all_recipes.Add(this.porkchops);
            this.all_recipes.Add(this.lambburger);
            this.all_recipes.Add(this.quesadilla);
        }

        //Draws the recipes on the screen
        private void drawRecipes() {

            if (highlighted != null) {
                Border_dehighlight(highlighted);
            }

            this.recipeCanvas.Height = 392;

            //Reset the recipes prior to displaying them
            for (int i = 0; i < this.all_recipes.Count; i++) {
                this.all_recipes[i].Visibility = Visibility.Hidden;
            }

            //Check if we have't selected any options
            if (this.cui_opt.Count == 0 && this.dif_opt.Count == 0 && this.ing_opt.Count == 0 && this.search_word == "")
            {
                drawAll();
            }
            else {
                drawFiltered();
            }
        }

        private void drawAll() {
            Thickness current_margin = new Thickness(96, 84, 0, 0);
            for (int i = 0; i < this.all_recipes.Count; i++) {

                this.all_recipes[i].Margin = current_margin;
                this.all_recipes[i].Visibility = Visibility.Visible;

                if (current_margin.Left == 96)
                {
                    current_margin = new Thickness(464, current_margin.Top, 0, 0);
                }
                else if (current_margin.Left == 464)
                {
                    current_margin = new Thickness(96, current_margin.Top + 181, 0, 0);
                }

                if (this.recipeCanvas.Height - current_margin.Top <= 0)
                {
                    this.recipeCanvas.Height += 100;
                }
            }
        }

        private void drawFiltered() {
            this.btnReset.Visibility = Visibility.Visible;
            Thickness current_margin = new Thickness(96, 84, 0, 0);
            if (this.search_word.ToLower() == "bean salad" && this.cui_opt.Count == 0 && this.dif_opt.Count == 0 && this.ing_opt.Count == 0)
            {
                for (int i = 0; i < this.all_recipes.Count; i++)
                {
                    if (this.all_recipes[i].Name == "beansalad")
                    {
                        this.all_recipes[i].Margin = current_margin;
                        this.all_recipes[i].Visibility = Visibility.Visible;
                    }
                }
            }
            else if(this.search_word==""){
                for (int i = 0; i < this.all_recipes.Count; i++)
                {

                    //Check for Mexican
                    if (this.cui_opt.Contains("French") && (this.all_recipes[i].Name == "frenchomelet"))
                    {
                        this.all_recipes[i].Margin = current_margin;
                        this.all_recipes[i].Visibility = Visibility.Visible;
                        current_margin = drawNext(current_margin);
                        continue;
                    }

                    //Check for easy
                    if (this.dif_opt.Contains("Beginner") && (this.all_recipes[i].Name == "frenchomelet" || this.all_recipes[i].Name == "overnightoats" || this.all_recipes[i].Name == "quesadilla")) {
                        this.all_recipes[i].Margin = current_margin;
                        this.all_recipes[i].Visibility = Visibility.Visible;
                        current_margin = drawNext(current_margin);
                        continue;
                    }

                    //Check for 
                    if (this.ing_opt.Contains("Leafy Greens") && (this.all_recipes[i].Name == "beansalad")){
                        this.all_recipes[i].Margin = current_margin;
                        this.all_recipes[i].Visibility = Visibility.Visible;
                        current_margin = drawNext(current_margin);
                        continue;
                    }



                }
            }

            
        }

        private Thickness drawNext(Thickness current_margin) {
            if (current_margin.Left == 96)
            {
                current_margin = new Thickness(464, current_margin.Top, 0, 0);
            }
            else if (current_margin.Left == 464)
            {
                current_margin = new Thickness(96, current_margin.Top + 181, 0, 0);
            }

            if (this.recipeCanvas.Height - current_margin.Top <= 0)
            {
                this.recipeCanvas.Height += 100;
            }

            return current_margin;
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

        private void ShowMenu_Click(object sender, RoutedEventArgs e)
        {
            Button me = (Button)sender;
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

        private void Split_Screen(object sender, RoutedEventArgs e)
        {
            var workArea = SystemParameters.WorkArea;

            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Left = workArea.Left;
            this.Top = workArea.Top;
            this.WindowState = WindowState.Normal;
            this.mainGrid.Height = this.Height - 210;
            this.recipeGrid.Height = this.mainGrid.Height-50;
            
           // this.recipeGrid.Width = this.Width;
            
            MainWindow window2 = new MainWindow();
            window2.Width = SystemParameters.PrimaryScreenWidth / 2;
            window2.Height = SystemParameters.PrimaryScreenHeight;
            window2.WindowState = WindowState.Normal;
            window2.mainGrid.Height = window2.Height - 210;
            window2.recipeGrid.Height = window2.mainGrid.Height-50;
            //window2.recipeGrid.Width = window2.Width;
            setSplit(true, window2);
            window2.setSplit(true, this);

            window2.Top = this.Top;
            window2.Left = this.Left + this.Width-10;
            window2.Show();


            
            //window2.MaxWidth = SystemParameters.PrimaryScreenWidth / 2;
            //window2.Show();
            //SplitWindow split = new SplitWindow();
            //split.Show();
            //this.Close();
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid current = (Grid)sender;
            this.ChosenRecipe.Margin = current.Margin;
            this.ViewRecipeButton.Margin = new Thickness(current.Margin.Left+130, current.Margin.Top+70,0,0);
            this.ViewRecipeButton.Visibility = Visibility.Visible;
            Border_highlight(this.ChosenRecipe);

            //Do the preps depending on which one we chose
            if (current.Name == "frenchomelet") {
                this.current_recipe = prepOmelet();
            }
            else if (current.Name == "beansalad") {
                this.current_recipe = prepBeanSalad();
            }
            else if (current.Name == "quesadilla") {
                this.current_recipe = prepQuesadilla();
            }
        }

        private RecipeDetails prepOmelet() {
            RecipeDetails rd = new RecipeDetails();

            rd.setImage("frenchomelet.png");
            rd.setRecipeName("French Omelette ");
            rd.setCookingTime(15);
            rd.setServing(1);

            rd.addCategory("Beginner");
            rd.addCategory("Eggs");
            rd.addCategory("Vegeterian");
            rd.addCategory("French");

            //Steps
            rd.addIngridient("4 large eggs");
            rd.addIngridient("1 tbsp olive oil");
            rd.addIngridient("100 grams cheddar cheese");
            rd.addIngridient("pinch salt");
            rd.addIngridient("pinch pepper");

            //Tools
            rd.addTool("1 cup");
            rd.addTool("1 fork");
            rd.addTool("1 medium pan");
            rd.addTool("1 spatula");

            //Steps
            rd.addStep("Heat 1 table spoon of olive oil in a pan over medium heat");
            rd.addStep("In a cup, beat 4 large eggs");
            rd.addStep("Add salt and pepper to taste");
            rd.addStep("Pour eggs into pan");
            rd.addStep("Cook until eggs set");
            rd.addStep("Using the spatula, flip the omelette");
            rd.addStep("Spread cheese over the omelette");
            rd.addStep("Cook for 3 minutes and fold");
            rd.addStep("Serve immediately");

            //Tools to step
            rd.addToolsToStep(0, new List<string> {"1 medium pan", "1 tbsp olive oil"});
            rd.addToolsToStep(1, new List<string> {"1 cup", "4 large eggs"});
            rd.addToolsToStep(2, new List<string> {"pinch salt", "pinch pepper"});
            rd.addToolsToStep(3, new List<string> {""});
            rd.addToolsToStep(4, new List<string> {""});
            rd.addToolsToStep(5, new List<string> {"1 spatula"});
            rd.addToolsToStep(6, new List<string> {"100 grams cheddar cheese"});
            rd.addToolsToStep(7, new List<string> {""});
            rd.addToolsToStep(8, new List<string> {""});
            

            return rd;
        }

        private RecipeDetails prepBeanSalad() {
            RecipeDetails rd = new RecipeDetails();

            rd.setImage("beansalad.png");
            rd.setRecipeName("Broccoli Rabe and White Bean Salad");
            rd.setCookingTime(25);
            rd.setServing(4);

            rd.addCategory("Easy");
            rd.addCategory("Side");
            rd.addCategory("Vegeterian");
            rd.addCategory("Leafy Greens");

            //Steps
            rd.addIngridient("30 oz  of canned white kidney beans");
            rd.addIngridient("3 tbsp olive oil");
            rd.addIngridient("half bunch broccoli rabe");
            rd.addIngridient("4 garlic cloves");
            rd.addIngridient("1 small lemon");
            rd.addIngridient("pinch salt");
            rd.addIngridient("pinch pepper");

            //Tools
            rd.addTool("1 knife");
            rd.addTool("1 pot");
            rd.addTool("1 medium pan");

            //Steps
            rd.addStep("Slice garlic cloves and lemon thinly");
            rd.addStep("Chop broccoli rabe into large chunks");
            rd.addStep("Heat oil in a pan and add garlic and lemons, cook until golden brown");
            rd.addStep("Add broccoli into the pan and cook until wilted");
            rd.addStep("Drain beans and boil in pot with 1/2 cup water for 3 mins");
            rd.addStep("Drain beans and add to pan");
            rd.addStep("Salt and pepper to taste");
            rd.addStep("Serve immediately");

            //Tools to step
            rd.addToolsToStep(0, new List<string> {"1 knife", "4 garlic cloves", "1 small lemon"});
            rd.addToolsToStep(1, new List<string> {"half bunch broccoli rabe"});
            rd.addToolsToStep(2, new List<string> {"1 medium pan"});
            rd.addToolsToStep(3, new List<string> {""});
            rd.addToolsToStep(4, new List<string> {"30 oz canned white kidney beans", "1 pot"});
            rd.addToolsToStep(5, new List<string> {""});
            rd.addToolsToStep(6, new List<string> {"pinch of salt", "pinch of pepper"});
            rd.addToolsToStep(7, new List<string> {""});

            return rd;
        }


        private RecipeDetails prepQuesadilla() {
            RecipeDetails rd = new RecipeDetails();

            rd.setImage("quesadilla.png");
            rd.setRecipeName("Quick Cheesy Quesadillas");
            rd.setCookingTime(25);
            rd.setServing(6);

            rd.addCategory("Beginner");
            rd.addCategory("Lunch");
            rd.addCategory("Vegeterian");

            //Steps
            rd.addIngridient("6 flour torillas");
            rd.addIngridient("1 1/2 cups shredded cheddar cheese");
            rd.addIngridient("2 green onions");
            rd.addIngridient("jar of salsa");
            rd.addIngridient("canola oil");

            //Tools
            rd.addTool("1 knife");
            rd.addTool("1 large pan");

            //Steps
            rd.addStep("Slice green onions thinly");
            rd.addStep("Lay out tortilla and add 1/4 cheese, 1 tbsp salsa, and a sprinkle of green onions to half of the tortilla");
            rd.addStep("Brush edge of tortilla with water and fold it over, press to seal");
            rd.addStep("Heat oil in pan over medium heat, add in quesadilla and cook until golden on both sides");
            rd.addStep("Cut into triangles");
            rd.addStep("Serve immediately with salsa");

            //Tools to step
            rd.addToolsToStep(0, new List<string> {"1 knife", "2 green onions"});
            rd.addToolsToStep(1, new List<string> {"6 flour tortillas", "1 1/2 cups shredded cheddar cheese", "jar of salsa"});
            rd.addToolsToStep(2, new List<string> {""});
            rd.addToolsToStep(3, new List<string> {"1 large pan"});
            rd.addToolsToStep(4, new List<string> {""});
            rd.addToolsToStep(5, new List<string> {""});

            return rd;

        }

        private void Border_highlight(object sender)
        {
            highlighted = sender;
            Rectangle r = (Rectangle)sender;
            r.StrokeThickness = 6;
            r.Stroke = Brushes.LimeGreen;
        }

        private void Border_dehighlight(object sender)
        {
            Rectangle r = (Rectangle)sender;
            r.StrokeThickness = 0;
            this.ViewRecipeButton.Visibility = Visibility.Hidden;
        }

        private void ViewRecipeButton_Click(object sender, RoutedEventArgs e){
            
            

            DynamicRecipeView drv = new DynamicRecipeView(current_recipe, this);
            viewingRecipe = true;
            activeRecipe = drv;

            if (this.isSplit()) {
                drv.backupScale();
                drv.mainGrid.Height = this.Height-20;
                drv.mainGrid.Width = this.Width;
                drv.recipeGrid.Width = drv.mainGrid.Width;
                drv.recipeGrid.Height = drv.mainGrid.Height-20;
                drv.recipeGrid.Margin = new Thickness(0,-225,0,0);
                drv.header.Width = this.Width;
               // drv.HorizontalAlignment = HorizontalAlignment.Left;
                //drv.header.Margin = new Thickness(0, -15, 0, 0);
            }
            

            drv.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            //drv.Show();
            //this.Close();
            Border_dehighlight(highlighted);


        }

        

        private void OptIng_Click(object sender, RoutedEventArgs e)
        {
            this.IngFilterPopup.Visibility = Visibility.Visible;
            this.mainMenu.Visibility = Visibility.Hidden;
        }

        private void OptCui_Click(object sender, RoutedEventArgs e)
        {
            this.CuiFilterPopup.Visibility = Visibility.Visible;
            this.mainMenu.Visibility = Visibility.Hidden;

        }

        private void OptDiff_Click(object sender, RoutedEventArgs e)
        {
            this.DiffFilterPopup.Visibility = Visibility.Visible;
            this.mainMenu.Visibility = Visibility.Hidden;

        }

        

        public void setCuiFilter(HashSet<String> cui_opt) {

            this.cui_opt = cui_opt;

            if (cui_opt.Count > 0){
                filter_cui.Text = "Matching results for cusine(s): ";
                for (int i = 0; i < cui_opt.Count; i++)
                {
                    if (i != cui_opt.Count - 1)
                    {
                       filter_cui.Inlines.Add("\"" + cui_opt.ElementAt(i) + "\", ");
                    }
                    else
                    {
                        filter_cui.Inlines.Add("\"" + cui_opt.ElementAt(i) + "\"");
                    }


                }
            }
            else{
                filter_cui.Text = "";
            }
            drawRecipes();
        }

        public void setDifFilter(HashSet<String> dif_opt)
        {

            this.dif_opt = dif_opt;

            if (dif_opt.Count > 0)
            {
                filter_dif.Text = "Matching results for difficulty: ";
                for (int i = 0; i < dif_opt.Count; i++)
                {
                    if (i != dif_opt.Count - 1)
                    {
                        filter_dif.Inlines.Add("\"" + dif_opt.ElementAt(i) + "\", ");
                    }
                    else
                    {
                        filter_dif.Inlines.Add("\"" + dif_opt.ElementAt(i) + "\"");
                    }


                }
            }
            else
            {
                filter_dif.Text = "";
            }

            drawRecipes();

        }

        public void setIngFilter(HashSet<String> ing_opt)
        {

            this.ing_opt = ing_opt;

            if (ing_opt.Count > 0)
            {
                filter_ing.Text = "Matching results for ingredient(s): ";
                for (int i = 0; i < ing_opt.Count; i++)
                {
                    if (i != ing_opt.Count - 1)
                    {
                        filter_ing.Inlines.Add("\"" + ing_opt.ElementAt(i) + "\", ");
                    }
                    else
                    {
                        filter_ing.Inlines.Add("\"" + ing_opt.ElementAt(i) + "\"");
                    }


                }
            }
            else
            {
                filter_ing.Text = "";
            }
            drawRecipes();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.mainMenu.Visibility = Visibility.Hidden;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SearchBox.Text = "";
        }

        private void SearchSmall_Click(object sender, RoutedEventArgs e)
        {
            this.viewSearch.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Visible;
            this.SearchLarge.Visibility = Visibility.Visible;
            this.OSKeyboard.Visibility = Visibility.Visible;
            this.SearchSmall.Visibility = Visibility.Hidden;
        }

        private void SearchLarge_Click(object sender, RoutedEventArgs e)
        {
            
            this.search_word = this.SearchBox.Text;
            if (this.search_word.Length > 0) {
                this.viewSearch.Text = "Matching results for: \"" + this.SearchBox.Text + "\"";
                this.viewSearch.Visibility = Visibility.Visible;
            }
            
            this.SearchBox.Visibility = Visibility.Hidden;
            this.SearchLarge.Visibility = Visibility.Hidden;
            this.OSKeyboard.Visibility = Visibility.Hidden;
            this.SearchSmall.Visibility = Visibility.Visible;
            drawRecipes();
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
           //this.SearchBox.Text = "";
            
            
        }

        private void OptExit_Click(object sender, RoutedEventArgs e)
        {
            if (splitMode)
            {
                setSplit(false, otherWindow);

            }
            else
            {
                this.Close();
            }
            
        }

        public Boolean isSplit()
        {
            return splitMode;
        }

        public void setSplit(Boolean val, MainWindow other)
        {
            splitMode = val;
            if (val)
            {
                this.optExit.Header = "Exit split screen";
                this.optSplit.Visibility = Visibility.Hidden;
                otherWindow = other;
            }
            if (!val)
            {
                otherWindow.optExit.Header = "Exit";
                otherWindow.optSplit.Visibility = Visibility.Visible;
                otherWindow.WindowState = WindowState.Maximized;
                otherWindow.Height = 450;
                otherWindow.Width = 800;
                otherWindow.mainGrid.Height = 450;
                otherWindow.mainGrid.Width = 800;
                otherWindow.recipeGrid.Height = 392;
               // otherWindow.recipeGrid.Margin = new Thickness(0, 0, 0, -10);
                otherWindow.splitMode = false;
                if (otherWindow.viewingRecipe)
                {
                    otherWindow.activeRecipe.resetScale();
                }
                if (otherWindow.viewingRecipe)
                {
                    otherWindow.activeRecipe.Show();
                }
                else
                {
                    otherWindow.Show();
                }
                
                this.Close();
            }
        }


        private void OSKeyboard_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "osk.exe");
            ProcessStartInfo procStart = new ProcessStartInfo();
            procStart.FileName = @"c:\Windows\Sysnative\cmd.exe";
            procStart.Arguments = "/c osk.exe";
            procStart.CreateNoWindow = true;
            procStart.UseShellExecute = false;
            Process proc = new Process();
            proc.StartInfo = procStart;
            proc.Start();

        }

        /// <summary>
        /// Resets all the options selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.cui_opt.Clear();
            this.ing_opt.Clear();
            this.dif_opt.Clear();
            this.search_word = "";

            this.setCuiFilter(this.cui_opt);
            this.setDifFilter(this.dif_opt);
            this.setIngFilter(this.ing_opt);

            this.viewSearch.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            this.SearchLarge.Visibility = Visibility.Hidden;
            this.OSKeyboard.Visibility = Visibility.Hidden;
            this.SearchSmall.Visibility = Visibility.Visible;

            drawRecipes();
            this.btnReset.Visibility = Visibility.Hidden;
        }

        
    }
}
