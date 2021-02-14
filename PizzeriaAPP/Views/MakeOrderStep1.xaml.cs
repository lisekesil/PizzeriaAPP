using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MakeOrderStep1.xaml
    /// </summary>
    public partial class MakeOrderStep1 : Page
    {
        PizzeriaAPPEntities context;
        
        public MakeOrderStep1()
        {
            InitializeComponent();
            context = new PizzeriaAPPEntities();
        }

        private void GoMakeOrderStep2(object sender, RoutedEventArgs e)
        {
            if( txtFirstName.Text == "" || txtFirstName.Text.Length >= 50)
            {
                MessageBox.Show("Wpisz imię(nie dłuższe niż 50 znaków)");
            } else if (txtLastName.Text == "" || txtLastName.Text.Length >= 50)
            {
                MessageBox.Show("Wpisz nazwisko(nie dłuższe niż 50 znaków)");
            } else if (txtCity.Text == "" || txtCity.Text.Length >= 50)
            {
                MessageBox.Show("Wpisz miasto(nie dłuższe niż 50 znaków)");
            } else if (txtStreet.Text == "" || txtStreet.Text.Length >= 50)
            {
                MessageBox.Show("Wpisz adres(nie dłuższy niż 50 znaków)");
            } else if (txtPhoneNumber.Text == "" || txtPhoneNumber.Text.Length != 9)
            {
                MessageBox.Show("Wpisz dziewięciocyfrowy numer telefonu)");
            } else
            {

                var makeOrderStep2 = new MakeOrderStep2(
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtCity.Text,
                    txtStreet.Text,
                    txtPhoneNumber.Text
                    );
                NavigationService.Navigate(makeOrderStep2);
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
