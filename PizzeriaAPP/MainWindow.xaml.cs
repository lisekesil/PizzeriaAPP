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
        }

        private void ShowIngredients()
        {
            var context = new PizzeriaAPPEntities();

            lbtest.ItemsSource = context.Ingredients.Select(i => i.IngredientName).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = new PizzeriaAPPEntities();
            context.Ingredients.Add(new Ingredient { IngredientName = "Salami" });
            context.SaveChanges();

            ShowIngredients();
        }
    }
}
