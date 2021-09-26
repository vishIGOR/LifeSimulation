using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Wolf : Animal, IFeedable
    {
        private int HungerPoints;
        public Wolf(Tile tile, Map map)
        {
            CurrentTile = tile;
            HitPoints = 20;
            MaxHitPoints = 20;
            HungerPoints = 10;
            Age = 0;
            MaxAge = 35;
            EntityColor = Brushes.Black;
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