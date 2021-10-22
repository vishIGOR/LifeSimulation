using System.Collections.Generic;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public abstract class Fetus:Entity
    {
        protected int Age;
        protected int MaxAge;
        protected int ReadyToFall;
        protected int ReadyToSeed;

        protected int FallingRadius;
        protected abstract void CreateSeed();
        
        protected override void Die()
        {
            Map.Fetuses.Remove(this);
            Map.DeadEntities.Add(this);
        }
        
        protected void FallAway()
        {
            List<Tile> possibleTiles = new List<Tile>();
            int counter = 0;

            for (int deltaX = -FallingRadius; deltaX <= FallingRadius; ++deltaX)
            {
                for (int deltaY = -FallingRadius; deltaY <= FallingRadius; ++deltaY)
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
        
        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Age = 0;
            
            HitPoints = MaxHitPoints;

            Eatable = true;
        }
    }
}