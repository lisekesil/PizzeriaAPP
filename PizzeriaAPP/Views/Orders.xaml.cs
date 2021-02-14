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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        PizzeriaAPPEntities context;
        public Orders()
        {
            InitializeComponent();
            context = new PizzeriaAPPEntities();
            ShowOrders();
        }

        private void ShowOrders()
        {
            var orders = context.Orders.ToList();

            if(orders != null && orders.Count() > 0)
            {
                dgvOrders.ItemsSource = orders;
                dgvOrders.SelectedValuePath = "OrderId";
            }
            else
            {
                dgvOrders.ItemsSource = null;
            }
        }

        private void ShowOrderedPizzas()
        {
            if(dgvOrders.SelectedValue != null)
            {
                var orderId = int.Parse(dgvOrders.SelectedValue.ToString());
                var pizzaOrders = (from p in context.Pizzas
                                   join op in context.OrderedPizzas
                                   on p.PizzaId equals op.PizzaId
                                   where op.OrderId == orderId
                                   select new
                                   {
                                       p.PizzaName,
                                       op.Id
                                   }).ToList();

                listOrderedPizzas.DisplayMemberPath = "PizzaName";
                listOrderedPizzas.SelectedValuePath = "Id";
                listOrderedPizzas.ItemsSource = pizzaOrders;
            }
        }



        private void dgvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowOrderedPizzas();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            if(dgvOrders.SelectedValue != null)
            {
                var orderId = int.Parse(dgvOrders.SelectedValue.ToString());
                var deletedOrder = context.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();

                context.Orders.Remove(deletedOrder);
                context.SaveChanges();
                ShowOrders();
            }else
            {
                MessageBox.Show("Wybierz zamówienie które chcesz usunąć");
            }
        }
    }
}
