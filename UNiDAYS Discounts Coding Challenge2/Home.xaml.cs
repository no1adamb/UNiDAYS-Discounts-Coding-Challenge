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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            //I wasnt sure on what was meant by pricing rules, see read me for more details

            //Price rules
            //Item A has no discounts, item b is 2 for £20, item c is 3 for 10
            //Item D is Buy one get one free and is £7 so 2 for £7
            string[,] pricingRules = { {"A", null, null }, { "B", "2", "20" }, { "C", "3", "10" }, {"D", "2", "7" }, { "E", "3", "10" } };

            UnidaysDiscountChallenge unidaysDiscountChallenge = new UnidaysDiscountChallenge(pricingRules);

            //List of products, should be generated from a database connection, same for pricing rules
            List<Item> items = new List<Item>
            {
                //List of items, got from a DB
                new Item("A", "Item A", 8.00m),
                new Item("B", "Item B", 12.00m),
                new Item("C", "Item C", 4.00m),
                new Item("D", "Item D", 7.00m),
                new Item("E", "Item E", 5.00m),
            };

            //Add the items to the UI
            PopulateItems(unidaysDiscountChallenge, items);

            tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();


            //Filter the items based on the textbox
            tbSearch.TextChanged += (sender, EventArgs) =>
            {
                PopulateItems(unidaysDiscountChallenge, items);
            };

            //Shutdown the application
            butPower.Click += ButPower_Click;

            butBasket.Click += (sender, EventArgs) =>
            {
                ButBasket_Click(unidaysDiscountChallenge);
            };

        }

        private void ButPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButBasket_Click(UnidaysDiscountChallenge unidaysDiscountChallenge)
        {
            //Create a new window, send in the basket
            BasketWindow basketWindow = new BasketWindow(unidaysDiscountChallenge);

            if (basketWindow.ShowDialog() == true)
            {
                tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();
            }
        }


        private void PopulateItems(UnidaysDiscountChallenge unidaysDiscountChallenge, List<Item> items)
        {
            //Remove all the products
            lvItems.Items.Clear();

            //Foreach item that matches filter, display on UI
            foreach (Item item in items)
            {
                //If search bar is empty (Gets all), or the item name contains the searchbar value
                if (item.GetName().ToUpper().Contains(tbSearch.Text.ToUpper()) || tbSearch.Text == "")
                {
                    //Create 3 textboxs, item name, id and price
                    TextBlock tbItem = new TextBlock
                    {
                        Text = item.GetName(),
                        Foreground = Brushes.Black,
                        FontSize = 22,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center

                    };

                    TextBlock tbID = new TextBlock
                    {
                        Text = "#" + item.GetID(),
                        Foreground = Brushes.LightGray,
                        FontSize = 15,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center

                    };

                    TextBlock tbPrice = new TextBlock
                    {
                        Text = "£" + item.GetCost().ToString(),
                        Foreground = Brushes.Black,
                        FontSize = 18,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center

                    };
                    tbPrice.SetValue(Grid.ColumnProperty, 2);


                    //Store the id and name in a stack panel
                    StackPanel stackPanel = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    stackPanel.SetValue(Grid.ColumnProperty, 1);

                    //Container for textblocks
                    stackPanel.Children.Add(tbID);
                    stackPanel.Children.Add(tbItem);

                    //The view button
                    Button button = new Button
                    {
                        Content = "View",
                        Background = new SolidColorBrush(Color.FromRgb(29, 213, 119)),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(29, 213, 119)),
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 50,

                    };
                    button.SetValue(Grid.ColumnProperty, 3);

                    //Event for the button to open new window
                    button.Click += (sender, EventArgs) =>
                    {
                        //When the button is clicked, open a new window with the item information 
                        View(unidaysDiscountChallenge, item);
                    };

                    //Create a grid to layout
                    Grid grid = new Grid();

                    //Create 4 columns
                    ColumnDefinition col1 = new ColumnDefinition
                    {
                        Width = new GridLength(200)
                    };
                    ColumnDefinition col2 = new ColumnDefinition
                    {
                        Width = new GridLength(400)
                    };
                    ColumnDefinition col3 = new ColumnDefinition
                    {
                        Width = new GridLength(200)
                    };
                    ColumnDefinition col4 = new ColumnDefinition
                    {
                        Width = new GridLength(80)
                    };

                    //Add columns to grid
                    grid.ColumnDefinitions.Add(col1);
                    grid.ColumnDefinitions.Add(col2);
                    grid.ColumnDefinitions.Add(col3);
                    grid.ColumnDefinitions.Add(col4);


                    //Placeholder img, replace for actual product img
                    Image img = new Image
                    {
                        Source = new BitmapImage(new Uri("https://via.placeholder.com/1694x1435"))
                    };

                    //build the layout
                    ListViewItem lvi = new ListViewItem();

                    grid.Children.Add(img);
                    grid.Children.Add(stackPanel);
                    grid.Children.Add(tbPrice);
                    grid.Children.Add(button);

                    lvItems.Items.Add(grid);
                }
            }
        }

        private void View(UnidaysDiscountChallenge unidaysDiscountChallenge, Item item)
        {
            //Create a new window
            ItemWindow itemWindow = new ItemWindow(unidaysDiscountChallenge, item);

            //Check if diaglog is true, otherwise user hasnt click add
            if (itemWindow.ShowDialog() == true)
            {
                //Add the returned item to basket
                unidaysDiscountChallenge.AddToBasket(itemWindow.returnItem);

                //Update bsket size
                tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();

            }
        }
    }
}
