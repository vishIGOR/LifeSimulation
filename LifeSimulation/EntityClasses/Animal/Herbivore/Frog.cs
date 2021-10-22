using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Frog:Herbivore
    {
        public Frog(Tile tile, Map map)
        {
            MaxHitPoints = 16;
            MaxHungerPoints = 12;
            HungerBorder = 4;
            DamageForce = 15;
            MaxMatingCounter = 9;
            MaxAge = 15;
            Color = Brushes.Lime;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Frog child = new Frog(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}