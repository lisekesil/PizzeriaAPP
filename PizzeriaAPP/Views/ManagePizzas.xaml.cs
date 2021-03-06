﻿using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ManagePizzas.xaml
    /// </summary>
    public partial class ManagePizzas : Page
    {
        PizzeriaAPPEntities context;
        public ManagePizzas()
        {
            InitializeComponent();
            context = new PizzeriaAPPEntities();
            ShowPizzas();
            ShowIngredients();
            //GetData();
        
        }

        private void ShowIngredients()
        {

            var allIngredients = context.Ingredients.ToList();
            if (allIngredients != null && allIngredients.Count() > 0)
            {
                listAllIngredients.SelectedValuePath = "IngredientId";
                listAllIngredients.DisplayMemberPath = "IngredientName";
                listAllIngredients.ItemsSource = allIngredients;
            }
            else
            {
                listAllIngredients.ItemsSource = null;
            }


        }

        private void ShowPizzas()
        {
            var pizzas = context.Pizzas.ToList();
            if (pizzas != null && pizzas.Count() > 0)
            {
                dgvPizzas.ItemsSource = pizzas;
                dgvPizzas.SelectedValuePath = "PizzaId";

                cmbPizzas.ItemsSource = pizzas;
                cmbPizzas.DisplayMemberPath = "PizzaName";
                cmbPizzas.SelectedValuePath = "PizzaId";
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
                                        select new { 
                                            i.IngredientName, 
                                            ip.Id
                                        }).ToList();

                listPizzaIngredients.DisplayMemberPath = "IngredientName";
                listPizzaIngredients.SelectedValuePath = "Id";
                listPizzaIngredients.ItemsSource = pizzaIngredients;
                
            }

        }

        private void dgvPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            ShowPizzaIngredients();
        }

        private void AddPizza(object sender, RoutedEventArgs e)
        {

            if (tbPizzaName.Text == "")
            {
                MessageBox.Show("Proszę wpisać nazwę pizzy");
            }
            else if (Convert.ToDecimal(tbPizzaPrice.Text) <= 0)
            {
                MessageBox.Show("Proszę wpisać poprawną kwotę");
            }
            else
            {
                var pizzaName = tbPizzaName.Text;
                var pizzaPrice = Convert.ToDecimal(tbPizzaPrice.Text);
                var newPizza = new Pizza { PizzaName = pizzaName, PizzaPrice = pizzaPrice };

                context.Pizzas.Add(newPizza);
                context.SaveChanges();
                ShowPizzas();
                ClearForms();
            }
        }


        private void AddIngredientToPizza(object sender, RoutedEventArgs e)
        {
            if(listAllIngredients.SelectedValue != null && dgvPizzas.SelectedValue != null && int.Parse(listAllIngredients.SelectedValue.ToString()) > 0 && int.Parse(dgvPizzas.SelectedValue.ToString()) > 0)
            {
                context.IngredientsPizzas.Add(new IngredientsPizza
                {
                    IngredientId = int.Parse(listAllIngredients.SelectedValue.ToString()),
                    PizzaId = int.Parse(dgvPizzas.SelectedValue.ToString())
                });
                context.SaveChanges();
                ShowPizzaIngredients();
            }else
            {
                MessageBox.Show("Wybierz Pizzę oraz składnik który chcesz dodać");
            }
        }

        private void DeleteIngredientFromPizza(object sender, RoutedEventArgs e)
        {
            if(listPizzaIngredients.SelectedValue != null && int.Parse(listPizzaIngredients.SelectedValue.ToString()) > 0)
            {
                var deleteId = int.Parse(listPizzaIngredients.SelectedValue.ToString());
                var deletedIng = context.IngredientsPizzas.Where(ip => ip.Id == deleteId).FirstOrDefault();
                context.IngredientsPizzas.Remove(deletedIng);
                context.SaveChanges();
                ShowPizzaIngredients();
            }else
            {
                MessageBox.Show("Wybierz składnik który chcesz usunąć z pizzy");
            }
        }

        private void DeletePizza(object sender, RoutedEventArgs e)
        {
            if(cmbPizzas.SelectedValue != null)
            {
                var delPizzaId = int.Parse(cmbPizzas.SelectedValue.ToString());
                var delPizza = context.Pizzas.Where(p => p.PizzaId == delPizzaId).FirstOrDefault();
                context.Pizzas.Remove(delPizza);
                context.SaveChanges();
                // GetData();
                ShowPizzas();
            } else
            {
                MessageBox.Show("Wybierz pizzę którą chcesz usunąć");
            }
        }

        private void cmbPizzas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbPizzas.SelectedValue != null)
            {
                var delPizzaId = int.Parse(cmbPizzas.SelectedValue.ToString());
                var delPizza = context.Pizzas.Where(p => p.PizzaId == delPizzaId).FirstOrDefault();
                txtEditName.Text = delPizza.PizzaName;
                txtEditPrice.Text = delPizza.PizzaPrice.ToString();
            }else
            {
                txtEditName.Text = "";
                txtEditPrice.Text = "";
            }
        }

        private void EditPizza(object sender, RoutedEventArgs e)
        {
            if (cmbPizzas.SelectedValue != null)
            {
                var delPizzaId = int.Parse(cmbPizzas.SelectedValue.ToString());
                var delPizza = context.Pizzas.Where(p => p.PizzaId == delPizzaId).FirstOrDefault();
                delPizza.PizzaName = txtEditName.Text;
                delPizza.PizzaPrice = Convert.ToDecimal(txtEditPrice.Text);
                context.SaveChanges();
                ShowPizzas();
                ClearForms();
                cmbPizzas.SelectedValue = null;
            } else
            {
                MessageBox.Show("Wybierz pizzę którą chcesz edytować");
            }
        }
        private void ClearForms()
        {
            txtEditPrice.Text = "";
            txtEditName.Text = "";
            tbPizzaName.Text = "";
            tbPizzaPrice.Text = "";
        }

    }
}
