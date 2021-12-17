using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses;
using LifeSimulation.EntityClasses.BuildingClasses;
using LifeSimulation.EntityClasses.DeadBodyClasses;
using LifeSimulation.EntityClasses.Omnivore;
using LifeSimulation.EntityClasses.ResourceDeposit;
using LifeSimulation.EntityClasses.Scavenger;
using LifeSimulation.Enumerations;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;
using LifeSimulation.VillageClasses;


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
        public List<Fetus> Fetuses { get; private set; }
        public List<Building> Buildings { get; private set; }
        public List<ResourceDeposit> ResourceDeposits { get; private set; }
        public List<DeadBody> DeadBodies { get; private set; }
        public Randomizer Randomizer { get; private set; }
        public VillagesObserver VillagesObserver{ get; private set; }
        public Brush[,] ColorsOfTiles { get; private set; }
        public SeasonType Season { get; private set; }
        private int SeasonCounter;


        public Map(int height, int width, int numberOfAnimals, int percentOfPlants)
        {
            Randomizer = new Randomizer();
            VillagesObserver = new VillagesObserver(this);

            Entities = new List<Entity>();
            NewEntities = new List<Entity>();
            DeadEntities = new List<Entity>();

            Animals = new List<Animal>();
            Plants = new List<Plant>();
            Fetuses = new List<Fetus>();
            DeadBodies = new List<DeadBody>();
            Buildings = new List<Building>();
            ResourceDeposits = new List<ResourceDeposit>();
            
            Height = height;
            Width = width;

            Tiles = new Tile[Width, Height];

            Season = SeasonType.Summer;
            SeasonCounter = 0;

            ColorsOfTiles = new Brush[Width, Height];

            CreateRandomTiles(percentOfPlants);
            CreateRandomResourceDeposits(Width*Height/300);
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
                        if (Randomizer.GetRandomInt(1, 1000) <= percentOfPlants)
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
                return new AppleTree(tile, this, PlantStage.Grown);
            }

            if (randomInt <= 50)
            {
                return new WolfBerry(tile, this, PlantStage.Grown);
            }

            return new Grass(tile, this, PlantStage.Grown);

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

        void CreateRandomResourceDeposits(int numberOfResourceDeposits)
        {
            int currentX, currentY;
            ResourceDeposit newResourceDeposit;
            while (numberOfResourceDeposits > 0)
            {
                currentX = Randomizer.GetRandomInt(0, Width - 1);
                currentY = Randomizer.GetRandomInt(0, Height - 1);
                if (Tiles[currentX, currentY].SpecialObject == null)
                {
                    --numberOfResourceDeposits;
                    newResourceDeposit = new SaltpeterMine(Tiles[currentX, currentY], this);
                    ResourceDeposits.Add(newResourceDeposit);
                    Entities.Add(newResourceDeposit);
                }
            }
        }
        Animal CreateNewAnimal(Tile tile)
        {
            int maxNumber = 100;
            int randomInt = Randomizer.GetRandomInt(1, maxNumber);

            if (randomInt <= 30)
            {
                return new Human(tile, this);
            }

            if (randomInt <= 37)
            {
                return new Sheep(tile, this);
            }

            if (randomInt <= 44)
            {
                return new Bear(tile, this);
            }

            if (randomInt <= 51)
            {
                return new Tiger(tile, this);
            }

            if (randomInt <= 58)
            {
                return new Panther(tile, this);
            }

            if (randomInt <= 65)
            {
                return new Hyena(tile, this);
            }

            if (randomInt <= 72)
            {
                return new Pig(tile, this);
            }

            if (randomInt <= 79)
            {
                return new Monkey(tile, this);
            }

            if (randomInt <= 86)
            {
                return new Frog(tile, this);
            }

            if (randomInt <= 93)
            {
                return new Mouse(tile, this);
            }

            // if (randomInt <= 40)
            // {
            //     return new Human(tile, this);
            // }
            // if (randomInt <= 60)
            // {
            //     return new Bear(tile, this);
            // }
            // if (randomInt <= 80)
            // {
            //     return new Sheep(tile, this);
            // }
            return new Wolf(tile, this);

            // return new Sheep(tile, this);
        }


        public void UpdateMap()
        {
            ++SeasonCounter;
            if (SeasonCounter == 15)
            {
                SeasonCounter = 0;
                ChangeSeason();
            }

            foreach (var entity in Entities)
            {
                entity.ChooseAction();
            }
        }

        public void ReloadEntities()
        {
            foreach (var entity in NewEntities)
            {
                Entities.Add(entity);

                if (entity is Human)
                {
                    //Debug.WriteLine("added");
                }
            }

            NewEntities.Clear();

            foreach (var entity in DeadEntities)
            {
                entity.Tile.Entities.Remove(entity);
                Entities.Remove(entity);
            }

            DeadEntities.Clear();
        }

        void ChangeSeason()
        {
            switch (Season)
            {
                case SeasonType.Summer:
                    Season = SeasonType.Winter;
                    break;
                case SeasonType.Winter:
                    Season = SeasonType.Summer;
                    break;
            }

            foreach (var tile in Tiles)
            {
                tile.ReactToChangeSeason(Season);
            }

            for (int j = 0; j < Height; ++j)
            {
                for (int i = 0; i < Width; ++i)
                {
                    ColorsOfTiles[i, j] = Tiles[i, j].TileColor;
                }
            }

            foreach (var entity in Entities)
            {
                entity.ReactToChangeSeason(Season);
            }
        }
    }
}