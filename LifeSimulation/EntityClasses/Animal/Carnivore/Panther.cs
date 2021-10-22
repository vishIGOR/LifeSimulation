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
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Panther child = new Panther(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}