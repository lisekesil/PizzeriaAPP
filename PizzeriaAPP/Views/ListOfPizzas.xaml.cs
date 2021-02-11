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
        public ListOfPizzas()
        {
            InitializeComponent();
            GetData();
        }

        private void GetData()
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
    }
}
