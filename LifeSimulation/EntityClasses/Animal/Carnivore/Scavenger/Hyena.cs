using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Scavenger
{
    public class Hyena:Scavenger
    {
        public Hyena(Tile tile, Map map)
        {
            MaxHitPoints = 20;
            MaxHungerPoints = 25;
            HungerBorder = 10;
            DamageForce = 25;
            MaxMatingCounter = 13;
            MaxAge = 30;
            Color = Brushes.Tan;
            
            SetStandartValues(tile,map);
            Mover = new Mover(this,2, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 1;
        }

        protected override void CreateChild()
        {
            Hyena child = new Hyena(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}