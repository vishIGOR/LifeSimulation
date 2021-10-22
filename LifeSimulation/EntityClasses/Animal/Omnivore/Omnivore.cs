using System;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public abstract class Omnivore:Animal
    {
        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 20;
            Entity nearestFood = null;
            Type myType = GetType();

            foreach (var food in Map.Entities)
            {
                if (food.Eatable)
                {
                    if (food.GetType() != myType)
                    {
                    
                        currentDistance = CalculateDistance(food);
                        if (minDistance > currentDistance && currentDistance<maxDistance)
                        {
                            minDistance = currentDistance;
                            nearestFood = food;
                        }
                    }
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
                    Tile = Mover.MoveTo(Tile,nearestFood.Tile);
                }
            }
        }
    }
}