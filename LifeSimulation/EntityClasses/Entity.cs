using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;
namespace LifeSimulation.EntityClasses
{
    public abstract class Entity
    {
        public Tile Tile { get; protected set; }
        protected int HitPoints;
        protected int MaxHitPoints;
        protected Randomizer Randomizer;
        protected Map Map;
        
        public Brush Color { get; protected set; }
        public virtual void ChooseAction(){}
        protected virtual void Die(){}

        public void DamageIt(int damage)
        {
            HitPoints -= damage;
        }
    }
}