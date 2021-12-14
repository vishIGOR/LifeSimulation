using LifeSimulation.MapClasses;
using LifeSimulation.ResourceClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses.ResourceDeposit
{
    public class SaltpeterMine:ResourceDeposit
    {
        public SaltpeterMine(Tile tile, Map map)
        {
            ResourceType = new Saltpeter();
            MaxHitPoints = 100;
            MiningEfficiency = 8;
            SetStandartValues(tile, map);
        }
    }
}