using System;
using System.Collections.Generic;
using System.Data;
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

namespace PizzeriaAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            var context = new PizzeriaAPPEntities();

            var pizzas = context.Pizzas.ToList();
            var allIngredients = context.Ingredients.Select(i => i.IngredientName).ToList();

            if (pizzas != null && pizzas.Count() > 0)
            {
                dgvPizzas.ItemsSource = pizzas;
                dgvPizzas.SelectedValuePath = "PizzaId";
            } else
            {
                dgvPizzas.ItemsSource = null;
            }

            if (allIngredients != null && allIngredients.Count() > 0)
            {
                listAllIngredients.ItemsSource = allIngredients;
            }
            else
            {
                listAllIngredients.ItemsSource = null;
            }


        }

        private void ShowPizzaIngredients()
        {
            var context = new PizzeriaAPPEntities();
            var pizzaId = Int32.Parse(dgvPizzas.SelectedValue.ToString());

            var query = (from i in context.Ingredients
                          join ip in context.IngredientsPizzas
                          on i.IngredientId equals ip.IngredientId
                          where ip.PizzaId == pizzaId
                          select i.IngredientName).ToList();

            listPizzaIngredients.ItemsSource = query;

        }

        private void dgvPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
              ShowPizzaIngredients();
        }

    }
}
