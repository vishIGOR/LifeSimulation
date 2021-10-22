using System;
using System.Diagnostics;
using System.Reflection;

namespace LifeSimulation.EntityClasses
{
    public abstract class Carnivore : Animal
    {
        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 20;
            Entity nearestFood = null;
            Type myType = GetType();

            foreach (var animal in Map.Animals)
            {
                if (animal.GetType() != myType)
                {
                    currentDistance = CalculateDistance(animal);
                    if (minDistance > currentDistance && currentDistance < maxDistance)
                    {
                        minDistance = currentDistance;
                        nearestFood = animal;
                    }
                }
            }

            foreach (var dead in Map.DeadBodies)
            {
                currentDistance = CalculateDistance(dead);
                if (minDistance > currentDistance && currentDistance < maxDistance)
                {
                    minDistance = currentDistance;
                    nearestFood = dead;
                }
            }

            if (nearestFood == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                if (nearestFood.Tile == Tile)
                {
                    StartEat(nearestFood);
                }
                else
                {
                    Tile = Mover.MoveTo(Tile, nearestFood.Tile);
                }
            }
        }
    }
}