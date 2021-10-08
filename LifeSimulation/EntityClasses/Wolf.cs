using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Wolf : Animal, IFeedable
    {
        private int HungerPoints;
        private int MaxHungerPoints;

        public Wolf(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
            
            HitPoints = 25;
            MaxHitPoints = 25;

            MaxHungerPoints = 10;
            HungerPoints = 10;
            
            Age = 0;
            MaxAge = 40;
            
            Color = Brushes.Black;
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

            if (HungerPoints < 5)
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
            double maxDistance = 20;
            Entity nearestFood = null;

            foreach (var animal in Map.Animals)
            {
                if (animal is Sheep)
                {
                    currentDistance = CalculateDistance(animal);
                    if (minDistance > currentDistance && currentDistance<maxDistance)
                    {
                        minDistance = currentDistance;
                        nearestFood = animal;
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
            target.DamageIt(20);
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