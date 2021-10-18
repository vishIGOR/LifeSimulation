using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep : Herbivore
    {
        public Sheep(Tile tile, Map map)
        {
            MaxHitPoints = 10;
            MaxHungerPoints = 15;
            MaxMatingCounter = 20;
            MaxAge = 50;
            Color = Brushes.White;
            
            SetStandartValues(tile, map);
            Mover = new Mover(3, map);
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

            if (HungerPoints < 4)
            {
                LookForFood();
                return;
            }

            Tile = Mover.Walk(Tile);
        }
        
    }
}