using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionViewExample.Model
{
    public class Monkey
    {
        public string Specie { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public Monkey(string name, string species, int age)
        {
            this.Name = name;
            this.Specie = species;
            this.Age = age;
        }
    }
}
