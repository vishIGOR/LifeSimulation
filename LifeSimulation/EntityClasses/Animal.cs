using LifeSimulation.MapClasses;

namespace LifeSimulation.EntityClasses
{
    public abstract class Animal : Entity
    {
        protected int Speed;
        protected int Age;
        protected int MaxAge;

        protected virtual void Die(){}

        protected virtual void Move(int xMove, int yMove)
        {
            CurrentTile = EntityMap.Tiles[CurrentTile.X + xMove, CurrentTile.Y + yMove];
        }

        protected virtual void Walk()
        {
            
            int deltaX = EntityRandomizer.RandomInt(-1, 1);
            int deltaY = EntityRandomizer.RandomInt(-1, 1);

            if (CurrentTile.X + deltaX >= 0 && CurrentTile.Y + deltaY >= 0)
            {
                if (CurrentTile.X + deltaX < EntityMap.Width && CurrentTile.Y + deltaY < EntityMap.Height)
                {
                    if (EntityMap.Tiles[CurrentTile.X + deltaX, CurrentTile.Y + deltaY].LandPossibility)
                    {
                        Move(deltaX,deltaY);
                    }
                }
            }
            
            Walk();
        }
        
    }
}