using System;
using System.Collections.Generic;
using System.Text;

namespace UNiDAYS_Discounts_Coding_Challenge2
{
    public class Item
    {
        //Encapsulation for security
        private string ID { get; set; }
        private string Name { get; set; }
        private decimal Cost { get; set; }
        private int Quanity { get; set; }

        //itrem must have an ID, name and cost
        public Item(string id, string name, decimal cost)
        {
            ID = id;
            Name = name;
            Cost = cost;
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }

        public decimal GetCost()
        {
            return Cost;
        }

        public void SetQuanity(int quanity)
        {
            Quanity = quanity;
        }

        public int GetQuanity()
        {
            return Quanity;
        }
    }
}
