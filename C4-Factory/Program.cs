using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            PizzaStore nyStore = new NYStylePizzaStore();
            PizzaStore chStore = new ChicagoStylePizzaStore();
            Pizza p1=nyStore.orderPizza("cheese");
            Console.WriteLine("Ethan ordered "+p1.GetName()+"\n");
            Pizza p2=chStore.orderPizza("cheese");
            Console.WriteLine("Joel ordered "+p2.GetName()+"\n");
            PizzaStore caliStore = new CaliforniaStylePizzaStore();
            Pizza p3=caliStore.orderPizza("cheese");
            Console.WriteLine("SnoopDogg ordered "+p3.GetName()+"\n");
            Console.ReadLine();
        }
        
    }

    public abstract class Pizza
    {
        protected Dough dough;
        protected string name;
        protected Sauce sauce;
        protected Veggies[] veggies;
        protected Cheese cheese;
        protected Pepperoni pepperoni;
        protected Clams clam;
        public abstract void prepare();
        
        public virtual void bake()
        {
            Console.WriteLine("Bake for 20 minutes at 350.");
        }
        public virtual void cut()
        {
            Console.WriteLine("Cutting the pizza into diagonal slice.");
        }
        public virtual void box()
        {
            Console.WriteLine("Place pizza in official pizza store.");
        }
        public virtual string GetName() { return name; }
        public virtual void setName(string name) { this.name = name; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class CheesePizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;
        public CheesePizza(IPizzaIngredientFactory factory)
        {
            this.ingredientFactory = factory;
        }
         public override void prepare()
        {
            Console.WriteLine("Preparing " + name);
            dough = ingredientFactory.createDough();
            sauce = ingredientFactory.createSauce();
            cheese = ingredientFactory.createCheese();
        }
    }
    public class ClamPizza : Pizza
    {
        IPizzaIngredientFactory ingredientFactory;
        public ClamPizza(IPizzaIngredientFactory ingredientFactory)
        {
            this.ingredientFactory = ingredientFactory;
        }
        public override void prepare()
        {
            Console.WriteLine("Preparing "+name);
            dough = ingredientFactory.createDough();
            sauce = ingredientFactory.createSauce();
            clam = ingredientFactory.createClam();
            cheese = ingredientFactory.createCheese();
        }
    }

    //public class SimplePizzaFactory
    //{
    //    public Pizza createPizza(string type)
    //    {
    //        Pizza p=null;
    //        if (type.Equals("cheese"))
    //        {
    //            p = new CheesePizza();
    //        }
    //        else if (type.Equals("greek"))
    //        {
    //            p = new GreekPizza();
    //        }
    //        else if (type.Equals("pepperoni"))
    //        {
    //            p = new PepperoniPizza();
    //        }
    //        return p;
    //    }
    //}
    public abstract class PizzaStore
    {
        
        public Pizza orderPizza(string type)
        {
            Pizza p = null;

            p=createPizza(type);

            p.prepare();
            p.bake();
            p.cut();
            p.box();
            return p;
        }
        protected abstract Pizza createPizza(string type);
    }
    public class NYStylePizzaStore : PizzaStore
    {

        protected override Pizza createPizza(string type)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory = new NYIngredientFactory();
            if (type.Equals("cheese"))
            {
                pizza=new CheesePizza(ingredientFactory);
                pizza.setName("New York Style Cheese Pizza");
            }
            //else if (type.Equals("veggie"))
            //{
            //    return new NYStyleVeggiePizza();
            //}
            else if (type.Equals("clam"))
            {
                pizza=new ClamPizza(ingredientFactory);
                pizza.setName("New York Style Clam Pizza");
            }
            //else if (type.Equals("pepperoni"))
            //{
            //    return new NYStylePepperoniPizza();
            //}
            
            return pizza;
        }
    }
    public class ChicagoStylePizzaStore : PizzaStore
    {
        protected override Pizza createPizza(string type)
        {
            Pizza pizza = null;
            IPizzaIngredientFactory ingredientFactory = new ChicagoIngredientFactory();
            if (type.Equals("cheese"))
            {
                pizza= new CheesePizza(ingredientFactory);
                pizza.setName("Chicago Style Cheese Pizza");
            }
            //else if (type.Equals("veggie"))
            //{
            //    return new ChicagoStyleVeggiePizza();
            //}
            else if (type.Equals("clam"))
            {
                pizza= new ClamPizza(ingredientFactory);
                pizza.setName("Chicago Style Clam Pizza");

            }
            //else if (type.Equals("pepperoni"))
            //{
            //    return new ChicagoStylePepperoniPizza();
            //}
            //else
            //{
               return pizza;
            //}
        }
    }
    public class CaliforniaStylePizzaStore : PizzaStore
    {
        protected override Pizza createPizza(string type)
        {
            //if (type.Equals("cheese"))
            //{
            //   return new CheesePizza();
            //}
            //else if (type.Equals("veggie"))
            //{
            //    return new CaliforniaStyleVeggiePizza();
            //}
            //else if (type.Equals("clam"))
            //{
            //    return new CaliforniaStyleClamPizza();
            //}
            //else if (type.Equals("pepperoni"))
            //{
            //    return new CaliforniaStylePepperoniPizza();
            //}
            //else
            //{
                return null;
            //}
        }
    }

    public class DependentPizzaStore
    {
        public Pizza createPizza(string style,string type)
        {
            Pizza pizza = null;
            if (style.Equals("NY"))
            {
                if (type.Equals("cheese"))
                {
                    pizza= new NYStyleCheesePizza();
                }
            }
            else if (style.Equals("Chicago"))
            {
                if (type.Equals("cheese"))
                {
                    pizza= new ChicagoStyleCheesePizza();
                }
            }else { return null; }
            pizza.prepare();
            pizza.bake();
            pizza.cut();
            pizza.box();
            return pizza;
        }
    }


    public interface IPizzaIngredientFactory
    {
        Dough createDough();
        Sauce createSauce();
        Cheese createCheese();
        Veggies[] createVeggies();
        Clams createClam();
        Pepperoni createPepperoni();
    }
    public class NYIngredientFactory : IPizzaIngredientFactory
    {
        public Cheese createCheese()
        {
            return new ReggianoCheese();
        }

        public Clams createClam()
        {
            return new FreshClams();
        }

        public Dough createDough()
        {
            return new ThinCrustDough();
        }

        public Pepperoni createPepperoni()
        {
            return new SlicedPepperoni();
        }

        public Sauce createSauce()
        {
            return new MarinaraSauce();
        }

        public Veggies[] createVeggies()
        {
            return new Veggies[] { new Garlic(),new Onion(),new Mushroom(),new RedPepper()};
        }
    }
    public class ChicagoIngredientFactory : IPizzaIngredientFactory
    {
        public Cheese createCheese()
        {
            return new MozzarellaCheese();
        }

        public Clams createClam()
        {
            return new FrozenClams();
        }

        public Dough createDough()
        {
            return new ThickCrustDough();
        }

        public Pepperoni createPepperoni()
        {
            return new SlicedPepperoni();
        }

        public Sauce createSauce()
        {
            return new PlumTomatoSauce();
        }

        public Veggies[] createVeggies()
        {
            return new Veggies[] { new Parmesan(),new Spinach(),new BlackOlives(),new EggPlant(),new Oregano()} ;
        }
    }

    internal class Oregano : Veggies
    {
    }

    internal class EggPlant : Veggies
    {
    }

    internal class BlackOlives : Veggies
    {
    }

    internal class Spinach : Veggies
    {
    }

    internal class Parmesan : Veggies
    {
    }

    internal class PlumTomatoSauce : Sauce
    {
    }

    internal class ThickCrustDough : Dough
    {
    }

    internal class FrozenClams : Clams
    {
    }

    internal class MozzarellaCheese : Cheese
    {
    }

    internal class RedPepper : Veggies
    {
    }

    internal class Mushroom : Veggies
    {
    }

    internal class Onion : Veggies
    {
    }

    internal class Garlic : Veggies
    {
    }

    internal class MarinaraSauce : Sauce
    {
    }

    internal class SlicedPepperoni : Pepperoni
    {
    }

    internal class ThinCrustDough : Dough
    {
    }

    internal class FreshClams : Clams
    {
    }

    public class ReggianoCheese : Cheese
    {
    }

    public class Pepperoni
    {
    }

    public class Clams
    {
    }

    public class Veggies
    {
    }

    public class Cheese
    {
    }

    public class Sauce
    {
    }

    public class Dough
    {
    }
}
