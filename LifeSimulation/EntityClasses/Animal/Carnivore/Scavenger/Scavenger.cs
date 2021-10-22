using System;

namespace LifeSimulation.EntityClasses.Scavenger
{
    public abstract class Scavenger:Carnivore
    {
        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 15;
            Entity nearestFood = null;

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