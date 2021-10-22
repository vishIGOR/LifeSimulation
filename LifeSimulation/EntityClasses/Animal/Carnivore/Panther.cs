using System;
using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Panther:Carnivore
    {
        public Panther(Tile tile, Map map)
        {
            MaxHitPoints = 45;
            MaxHungerPoints = 32;
            HungerBorder = 10;
            DamageForce = 50;
            MaxMatingCounter = 25;
            MaxAge = 45;
            Color = Brushes.Black;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 1;
        }

        protected override void CreateChild()
        {
            Panther child = new Panther(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }

        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance=0;
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

            if (currentDistance <= 5)
                Mover.CurrentMovingWay = 1;
            
            if (nearestFood == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                if (nearestFood.Tile == Tile)
                {
                    StartEat(nearestFood);
                    Mover.CurrentMovingWay = 3;
                }
                else
                {
                    Tile = Mover.MoveTo(Tile, nearestFood.Tile);
                }
            }
        }
    }
}