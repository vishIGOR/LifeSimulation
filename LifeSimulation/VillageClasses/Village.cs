using System;
using System.Collections.Generic;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.BuildingClasses;

namespace LifeSimulation.VillageClasses
{
    public class Village
    {
        private List<Human> Members = new List<Human>();
        private List<Human> Newbies = new List<Human>();
        private List<Building> Buildings = new List<Building>();
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
            Members.Add(human);
        }

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }
    }
}