using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAndObjects
{
    class Color
    {
        public float r, g, b;
    }

    class Dog
    {
        public string name;
        public int size;
        public string breed;
        //public Color color;
        public ConsoleColor color;

        public void Walk()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is walking.");
        }
        public void Eat(string food)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is eating " + food);
        } 
        public void Shit()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is shitting.");
        }
        public void sleep()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is sleeping.");
        }
        public void Wag()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " is wagging it's tail");
        }
        public void Speak()
        {
            Console.ForegroundColor = color;
            Console.WriteLine(name + " says: Woof! ");
        }
    }

    


    class Program
    {
        static void Main(string[] args)
        {
            // create instance of color
            /*Color brown = new Color();
            brown.r = 139;
            brown.g = 69;
            brown.b = 19;

            Color white = new Color();
            white.r = 255;
            white.g = 255;
            white.b = 255;

            Color black = new Color();
            black.r = 0;
            black.g = 0;
            black.b = 0;*/

            // create instance of dog
            Dog dog1 = new Dog();
            dog1.name = "Max";
            dog1.size = 6;
            dog1.breed = "Boxer";
            dog1.color = ConsoleColor.Green;

            Dog dog2 = new Dog();
            dog2.name = "Demon";
            dog2.size = 8;
            dog2.breed = "Husky";
            dog2.color = ConsoleColor.Cyan;

            // Call Methods on dog
            dog1.Speak();
            dog2.Speak();
            dog2.Eat("Pizza");
            dog1.Walk();
            dog1.Eat("Taco");
            dog2.Walk();
            dog2.Shit();
            dog1.Wag();
            dog1.Shit();
            dog2.Speak();
            

            Console.ReadLine();
        }
    }
}
