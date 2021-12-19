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
        public int ResourcesInventorySize = 80;
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
            MaxAge = 75;
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

            if (Builder.ID == newProfessionID)
            {
                MyProfession = new Builder(this);
                return;
            }
            
            if (Hunter.ID == newProfessionID)
            {
                MyProfession = new Hunter(this);
                return;
            }
            
            if (Collector.ID == newProfessionID)
            {
                MyProfession = new Collector(this);
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
            House.JoinTheVillage();
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

            if (Village != null)
            {
                Village.DeleteHuman(this);
            }

            if (MyProfession is VillageHead)
            {
                if (Village.Members.Count > 0)
                    Village.ChooseNewHead();
            }

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

            if (MyProfession is Hunter)
            {
                return "охотник";
            }

            if (MyProfession is Collector)
            {
                return "собиратель";
            }

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
                foreach (var newbie in Human.Village.Newbies)
                {
                    // int randomInt = Human.Randomizer.GetRandomInt(1, 100);
                    //
                    // if (randomInt <= 50)
                    // {
                    //     newbie.ChangeProfession(Hunter.ID);
                    //     Human.Village.Hunters.Add(newbie);
                    // }
                    // else
                    // {
                    //     
                    //     newbie.ChangeProfession(Builder.ID);
                    //     Human.Village.Builders.Add(newbie);
                    // }
                    int mostNeededProfession = Human.Village.ReturnMostNeededProfession();
                    if (newbie.Sex)
                    {
                        if (mostNeededProfession != Collector.ID)
                        {
                            newbie.ChangeProfession(mostNeededProfession);
                            if (mostNeededProfession == Builder.ID)
                            {
                                Human.Village.Builders.Add(newbie);
                            }

                            if (mostNeededProfession == Hunter.ID)
                            {
                                Human.Village.Hunters.Add(newbie);
                            }
                        }
                        else
                        {
                            int randomInt = Human.Randomizer.GetRandomInt(1, 100);

                            if (randomInt <= 60)
                            {
                                newbie.ChangeProfession(Hunter.ID);
                                Human.Village.Hunters.Add(newbie);
                            }
                            else
                            {
                        
                                newbie.ChangeProfession(Builder.ID);
                                Human.Village.Builders.Add(newbie);
                            }
                        }
                    }
                    else
                    {
                        if (mostNeededProfession != Hunter.ID)
                        {
                            newbie.ChangeProfession(mostNeededProfession);
                            if (mostNeededProfession == Builder.ID)
                            {
                                Human.Village.Builders.Add(newbie);
                            }

                            if (mostNeededProfession == Collector.ID)
                            {
                                Human.Village.Collectors.Add(newbie);
                            }
                        }
                        else
                        {
                            int randomInt = Human.Randomizer.GetRandomInt(1, 100);

                            if (randomInt <= 60)
                            {
                                newbie.ChangeProfession(Collector.ID);
                                Human.Village.Collectors.Add(newbie);
                            }
                            else
                            {
                        
                                newbie.ChangeProfession(Builder.ID);
                                Human.Village.Builders.Add(newbie);
                            }
                        }
                    }
                    
                    if (mostNeededProfession == Builder.ID)
                    {
                        Human.Village.Builders.Add(newbie);
                    }

                    if (mostNeededProfession == Hunter.ID)
                    {
                        Human.Village.Hunters.Add(newbie);
                    }
                    
                    if (mostNeededProfession == Collector.ID)
                    {
                        Human.Village.Collectors.Add(newbie);
                    }
                    
                }

                Human.Village.TransformNewbiesInMembers();

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
                    if (Human.House == null)
                    {
                        if (Human.Village.FindFreeLivingHouse() != null)
                        {
                            Human.House = Human.Village.FindFreeLivingHouse();
                        }
                    }


                    Human.LookForMating();
                    return;
                }
            }
        }

        private class Hunter : Profession
        {
            public static int ID = 2;
            public StoreHouse CurrentTarget;

            public Hunter(Human human)
            {
                Human = human;
            }
            
            private void LookForAnimals()
            {
                double minDistance = 10000000000;
                double currentDistance;
                double maxDistance = 30;
                Entity nearestFood = null;
                Type myType = Human.GetType();

                foreach (var food in Human.Map.Entities)
                {
                    if (food.Eatable && (food is Animal))
                    {
                        if (food.GetType() != myType)
                        {
                            currentDistance = Human.CalculateDistance(food);
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
                    Human.Tile = Human.Mover.Walk(Human.Tile);
                }
                else
                {
                    if (nearestFood.Tile == Human.Tile)
                    {
                        Human.PickUpFood(nearestFood);
                    }
                    else
                    {
                        Human.Tile = Human.Mover.MoveTo(Human.Tile, nearestFood.Tile);
                    }
                }
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
                        LookForAnimals();
                    }

                    return;
                }

                if (Human.ReadyToMate)
                {
                    if (Human.House == null)
                    {
                        if (Human.Village.FindFreeLivingHouse() != null)
                        {
                            Human.House = Human.Village.FindFreeLivingHouse();
                        }
                    }


                    Human.LookForMating();
                    return;
                }

                if (Human.FoodInventoryFullness < Human.FoodInventorySize - 3)
                {
                    LookForAnimals();
                }
                else
                {
                    if (CurrentTarget == null)
                    {
                        CurrentTarget = Human.Village.FindStoreWithNoFood();
                        if (CurrentTarget == null)
                        {
                            Human.Tile = Human.Mover.Walk(Human.Tile);
                            return;
                        }
                    }

                    Human.Tile = Human.Mover.MoveTo(Human.Tile, CurrentTarget.Tile);
                    if (Human.Tile == CurrentTarget.Tile)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness >
                                Human.FoodInventory[i])
                            {
                                CurrentTarget.PutFood(Human.FoodInventory[i], i);
                                Human.FoodInventoryFullness -= Human.FoodInventory[i];
                                Human.FoodInventory[i] = 0;
                            }
                            else
                            {
                                CurrentTarget.PutFood(
                                    CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness, i);
                                Human.FoodInventoryFullness -= CurrentTarget.FoodInventorySize -
                                                               CurrentTarget.FoodInventoryFullness;
                                Human.FoodInventory[i] -=
                                    CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness;
                                break;
                            }
                        }

                        CurrentTarget = null;
                    }
                }
            }
        }

        private class Builder : Profession
        {
            public static int ID = 3;

            private int currentState;

            public Builder(Human human)
            {
                Human = human;
                currentState = 0;
            }

            private void BuildStoreHouse()
            {
                StoreHouse newStore = new StoreHouse(Human.Tile, Human.Map);
                newStore.JoinTheVillage();

                Human.Tile.SpecialObject = newStore;

                var cost = StoreHouse.ResourceCost;
                Human.ResourcesInventory[cost.Item1.ID] -= cost.Item2;
                Human.ResourcesInventoryFullness -= cost.Item2;
                currentState = 0;
            }

            private void BuildLivingHouse()
            {
                var newLivingHouse = new LivingHouse(Human.Tile, Human.Map);
                Human.Tile.SpecialObject = newLivingHouse;
                newLivingHouse.JoinTheVillage();

                var cost = LivingHouse.ResourceCost;
                Human.ResourcesInventory[cost.Item1.ID] -= cost.Item2;
                Human.ResourcesInventoryFullness -= cost.Item2;
                currentState = 0;
            }

            private Tile LookForNewBuildingPlace()
            {
                List<Tile> possiblePlacesForHouse = new List<Tile>();

                foreach (var building in Human.Village.Buildings)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (i + building.Tile.X > 0 && i + building.Tile.X < Human.Map.Width &&
                                j + building.Tile.Y > 0 && j + building.Tile.Y < Human.Map.Height)
                            {
                                if (!(Human.Map.Tiles[i + building.Tile.X, j + building.Tile.Y].SpecialObject is
                                    Building))
                                {
                                    possiblePlacesForHouse.Add(
                                        Human.Map.Tiles[i + building.Tile.X, j + building.Tile.Y]);
                                }
                            }
                        }
                    }

                    if (possiblePlacesForHouse.Count > 0)
                    {
                        break;
                    }
                }

                Tile randomHousePlace = null;
                if (possiblePlacesForHouse.Count > 0)
                    randomHousePlace =
                        possiblePlacesForHouse[Human.Randomizer.GetRandomInt(0, possiblePlacesForHouse.Count - 1)];

                if (randomHousePlace != null)
                {
                    return randomHousePlace;
                }
                else
                {
                    return null;
                }
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
                    if (Human.House == null)
                    {
                        if (Human.Village.FindFreeLivingHouse() != null)
                        {
                            Human.House = Human.Village.FindFreeLivingHouse();
                        }
                    }


                    Human.LookForMating();
                    return;
                }

                if (currentState == 0)
                {
                    int randomIntForState = Human.Randomizer.GetRandomInt(1, 15);
                    // if ( randomIntForState <= 8)
                    // {
                    //     currentState = 1;
                    // }
                    // else
                    // {
                    //     if (randomIntForState <= 12)
                    //     {
                    //
                    //         currentState = 2;
                    //     }
                    //     else
                    //     {
                    //         currentState = 3;
                    //     }
                    // }
                    if (randomIntForState <= 10)
                    {
                        currentState = 1;
                    }
                    else
                    {
                        currentState = 2;
                    }
                }

                if (currentState == 1)
                {
                    var costOfLivingHouse = LivingHouse.ResourceCost;
                    if (Human.ResourcesInventory[costOfLivingHouse.Item1.ID] >= costOfLivingHouse.Item2)
                    {
                        var newTileForBuilding = LookForNewBuildingPlace();
                        if (newTileForBuilding != null)
                        {
                            Human.Tile = Human.Mover.MoveTo(Human.Tile, newTileForBuilding);
                        }

                        if (Human.Tile == newTileForBuilding)
                        {
                            BuildStoreHouse();
                        }
                    }
                    else
                    {
                        if (!Human.GetResourcesFromStore(new Wood()))
                            Human.LookForResources(1);
                    }

                    return;
                }

                if (currentState == 2)
                {
                    var costOfStore = StoreHouse.ResourceCost;
                    if (Human.ResourcesInventory[costOfStore.Item1.ID] >= costOfStore.Item2)
                    {
                        var newTileForBuilding = LookForNewBuildingPlace();
                        if (newTileForBuilding != null)
                        {
                            Human.Tile = Human.Mover.MoveTo(Human.Tile, newTileForBuilding);
                        }

                        if (Human.Tile == newTileForBuilding)
                        {
                            BuildLivingHouse();
                        }
                    }
                    else
                    {
                        if (!Human.GetResourcesFromStore(new Wood()))
                            Human.LookForResources(1);
                    }

                    return;
                }
            }
        }
        
        private class Collector : Profession
        {
            public static int ID = 4;
            public StoreHouse CurrentTarget;

            public Collector(Human human)
            {
                Human = human;
            }

            private void LookForPlantsAndFetuses()
            {
                double minDistance = 10000000000;
                double currentDistance;
                double maxDistance = 30;
                Entity nearestFood = null;
                Type myType = Human.GetType();

                foreach (var food in Human.Map.Entities)
                {
                    if (food.Eatable && (food is Fetus || food is Plant))
                    {
                        if (food.GetType() != myType)
                        {
                            currentDistance = Human.CalculateDistance(food);
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
                    Human.Tile = Human.Mover.Walk(Human.Tile);
                }
                else
                {
                    if (nearestFood.Tile == Human.Tile)
                    {
                        Human.PickUpFood(nearestFood);
                    }
                    else
                    {
                        Human.Tile = Human.Mover.MoveTo(Human.Tile, nearestFood.Tile);
                    }
                }
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
                        LookForPlantsAndFetuses();
                    }

                    return;
                }

                if (Human.ReadyToMate)
                {
                    if (Human.House == null)
                    {
                        if (Human.Village.FindFreeLivingHouse() != null)
                        {
                            Human.House = Human.Village.FindFreeLivingHouse();
                        }
                    }


                    Human.LookForMating();
                    return;
                }

                if (Human.FoodInventoryFullness < Human.FoodInventorySize - 3)
                {
                    LookForPlantsAndFetuses();
                }
                else
                {
                    if (CurrentTarget == null)
                    {
                        CurrentTarget = Human.Village.FindStoreWithNoFood();
                        if (CurrentTarget == null)
                        {
                            Human.Tile = Human.Mover.Walk(Human.Tile);
                            return;
                        }
                    }

                    Human.Tile = Human.Mover.MoveTo(Human.Tile, CurrentTarget.Tile);
                    if (Human.Tile == CurrentTarget.Tile)
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            if (CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness >
                                Human.FoodInventory[i])
                            {
                                CurrentTarget.PutFood(Human.FoodInventory[i], i);
                                Human.FoodInventoryFullness -= Human.FoodInventory[i];
                                Human.FoodInventory[i] = 0;
                            }
                            else
                            {
                                CurrentTarget.PutFood(
                                    CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness, i);
                                Human.FoodInventoryFullness -= CurrentTarget.FoodInventorySize -
                                                               CurrentTarget.FoodInventoryFullness;
                                Human.FoodInventory[i] -=
                                    CurrentTarget.FoodInventorySize - CurrentTarget.FoodInventoryFullness;
                                break;
                            }
                        }

                        CurrentTarget = null;
                    }
                }
            }
        }
    }
}