using System;
using System.Collections.Generic;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.BuildingClasses;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.VillageClasses
{
    public class Village
    {
        public List<Human> Members{ get; private set; } = new List<Human>();
        public List<Human> Builders = new List<Human>();
        public List<Human> Newbies{ get; private set; } = new List<Human>();
        public List<Building> Buildings{ get; private set; } = new List<Building>();
        private bool ReadyForWar;
        private int AmountOfSaltpeter;
        public String Name { get; private set; }
        private List<Village> Enemies = new List<Village>();
        private VillagesObserver VillagesObserver;
        public Human VillageHead;

        public Village(String name, VillagesObserver observer)
        {
            Name = name;
            AmountOfSaltpeter = 0;
            ReadyForWar = false;
            VillagesObserver = observer;
        }

        public void AddHuman(Human human)
        {
            Newbies.Add(human);
        }

        public void TransformNewbiesInMembers()
        {
            foreach (var newbie in Newbies)
            {
                Members.Add(newbie);
            }
            Newbies.Clear();
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
                        if (possibleStore.GetFullnessOfResource(preferredResourceType) >=60)
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