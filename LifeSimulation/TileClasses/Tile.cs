using System.Collections.Generic;
using System.Drawing;

namespace LifeSimulation.TileClasses
{
    public abstract class Tile
    {
        public bool LandPossibility { get; protected set; }
        public bool PlantPossibility{ get; protected set; }
        public Brush TileColor { get; protected set; }
        public bool IsSeeded;
        public int X{ get; protected set; }
        public int Y{ get; protected set; }

        public void SeedThisTile()
        {
            IsSeeded = true;
        }
    }

}