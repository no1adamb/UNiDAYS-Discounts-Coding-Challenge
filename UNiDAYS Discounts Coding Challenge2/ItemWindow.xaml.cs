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

namespace UNiDAYS_Discounts_Coding_Challenge2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        //Var for the item that will be added to the basket
        public Item returnItem { get; set; }
        public ItemWindow(UnidaysDiscountChallenge unidaysDiscountChallenge, Item item)
        {
            InitializeComponent();

            //Display the details of the item that was sent in
            tbID.Text = "#" + item.GetID().ToString();
            tbItemName.Text = item.GetName();
            tbPrice.Text = item.GetCost().ToString();

            butAdd.Click += (sender, EventArgs) => 
            { 
                //Send in the item that has been clicked
                ButAdd_Click(item); 
            };

            butBack.Click += ButBack_Click;
            butPower.Click += ButPower_Click;


            butBasket.Click += (sender, EventArgs) =>
            {
                //Take the user to the basket page, send in the basket
                ButBasket_Click(unidaysDiscountChallenge);
            };

            tbBasketSize.Text = unidaysDiscountChallenge.GetBasketSize().ToString();


        }

        private void ButBasket_Click(UnidaysDiscountChallenge unidaysDiscountChallenge)
        {
            //Create a new window
            BasketWindow basketWindow = new BasketWindow(unidaysDiscountChallenge);
            basketWindow.Show();

            Close();
        }

        private void ButPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButAdd_Click(Item item)
        {
            //Check the user has selected a quanity 
            if (cbQTY.SelectedIndex == -1)
            {
                cbQTY.BorderBrush = Brushes.Red;
            }
            else
            {
                item.SetQuanity(cbQTY.SelectedIndex + 1);

                DialogResult = true;

                returnItem = item;
            }
            
        }
    }
}
