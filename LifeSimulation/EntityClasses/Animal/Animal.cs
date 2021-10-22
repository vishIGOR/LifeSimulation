using System;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
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
        protected Animal MatingTarget;
        
        protected int DamageForce;
        
        protected int HungerPoints;
        protected int MaxHungerPoints;
        protected int HungerBorder;
        protected abstract void LookForFood();
        protected double CalculateDistance(Entity target)
        {
            return Math.Sqrt(Math.Pow(target.Tile.X - Tile.X, 2) + Math.Pow(target.Tile.Y - Tile.Y, 2));
        }

        protected override void Die()
        {
            DeadBody deadBody = new DeadBody(Tile, Map);
            Map.DeadBodies.Add(deadBody);
            Map.NewEntities.Add(deadBody);

            if(MatingTarget!= null)
                MatingTarget.MatingTarget = null;
            ReadyToMate = false;
            MatingTarget = null;
            
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

        public override void ChooseAction()
        {
            ++Age;
            if (Age > MaxAge)
            {
                Die();
                return;
            }

            if (HungerPoints == 0)
            {
                --HitPoints;
            }
            else
            {
                --HungerPoints;
            }

            
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            --MatingCounter;
            if (MatingCounter <= 0 && ReadyToMate==false)
            {
                ReadyToMate = true;
            }
            
            if (HungerPoints < HungerBorder)
            {
                if(MatingTarget!= null)
                    MatingTarget.MatingTarget = null;
                ReadyToMate = false;
                MatingTarget = null;
                
                LookForFood();
                return;
            }

            if (ReadyToMate)
            {
                LookForMating();
            }
            
            Tile = Mover.Walk(Tile);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Age = 0;

            HitPoints = MaxHitPoints;

            HungerPoints = Randomizer.GetRandomInt(5,MaxHungerPoints);
            
            MatingCounter = Randomizer.GetRandomInt(5,MaxMatingCounter);
            ReadyToMate = false;
            MatingTarget = null;

            Sex = Randomizer.GetRandomBool();
            
            Eatable = true;
        }

        protected abstract void CreateChild();
        protected void StartMate()
        {
            CreateChild();
            
            MatingTarget.MatingCounter = MatingTarget.MaxMatingCounter;
            MatingCounter = MaxMatingCounter;
            
            MatingTarget.ReadyToMate = false;
            ReadyToMate = false;
            
            MatingTarget.MatingTarget = null;
            MatingTarget = null;
        }
        protected void LookForMating()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 15;
            Animal target = null;
            Type myType = GetType();

            if (MatingTarget == null)
            {
                foreach (var possibleTarget in Map.Animals)
                {
                    currentDistance = CalculateDistance(possibleTarget);
                    if (minDistance > currentDistance && currentDistance < maxDistance)
                    {
                        if (possibleTarget.GetType() == myType)
                        {
                            if (possibleTarget.Sex != Sex)
                            {
                                if (possibleTarget.ReadyToMate)
                                {
                                    minDistance = currentDistance;
                                    target = possibleTarget;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                target = MatingTarget;
            }
            
            
            if (target == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                target.MatingTarget = this;
                MatingTarget = target;
                if (target.Tile == Tile)
                {
                    StartMate();
                }
                else
                {
                    Tile = Mover.MoveTo(Tile, target.Tile);
                }
            }
        }
    }
}