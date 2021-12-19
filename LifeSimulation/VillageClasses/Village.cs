using System;
using System.Collections.Generic;
using System.Net.Http;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.BuildingClasses;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.VillageClasses
{
    public class Village
    {
        public List<Human> Members { get; private set; } = new List<Human>();
        public List<Human> Builders = new List<Human>();
        public List<Human> Hunters = new List<Human>();
        public List<Human> Collectors = new List<Human>();
        public List<Human> Newbies { get; private set; } = new List<Human>();
        public List<Building> Buildings { get; private set; } = new List<Building>();
        private bool ReadyForWar;
        private int AmountOfSaltpeter;
        public String Name { get; private set; }
        private List<Village> Enemies = new List<Village>();
        private VillagesObserver VillagesObserver;
        public Human VillageHead;
        private Randomizer Randomizer;

        public Village(String name, VillagesObserver observer)
        {
            Name = name;
            AmountOfSaltpeter = 0;
            ReadyForWar = false;
            VillagesObserver = observer;
            Randomizer = VillagesObserver.Randomizer;
        }

        public void AddHuman(Human human)
        {
            Newbies.Add(human);
        }

        public void DeleteHuman(Human human)
        {
            Members.Remove(human);
        }

        public void ChooseNewHead()
        {
            Members[Randomizer.GetRandomInt(0, Members.Count - 1)].ChangeProfession(1);
        }

        public void TransformNewbiesInMembers()
        {
            foreach (var newbie in Newbies)
            {
            
                newbie.ChangeVillage(this);
                Members.Add(newbie);
            }

            Newbies.Clear();
        }

        public int ReturnMostNeededProfession()
        {
            int currentID = 2;
            int minCount = 10000000;
            if (Builders.Count < minCount)
            {
                minCount = Builders.Count;
                currentID = 3;
            }

            if (Hunters.Count < minCount)
            {
                minCount = Hunters.Count;
                currentID = 2;
            }

            if (Collectors.Count < minCount)
            {
                minCount = Collectors.Count;
                currentID = 4;
            }
            return currentID;
        }

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public LivingHouse FindFreeLivingHouse()
        {
            foreach (var building in Buildings)
            {
                if (building is LivingHouse)
                {
                    if ((building as LivingHouse).Owners.Count == 0)
                    {
                        return building as LivingHouse;
                    }
                }
            }

            return null;
        }

        public StoreHouse FindStoreWithFood()
        {
            StoreHouse possibleStore = null;
            foreach (var building in Buildings)
            {
                if (building is StoreHouse)
                {
                    if ((building as StoreHouse).FoodInventoryFullness > 0)
                    {
                        possibleStore = building as StoreHouse;
                        if (possibleStore.FoodInventoryFullness > 7)
                        {
                            return possibleStore;
                        }
                    }
                }
            }

            return possibleStore;
        }

        public StoreHouse FindStoreWithResources(ResourceType preferredResourceType)
        {
            StoreHouse possibleStore = null;
            foreach (var building in Buildings)
            {
                if (building is StoreHouse)
                {
                    if ((building as StoreHouse).GetFullnessOfResource(preferredResourceType) > 0)
                    {
                        possibleStore = building as StoreHouse;
                        if (possibleStore.GetFullnessOfResource(preferredResourceType) >= 60)
                        {
                            return possibleStore;
                        }
                    }
                }
            }

            return possibleStore;
        }

        public StoreHouse FindStoreWithNoFood()
        {
            StoreHouse possibleStore = null;
            foreach (var building in Buildings)
            {
                if (building is StoreHouse)
                {
                    if ((building as StoreHouse).FoodInventoryFullness < (building as StoreHouse).FoodInventorySize)
                    {
                        possibleStore = building as StoreHouse;
                        if (possibleStore.FoodInventoryFullness <= 10)
                        {
                            return possibleStore;
                        }
                    }
                }
            }

            return possibleStore;
        }
    }
}