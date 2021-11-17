using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SousChapp
{
    public class RecipeDetails
    {
        private ArrayList _steps;
        private ArrayList _ingridients;
        private ArrayList _tools;
        private ArrayList _categories;
        private Dictionary<int, List<String>> _tools_to_step;

        private String image;
        private String recipe_name;
        private int serving;
        private int cooking_time;
        

        public RecipeDetails() {
            _steps = new ArrayList();
            _ingridients = new ArrayList();
            _tools = new ArrayList();
            _categories = new ArrayList();
            _tools_to_step = new Dictionary<int, List<string>>();
        }


        public void addStep(String step)
        {
            _steps.Add(step);
        }

        public void addIngridient(String ing) {
            _ingridients.Add(ing);
        }

        public void addTool(String tool)
        {
            _tools.Add(tool);
        }

        public void setImage(String image) {
            this.image = image;
        }

        public void setRecipeName(String name) {
            this.recipe_name = name;
        }

        public String getRecipeName() {
            return this.recipe_name;
        }

        public void setServing(int serv) {
            this.serving = serv;
        }

        public int getServing() {
            return this.serving;
        }

        public void setCookingTime(int time) {
            this.cooking_time = time;
        }

        public int getCookingTime() {
            return this.cooking_time;
        }

        public void addCategory(String category) {
            _categories.Add(category);
        }

        public ArrayList getCategories() {
            return _categories;
        }

        public String getImage() {
            return "Resources/" + this.image;
        }

        public ArrayList getSteps() {
            return _steps;
        }

        public ArrayList getIngridients() {
            return _ingridients;
        }

        public ArrayList getTools() {
            return _tools;
        }

        public void addToolsToStep(int step, List<String> tools) {
            _tools_to_step.Add(step, tools);
        }

        public Dictionary<int, List<String>> getToolsToStep() {
            return _tools_to_step;
        }



    }


     
}
