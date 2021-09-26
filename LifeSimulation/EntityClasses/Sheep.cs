using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep : Animal,  IFeedable
    {
        
        private int HungerPoints;
        public Sheep(Tile tile, Map map)
        {
            HungerPoints = 10;
            MaxHitPoints = 10;
            CurrentTile = tile;
            HitPoints = 10;
            Age = 0;
            MaxAge = 50;
            EntityColor = Brushes.White;
            EntityRandomizer = new Randomizer();
            EntityMap = map;
        }

        public override void ChooseAction()
        {
            // ++Age;
            // if (HungerPoints == 0)
            // {
            //     --HitPoints;
            // }
            // else
            // {
            //     --HungerPoints;
            // }
            //
            // if (HitPoints <= 0)
            // {
            //     Die();
            //     return;
            // }
            //
            // if (Age > MaxAge)
            // {
            //     
            //     if (EntityRandomizer.RandomInt(1,10) > 4)
            //     {
            //         Die();
            //         return;
            //     }
            // }
            //
            // if (HungerPoints < 3)
            // {
            //     LookForFood();
            //     return;
            // }
            //
            // if (EntityRandomizer.RandomInt(1,10) > 3)
            // {
            //     Walk();
            //     return;
            // }
        }

        public void LookForFood()
        {
            
        }

        public void StartEat()
        {
            
        }
    }
}