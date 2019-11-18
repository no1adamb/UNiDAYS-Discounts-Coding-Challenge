using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UNiDAYS_Discounts_Coding_Challenge2
{
    /// <summary>
    /// Interaction logic for BasketWindow.xaml
    /// </summary>
    public partial class BasketWindow : Window
    {
        public BasketWindow(UnidaysDiscountChallenge unidaysDiscountChallenge)
        {
            InitializeComponent();

            GetItems(unidaysDiscountChallenge, unidaysDiscountChallenge.GetBasket());

            //Get the cost of all the items
            decimal price = unidaysDiscountChallenge.CalculateTotalPrice();
            decimal totalPrice = price;

            //Factor delivery charge
            if (price < 50)
            {
                tbShipping.Text = "Delivery - £7.00";
                totalPrice += 7.00m;
            }
            else
            {
                tbShipping.Text = "Delivery - £0.00";
            }

            //Display 
            tbPrice.Text = "Item cost - £" + price.ToString();
            tbTotal.Text = "Total Cost - £" + totalPrice.ToString();

            tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();

            butBack.Click += ButBack_Click;
        }

        private void ButBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void GetItems(UnidaysDiscountChallenge unidaysDiscountChallenge, List<Item> items)
        {
            //Remove all the products
            lvBasket.Items.Clear();

            if (items.Count > 0)
            {
                //Foreach item that matches filter, display on UI
                foreach (Item item in items)
                {

                    //Create 3 textboxs, item name, id and price
                    TextBlock tbItem = new TextBlock
                    {
                        Text = item.GetName(),
                        Foreground = Brushes.Black,
                        FontSize = 22,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(5)

                    };
                    tbItem.SetValue(Grid.ColumnProperty, 1);


                    TextBlock tbQTY = new TextBlock
                    {
                        Text = item.GetQuanity().ToString() + "x",
                        Foreground = Brushes.LightGray,
                        FontSize = 15,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(5)

                    };
                    tbQTY.SetValue(Grid.ColumnProperty, 0);


                    TextBlock tbPrice = new TextBlock
                    {
                        Text = "£" + item.GetCost().ToString(),
                        Foreground = Brushes.Black,
                        FontSize = 18,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(5)
                    };
                    tbPrice.SetValue(Grid.ColumnProperty, 2);

                    Button button = new Button
                    {
                        Content = "Delete",
                        Background = Brushes.Red,
                        BorderBrush = Brushes.Red,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Height = 35,
                        Margin = new Thickness(5)

                    };
                    button.SetValue(Grid.ColumnProperty, 3);

                    button.Click += (sender, EventArgs) =>
                    {
                        //Remove the item from the basket
                        unidaysDiscountChallenge.RemoveFromBasket(item);
                        //Reload ui
                        GetItems(unidaysDiscountChallenge, items);
                        tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();
                    };

                    Grid grid = new Grid();

                    //Create 4 columns
                    ColumnDefinition col1 = new ColumnDefinition
                    {
                        Width = new GridLength(75)
                    };
                    ColumnDefinition col2 = new ColumnDefinition
                    {
                        Width = new GridLength(150)
                    };
                    ColumnDefinition col3 = new ColumnDefinition
                    {
                        Width = new GridLength(100)
                    };
                    ColumnDefinition col4 = new ColumnDefinition
                    {
                        Width = new GridLength(100)
                    };

                    grid.ColumnDefinitions.Add(col1);
                    grid.ColumnDefinitions.Add(col2);
                    grid.ColumnDefinitions.Add(col3);
                    grid.ColumnDefinitions.Add(col4);

                    grid.Children.Add(tbQTY);
                    grid.Children.Add(tbItem);
                    grid.Children.Add(tbPrice);
                    grid.Children.Add(button);


                    //Add pannel to listview
                    lvBasket.Items.Add(grid);
                } 
            }
            else
            {
                //If there are no items in the basket
                TextBlock tbResponse = new TextBlock
                {
                    Text = "No items added to basket!",
                    Foreground = Brushes.Black,
                    FontSize = 25,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)

                };

                lvBasket.Items.Add(tbResponse);

            }
        }
               
    }
}
