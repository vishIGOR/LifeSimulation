using System;
using System.Diagnostics;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.SupportClasses
{
    public class Mover
    {
        private int Speed;
        private int SpeedCounter;
        public int CurrentMovingWay;
        public int CurrentWalkingWay;
        private Randomizer Randomizer;
        private int CircleX = 1;
        private int CircleY = 1;
        private int CircleCounterX=0;
        private int CircleCounterY = 0;
        private Animal Animal;
        public Map Map{ get; protected set; }
        public Mover(Animal animal, int speed,Map map)
        {
            Speed = speed;
            SpeedCounter = 0;
            Map = map;
            Randomizer = Map.Randomizer;
            Animal = animal;

        }
        
        private Tile Move(Tile currentTile, int xMove, int yMove)
        {
            currentTile.Entities.Remove(Animal);
            Map.Tiles[currentTile.X + xMove, currentTile.Y + yMove].Entities.Add(Animal);
            return Map.Tiles[currentTile.X + xMove, currentTile.Y + yMove];
        }
        private Tile SimpleMove(Tile currentTile,Tile target)
        {
            int directionX, directionY;
            if (target.X - currentTile.X > 0)
            {
                directionX = 1;
            }
            else
            {
                if (target.X - currentTile.X == 0)
                {
                    directionX = 0;
                }
                else
                {
                    directionX = -1;
                }
            }
            
            if (target.Y - currentTile.Y > 0)
            {
                directionY = 1;
            }
            else
            {
                if (target.Y - currentTile.Y == 0)
                {
                    directionY = 0;
                }
                else
                {
                    directionY = -1;
                }
            }
            
            return Move(currentTile, directionX,directionY);
        }
        
        private Tile OrtogonalMove(Tile currentTile,Tile target)
        {
            int directionX = 0, directionY = 0;
            if (target.X - currentTile.X > 0)
            {
                directionX = 1;
            }
            else
            {
                if (target.X - currentTile.X == 0)
                {
                    directionX = 0;
                }
                else
                {
                    directionX = -1;
                }
            }

            if (directionX != 0)
            {
                return Move(currentTile, directionX,directionY);
            }
            
            if (target.Y - currentTile.Y > 0)
            {
                directionY = 1;
            }
            else
            {
                if (target.Y - currentTile.Y == 0)
                {
                    directionY = 0;
                }
                else
                {
                    directionY = -1;
                }
            }
            
            return Move(currentTile, directionX,directionY);
        }
        
        private Tile EuclidMove(Tile currentTile,Tile target)
        {
            int directionX = 0, directionY = 0;
            int deltaX = target.X - currentTile.X;
            int deltaY = target.Y- currentTile.Y;

            if (Math.Abs(deltaX) >= Math.Abs(deltaY))
            {
                if (target.X - currentTile.X > 0)
                {
                    directionX = 1;
                }
                else
                {
                    if (target.X - currentTile.X == 0)
                    {
                        directionX = 0;
                    }
                    else
                    {
                        directionX = -1;
                    }
                }
                return Move(currentTile, directionX, directionY);
            }
            
            
            if (target.Y - currentTile.Y > 0)
            {
                directionY = 1;
            }
            else
            {
                if (target.Y - currentTile.Y == 0)
                {
                    directionY = 0;
                }
                else
                {
                    directionY = -1;
                }
            }
            return Move(currentTile, directionX, directionY);
        }
        public Tile MoveTo(Tile currentTile, Tile target)
        {
            switch (CurrentMovingWay)
            {
                case 1:
                    return SimpleMove(currentTile, target);
                case 2:
                    return OrtogonalMove(currentTile, target);
                case 3:
                    return EuclidMove(currentTile, target);
            }


            return null;
        }

        private Tile SimpleWalk(Tile currentTile)
        {
            int deltaX = Randomizer.GetRandomInt(-1, 1);
            int deltaY = Randomizer.GetRandomInt(-1, 1);

            if (currentTile.X + deltaX >= 0 && currentTile.Y + deltaY >= 0)
            {
                if (currentTile.X + deltaX < Map.Width && currentTile.Y + deltaY < Map.Height)
                {
                    if (Map.Tiles[currentTile.X + deltaX, currentTile.Y + deltaY].LandPossibility)
                    {
                        return Move(currentTile, deltaX,deltaY);
                    }
                }
            }
            
            return SimpleWalk(currentTile);
        }
        
        private Tile CenterWalk(Tile currentTile)
        {
            int height = Map.Height/2;
            int width = Map.Width/2;

            int deltaX;
            int deltaY;
            
            if (currentTile.X > width)
            {
                if (Randomizer.GetRandomInt(0, 1) == 1)
                {
                    deltaX = -1;
                }
                else
                {
                    deltaX = Randomizer.GetRandomInt(0, 1);
                }
            }
            else
            {
                if (Randomizer.GetRandomInt(0, 1) == 1)
                {
                    deltaX = 1;
                }
                else
                {
                    deltaX = Randomizer.GetRandomInt(-1, 0);
                }
            }
            
            
            if (currentTile.Y > height)
            {
                if (Randomizer.GetRandomInt(0, 1) == 1)
                {
                    deltaY = -1;
                }
                else
                {
                    deltaY = Randomizer.GetRandomInt(0, 1);
                }
            }
            else
            {
                if (Randomizer.GetRandomInt(0, 1) == 1)
                {
                    deltaY = 1;
                }
                else
                {
                    deltaY = Randomizer.GetRandomInt(-1, 0);
                }
            }
            

            if (currentTile.X + deltaX >= 0 && currentTile.Y + deltaY >= 0)
            {
                if (currentTile.X + deltaX < Map.Width && currentTile.Y + deltaY < Map.Height)
                {
                    if (Map.Tiles[currentTile.X + deltaX, currentTile.Y + deltaY].LandPossibility)
                    {
                        return Move(currentTile, deltaX,deltaY);
                    }
                }
            }
            
            return SimpleWalk(currentTile);
        }
        
        private Tile CircleWalk(Tile currentTile)
        {
            if (currentTile.X + CircleX < 0)
            {
                CircleCounterX = 2;
            }
            
            if (currentTile.Y + CircleY < 0 )
            {
                CircleCounterY = 2;
            }
            
            if (currentTile.X + CircleX >= Map.Width)
            {
                CircleCounterX = -2;
            }
            
            if ( currentTile.Y + CircleY >= Map.Height )
            {
                CircleCounterY = -2;
            }

            if (CircleCounterX != 0 || CircleCounterY != 0)
            {
                int deltaX = Math.Sign(CircleCounterX), deltaY = Math.Sign(CircleCounterY);
                CircleCounterX -= deltaX;
                CircleCounterY -= deltaY;
                return Move(currentTile, deltaX, deltaY);
            }
            
            if (CircleX == 1)
            {
                if (CircleY == 1)
                {
                    CircleY -= 2;
                    return Move(currentTile, CircleX, CircleY+2);
                }
                else
                {
                    CircleX -= 2;
                    return Move(currentTile, CircleX+2, CircleY);
                }
            }
            else
            {
                if (CircleY == 1)
                {
                    CircleX += 2;
                    return Move(currentTile, CircleX-2, CircleY);
                }
                else
                {
                    CircleY += 2;
                    return Move(currentTile, CircleX, CircleY-2);
                }
            }
        }
        
        public Tile Walk(Tile currentTile)
        {
            switch (CurrentWalkingWay)
            {
                case 1:
                    return SimpleWalk(currentTile);
                case 2:
                    return CenterWalk(currentTile);
                case 3:
                    return CircleWalk(currentTile);
            }

            // Debugger.Launch();
            // Debugger.Log(1, "movingError", "ошибка выбора передвижения");
            return null;
        }
    }
}