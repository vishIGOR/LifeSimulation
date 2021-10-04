using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Grass:Plant
    {
        public Grass(Tile tile, Map map)
        {
            Tile = tile;
            Map = map;
            Randomizer = Map.Randomizer;
            Tile.IsSeeded = true;
            HitPoints = 30;
            MaxHitPoints = 30;
            ReadyToCreep = 25;
            CreepCounter = Randomizer.GetRandomInt(0,15);
            Color = Brushes.LightGreen;
        }

        protected override void Die()
        {
            Map.Plants.Remove(this);
            Map.DeadEntities.Add(this);
        }

        public override void ChooseAction()
        {
            ++CreepCounter;
            
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            if (CreepCounter >= ReadyToCreep)
            {
                CreepCounter = 0;
                Creep();
                return;
            }
        }

        protected override void Creep()
        {
            // int deltaX = Randomizer.GetRandomInt(-1, 1);
            // int deltaY = Randomizer.GetRandomInt(-1, 1);
            List<Tile> possibleTiles = new List<Tile>();
            int counter = 0;
            for (int deltaX = -1; deltaX <= 1; ++deltaX)
            {
                for (int deltaY = -1; deltaY <= 1; ++deltaY)
                {
                    if (Tile.X + deltaX >= 0 && Tile.Y + deltaY >= 0)
                    {
                        if (Tile.X + deltaX < Map.Width && Tile.Y + deltaY < Map.Height)
                        {
                            if (!Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].IsSeeded && Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY].PlantPossibility)
                            {
                                ++counter;
                                possibleTiles.Add(Map.Tiles[Tile.X + deltaX, Tile.Y + deltaY]);
                            }
                        }
                    }
                }
            }

            if (counter > 0)
            {
                Plant newPlant = new Grass(possibleTiles[Randomizer.GetRandomInt(0, counter - 1)], Map);
                Map.Plants.Add(newPlant);
                Map.NewEntities.Add(newPlant);
            }
            
        }
    }
}