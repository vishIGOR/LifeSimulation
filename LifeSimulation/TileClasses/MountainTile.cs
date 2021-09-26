using System.Drawing;

namespace LifeSimulation.TileClasses
{
    public class MountainTail:Tile
    {
        public MountainTail(int x, int y)
        {
            LandPossibility = false;
            WaterPossibility = false;
            PlantPossibility = false;
            X = x;
            Y = y;
            IsSeeded = false;
            TileColor = Brushes.LightSlateGray;
        }
    }
}