using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.BuildingClasses
{
    public abstract class Building : Entity
    {
        public override void ChooseAction()
        {
            if (HitPoints < MaxHitPoints)
            {
                Die();
            }
        }

        protected override void Die()
        {
            Tile.SpecialObject = null;
            Map.Buildings.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Eatable = false;
            HitPoints = MaxHitPoints;

            Tile.SpecialObject = this;
            Map.Buildings.Add(this);
        }
    }
}