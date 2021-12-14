using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class AppleTreeFetus : Fetus
    {
        public AppleTreeFetus(Tile tile, Map map)
        {
            MaxAge = 36;
            MaxHitPoints = 10;
            Color = Brushes.Red;

            Toxicity = false;
            ToxicityValue = 0;

            ReadyToFall = 12;
            ReadyToSeed = 18;

            FallingRadius = 2;
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
                Plant newPlant = new AppleTree(Tile, Map,PlantStage.Seed);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
                
                Die();
            }
        }
    }
}