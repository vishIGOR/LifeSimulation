using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
using LifeSimulation.VillageClasses;

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

        private Profession MyProfession;

        public Village Village { get; protected set; }
        public VillagesObserver VillagesObserver { get; protected set; }

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

            MyProfession = new Unemployed(this);

            VillagesObserver = Map.VillagesObserver;
        }

        public void ChangeVillage(Village newVillage)
        {
            Village = newVillage;
        }

        public void ChangeProfession(int newProfessionID)
        {
            if (Unemployed.ID == newProfessionID)
            {
                MyProfession = new Unemployed(this);
                return;
            }

            if (VillageHead.ID == newProfessionID)
            {
                MyProfession = new VillageHead(this);
                if (Village == null)
                {
                    Village = VillagesObserver.CreateVillage();
                }

                return;
            }
        }

        private void LookForHousePlace()
        {
            if (ResourcesInventory[1] <= 20)
            {
                LookForResources(1);
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

            List<Tile> possiblePlacesForHouse = new List<Tile>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + nearestBuilding.Tile.X > 0 && i + nearestBuilding.Tile.X < Map.Width &&
                        j + nearestBuilding.Tile.Y > 0 && j + nearestBuilding.Tile.Y < Map.Height)
                    {
                        if (!(Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y].SpecialObject is
                            Building))
                        {
                            possiblePlacesForHouse.Add(
                                Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y]);
                            // Tile = Mover.MoveTo(Tile,
                            //     Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y]);
                            // if (Tile == Map.Tiles[i + nearestBuilding.Tile.X, j + nearestBuilding.Tile.Y])
                            // {
                            //     
                            //     
                            // }
                        }
                    }
                }
            }

            var randomHousePlace = possiblePlacesForHouse[Randomizer.GetRandomInt(0, possiblePlacesForHouse.Count - 1)];
            Tile = Mover.MoveTo(Tile, randomHousePlace);
            if (Tile == randomHousePlace)
            {
                BuildHouse();
            }
        }

        private void BuildHouse()
        {
            House = new LivingHouse(Tile, Map);
            House.Assign(this);
            Tile.SpecialObject = House;
            if (MatingTarget != null)
            {
                House.Assign(MatingTarget as Human);
                (MatingTarget as Human).House = House;
            }
        }

        public void LookForResources(int soughtResourceTypeID = -666)
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
            // Tile.DeleteMeIDontWannaLiveAnymore(this);

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
                House.UnAssign(this);
            }

            if (MatingTarget != null)
            {
                MatingTarget.MatingTarget = null;
            }

            base.Die();
        }

        protected override void CreateChild()
        {
            //Debug.WriteLine("Birthed");

            Human child = new Human(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);

            if (Village != null)
            {
                child.ChangeVillage(Village);
                Village.AddHuman(child);
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

            MyProfession.DoProfessionalAction();

            // --MatingCounter;
            // if (MatingCounter <= 0 && ReadyToMate == false)
            // {
            //     ReadyToMate = true;
            // }
            //
            // if (HungerPoints < HungerBorder)
            // {
            //     EatFoodFromInventory();
            // }
            //
            // if (HungerPoints < HungerBorder)
            // {
            //     ReadyToMate = false;
            //
            //     LookForFood();
            //     return;
            // }
            //
            // if (ReadyToMate)
            // {
            //     LookForMating();
            //     return;
            // }
            //
            // if (FoodInventoryFullness < FoodInventorySize - 2)
            // {
            //     LookForFood();
            // }
            // else
            // {
            //     Tile = Mover.Walk(Tile);
            // }
        }

        protected override void LookForMating()
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
                                    if (possibleTarget.MatingTarget == null)
                                    {
                                        minDistance = currentDistance;
                                        target = possibleTarget;
                                    }
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

                if (House == null)
                {
                    LookForHousePlace();
                }
                else
                {
                    if ((MatingTarget as Human).House != House)
                    {
                        (MatingTarget as Human).House = House;
                        House.Assign(MatingTarget as Human);
                    }

                    if (MatingTarget.ReadyToMate)
                    {
                        Tile = Mover.MoveTo(Tile, House.Tile);
                        if (Tile == House.Tile && Tile == MatingTarget.Tile)
                        {
                            StartMate();
                        }
                    }
                }
                // if (MatingTarget.ReadyToMate)
                // {
                //     Tile = Mover.MoveTo(Tile, MatingTarget.Tile);
                //     if (Tile == MatingTarget.Tile)
                //     {
                //         StartMate();
                //     }
                // }
                // else
                // {
                //     Tile = Mover.Walk(Tile);
                // }
            }
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

        public String GetProfessionName()
        {
            if (MyProfession is Unemployed)
            {
                return "Бродяга";
            }

            if (MyProfession is VillageHead)
            {
                return "Глава деревни";
            }

            if (MyProfession is Builder)
            {
                return "строитель";
            }

            // if (MyProfession is Hunter)
            // {
            //     return "охотник";
            // }

            return "name error";
        }

        private bool GetFoodFromStore()
        {
            StoreHouse possibleStore = Village.FindStoreWithFood();
            if (possibleStore == null)
            {
                return false;
            }

            Tile = Mover.MoveTo(Tile, possibleStore.Tile);
            if (Tile == possibleStore.Tile)
            {
                int resInStore;
                int resThatWeNeed = FoodInventorySize - FoodInventoryFullness;
                for (int i = 0; i < 4; ++i)
                {
                    resInStore = possibleStore.GetFullnessOfFood(i);
                    if (resInStore >= resThatWeNeed)
                    {
                        int amountOfFood = possibleStore.GetFood(resThatWeNeed, i);
                        FoodInventory[i] += amountOfFood;
                        FoodInventoryFullness += amountOfFood;
                        return true;
                    }
                    else
                    {
                        int amountOfFood = possibleStore.GetFood(resInStore, i);
                        FoodInventory[i] += amountOfFood;
                        FoodInventoryFullness += amountOfFood;
                        resThatWeNeed -= amountOfFood;
                    }
                }
            }

            return true;
        }

        private bool GetResourcesFromStore(ResourceType preferredResourceType)
        {
            StoreHouse possibleStore = Village.FindStoreWithResources(preferredResourceType);
            if (possibleStore == null)
            {
                return false;
            }

            Tile = Mover.MoveTo(Tile, possibleStore.Tile);
            if (Tile == possibleStore.Tile)
            {
                int resInStore;
                int resThatWeNeed = ResourcesInventorySize - ResourcesInventoryFullness;
                int amountOfRes;

                resInStore = possibleStore.GetFullnessOfResource(preferredResourceType);
                if (resThatWeNeed <= resInStore)
                {
                    amountOfRes = possibleStore.GetResource(resThatWeNeed, preferredResourceType);
                }
                else
                {
                    amountOfRes = possibleStore.GetResource(resInStore, preferredResourceType);
                }
                
                ResourcesInventoryFullness += amountOfRes;
                ResourcesInventory[preferredResourceType.ID] += amountOfRes;
            }

            return true;
        }

        private abstract class Profession
        {
            public Human Human;
            public static int ID;
            public abstract void DoProfessionalAction();
        }

        private class Unemployed : Profession
        {
            public static int ID = 0;

            public Unemployed(Human human)
            {
                ID = 0;
                Human = human;
            }

            public override void DoProfessionalAction()
            {
                --Human.MatingCounter;
                if (Human.MatingCounter <= 0 && Human.ReadyToMate == false)
                {
                    Human.ReadyToMate = true;
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.EatFoodFromInventory();
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.ReadyToMate = false;

                    Human.LookForFood();
                    return;
                }

                if (Human.ReadyToMate)
                {
                    Human.LookForMating();
                    return;
                }

                if (Human.FoodInventoryFullness < Human.FoodInventorySize - 2)
                {
                    Human.LookForFood();
                }
                else
                {
                    Human.Tile = Human.Mover.Walk(Human.Tile);
                }
            }
        }

        private class VillageHead : Profession
        {
            public static int ID = 1;

            public VillageHead(Human human)
            {
                Human = human;
            }

            public override void DoProfessionalAction()
            {
                --Human.MatingCounter;
                if (Human.MatingCounter <= 0 && Human.ReadyToMate == false)
                {
                    Human.ReadyToMate = true;
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.EatFoodFromInventory();
                    return;
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.ReadyToMate = false;

                    Human.LookForFood();
                    return;
                }

                if (Human.ReadyToMate)
                {
                    Human.LookForMating();
                    return;
                }

                if (Human.FoodInventoryFullness < Human.FoodInventorySize - 2)
                {
                    Human.LookForFood();
                }
                else
                {
                    Human.Tile = Human.Mover.Walk(Human.Tile);
                }
            }
        }

        // private class Hunter:Profession
        // {
        //     public static int ID = 2;
        //     public Hunter(Human human)
        //     {
        //         Human = human;
        //     }
        //
        //     public override void DoProfessionalAction()
        //     {
        //         --Human.MatingCounter;
        //         if (Human.MatingCounter <= 0 && Human.ReadyToMate == false)
        //         {
        //             Human.ReadyToMate = true;
        //         }
        //
        //         if (Human.HungerPoints < Human.HungerBorder)
        //         {
        //             Human.EatFoodFromInventory();
        //             return;
        //         }
        //
        //         if (Human.HungerPoints < Human.HungerBorder)
        //         {
        //             Human.ReadyToMate = false;
        //
        //             if (!Human.GetFoodFromStore())
        //             {
        //                 Human.LookForFood();
        //             }
        //             return;
        //         }
        //
        //         if (Human.ReadyToMate)
        //         {
        //             Human.LookForMating();
        //             return;
        //         }
        //
        //         if (Human.FoodInventoryFullness < Human.FoodInventorySize - 2)
        //         {
        //             Human.LookForFood();
        //         }
        //         else
        //         {
        //             Human.Tile = Human.Mover.Walk(Human.Tile);
        //         }
        //     }
        // }
        private class Builder : Profession
        {
            public static int ID = 3;

            public Builder(Human human)
            {
                Human = human;
            }

            public override void DoProfessionalAction()
            {
                --Human.MatingCounter;
                if (Human.MatingCounter <= 0 && Human.ReadyToMate == false)
                {
                    Human.ReadyToMate = true;
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.EatFoodFromInventory();
                    return;
                }

                if (Human.HungerPoints < Human.HungerBorder)
                {
                    Human.ReadyToMate = false;
                    if (!Human.GetFoodFromStore())
                    {
                        Human.LookForFood();
                    }

                    return;
                }

                if (Human.ReadyToMate)
                {
                    if (Human.Village.FindFreeLivingHouse() != null)
                    {
                        Human.House = Human.Village.FindFreeLivingHouse();
                    }

                    Human.LookForMating();
                    return;
                }

               
            }
        }
    }
}