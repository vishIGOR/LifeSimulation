using System;

namespace LifeSimulation.EntityClasses
{
    public abstract class Animal : Entity
    {
        protected int Speed;
        protected int Age;
        protected int MaxAge;
        protected double CalculateDistance(Entity target)
        {
            return Math.Sqrt(Math.Pow(target.Tile.X - Tile.X, 2) + Math.Pow(target.Tile.Y - Tile.Y, 2));
        }
        
        protected virtual void Move(int xMove, int yMove)
        {
            Tile = Map.Tiles[Tile.X + xMove, Tile.Y + yMove];
        }

        protected void MoveTo(Entity target)
        {
            int directionX, directionY;
            if (target.Tile.X - Tile.X > 0)
            {
                directionX = 1;
            }
            else
            {
                if (target.Tile.X - Tile.X == 0)
                {
                    directionX = 0;
                }
                else
                {
                    directionX = -1;
                }
            }
            
            if (target.Tile.Y - Tile.Y > 0)
            {
                directionY = 1;
            }
            else
            {
                if (target.Tile.Y - Tile.Y == 0)
                {
                    directionY = 0;
                }
                else
                {
                    directionY = -1;
                }
            }
            
            Move(directionX,directionY);
        }
        

        protected virtual void Walk()
        {
            
            int deltaX = Randomizer.GetRandomInt(-1, 1);
            int deltaY = Randomizer.GetRandomInt(-1, 1);

            if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
            {
                if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                {
                    if (Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].LandPossibility)
                    {
                        Move(deltaX,deltaY);
                        return;
                    }
                }
            }
            
            Walk();
        }
        
    }
}