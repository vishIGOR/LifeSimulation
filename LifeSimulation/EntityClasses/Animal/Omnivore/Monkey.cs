using System.Drawing;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.Omnivore
{
    public class Monkey:Omnivore
    {
        public Monkey(Tile tile, Map map)
        {
            MaxHitPoints = 30;
            MaxHungerPoints = 20;
            HungerBorder = 6;
            DamageForce = 30;
            MaxMatingCounter = 15;
            MaxAge = 45;
            Color = Brushes.Chocolate;
            
            SetStandartValues(tile,map);
            Mover = new Mover(2, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 2;
        }

        protected override void CreateChild()
        {
            Monkey child = new Monkey(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
        
        public override void ChooseAction()
        {
            ++Age;
            if (Age > MaxAge)
            {
                Die();
                return;
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

            if (Age % 5 == 0)
            {
                Mover.CurrentMovingWay = Randomizer.GetRandomInt(1, 3);
                Mover.CurrentWalkingWay = Randomizer.GetRandomInt(1, 3);
            }

            --MatingCounter;
            if (MatingCounter <= 0 && ReadyToMate==false)
            {
                ReadyToMate = true;
            }
            
            if (HungerPoints < HungerBorder)
            {
                if(MatingTarget!= null)
                    MatingTarget.MatingTarget = null;
                ReadyToMate = false;
                MatingTarget = null;
                
                LookForFood();
                return;
            }

            if (ReadyToMate)
            {
                LookForMating();
            }
            
            Tile = Mover.Walk(Tile);
        }

    }
}