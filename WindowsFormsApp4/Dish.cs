    
namespace Dish
{
    using System;
    using System.Collections;
    [Serializable]
    public abstract class Dish
    {
        string dish_name;
        string price;
        string allergenic_ingredients;
        
        public string Dish_name
        {
            get { return dish_name; }
            set { dish_name = string.Copy(value); }
        }

        public string Price
        {
            get { return price; }
            set { price = string.Copy(value); }
        }

        public string Allergenic_Ingredients
        {
            get { return allergenic_ingredients; }
            set { allergenic_ingredients = string.Copy(value); }
        }
    }

    [Serializable]
    public class Appetizers_dishes : Dish
    {
        public Appetizers_dishes()
        {
            Price = string.Empty;
            Dish_name = string.Empty;
            Allergenic_Ingredients= string.Empty;
        }

        public Appetizers_dishes(string Dish_name, string Price, string Allergenic_Ingredients)
        {
            this.Dish_name = string.Copy(Dish_name);
            this.Price = string.Copy(Price);
            this.Allergenic_Ingredients = string.Copy(Allergenic_Ingredients);
        }
       ~Appetizers_dishes() { }
    }

    [Serializable]
    public class Desserts : Dish
    {
        public Desserts()
           : this("new dessert", "", "none") { }

        public Desserts(string Dish_name, string Price, string Allergenic_Ingredients)
        {
            this.Dish_name = string.Copy(Dish_name);
            this.Price = string.Copy(Price);
            this.Allergenic_Ingredients = string.Copy(Allergenic_Ingredients);
        }
        ~Desserts() { }
    }

    [Serializable]
    public abstract class Main_dishes : Dish
    {
        string description;
        string type;

        public string Description
        {
            get { return description; }
            set { description = string.Copy(value); }
        }

        public string Type
        {
            get { return type; }
            set { type = string.Copy(value); }
        }
    }

    [Serializable]
    public class Vegan_dishes : Main_dishes
    {
        public Vegan_dishes()
          : this("new vegan dish",string.Empty, "none", "none") {
            Type = string.Copy("VEGAN");
        }

        public Vegan_dishes(string Dish_name, string Price, string Allergenic_Ingredients, string Description)
        {
            this.Dish_name = string.Copy(Dish_name);
            this.Price = string.Copy(Price);
            this.Allergenic_Ingredients = string.Copy(Allergenic_Ingredients);
            this.Description = string.Copy(Description);
            this.Type = string.Copy("VEGAN");
        }
        ~Vegan_dishes() { }
    }

    [Serializable]
    public class Meat_dishes : Main_dishes
    {
        public Meat_dishes()
         : this("new meat dish", "", "none", "none") { this.Type = string.Copy("MEAT"); }

        public Meat_dishes(string Dish_name, string Price, string Allergenic_Ingredients, string Description)
        {
            this.Dish_name = string.Copy(Dish_name);
            this.Price = string.Copy(Price);
            this.Allergenic_Ingredients = string.Copy(Allergenic_Ingredients);
            this.Description = string.Copy(Description);
            this.Type = string.Copy("MEAT"); 
        }
        ~Meat_dishes() { }
    }

    [Serializable]
    public class Dairy_dishes : Main_dishes
    {
        public Dairy_dishes()
        : this("new dairy dish","", "none", "none") { this.Type = string.Copy("DAIRY"); }

        public Dairy_dishes(string Dish_name, string Price, string Allergenic_Ingredients, string Description)
        {
            this.Dish_name = string.Copy(Dish_name);
            this.Price = string.Copy(Price);
            this.Allergenic_Ingredients = string.Copy(Allergenic_Ingredients);
            this.Description = string.Copy(Description);
            this.Type = string.Copy("DAIRY"); 
        }
        ~Dairy_dishes() { }
    }

    [Serializable]
    public class Menu_Appetizers
    {
        public SortedList Appetizers_dishes_list;
        public Menu_Appetizers()
        {
            Appetizers_dishes_list = new SortedList();
            Appetizers_dishes ap1 = new Appetizers_dishes("Arencini", "55", "Milk");
            Appetizers_dishes_list.Add(0,ap1);
        }

        public int NextIndexAppetizers
        {
            get
            {
                return Appetizers_dishes_list.Count;
            }
        }

        public Dish this[int index]
        {
            get
            {
                if (index < 0 || index >= Appetizers_dishes_list.Count)
                    return (Appetizers_dishes)null;
                return (Appetizers_dishes)Appetizers_dishes_list.GetByIndex(index);
            }
            set
            {
                if (index <= Appetizers_dishes_list.Count)
                {
                    Appetizers_dishes_list[index] = value;
                }
            }
        }
    }

    [Serializable]
    public class Menu_Vegan
    {
        public SortedList Vegan_dishes_list;
        public Menu_Vegan()
        {
            Vegan_dishes_list = new SortedList();
       
            Vegan_dishes v1= new Vegan_dishes("Hummus","35", "Olives, Sesame,Wheat", "A plate full of rich hummus with olive oil,\r\nserved with handmade pita bread.\r\n");
            Vegan_dishes_list.Add(0, v1);
        }

        public int NextIndexVegan
        {
            get
            {
                return Vegan_dishes_list.Count;
            }
        }

        public Main_dishes this[int index]
        {
            get
            {
                if (index < 0 || index >= Vegan_dishes_list.Count)
                    return (Vegan_dishes)null;
                return (Vegan_dishes)Vegan_dishes_list.GetByIndex(index);
            }
            set
            {
                if (index <= Vegan_dishes_list.Count)
                    Vegan_dishes_list[index] = value;
            }
        }
    }

    [Serializable]
    public class Menu_Meat
    {
        public SortedList Meat_dishes_list;
        public Menu_Meat()
        {
            Meat_dishes_list = new SortedList();
            Meat_dishes m1 = new Meat_dishes("Sinta Steak","90","none", "Grilled sinta steak\r\nwith fresh vegetables from the field.\r\nWith a truffle aroma.\r\n");
            Meat_dishes_list.Add(0, m1);
        }

        public int NextIndexMeat
        {
            get
            {
                return Meat_dishes_list.Count;
            }
        }

        public Main_dishes this[int index]
        {
            get
            {
                if (index < 0 || index >= Meat_dishes_list.Count)
                    return (Meat_dishes)null;
                return (Meat_dishes)Meat_dishes_list.GetByIndex(index);
            }
            set
            {
                if (index <= Meat_dishes_list.Count)
                    Meat_dishes_list[index] = value;
            }
        }
    }

    [Serializable]
    public class Menu_Dairy
    {
        public SortedList Dairy_dishes_list;
        public Menu_Dairy()
        {
            Dairy_dishes_list = new SortedList();
            Dairy_dishes da1 = new Dairy_dishes("dish1","0","none","none");
            Dairy_dishes_list.Add(0, da1);
        }

        public int NextIndexDairy
        {
            get
            {
                return Dairy_dishes_list.Count;
            }
        }

        public Main_dishes this[int index]
        {
            get
            {
                if (index < 0 || index >= Dairy_dishes_list.Count)
                    return (Dairy_dishes)null;
                return (Dairy_dishes)Dairy_dishes_list.GetByIndex(index);
            }
            set
            {
                if (index <= Dairy_dishes_list.Count)
                    Dairy_dishes_list[index] = value;
            }
        }
    }


    [Serializable]
    public class Menu_Desserts
    {
        public SortedList Desserts_list;
        public Menu_Desserts()
        {
            Desserts_list = new SortedList();
            Desserts d1 = new Desserts("Tiramisu","40", "Milk, Wheat");
            Desserts_list.Add(0, d1);
        }

        public int NextIndexDesserts
        {
            get
            {
                return Desserts_list.Count;
            }
        }

        public Dish this[int index]
        {
            get
            {
                if (index < 0 || index >= Desserts_list.Count)
                    return (Desserts)null;
                return (Desserts)Desserts_list.GetByIndex(index);
            }
            set
            {
                if (index <= Desserts_list.Count)
                    Desserts_list[index] = value;
            }
        }
    }

    [Serializable]
    public class The_menu
    {
        public Menu_Appetizers appetizers= new Menu_Appetizers();
        public Menu_Desserts desserts= new Menu_Desserts();
        public Menu_Vegan vegans= new Menu_Vegan();
        public Menu_Meat meats= new Menu_Meat();
        public Menu_Dairy dairy= new Menu_Dairy();

        public The_menu()
        {
            appetizers = null;
            desserts = null;
            vegans= null;
            meats= null;
            dairy = null;
        }
        public The_menu(Menu_Appetizers ap, Menu_Desserts d,Menu_Vegan v, Menu_Meat m, Menu_Dairy da)
        {
            appetizers=ap;
            desserts = d;
            vegans = v;
            meats= m;
            dairy = da;
        }


    }


}

