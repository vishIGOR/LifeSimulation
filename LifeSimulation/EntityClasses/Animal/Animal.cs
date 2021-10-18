using System;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public abstract class Animal : Entity
    {
        protected int Age;
        protected int MaxAge;

        public Mover Mover{ get; protected set; }
        public bool Sex{ get; protected set; }
        public bool ReadyToMate{ get; protected set; }
        protected int MatingCounter;
        protected int MaxMatingCounter; 
        
        protected int DamageForce;
        
        protected int HungerPoints;
        protected int MaxHungerPoints;
        protected double CalculateDistance(Entity target)
        {
            return Math.Sqrt(Math.Pow(target.Tile.X - Tile.X, 2) + Math.Pow(target.Tile.Y - Tile.Y, 2));
        }

        protected override void Die()
        {
            Map.Animals.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected void StartEat(Entity target)
        {
            target.DamageIt(DamageForce);

            if (HitPoints < MaxHitPoints - 5)
            {
                HitPoints += 5;
            }
            else
            {
                HitPoints = MaxHitPoints;
            }

            HungerPoints = MaxHungerPoints;
            
            if (target.Toxicity)
            {
                DamageIt(target.ToxicityValue);
                Toxicity = true;
                ToxicityValue = target.ToxicityValue;
                ToxicityCounter = target.ToxicityCounter;
            }
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;

            HitPoints = MaxHitPoints;
            
            Age = 0;

            Sex = Randomizer.GetRandomBool();
            ReadyToMate = false;
            MatingCounter = Randomizer.GetRandomInt(0, MaxMatingCounter - 5);

            HungerPoints = Randomizer.GetRandomInt(5, MaxHungerPoints);

            Toxicity = false;
            ToxicityValue = 0;
            ToxicityCounter = 0;
        }
    }
}