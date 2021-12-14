using System.Drawing;
using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Wolf : Carnivore, IDomesticable
    {
        private Human Master;
        public Wolf(Tile tile, Map map)
        {
            MaxHitPoints = 25;
            MaxHungerPoints = 20;
            HungerBorder = 6;
            DamageForce = 30;
            MaxMatingCounter = 15;
            MaxAge = 40;
            Color = Brushes.DimGray;
            
            SetStandartValues(tile,map);
            Mover = new Mover(this,2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }

        protected override void CreateChild()
        {
            Wolf child = new Wolf(Tile, Map);
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
                if (Tile.Entities.Count > 2)
                {
                    foreach (var entity in Tile.Entities)
                    {
                        if (entity is Animal && !(entity is Human))
                        {
                            StartEat(entity);
                        }
                    }
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
                if (Master.FoodInventory[(int) FoodType.Meat] > 0)
                {
                    Tile = Mover.MoveTo(Tile, Master.Tile);
                    if (Tile == Master.Tile)
                    {
                        EatMasterFood(FoodType.Meat);
                    }

                    return;
                }
            }
            
            
            base.LookForFood();
        }
    }
}