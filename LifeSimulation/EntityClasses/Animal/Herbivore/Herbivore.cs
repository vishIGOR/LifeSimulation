namespace LifeSimulation.EntityClasses
{
    public abstract class Herbivore:Animal
    {
        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 10;
            Entity nearestFood = null;

            foreach (var food in Map.Fetuses)
            {
                if (food.Eatable)
                {
                    currentDistance = CalculateDistance(food);
                    if (minDistance > currentDistance && currentDistance < maxDistance)
                    {
                        minDistance = currentDistance;
                        nearestFood = food;
                    }
                }
            }
            
            if (nearestFood == null)
            {
                foreach (var food in Map.Plants)
                {
                    if (food.Eatable)
                    {
                        currentDistance = CalculateDistance(food);
                        if (minDistance > currentDistance && currentDistance < maxDistance)
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