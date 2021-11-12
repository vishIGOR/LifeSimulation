﻿using System.Drawing;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.DeadBodyClasses
{
    public class DeadBody:Entity
    {
        private int Age;
        private int MaxAge;
        public DeadBody(Tile tile, Map map)
        {
            MaxHitPoints = 20;
            HitPoints = 20;
            Age = 0;
            MaxAge = 10;
            Eatable = true;
            Color = Brushes.Red;
            
            SetStandartValues(tile,map);
            
            Tile.Entities.Add(this);
        }


        public override void ChooseAction()
        {
            ++Age;
            if (Age > MaxAge)
            {
                Die();
                return;
            }

            if (HitPoints <= 0)
            {
                Die();
                return;
            }
            
        }

        protected override void Die()
        {
            Map.DeadBodies.Remove(this);
            Map.DeadEntities.Add(this);
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if (newSeason == SeasonType.Winter)
            {
                MaxAge += 42;
            }
            if (newSeason == SeasonType.Summer)
            {
                MaxAge -= 42;
            }
        }
    }
}