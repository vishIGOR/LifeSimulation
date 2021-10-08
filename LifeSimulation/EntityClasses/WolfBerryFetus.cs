using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class WolfBerryFetus:Fetus
    {
        public WolfBerryFetus(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;

            Toxicity = true;
            ToxicityValue = 100;

            Age = 0;
            MaxAge = 36;

            HitPoints = 10;
            MaxHitPoints = 10;

            Eatable = true;

            Color = Brushes.MidnightBlue;

            ReadyToFall = 12;
            ReadyToSeed = 18;
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

        protected override void Die()
        {
            Map.Fetuses.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void FallAway()
        {
            List<Tile> possibleTiles = new List<Tile>();
            int counter = 0;

            for (int deltaX = -1; deltaX <= 1; ++deltaX)
            {
                for (int deltaY = -1; deltaY <= 1; ++deltaY)
                {
                    if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
                    {
                        if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                        {
                            if (deltaX != 0 || deltaY != 0)
                            {
                                ++counter;
                                possibleTiles.Add(Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY]);
                            }
                        }
                    }
                }
            }
            Tile = possibleTiles[Randomizer.GetRandomInt(0, counter - 1)];
            
        }

        protected override void CreateSeed()
        {
            if (!Tile.IsSeeded)
            {
                Plant newPlant = new WolfBerry(Tile, Map);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
                
                Die();
            }
        }
    }
}