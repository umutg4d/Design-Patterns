using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            Beverage b = new Espresso();
            Console.WriteLine(b.GetDescription()+" $"+b.cost());
            
            
            Beverage b2 = new DarkRoast();
            b2 = new Mocha(b2);
            b2 = new Mocha(b2);
            b2 = new Whip(b2);
            Console.WriteLine(b2.GetDescription()+" $"+b2.cost());
           

            Beverage b3 = new DarkRoast();
            b3.SetSize(Size.Large);
            b3 = new Soy(b3);
            b3 = new Mocha(b3);
            Console.WriteLine(b3.GetDescription()+" $"+b3.cost());
            Console.ReadLine();
        }
    }
    public enum Size
    {
        Small,Medium,Large
    }
    abstract class Beverage
    {
        protected string description = "Unknown Beverage";
        public virtual string GetDescription()
        {
            return description;
        }
        public abstract float cost();
        protected Size size;
        public virtual Size GetSize()
        {
            return size;
        }
        public virtual void SetSize(Size s)
        {
            this.size = s;
        }
    }
    class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            description = "HouseBlend";
        }
        public override float cost()
        {
            return 0.89f;
        }
    }
    class DarkRoast : Beverage
    {
        public DarkRoast()
        {
            description = "Dark Roast";
        }
        public override float cost()
        {
            
            return 0.99f;
        }
    }
    class Decaf : Beverage
    {
        public Decaf()
        {
            description = "Decaf";
        }
        public override float cost()
        {
            return 1.05f;
        }
    }
    class Espresso : Beverage
    {
        public Espresso()
        {
            description = "Espresso";
        }
        public override float cost()
        {
            return 1.99f;
        }
    }
    abstract class CondimentDecorator : Beverage
    {
        public override abstract string GetDescription();
        public abstract override Size GetSize();

    }
    class Soy : CondimentDecorator
    {
        Beverage beverage;
        public Soy(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Soy";
        }
        public override Size GetSize()
        {
            return beverage.GetSize();
        }
        public override float cost()
        {
            switch (GetSize())
            {
                case Size.Small:
                    return 0.1f + beverage.cost();
                case Size.Medium:
                    return 0.15f + beverage.cost();
                case Size.Large:
                    return 0.2f + beverage.cost();
                
            }
            return 0.15f+beverage.cost() ;
        }
        
    }
    class Milk : CondimentDecorator
    {
        Beverage beverage;
        public Milk(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Milk";
        }
        public override Size GetSize()
        {
            return beverage.GetSize();
        }
        public override float cost()
        {
            switch (GetSize())
            {
                case Size.Small:
                    return 0.05f + beverage.cost();
                case Size.Medium:
                    return 0.1f + beverage.cost();
                case Size.Large:
                    return 0.15f + beverage.cost();

            }
            return 0.1f+beverage.cost();
        }
    }
    class Mocha : CondimentDecorator
    {
        Beverage beverage;
        public Mocha(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Mocha";
        }
        public override Size GetSize()
        {
            return beverage.GetSize();
        }
        public override float cost()
        {
            switch (GetSize())
            {
                case Size.Small:
                    return 0.15f + beverage.cost();
                case Size.Medium:
                    return 0.2f + beverage.cost();
                case Size.Large:
                    return 0.25f + beverage.cost();

            }
            return 0.2f+beverage.cost();
        }
    }
    class Whip : CondimentDecorator
    {
        Beverage beverage;
        public Whip(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override string GetDescription()
        {
            return beverage.GetDescription() + ", Whip";
        }
        public override Size GetSize()
        {
            return beverage.GetSize();
        }
        public override float cost()
        {
            switch (GetSize())
            {
                case Size.Small:
                    return 0.05f + beverage.cost();
                case Size.Medium:
                    return 0.1f + beverage.cost();
                case Size.Large:
                    return 0.15f + beverage.cost();

            }
            return 0.1f+beverage.cost()  ;
        }
    }
}
