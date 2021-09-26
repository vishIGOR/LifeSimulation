using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;
namespace LifeSimulation.EntityClasses
{
    public abstract class Entity
    {
        public Tile CurrentTile { get; protected set; }
        protected int HitPoints;
        protected int MaxHitPoints;
        protected Randomizer EntityRandomizer;
        protected Map EntityMap;
        
        public Brush EntityColor { get; protected set; }
        public virtual void ChooseAction(){}

        
    }
}