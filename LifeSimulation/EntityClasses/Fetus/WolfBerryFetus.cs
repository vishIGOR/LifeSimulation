using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class WolfBerryFetus:Fetus
    {
        public WolfBerryFetus(Tile tile, Map map)
        {
            MaxAge = 36;
            MaxHitPoints = 10;
            Color = Brushes.MidnightBlue;

            Toxicity = true;
            ToxicityValue = 100;
            ToxicityCounter = 5;
            
            ReadyToFall = 12;
            ReadyToSeed = 18;

            FallingRadius = 1;
            
            SetStandartValues(tile,map);
        }

        public override void ChooseAction()
        {
            ++Age;
            if (HitPoints <= 0 || Age>MaxAge)
            {
                Die();
                return;
            }

            if (Age == ReadyToFall)
            {
                FallAway();
            }

            if (Age >= ReadyToSeed)
            {
                CreateSeed();
            }
        }
        
        protected override void CreateSeed()
        {
            if (Tile.SpecialObject == null)
            {
                Plant newPlant = new WolfBerry(Tile, Map,PlantStage.Seed);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
                
                Die();
            }
        }
    }
}