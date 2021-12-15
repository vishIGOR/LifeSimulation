using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public class LivingHouse:Building
    {
        public Human Owner
        {
            get;
            protected set;
        }
        
        public LivingHouse(Tile tile, Map map)
        {
            MaxHitPoints = 120;
            SetStandartValues(tile,map);
        }

        public void Assign(Human newOwner)
        {
            Owner = newOwner;
        }
        
        public void UnAssign()
        {
            Owner = null;
        }
        protected override void Die()
        {
            base.Die();
            Owner.House = null;
        }
    }
}