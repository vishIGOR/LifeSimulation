﻿using System;
using System.Drawing;
using LifeSimulation.AdditionalClasses;
using LifeSimulation.EntityClasses.SupportClasses;
using LifeSimulation.MapClasses;
using LifeSimulation.TileClasses;

namespace LifeSimulation.EntityClasses
{
    public class Sheep : Herbivore
    {
        public Sheep(Tile tile, Map map)
        {
            MaxHitPoints = 10;
            MaxHungerPoints = 15;
            HungerBorder = 4;
            MaxMatingCounter = 20;
            MaxAge = 50;
            Color = Brushes.White;
            DamageForce = 40;
            
            SetStandartValues(tile, map);
            Mover = new Mover(3, map);
            Mover.CurrentMovingWay = 2;
            Mover.CurrentWalkingWay = 2;
        }
        protected override void CreateChild()
        {
            Sheep child = new Sheep(Tile, Map);
            Map.NewEntities.Add(child);
            Map.Animals.Add(child);
        }
        
        public override void ChooseAction()
        {
            ++Age;
            if (Age > MaxAge)
            {
                Die();
                return;
            }

            if (HungerPoints == 0)
            {
                --HitPoints;
            }
            else
            {
                --HungerPoints;
            }

            
            if (HitPoints <= 0)
            {
                Die();
                return;
            }

            if (MatingCounter + 5 >= MaxMatingCounter)
            {
                Mover.CurrentWalkingWay = 3;
            }
            else
            {
                Mover.CurrentWalkingWay = 2;
            }
            
            --MatingCounter;
            if (MatingCounter <= 0 && ReadyToMate==false)
            {
                ReadyToMate = true;
            }
            
            if (HungerPoints < HungerBorder)
            {
                if(MatingTarget!= null)
                    MatingTarget.MatingTarget = null;
                ReadyToMate = false;
                MatingTarget = null;
                
                LookForFood();
                return;
            }

            if (ReadyToMate)
            {
                LookForMating();
            }
            
            Tile = Mover.Walk(Tile);
        }
    }
}