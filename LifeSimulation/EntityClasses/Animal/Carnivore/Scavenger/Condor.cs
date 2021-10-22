using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Scavenger
{
    public class Condor:Scavenger
    {
        public Condor(Tile tile, Map map)
        {
            MaxHitPoints = 20;
            MaxHungerPoints = 25;
            HungerBorder = 10;
            DamageForce = 25;
            MaxMatingCounter = 13;
            MaxAge = 30;
            Color = Brushes.Tan;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Condor child = new Condor(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}