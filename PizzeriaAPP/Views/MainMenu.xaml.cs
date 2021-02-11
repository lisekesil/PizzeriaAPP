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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void GoManage(object sender, RoutedEventArgs e)
        {
            var manage = new Manage();
            NavigationService.Navigate(manage);
        }

        private void GoListOfPizza(object sender, RoutedEventArgs e)
        {
            var listOfPizzas = new ListOfPizzas();
            NavigationService.Navigate(listOfPizzas);
        }
    }
}
