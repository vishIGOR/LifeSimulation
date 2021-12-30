using System;
using System.Collections.Generic;
using System.IO;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.GiftClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.MapClasses.Enumerators;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Elf:Entity,IChristmas
    {
        private Mover Mover;
        private bool IsActive = false;
        private List<Gift> Gifts = new List<Gift>();
        public Elf(Map map, Tile tile)
        {
            MaxHitPoints = 100;
            SetStandartValues(tile,map);
            
            Tile.Entities.Add(this);
        }
        public void BeNotified(bool newState)
        {
            IsActive = newState;
            if (IsActive == false)
            {
                Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","Nothing.png"));
            }
            else
            {
                Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","LifeSimulation.EntityClasses.Elf.png"));
            }
            
        }
        public override void ChooseAction()
        {
            if (HitPoints <= 0)
            {
                Die();
            }

            if (IsActive == false)
            {
                return;
            }
            
            if (Gifts.Count == 0)
            {
                LookForElfFactory();
                return;
            }
            
            LookForHumanToGiftGifts();
        }

        private void LookForElfFactory()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 50;
            ElfFactory nearestElfFactory = null;

            foreach (var elfFactory in Map.ElfFactories)
            {
                currentDistance = CalculateDistance(elfFactory);
                if (minDistance > currentDistance && currentDistance < maxDistance)
                {
                    minDistance = currentDistance;
                    nearestElfFactory = elfFactory;
                }
            }

            if (nearestElfFactory == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                Tile = Mover.MoveTo(Tile, nearestElfFactory.Tile);
                if (Tile == nearestElfFactory.Tile)
                {
                    CreateGift();
                }
            }
        }
        
        protected double CalculateDistance(Entity target)
        {
            return Math.Sqrt(Math.Pow(target.Tile.X - Tile.X, 2) + Math.Pow(target.Tile.Y - Tile.Y, 2));
        }

        private void CreateGift()
        {
            Gift newGift=null;
            while (Gifts.Count < 3)
            {
               
                switch (Randomizer.GetRandomInt(1,5))
                {
                    case 1:
                        newGift = new Bag(Map);
                        break;
                    case 2:
                        newGift = new Candy(Map);
                        break;
                    case 3:
                        newGift = new Hat(Map);
                        break;
                    case 4:
                        newGift = new ResourceGift(Map);
                        break;
                    case 5:
                        newGift = new FoodGift(Map);
                        break;
                }

                if (Gifts.Count == 0 || Randomizer.GetRandomInt(1, 10) > 3)
                {
                    Gifts.Add(newGift);
                }
                else
                {
                    Gifts[Randomizer.GetRandomInt(0,Gifts.Count-1)].InsertGift(newGift);
                }
            }
        }

        private void LookForHumanToGiftGifts()
        {
            double minDistance = 10000000000;
            double currentDistance;
            double maxDistance = 30;
            Entity nearestHuman = null;

            foreach (var entity in Map.Entities)
            {
                if (entity is Human)
                {
                    if ((entity as Human).Gift == null)
                    {
                        currentDistance = CalculateDistance(entity);
                        if (minDistance > currentDistance && currentDistance < maxDistance)
                        {
                            minDistance = currentDistance;
                            nearestHuman = entity;
                        }
                    }
                }
            }

            if (nearestHuman == null)
            {
                Tile = Mover.Walk(Tile);
            }
            else
            {
                Tile = Mover.MoveTo(Tile, nearestHuman.Tile);
                if (Tile == nearestHuman.Tile)
                {
                    (nearestHuman as Human).GetGift(Gifts[0]);
                    Gifts.RemoveAt(0);
                }
            }
        }
        public override void ReactToChangeSeason(SeasonType newSeason)
        {
            if(newSeason == SeasonType.Winter)
                HitPoints = MaxHitPoints;
        }
        protected override void Die()
        {
            Map.Entities.Remove(this);
            Map.Elves.Remove(this);
            Map.ChristmasObjects.Remove(this);
            Tile.Entities.Remove(this);
        }

        protected override void SetStandartValues(Tile tile, Map map)
        {
            Mover = new Mover(this, 2, map);
            Mover.CurrentMovingWay = 1;
            Mover.CurrentWalkingWay = 2;
            
            Eatable = false;

            HitPoints = MaxHitPoints;
            base.SetStandartValues(tile, map);
            Image = System.Drawing.Image.FromFile(Path.Combine(Environment.CurrentDirectory, @"../../Images/","Nothing.png"));

        }
        
        
    }
}