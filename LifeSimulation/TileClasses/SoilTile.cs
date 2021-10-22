using System.Drawing;

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
            IsSeeded = false;
            TileColor = Brushes.Peru;
        }
       
    }
}