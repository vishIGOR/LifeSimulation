using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.ResourceDeposit
{
    public abstract class ResourceDeposit:Entity,IMeneable
    {
        // public int MaxFullness{ get; protected set; }
        // public int Fullness{ get; protected set; }
        public ResourceType ResourceType{ get; protected set; }
        protected int MiningEfficiency;
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
            Map.ResourceDeposits.Remove(this);
            Map.DeadEntities.Add(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            base.SetStandartValues(tile, map);

            Eatable = false;
            HitPoints = MaxHitPoints;
            
            
            Tile.SpecialObject = this;
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if (newSeason == SeasonType.Winter)
            {
                MiningEfficiency /= 2;
            }
            
            if (newSeason == SeasonType.Summer)
            {
                MiningEfficiency *= 2;
            }
        }


        public (ResourceType, int) BeMined()
        {
            return (this.ResourceType, MiningEfficiency);
        }
    }
}