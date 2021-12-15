using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.EntityClasses.BuildingClasses;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;
using LifeSimulation.ToolClasses.MiningTool;
using LifeSimulation.ToolClasses.Weapon;

namespace LifeSimulation.EntityClasses
{
    public class Human : Animal
    {
        public Dictionary<int, int> FoodInventory { get; protected set; }
        public int FoodInventorySize = 10;
        public int FoodInventoryFullness;

        public Dictionary<int, int> ResourcesInventory { get; protected set; }
        public int ResourcesInventorySize = 60;
        public int ResourcesInventoryFullness;

        public MiningTool MiningTool { get; protected set; }
        public Weapon Weapon { get; protected set; }

        public LivingHouse House;
        public List<Animal> DomesticAnimals { get; protected set; }
        public List<Animal> UndomesticAnimals { get; protected set; }
        public IProfession Profession { get; protected set; }

        public Human(Tile tile, Map map)
        {
            MaxHitPoints = 55;
            MaxHungerPoints = 40;
            HungerBorder = 5;
            DamageForce = 40;
            MaxMatingCounter = 25;
            MaxAge = 55;
            Color = Brushes.Bisque;

            SetStandartValues(tile, map);
            Mover = new Mover(this, 2, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 2;

            FoodInventory = new Dictionary<int, int>();
            for (int i = 0; i < 4; ++i)
            {
                FoodInventory.Add(i, 0);
            }

            ResourcesInventory = new Dictionary<int, int>();
            for (int i = 0; i < 2; ++i)
            {
                ResourcesInventory.Add(i, 0);
            }

            FoodInventoryFullness = 0;
            ResourcesInventoryFullness = 0;

            DomesticAnimals = new List<Animal>();
            UndomesticAnimals = new List<Animal>();
        }

        private void LookForHousePlace()
        {
            if (ResourcesInventory[1] <= 20)
            {
                LookingForResources(1);
                return;
            }

            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 30;
            Entity nearestBuilding = null;
            
            foreach (var building in Map.Buildings)
            {
                currentDistance = CalculateDistance(building);
                if (minDistance > currentDistance && currentDistance < maxDistance)
                {
                    minDistance = currentDistance;
                    nearestBuilding = building;
                }
            }

            if (nearestBuilding == null)
            {
                if (Tile.SpecialObject != null)
                {
                    BuildHouse();
                }
                else
                {
                    Tile = Mover.Walk(Tile);
                }

                return;
            }

            
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!(Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y].SpecialObject is Building))
                    {
                        Tile = Mover.MoveTo(Tile, Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y]);
                        if (Tile == Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y])
                        {
                            BuildHouse();
                        }

                        return;
                    }
                }
            }
        }

        private void BuildHouse()
        {
            House = new LivingHouse(Tile, Map);
            House.Assign(this);
            Tile.SpecialObject = House;
        }
        
        private void LookingForResources(int soughtResourceTypeID = -666)
        {
            if (Tile.SpecialObject is IMineable)
            {
                IMineable deposit = Tile.SpecialObject as IMineable;
                if (soughtResourceTypeID == -666 || deposit.ReturnResourceType().ID == soughtResourceTypeID)
                {
                    ExtractResource();
                    return;
                }
            }

            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 30;
            Entity nearestDeposit = null;


            if (soughtResourceTypeID == -666)
            {
                //переопределение, например:
                // int soughtResourceType = Randomizer.GetRandomInt(0, 1);
                soughtResourceTypeID = 1;
            }

            foreach (var entity in Map.Entities)
            {
                if (entity is IMineable)
                {
                    IMineable possibleDeposit = entity as IMineable;
                    if (possibleDeposit.ReturnResourceType().ID == soughtResourceTypeID)
                    {
                        currentDistance = CalculateDistance(entity);
                        if (minDistance > currentDistance && currentDistance < maxDistance)
                        {
                            minDistance = currentDistance;
                            nearestDeposit = entity;
                        }
                    }
                }

                if (nearestDeposit == null)
                {
                    Tile = Mover.Walk(Tile);
                }
                else
                {
                    Tile = Mover.MoveTo(Tile, nearestDeposit.Tile);
                }
            }
        }

        private void ExtractResource()
        {
            IMineable currentDeposit = Tile.SpecialObject as IMineable;
            (ResourceType, int) resource = currentDeposit.BeMined();

            if (MiningTool != null)
            {
                if (resource.Item1.ID == MiningTool.ResourceType.ID)
                {
                    ResourcesInventory[resource.Item1.ID] += resource.Item2 * 2;
                    ResourcesInventoryFullness += resource.Item2 * 2;
                    if (ResourcesInventoryFullness > ResourcesInventorySize)
                    {
                        ResourcesInventoryFullness = ResourcesInventorySize;
                    }

                    return;
                }
            }

            //else
            ResourcesInventory[resource.Item1.ID] += resource.Item2;
            ResourcesInventoryFullness += resource.Item2;
            if (ResourcesInventoryFullness > ResourcesInventorySize)
            {
                ResourcesInventoryFullness = ResourcesInventorySize;
            }
        }

        public void SetProfession(IProfession newProfession)
        {
            Profession = newProfession;
        }

        protected override void StartMate()
        {
            CreateChild();
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

            if (House != null)
            {
                House.UnAssign();
            }
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
                EatFoodFromInventory();
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
                return;
            }

            LookForHousePlace();
            return;
            // LookingForResources();
            // return;
            // if (FoodInventoryFullness < FoodInventorySize - 2)
            // {
            //     LookForFood();
            // }
            // else
            // {
            //     Tile = Mover.Walk(Tile);
            // }
        }

        private void EatFoodFromInventory()
        {
            foreach (var key in FoodInventory.Keys)
            {
                if (FoodInventory[key] > 0)
                {
                    --FoodInventory[key];
                    if (HitPoints < MaxHitPoints - 5)
                    {
                        HitPoints += 5;
                    }
                    else
                    {
                        HitPoints = MaxHitPoints;
                    }

                    HungerPoints = MaxHungerPoints;
                    --FoodInventoryFullness;
                    return;
                }
            }
        }

        public void UnTame(Animal animal)
        {
            UndomesticAnimals.Add(animal);
            IDomesticable DomesticAnimal = (IDomesticable) animal;
            DomesticAnimal.BeUntamed();
        }


        protected void PickUpFood(Entity target)
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
                ++FoodInventory[(int) FoodType.Meat];
                ++FoodInventoryFullness;
                return;
            }

            if (target is Plant)
            {
                ++FoodInventory[(int) FoodType.Plant];
                ++FoodInventoryFullness;
                return;
            }

            if (target is Fetus)
            {
                ++FoodInventory[(int) FoodType.Fetus];
                ++FoodInventoryFullness;
            }
        }

        public void FeedAnimal(FoodType foodType)
        {
            --FoodInventory[(int) foodType];
            --FoodInventoryFullness;
        }

        public void GetWool()
        {
            MaxHitPoints += 10;
        }

        public void GetHoney()
        {
            ++FoodInventory[(int) FoodType.Honey];
            ++FoodInventoryFullness;
        }

        protected override void LookForFood()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 30;
            Entity nearestFood = null;
            Type myType = GetType();

            foreach (var food in Map.Entities)
            {
                if (food.Eatable)
                {
                    if (food.GetType() != myType)
                    {
                        currentDistance = CalculateDistance(food);
                        if (minDistance > currentDistance && currentDistance < maxDistance)
                        {
                            minDistance = currentDistance;
                            nearestFood = food;
                        }
                    }
                }
            }

            if (nearestFood == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                if (nearestFood.Tile == Tile)
                {
                    PickUpFood(nearestFood);
                }
                else
                {
                    Tile = Mover.MoveTo(Tile, nearestFood.Tile);
                }
            }
        }
    }
}