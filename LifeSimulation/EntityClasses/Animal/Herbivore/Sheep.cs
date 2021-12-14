using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep:Herbivore, IDomesticable
    {
        private Human Master;
        private int AbilityCounter;
        public Sheep(Tile tile, Map map)
        {
            MaxHitPoints = 10;
            MaxHungerPoints = 15;
            HungerBorder = 4;
            MaxMatingCounter = 20;
            MaxAge = 50;
            Color = Brushes.White;
            DamageForce = 40;
            
            SetStandartValues(tile, map);
            Mover = new Mover(this,3, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 1;
            
            
        }
        protected override void CreateChild()
        {
            Sheep child = new Sheep(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
        
        protected override void Die()
        {
            base.Die();

            if (Master != null)
            {
                Master.UnTame(this);
            }
            
        }
        
        public bool TryToBeTamed(Human master)
        {
            if (Master == null)
            {
                AbilityCounter = 0;
                Master = master;
                return true;
            }

            return false;
        }
        public void BeUntamed()
        {
            Master = null;
        }

        public void EatMasterFood(FoodType foodType)
        {
            Master.FeedAnimal(foodType);
            if (HitPoints < MaxHitPoints - 5)
            {
                HitPoints += 5;
            }
            else
            {
                HitPoints = MaxHitPoints;
            }

            HungerPoints = MaxHungerPoints;
        }
        

        public void DoSpecialAbility()
        {
            if (Master.Tile != Tile)
            {
                Tile = Mover.MoveTo(Tile, Master.Tile);
            }

            if (Master.Tile == Tile)
            {
                if (AbilityCounter <= 0)
                {
                    Master.GetWool();
                    AbilityCounter = 15;
                }
                else
                {
                    --AbilityCounter;
                }
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

            if (Map.Season == SeasonType.Winter)
            {
                if (HungerPoints == 0)
                {
                    --HitPoints;
                }
                else
                {
                    --HungerPoints;
                }
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

            if (Master != null)
            {
                DoSpecialAbility();
            }
            else
            {
                Tile = Mover.Walk(Tile);
            }
                
        }
        
        protected override void LookForFood()
        {
            if (Master != null)
            {
                if (Master.FoodInventory[(int) FoodType.Plant] > 0)
                {
                    Tile = Mover.MoveTo(Tile, Master.Tile);
                    if (Tile == Master.Tile)
                    {
                        EatMasterFood(FoodType.Plant);
                    }

                    return;
                }
            }
            
            
            base.LookForFood();
        }
    }
}