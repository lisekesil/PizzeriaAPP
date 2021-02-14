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

namespace PizzeriaAPP.Views
{
    /// <summary>
    /// Interaction logic for ListOfPizzas.xaml
    /// </summary>
    public partial class ListOfPizzas : Page
    {
        PizzeriaAPPEntities context;
        public ListOfPizzas()
        {
            InitializeComponent();
            context = new PizzeriaAPPEntities();
            GetPizzas();
        }

        private void GetPizzas()
        {

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
            if (dgvPizzas.SelectedValue != null)
            {
                var pizzaId = Int32.Parse(dgvPizzas.SelectedValue.ToString());

                var pizzaIngredients = (from i in context.Ingredients
                                        join ip in context.IngredientsPizzas
                                        on i.IngredientId equals ip.IngredientId
                                        where ip.PizzaId == pizzaId
                                        select new
                                        {
                                            i.IngredientName,
                                            ip.Id
                                        }).ToList();

                listIngredients.DisplayMemberPath = "IngredientName";
                listIngredients.SelectedValuePath = "Id";
                listIngredients.ItemsSource = pizzaIngredients;

            }

        }

        private void dgvPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPizzaIngredients();
        }
    }
}
