using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep : Herbivore
    {
        public Sheep(Tile tile, Map map)
        {
            MaxHitPoints = 10;
            MaxHungerPoints = 15;
            HungerBorder = 4;
            MaxMatingCounter = 20;
            MaxAge = 50;
            Color = Brushes.White;
            DamageForce = 40;
            
            SetStandartValues(tile, map);
            Mover = new Mover(3, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }
        protected override void CreateChild()
        {
            Sheep child = new Sheep(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}