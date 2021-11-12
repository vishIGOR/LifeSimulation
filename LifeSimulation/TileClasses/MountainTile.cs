using System.Drawing;
using LifeSimulation.MapClasses.Enumerators;

namespace LifeSimulation.TileClasses
{
    public class MountainTail:Tile
    {
        public MountainTail(int x, int y)
        {
            LandPossibility = false;
            PlantPossibility = false;
            X = x;
            Y = y;
            IsSeeded = false;
            TileColor = Brushes.LightSlateGray;
        }
        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            switch (newSeason)
            {
                case SeasonType.Summer:
                    TileColor = Brushes.LightSlateGray;
                    break;
                case SeasonType.Winter:
                    TileColor = Brushes.AliceBlue;
                    break;
            }
        }
    }
}