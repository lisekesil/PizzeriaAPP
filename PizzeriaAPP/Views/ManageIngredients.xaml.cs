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
        PizzeriaAPPEntities context;
        public ManageIngredients()
        {
            InitializeComponent();
            context = new PizzeriaAPPEntities();
            ShowIngredients();

        }

        private void ShowIngredients()
        {
            var allIngredients = context.Ingredients.ToList();

            if (allIngredients != null && allIngredients.Count() > 0)
            {
                listAllIngredients.DisplayMemberPath = "IngredientName";
                listAllIngredients.SelectedValuePath = "IngredientId";
                listAllIngredients.ItemsSource = allIngredients;
            }
            else
            {
                listAllIngredients.ItemsSource = null;
            }
        }

        private void AddIngredient(object sender, RoutedEventArgs e)
        {
            if (txtAddIng.Text != "" || txtAddIng.Text.Length < 50)
            {
                context.Ingredients.Add(new Ingredient { IngredientName = txtAddIng.Text });
                context.SaveChanges();
                ShowIngredients();
                ClearTxt();
            }else
            {
                MessageBox.Show("Spróbuj jeszcze raz! Nazwa skladnika nie może być dłuższa niż 50");
            }
        }

        private void listAllIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listAllIngredients.SelectedValue != null)
            {
                var ingId = int.Parse(listAllIngredients.SelectedValue.ToString());
                var ing = context.Ingredients.Where(i => i.IngredientId == ingId).FirstOrDefault();
                txtEditing.Text = ing.IngredientName;

            }
        }

        private void DeleteIngredient(object sender, RoutedEventArgs e)
        {
            if (listAllIngredients.SelectedValue != null)
            {
                var deletedId = int.Parse(listAllIngredients.SelectedValue.ToString());
                var deletedIng = context.Ingredients.Where(i => i.IngredientId == deletedId).FirstOrDefault();
                context.Ingredients.Remove(deletedIng);
                context.SaveChanges();
                ShowIngredients();
                listAllIngredients.SelectedValue = 0;

            } else
            {
                MessageBox.Show("Wybierz składnik który chcesz usunąć");
            }
        }

        private void EditIngredient(object sender, RoutedEventArgs e)
        {
            if(listAllIngredients.SelectedValue != null)
            {

            var ingId = int.Parse(listAllIngredients.SelectedValue.ToString());
            var ing = context.Ingredients.Where(i => i.IngredientId == ingId).FirstOrDefault();
            ing.IngredientName = txtEditing.Text;
            context.SaveChanges();
            ShowIngredients();
            ClearTxt();
            } else
            {
                MessageBox.Show("Wybierz składnik który chcesz edytować");
            }
        }

        private void ClearTxt()
        {
            txtEditing.Text = "";
            txtAddIng.Text = "";
        }
    }
}
