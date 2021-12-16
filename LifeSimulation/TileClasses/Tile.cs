using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.EntityClasses;
using LifeSimulation.MapClasses.Enumerators;

namespace LifeSimulation.TileClasses
{
    public abstract class Tile
    {
        public bool LandPossibility { get; protected set; }
        public bool PlantPossibility { get; protected set; }
        public Brush TileColor { get; protected set; }
        public List<Entity> Entities = new List<Entity>();
        public Entity SpecialObject;
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public abstract void ReactToChangeSeason(SeasonType newSeason);

        public void DeleteMeIDontWannaLiveAnymore(Entity entity)
        {
            List<Entity> DeletingEntities = new List<Entity>();
            Entities.ForEach((entityIn =>
            {
                if (entity == entityIn)
                {
                    DeletingEntities.Add(entityIn);
                }
            }));
            DeletingEntities.ForEach((entityIn => { Entities.Remove(entityIn); }));
        }
    }
}