using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public class Pig:Omnivore
    {
        public Pig(Tile tile, Map map)
        {
            MaxHitPoints = 32;
            MaxHungerPoints = 16;
            HungerBorder = 6;
            DamageForce = 30;
            MaxMatingCounter = 12;
            MaxAge = 26;
            Color = Brushes.Pink;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Pig child = new Pig(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}