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
        public int Height{get; private set;}
        public int Width{get; private set;}
        public Tile[,] Tiles{get; private set;}
        public List<Entity> Entities{get; private set;}
        private Randomizer MapRandomizer;
        
        public Map(int height, int width, int numberOfAnimals, int percentOfPlants)
        {
            MapRandomizer = new Randomizer();
            Entities = new List<Entity>();
            
            Height = height;
            Width = width;
            
            Tiles = new Tile[Width, Height];
            
            CreateRandomTiles(percentOfPlants);
            CreateRandomAnimals(numberOfAnimals);
           
        }

        void CreateRandomTiles(int percentOfPlants)
        {
            
            for (int j = 0; j < Height; ++j)
            {
                for (int i = 0; i < Width; ++i)
                {
                    Tiles[i, j] = CreateNewTile(i, j);

                    if (Tiles[i, j].PlantPossibility == true)
                    {
                        if (MapRandomizer.RandomInt(1, 100) <= percentOfPlants)
                        {
                            Entities.Add(CreateNewPlant(Tiles[i, j]));
                        }
                    }
                    
                }
            }
            
        }
        
        // void CreateRandomPlants(int numberOfPlants)
        // {
        //     Random randomizer = new Random();
        //     int currentX, currentY;
        //     while (numberOfPlants > 0)
        //     {
        //         currentX = randomizer.Next(0, Width);
        //         currentY = randomizer.Next(0, Height);
        //         if (Tiles[currentX, currentY].PlantPossibility && Tiles[currentX, currentY].IsSeeded == false)
        //         {
        //             --numberOfPlants;
        //             Entities.Add(CreateNewPlant(Tiles[currentX, currentY]));
        //             Tiles[currentX, currentY].IsSeeded = true;
        //         }
        //     }
        // }

        Tile CreateNewTile(int x, int y)
        {
            int maxNumber = 10;
            int randomInt = MapRandomizer.RandomInt(1,maxNumber);
            
            if (randomInt < 1)
            {
                return new MountainTail(x,y);
            }
                    
            return new SoilTail(x,y);
        }
        
        Plant CreateNewPlant(Tile tile)
        {
            int maxNumber = 10;
            int randomInt = MapRandomizer.RandomInt(1,maxNumber);

            return new Grass(tile, this);
        }
        
        void CreateRandomAnimals(int numberOfAnimals)
        {
            int currentX, currentY;
            while (numberOfAnimals > 0)
            {
                currentX = MapRandomizer.RandomInt(0, Width - 1);
                currentY = MapRandomizer.RandomInt(0, Height - 1);
                if (Tiles[currentX, currentY].LandPossibility)
                {
                    --numberOfAnimals;
                    Entities.Add(CreateNewAnimal(Tiles[currentX, currentY]));
                }
            }
        }
        
        Animal CreateNewAnimal(Tile tile)
        {
            int maxNumber = 10;
            int randomInt = MapRandomizer.RandomInt(1,maxNumber);
            if (randomInt < 5)
            {
                return new Sheep(tile, this);
            }

            return new Wolf(tile, this);
        }

        public Brush[,] ReturnColorsOfTiles()
        {
            Brush[,] arrayOfColors = new Brush[Width,Height];
            for (int j = 0; j < Height; ++j)
            {
                for (int i = 0; i < Width; ++i)
                {
                    arrayOfColors[i, j] = Tiles[i, j].TileColor;
                }
            }
            return arrayOfColors;
        }

        public List<Entity> ReturnAllPlants()
        {
            List<Entity> listOfPlants = new List<Entity>();

            foreach (var entity in Entities)
            {
                if (entity is Plant)
                {
                    listOfPlants.Add(entity);
                }
            }

            return listOfPlants;
        }

        public List<Entity> ReturnAllAnimals()
        {
            List<Entity> listOfAnimals = new List<Entity>();
            
            foreach (var entity in Entities)
            {
                if (entity is Animal)
                {
                    listOfAnimals.Add(entity);
                }
            }

            return listOfAnimals;
        }

        public void UpdateMap()
        {
            foreach (var entity in Entities)
            {
                entity.ChooseAction();
            }
        }
    }
}