using System;
using System.Collections.Generic;
using System.Text;

namespace UNiDAYS_Discounts_Coding_Challenge2
{
    public class UnidaysDiscountChallenge
    {
        //A list of items added to the basket
        private List<Item> Basket { get; set; }

        //A 2D array storing the pricing rules, element 1 store id, element 2 store how many products quailfy for the deal, 3 stores the cost after discount
        private string[,] PricingRules { get; set; }

        public UnidaysDiscountChallenge(string[,] pricingRules)
        {
            //Create an instance of the list and set the pricing rules
            Basket = new List<Item>();

            PricingRules = pricingRules;
        }

        //Add an item to the basket
        public void AddToBasket(Item item)
        {
            bool itemAdded = false;

            foreach (Item basketItem in Basket)
            {
                //If the item exists in the basket, increase the quantity, else add it as a new item
                if (basketItem.GetName() == item.GetName())
                {
                    //Is in basket, increase quanity
                    itemAdded = true;
                    basketItem.SetQuanity(basketItem.GetQuanity() + item.GetQuanity());
                }
            }
            if (itemAdded == false)
            {                    
                Basket.Add(item);
            }
        }

        public decimal CalculateTotalPrice()
        {
            decimal result = 0;

            foreach (Item item in Basket)
            {

                //Find pricing rules, /3 beacuse each rule has 3 elements
                for (int i = 0; i < PricingRules.Length / 3; i++)
                {
                    //Check if the deal is null
                    if ((PricingRules[i, 1] != null || PricingRules[i, 2] != null))
                    {
                        //Find the rule that matches the item ID
                        if (PricingRules[i, 0].Contains(item.GetID()))
                        {
                            //Get the prerequitst for the discount and the cost after discount and the item qty
                            int prQTY = int.Parse(PricingRules[i, 1]);
                            int prCost = int.Parse(PricingRules[i, 2]);
                            int qty = item.GetQuanity();

                            while (prQTY <= qty)
                            {
                                result += prCost;
                                qty -= prQTY;
                            }

                            if (qty > 0)
                            {
                                result += item.GetCost();
                            }
                        } 
                    }
                    else
                    {
                        result += (item.GetCost() * item.GetQuanity());
                    }
                }
            }
                
            return result;
        }

        public int GetBasketSize()
        {
            return Basket.Count;
        }

        public List<Item> GetBasket()
        {
            return Basket;
        }

        public void RemoveFromBasket(Item item)
        {
            Basket.Remove(item);
        }
    }
}
