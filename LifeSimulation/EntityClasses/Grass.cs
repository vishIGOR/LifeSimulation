using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Grass:Plant
    {
        public Grass(Tile tile, Map map)
        {
            CurrentTile = tile;
            HitPoints = 40;
            MaxHitPoints = 40;
            ReadyToCreep = 15;
            CreepCounter = 0;
            EntityColor = Brushes.LightGreen;
            EntityRandomizer = new Randomizer();
            EntityMap = map;
        }
        
        public override void ChooseAction()
        {
            ++CreepCounter;
            
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            if (CreepCounter >= ReadyToCreep)
            {
                CreepCounter = 0;
                Creep();
                return;
            }
        }
    }
}