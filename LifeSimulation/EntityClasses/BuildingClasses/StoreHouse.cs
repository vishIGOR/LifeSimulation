using System.Collections.Generic;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public class StoreHouse:Building
    {
        public Dictionary<int, int> FoodInventory { get; protected set; }
        public int FoodInventorySize{ get; protected set; } = 60;
        public int FoodInventoryFullness{ get; protected set; } = 0;

        public Dictionary<int, int> ResourcesInventory { get; protected set; }
        public int ResourcesInventorySize{ get; protected set; } = 300;
        public int ResourcesInventoryFullness{ get; protected set; } = 0;
        
        public StoreHouse(Tile tile, Map map)
        {
            MaxHitPoints = 140;
            SetStandartValues(tile, map);
            ResourceCost = (new Wood(), 50);
            
            
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
        }

        // я бы и сделал дженерики, но в моей ситуации аналогичный им механизм и так есть
        // вообще я мог бы сделать еще одно статик поле ID, и я мог бы здесь использовать
        // дженерики, но не хочу / снизу пример как можно было бы если было бы статик поле ай ди
        // public int GetFullnessOfResource<T>() where T : ResourceType
        // {
        //     return ResourcesInventory[T.ID];
        // }
        public int GetFullnessOfResource( ResourceType resourceType)
        {
            return ResourcesInventory[resourceType.ID];
        }
        
        public int GetResource(int amount, ResourceType resourceType) 
        {
            ResourcesInventoryFullness -= amount;
            ResourcesInventory[resourceType.ID]-=amount;
            return amount;
        }

        public void PutResource(int amount, ResourceType resourceType)
        {
            ResourcesInventoryFullness += amount;
            ResourcesInventory[resourceType.ID]+=amount;
        }
        
        public int GetFullnessOfFood(int foodType)
        {
            return FoodInventory[foodType];
        }

        public int GetFood(int amount, int foodType)
        {
            FoodInventoryFullness -= amount;
            FoodInventory[foodType] -= amount;
            return amount;
        }
        
        public void PutFood(int amount, int foodType)
        {
            FoodInventoryFullness += amount;
            FoodInventory[foodType] += amount;
        }
    }
}