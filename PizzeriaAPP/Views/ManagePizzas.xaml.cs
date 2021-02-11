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

namespace PizzeriaAPP.Views
{
    /// <summary>
    /// Interaction logic for ManagePizzas.xaml
    /// </summary>
    public partial class ManagePizzas : Page
    {
        public ManagePizzas()
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
            }
            else
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

        private void ShowPizzas()
        {
            var context = new PizzeriaAPPEntities();
            var pizzas = context.Pizzas.ToList();
            if (pizzas != null && pizzas.Count() > 0)
            {
                dgvPizzas.ItemsSource = pizzas;
                dgvPizzas.SelectedValuePath = "PizzaId";
            }
            else
            {
                dgvPizzas.ItemsSource = null;
            }
        }

        private void ShowPizzaIngredients()
        {
            var context = new PizzeriaAPPEntities();


            if (dgvPizzas.SelectedValue != null)
            {
                var pizzaId = Int32.Parse(dgvPizzas.SelectedValue.ToString());

                var pizzaIngredients = (from i in context.Ingredients
                                        join ip in context.IngredientsPizzas
                                        on i.IngredientId equals ip.IngredientId
                                        where ip.PizzaId == pizzaId
                                        select i.IngredientName).ToList();

                listPizzaIngredients.ItemsSource = pizzaIngredients;
            }

        }

        private void dgvPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Create object for DataGrid
            DataGrid grd = (DataGrid)sender;
            //Create object for DataRowView
            DataRowView row_selected = grd.SelectedItem as DataRowView;

            if (dgvPizzas.SelectedCells[0].Column.DisplayIndex == 0)
            {
                //MessageBox.Show("ss");
               tbPizzaName.Text = row_selected["PizzaName"].ToString();
            }
            ShowPizzaIngredients();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = new PizzeriaAPPEntities();

            if (tbPizzaName.Text == "" || tbPizzaName.Text == "Nazwa Pizzy")
            {
                MessageBox.Show("Proszę wpisać nazwę pizzy");
            }
            else if (Convert.ToDecimal(tbPizzaPrice.Text) <= 0 || tbPizzaPrice.Text == "Cena Pizzy")
            {
                MessageBox.Show("Proszę wpisać poprawną kwotę");
            }
            else
            {
                var pizzaName = tbPizzaName.Text;
                var pizzaPrice = Convert.ToDecimal(tbPizzaPrice.Text);
                var newPizza = new Pizza { PizzaName = pizzaName, PizzaPrice = pizzaPrice };

                context.Pizzas.Add(newPizza);
                context.SaveChanges();
                ShowPizzas();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
