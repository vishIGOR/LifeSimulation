using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Wolf : Carnivore
    {

        public Wolf(Tile tile, Map map)
        {
            MaxHitPoints = 25;
            MaxHungerPoints = 10;
            MaxMatingCounter = 15;
            MaxAge = 40;
            Color = Brushes.Black;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 3;
            Mover.CurrentWalkingWay = 3;
        }

        public override void ChooseAction()
        {
            ++Age;
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

            if (Age > MaxAge)
            {
                Die();
                return;
            }

            if (HungerPoints < 5)
            {
                LookForFood();
                return;
            }
            
            Tile = Mover.Walk(Tile);
        }
        
    }
}