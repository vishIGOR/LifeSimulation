using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Wolf : Carnivore
    {

        public Wolf(Tile tile, Map map)
        {
            MaxHitPoints = 25;
            MaxHungerPoints = 20;
            HungerBorder = 6;
            DamageForce = 30;
            MaxMatingCounter = 15;
            MaxAge = 40;
            Color = Brushes.DimGray;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 2;
        }

        protected override void CreateChild()
        {
            Wolf child = new Wolf(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}