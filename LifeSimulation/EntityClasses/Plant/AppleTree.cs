using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class AppleTree : Plant, IProductingFetuses, IMineable
    {
        private int ReadyToFetus;
        private int FetusCounter;
        private ResourceType ResourceType = new Wood();
        private int MiningEfficiency = 10; 
        public AppleTree(Tile tile, Map map, PlantStage growthStage)
        {
            MaxHitPoints = 10;
            ReadyToSeed = 25;
            MaxAge = 120;
            Color = Brushes.DarkGreen;

            Toxicity = false;
            ToxicityValue = 0;
            SeedRadius = 2;
            
            SetStandartValues(tile,map);
            ChangePlantStage(growthStage);
            
            switch (growthStage)
            {
                case PlantStage.Sprout:
                    Age = 10;
                    break;
                case PlantStage.Grown:
                    Age = 25;
                    break;
                case PlantStage.Elder:
                    Age = 105;
                    break;
            }
            ReadyToFetus = 35;
            FetusCounter = Randomizer.GetRandomInt(0, ReadyToFetus-5);
        }
        public void CreateFetuses()
        {
            Fetus newFetus = new AppleTreeFetus(Tile, Map);
            Map.Fetuses.Add(newFetus);
            Map.NewEntities.Add(newFetus);
        }

        protected override void ChangePlantStage(PlantStage newStage)
        {
            PlantStage = newStage;
            switch (newStage)
            {
                case PlantStage.Sprout:
                    HitPoints = 20;
                    MaxHitPoints = 20;
                    Eatable = true;
                    break;
                case PlantStage.Grown:
                    HitPoints = 80;
                    MaxHitPoints = 80;
                    Eatable = true;
                    break;
                case PlantStage.Elder:
                    HitPoints = 20;
                    MaxHitPoints = 20;
                    Eatable = true;
                    break;
            }
        }

        public override void ChooseAction()
        {
            if (Map.Season == SeasonType.Summer)
            {
                ++Age;
            }
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            switch (PlantStage)
            {
                case PlantStage.Seed:
                    if (Age == 10)
                    {
                        ChangePlantStage(PlantStage.Sprout);
                    }

                    break;
                case PlantStage.Sprout:
                    if (Age == 25)
                    {
                        ChangePlantStage(PlantStage.Grown);
                    }

                    break;
                case PlantStage.Grown:
                    if (Map.Season != SeasonType.Winter)
                    {
                        ++SeedCounter;
                        ++FetusCounter;
                        if (Age == 105)
                        {
                            ChangePlantStage(PlantStage.Elder);
                        }

                        if (SeedCounter == ReadyToSeed)
                        {
                            FindPlaceToSeed();
                            SeedCounter = 0;
                        }

                        if (FetusCounter == ReadyToFetus)
                        {
                            CreateFetuses();
                            FetusCounter = 0;
                        }
                    }
                    break;
                case PlantStage.Elder:
                    if (Age == MaxAge)
                    {
                        Die();
                    }

                    break;
            }
        }

        protected override void CreateNewSeeds(List<Tile> possibleTiles, int numberOfPossibleTiles)
        {
            int randomInt;
            if (numberOfPossibleTiles >= 2)
            {
                for (int i = 0; i < 2; ++i)
                {
                    randomInt = Randomizer.GetRandomInt(0, numberOfPossibleTiles - 1);
                    Plant newPlant = new AppleTree(possibleTiles[randomInt], Map,PlantStage.Seed);
                    Map.Plants.Add(newPlant);
                    Map.NewEntities.Add(newPlant);

                    possibleTiles.RemoveAt(randomInt);
                    --numberOfPossibleTiles;
                }
            }
            else
            {
                if (numberOfPossibleTiles == 1)
                {
                    randomInt = Randomizer.GetRandomInt(0, numberOfPossibleTiles - 1);
                    Plant newPlant = new AppleTree(possibleTiles[randomInt], Map,PlantStage.Seed);
                    Map.Plants.Add(newPlant);
                    Map.NewEntities.Add(newPlant);
                }
            }
        }

        public (ResourceType, int) BeMined()
        {
            Die();
            return (ResourceType, MiningEfficiency);
        }

        public ResourceType ReturnResourceType()
        {
            return ResourceType;
        }
    }
}