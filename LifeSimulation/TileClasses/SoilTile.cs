using System.Drawing;
using LifeSimulation.MapClasses.Enumerators;

namespace LifeSimulation.TileClasses
{
    public class SoilTail:Tile
    {
        public SoilTail(int x, int y)
        {
            LandPossibility = true;
            PlantPossibility = true;
            X = x;
            Y = y;
            TileColor = Brushes.Peru;
        }

        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            switch (newSeason)
            {
                case SeasonType.Summer:
                    TileColor = Brushes.Peru;
                    break;
                case SeasonType.Winter:
                    TileColor = Brushes.AliceBlue;
                    break;
            }
        }
    }
}