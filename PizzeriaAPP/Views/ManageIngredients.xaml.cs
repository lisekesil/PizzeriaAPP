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
    /// Interaction logic for ManageIngredients.xaml
    /// </summary>
    public partial class ManageIngredients : Page
    {
        public ManageIngredients()
        {
            InitializeComponent();
            GetData();

        }

        private void GetData()
        {

            var context = new PizzeriaAPPEntities();

            var allIngredients = context.Ingredients.Select(i => i.IngredientName).ToList();

            if (allIngredients != null && allIngredients.Count() > 0)
            {
                listAllIngredients.ItemsSource = allIngredients;
            }
            else
            {
                listAllIngredients.ItemsSource = null;
            }
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            var context = new PizzeriaAPPEntities();

            if (txtAddIng.Text != "" || txtAddIng.Text.Length < 50)
            {
                context.Ingredients.Add(new Ingredient { IngredientName = txtAddIng.Text });
                context.SaveChanges();
                GetData();
            }else
            {
                MessageBox.Show("Spróbuj jeszcze raz! Nazwa skladnika nie może być dłuższa niż 50");
            }
        }
    }
}
