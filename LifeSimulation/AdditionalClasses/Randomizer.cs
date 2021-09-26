using System;

namespace LifeSimulation.AdditionalClasses
{
    public class Randomizer
    {
        private Random RandomVar;

        public Randomizer()
        {
            RandomVar = new Random();
        }

        public int RandomInt(int min,int max)
        {
            int temporaryInt = RandomVar.Next(min, max + 1);
            return temporaryInt;
        }
    }
}