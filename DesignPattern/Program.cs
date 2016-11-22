using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Duck d = new MallardDuck();
            d.quackingBehaviour = new Quack();
            d.flyingBehaviour = new FlyWithWings();
            d.PerformQuack();
            Duck d2 = new ModelDuck();
            d2.PerformFly();
            d2.flyingBehaviour = new FlyWithRockets();
            d2.PerformFly();
            Console.ReadLine();
        }
    }
    abstract class Duck
    {
        public IFlyable flyingBehaviour { get; set; }
        public IQuakable quackingBehaviour { get; set; }
        public abstract void display();
        public void swim()
        {
            Console.WriteLine("All ducks can float even decoys!");
        }   
        public void PerformQuack()
        {
            quackingBehaviour.quack();
        }
        public void PerformFly()
        {
            flyingBehaviour.Fly();
        }
    }
    interface IFlyable
    {
      void Fly();

    }
    interface IQuakable
    {
        void quack();
    }
    class FlyWithWings : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Flying with wings.");
        }
    }
    class FlyNoWay : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("I cant fly.");
        }
    }
    class FlyWithRockets : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("I am flying with rockets.");
        }
    }
    class Quack : IQuakable
    {
        public void quack()
        {
            Console.WriteLine("Quack Quack");
        }
    }
    class Squeak : IQuakable
    {
        public void quack()
        {
            Console.WriteLine("Squeak Squeak");
        }
    }
    class MuteQuack : IQuakable
    {
        public void quack()
        {
            Console.WriteLine("Cant quack");
        }
    }
    class MallardDuck : Duck
    {
        
        public override void  display()
        {
            Console.WriteLine("I am MallardDuck.");
        }

    }
    class RedHeadDuck : Duck
    {
        public override void display()
        {
            Console.WriteLine("I am RedHeadDuck.");
        }
    }
    class RubberDuck : Duck
    {
        public override void display()
        {
            Console.WriteLine("I am RubberDuck.");
        }
    }
    class ModelDuck:Duck
    {
        public ModelDuck()
        {
            quackingBehaviour = new Quack();
            flyingBehaviour = new FlyNoWay();
        }

        public override void display()
        {
            Console.WriteLine("I am ModelDuck.");
        }
    }
    class DuckCall
    {
        public IQuakable quackingBehaviour { get; set; }
        public DuckCall()
        {
            quackingBehaviour = new Quack();
        }
        public void PerformQuack()
        {
            quackingBehaviour.quack();
        }
    }
    }
