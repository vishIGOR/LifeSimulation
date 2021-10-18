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

        public bool Eatable{ get; protected set; }
        public bool Toxicity{ get; protected set; }
        public int ToxicityValue{ get; protected set; }
        public int ToxicityCounter{ get; protected set; }
        
        public Brush Color { get; protected set; }
        public abstract void ChooseAction();
        protected abstract void Die();

        public void DamageIt(int damage)
        {
            HitPoints -= damage;
        }

        protected virtual void SetStandartValues(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
        }
    }
}