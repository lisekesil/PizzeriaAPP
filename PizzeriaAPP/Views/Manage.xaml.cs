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
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Page
    {
        public Manage()
        {
            InitializeComponent();
        }



        private void GoManagePizzas(object sender, RoutedEventArgs e)
        {
            var managePizzas = new ManagePizzas();
            NavigationService.Navigate(managePizzas);
        }

        private void GoManageIngredients(object sender, RoutedEventArgs e)
        {
            var manageIngredients = new ManageIngredients();
            NavigationService.Navigate(manageIngredients);
        }
    }
}
