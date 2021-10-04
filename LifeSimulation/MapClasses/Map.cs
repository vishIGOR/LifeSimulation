using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses;
using LifeSimulation.TileClasses;


namespace LifeSimulation.MapClasses
{
    public class Map
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public List<Entity> Entities { get; private set; }
        public List<Entity> NewEntities { get; private set; }
        public List<Entity> DeadEntities { get; private set; }
        public List<Animal> Animals { get; private set; }
        public List<Plant> Plants { get; private set; }
        public Randomizer Randomizer{ get; private set; }
        public Brush[,] ColorsOfTiles { get; private set; }
        

        public Map(int height, int width, int numberOfAnimals, int percentOfPlants)
        {
            Randomizer = new Randomizer();
            
            Entities = new List<Entity>();
            NewEntities = new List<Entity>();
            DeadEntities = new List<Entity>();

            Animals = new List<Animal>();
            Plants = new List<Plant>();
            
            Height = height;
            Width = width;

            Tiles = new Tile[Width, Height];

            ColorsOfTiles = new Brush[Width, Height];
            
            CreateRandomTiles(percentOfPlants);
            CreateRandomAnimals(numberOfAnimals);
        }

        void CreateRandomTiles(int percentOfPlants)
        {
            Plant newPlant;
            for (int j = 0; j < Height; ++j)
            {
                for (int i = 0; i < Width; ++i)
                {
                    Tiles[i, j] = CreateNewTile(i, j);

                    if (Tiles[i, j].PlantPossibility == true)
                    {
                        if (Randomizer.GetRandomInt(1, 100) <= percentOfPlants)
                        {
                            newPlant = CreateNewPlant(Tiles[i, j]);
                            Plants.Add(newPlant);
                            Entities.Add(newPlant);
                        }
                    }
                }
            }
            
            for (int j = 0; j < Height; ++j)
            {
                for (int i = 0; i < Width; ++i)
                {
                    ColorsOfTiles[i, j] = Tiles[i, j].TileColor;
                }
            }
        }

        Tile CreateNewTile(int x, int y)
        {
            int maxNumber = 10;
            int GetRandomInt = Randomizer.GetRandomInt(1, maxNumber);

            if (GetRandomInt < 1)
            {
                return new MountainTail(x, y);
            }

            return new SoilTail(x, y);
        }

        Plant CreateNewPlant(Tile tile)
        {
            int maxNumber = 10;
            int GetRandomInt = Randomizer.GetRandomInt(1, maxNumber);

            return new Grass(tile, this);
        }

        void CreateRandomAnimals(int numberOfAnimals)
        {
            int currentX, currentY;
            Animal newAnimal;
            while (numberOfAnimals > 0)
            {
                currentX = Randomizer.GetRandomInt(0, Width - 1);
                currentY = Randomizer.GetRandomInt(0, Height - 1);
                if (Tiles[currentX, currentY].LandPossibility)
                {
                    --numberOfAnimals;
                    newAnimal = CreateNewAnimal(Tiles[currentX, currentY]);
                    Animals.Add(newAnimal);
                    Entities.Add(newAnimal);
                }
            }
        }

        Animal CreateNewAnimal(Tile tile)
        {
            int maxNumber = 10;
            int GetRandomInt = Randomizer.GetRandomInt(1, maxNumber);
            if (GetRandomInt < 5)
            {
                return new Sheep(tile, this);
            }

            return new Wolf(tile, this);
        }


        public void UpdateMap()
        {
            foreach (var entity in Entities)
            {
                entity.ChooseAction();
            }

            foreach (var entity in NewEntities)
            {
                Entities.Add(entity);
            }
            NewEntities.Clear();

            foreach (var entity in DeadEntities)
            {
                Entities.Remove(entity);
            }
            DeadEntities.Clear();
        }
    }
}