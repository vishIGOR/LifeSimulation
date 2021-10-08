using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep : Animal, IFeedable
    {
        private int HungerPoints;
        private int MaxHungerPoints;

        public Sheep(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;

            HungerPoints = 15;
            MaxHungerPoints = 15;

            MaxHitPoints = 10;
            HitPoints = 10;

            Age = 0;
            MaxAge = 50;

            Color = Brushes.White;
        }

        protected override void Die()
        {
            Map.Animals.Remove(this);
            Map.DeadEntities.Add(this);
        }

        public override void ChooseAction()
        {
            ++Age;
            if (HungerPoints == 0)
            {
                --HitPoints;
            }
            else
            {
                --HungerPoints;
            }

            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            if (Age > MaxAge)
            {
                if (Randomizer.GetRandomInt(1, 10) > 4)
                {
                    Die();
                    return;
                }
            }

            if (HungerPoints < 3)
            {
                LookForFood();
                return;
            }

            Walk();
        }

        public void LookForFood()
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
                Walk();
            }
            else
            {
                if (nearestFood.Tile == Tile)
                {
                    StartEat(nearestFood);
                }
                else
                {
                    MoveTo(nearestFood);
                }
            }
            
            
        }

        public void StartEat(Entity target)
        {
            target.DamageIt(40);
            if (target is Fetus)
            {
                Fetus eatenFetus = target as Fetus;
                if (eatenFetus.Toxicity)
                {
                    DamageIt(eatenFetus.ToxicityValue);
                    return;
                }
            }
            
            if (HitPoints < MaxHitPoints - 5)
            {
                HitPoints += 5;
            }
            else
            {
                HitPoints = MaxHitPoints;
            }

            HungerPoints = MaxHungerPoints;
        }
    }
}