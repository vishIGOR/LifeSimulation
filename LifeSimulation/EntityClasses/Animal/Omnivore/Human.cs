using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public class Human : Omnivore
    {
        public int[] Inventory { get; protected set; }
        public int InventorySize = 10;
        public int InventoryFullness;
        public List<Animal> DomesticAnimals { get; protected set; }
        public List<Animal> UndomesticAnimals { get; protected set; }

        public Human(Tile tile, Map map)
        {
            MaxHitPoints = 35;
            MaxHungerPoints = 20;
            HungerBorder = 10;
            DamageForce = 40;
            MaxMatingCounter = 20;
            MaxAge = 45;
            Color = Brushes.Bisque;

            SetStandartValues(tile, map);
            Mover = new Mover(this, 2, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 2;

            Inventory = new int[4];
            for (int i = 0; i < 4; ++i)
            {
                Inventory[i] = 0;
            }

            InventoryFullness = 0;

            DomesticAnimals = new List<Animal>();
            UndomesticAnimals = new List<Animal>();
        }

        protected override void StartMate()
        {
            CreateChild();

            MatingTarget.MatingCounter = MatingTarget.MaxMatingCounter;
            MatingCounter = MaxMatingCounter;

            MatingTarget.ReadyToMate = false;
            ReadyToMate = false;
        }

        protected override void Die()
        {
            base.Die();

            foreach (var domestic in DomesticAnimals)
            {
                UnTame(domestic);
            }

            foreach (var undomestic in UndomesticAnimals)
            {
                DomesticAnimals.Remove(undomestic);
            }

            UndomesticAnimals.Clear();
        }

        protected override void CreateChild()
        {
            Human child = new Human(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
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
            if (MatingCounter <= 0 && ReadyToMate == false)
            {
                ReadyToMate = true;
            }

            if (HungerPoints < HungerBorder)
            {
                EatFromInventory();
            }

            if (HungerPoints < HungerBorder)
            {
                ReadyToMate = false;

                LookForFood();
                return;
            }

            if (ReadyToMate)
            {
                LookForMating();
            }

            if (InventoryFullness > InventorySize - 2)
            {
                LookForFood();
            }
            else
            {
                Tile = Mover.Walk(Tile);
            }
        }

        private void EatFromInventory()
        {
            for (int i = 0; i < Inventory.Length; ++i)
            {
                if (Inventory[i] > 0)
                {
                    --Inventory[i];
                    if (HitPoints < MaxHitPoints - 5)
                    {
                        HitPoints += 5;
                    }
                    else
                    {
                        HitPoints = MaxHitPoints;
                    }

                    HungerPoints = MaxHungerPoints;
                    --InventoryFullness;
                }
            }
        }

        public void UnTame(Animal animal)
        {
            UndomesticAnimals.Add(animal);
            IDomesticable DomesticAnimal = (IDomesticable) animal;
            DomesticAnimal.BeUntamed();
        }

        protected override void StartEat(Entity target)
        {
            if (target is IDomesticable)
            {
                IDomesticable potentialDomestic = (IDomesticable) target;
                if (Randomizer.GetRandomBool())
                {
                    if (potentialDomestic.TryToBeTamed(this))
                    {
                        DomesticAnimals.Add((Animal) target);
                        return;
                    }
                }
            }

            target.DamageIt(DamageForce);

            if (target.Toxicity)
            {
                return;
            }

            if (target is Animal || target is DeadBody)
            {
                ++Inventory[(int) FoodType.Meat];
                ++InventoryFullness;
                return;
            }

            if (target is Plant)
            {
                ++Inventory[(int) FoodType.Plant];
                ++InventoryFullness;
                return;
            }

            if (target is Fetus)
            {
                ++Inventory[(int) FoodType.Fetus];
                ++InventoryFullness;
            }
        }

        public void FeedAnimal(FoodType foodType)
        {
            --Inventory[(int) foodType];
            --InventoryFullness;
        }

        public void GetWool()
        {
            MaxHitPoints += 10;
        }

        public void GetHoney()
        {
            ++Inventory[(int) FoodType.Honey];
            ++InventoryFullness;
        }
    }
}