using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Mouse:Herbivore
    {
        public Mouse(Tile tile, Map map)
        {
            MaxHitPoints = 10;
            MaxHungerPoints = 11;
            HungerBorder = 3;
            DamageForce = 15;
            MaxMatingCounter = 7;
            MaxAge = 20;
            Color = Brushes.DarkGray;
            
            SetStandartValues(tile,map);
            Mover = new Mover(this,2, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 2;
        }

        protected override void CreateChild()
        {
            Mouse child = new Mouse(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}