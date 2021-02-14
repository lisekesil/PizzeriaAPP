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
    /// Interaction logic for MakeOrderStep2.xaml
    /// </summary>
    public partial class MakeOrderStep2 : Page
    {
        PizzeriaAPPEntities context;
        private List<Pizza> pizzas;
        private string firstName;
        private string lastName;
        private string city;
        private string street;
        private string phoneNumber;
        private decimal amount;
        public MakeOrderStep2()
        {
            InitializeComponent();
            //Loaded += MakeOrderStep2_Loaded;
            pizzas = new List<Pizza>();
            context = new PizzeriaAPPEntities();
            ShowPizzas();

        }

        public MakeOrderStep2(string firstName, string lastName, string city, string street, string phoneNumber) : this()
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.city = city;
            this.street = street;
            this.phoneNumber = phoneNumber;

            this.Loaded += MakeOrderStep2_Loaded;
        }

        private void MakeOrderStep2_Loaded(object sender, RoutedEventArgs e)
        {
            lblFirstName.Content = this.firstName;
            lblLastName.Content = this.lastName;
            lblCity.Content = this.city;
            lblStreet.Content = this.street;
            lblPhoneNumber.Content = this.phoneNumber;
        }

        private void ShowPizzas()
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

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            if(dgvPizzas.SelectedValue != null)
            {
                 var pizzaId = int.Parse(dgvPizzas.SelectedValue.ToString());
                 var pizza = context.Pizzas.Where(p => p.PizzaId == pizzaId).FirstOrDefault();
                pizzas.Add(pizza);
                ShowOrderedPizzas();

            } else
            {
                MessageBox.Show("Wybierz pizzę którą chcesz dodać do koszyka.");
            }
        }

        private void dgvPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowOrderedPizzas()
        {
            lbCart.ItemsSource = null;
            lbCart.ItemsSource = pizzas;
            lbCart.DisplayMemberPath = "PizzaName";
            lbCart.SelectedValuePath = "PizzaId";

            amount = pizzas.Sum(p => p.PizzaPrice);
            lblAmount.Content = amount + " PLN";
        }

        private void DeleteFromCart(object sender, RoutedEventArgs e)
        {
            if(lbCart.SelectedValue != null)
            {
                var deleteId = int.Parse(lbCart.SelectedValue.ToString());
                var deletePizza = pizzas.Where(p => p.PizzaId == deleteId).FirstOrDefault();
                pizzas.Remove(deletePizza);

                ShowOrderedPizzas();

            } else
            {
                MessageBox.Show("Wybierz pizzę z koszyka którą chcesz usunąć.");
            }
        }

        private void MakeOrder(object sender, RoutedEventArgs e)
        {
            if(pizzas.Count() > 0)
            {

                var newOrder = new Order
                {
                    FirstName = this.firstName,
                    LastName = this.lastName,
                    City = this.city,
                    Street = this.street,
                    PhoneNumber = this.phoneNumber,
                    Amount = this.amount
                };

                context.Orders.Add(newOrder);
            
                foreach(var pizza in pizzas)
                {
                    context.OrderedPizzas.Add(new OrderedPizza { OrderId = newOrder.OrderId, PizzaId = pizza.PizzaId });
                }

                context.SaveChanges();
                MessageBox.Show("GRATULACJE! Udało ci się dodać zamówienie! Sprawdź zakładkę Zamówienia");

                var mainMenu = new MainMenu();
                NavigationService.Navigate(mainMenu);
            } else
            {
                MessageBox.Show("Proszę wybrać jakąś pizzę!");
            }
        }
    }
}
