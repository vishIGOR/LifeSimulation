using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public class Bear:Omnivore
    {
        public Bear(Tile tile, Map map)
        {
            MaxHitPoints = 60;
            MaxHungerPoints = 25;
            HungerBorder = 30;
            DamageForce = 60;
            MaxMatingCounter = 20;
            MaxAge = 60;
            Color = Brushes.Brown;
            
            SetStandartValues(tile,map);
            Mover = new Mover(3, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;

        }
        
        protected override void CreateChild()
        {
            Bear child = new Bear(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
    }
}