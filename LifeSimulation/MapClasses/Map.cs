using System;
using System.Collections.Generic;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.EntityClasses.Scavenger;
using LifeSimulation.Enumerations;
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
        public List<Fetus> Fetuses{ get; private set; }
        public List<DeadBody> DeadBodies{ get; private set; }
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
            Fetuses = new List<Fetus>();
            DeadBodies = new List<DeadBody>();
            
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
            int randomInt = Randomizer.GetRandomInt(1, maxNumber);

            if (randomInt < 1)
            {
                return new MountainTail(x, y);
            }

            return new SoilTail(x, y);
        }

        Plant CreateNewPlant(Tile tile)
        {
            int maxNumber = 100;
            int randomInt = Randomizer.GetRandomInt(1, maxNumber);

            if (randomInt <= 40)
            {
                return new AppleTree(tile,this,PlantStage.Grown);
            }
            
            if (randomInt <= 50)
            {
                return new WolfBerry(tile,this,PlantStage.Grown);
            }
            return new Grass(tile, this,PlantStage.Grown);
            
            // return new WolfBerry(tile,this,PlantStage.Grown);
            // return new Grass(tile, this,PlantStage.Grown);
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
            int maxNumber = 100;
            int randomInt = Randomizer.GetRandomInt(1, maxNumber);
            if (randomInt <= 10)
            {
                return new Sheep(tile, this);
            }
            
            if (randomInt <= 20)
            {
                return new Bear(tile, this);
            }
            
            if (randomInt <= 30)
            {
                return new Tiger(tile, this);
            }
            
            if (randomInt <= 40)
            {
                return new Panther(tile, this);
            }
            
            if (randomInt <= 50)
            {
                return new Condor(tile, this);
            }
            return new Wolf(tile, this);
            
            // return new Sheep(tile, this);
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