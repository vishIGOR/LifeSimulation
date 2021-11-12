using System;
using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Tiger:Carnivore
    {
        public Tiger(Tile tile, Map map)
        {
            MaxHitPoints = 60;
            MaxHungerPoints = 22;
            HungerBorder = 6;
            DamageForce = 80;
            MaxMatingCounter = 17;
            MaxAge = 45;
            Color = Brushes.OrangeRed;
            
            SetStandartValues(tile,map);
            Mover = new Mover(this,2, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 2;
        }

        protected override void CreateChild()
        {
            Tiger child = new Tiger(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
        
        // protected override void LookForFood()
        // {
        //     double minDistance = 10000000000;
        //     double currentDistance=0;
        //     double maxDistance = 20;
        //     Entity nearestFood = null;
        //     Type myType = GetType();
        //
        //     foreach (var animal in Map.Animals)
        //     {
        //         if (animal.GetType() != myType)
        //         {
        //             currentDistance = CalculateDistance(animal);
        //             if (minDistance > currentDistance && currentDistance < maxDistance)
        //             {
        //                 minDistance = currentDistance;
        //                 nearestFood = animal;
        //             }
        //         }
        //     }
        //
        //     if (currentDistance <= 5)
        //         Mover.CurrentMovingWay = 1;
        //     
        //     if (nearestFood == null)
        //     {
        //         Tile = Mover.Walk(Tile);
        //     }
        //     else
        //     {
        //         if (nearestFood.Tile == Tile)
        //         {
        //             StartEat(nearestFood);
        //             Mover.CurrentMovingWay = 3;
        //         }
        //         else
        //         {
        //             Tile = Mover.MoveTo(Tile, nearestFood.Tile);
        //         }
        //     }
        // }
    }
}