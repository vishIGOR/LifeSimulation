using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public class Bear:Omnivore,IDomesticable
    {
        private Human Master;
        private int AbilityCounter;
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
            Mover = new Mover(this,3, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 2;

        }
        
        protected override void CreateChild()
        {
            Bear child = new Bear(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
        
        public override void ChooseAction()
        {
            if (Map.Season != SeasonType.Winter)
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
            else
            {
                ++Age;
                if (Age > MaxAge)
                {
                    Die();
                    return;
                }
                
                if (HitPoints <= 0)
                {
                    Die();
                    return;
                }
                
                HitPoints += 5;
            }
            
            
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
                Master = master;
                AbilityCounter = 0;
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
                    Master.GetHoney();
                    AbilityCounter = 15;
                }
                else
                {
                    --AbilityCounter;
                }
            }
        }

        protected override void LookForFood()
        {
            if (Master != null)
            {
                for (int i = 0; i < Master.Inventory.Length; ++i)
                {
                    if (Master.Inventory[i] > 0)
                    {
                        Tile = Mover.MoveTo(Tile, Master.Tile);
                        if (Tile == Master.Tile)
                        {
                            EatMasterFood((FoodType)i);
                        }

                        return;
                    }
                }
                
            }
            
            
            base.LookForFood();
        }
    }
}