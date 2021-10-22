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
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Tiger child = new Tiger(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}