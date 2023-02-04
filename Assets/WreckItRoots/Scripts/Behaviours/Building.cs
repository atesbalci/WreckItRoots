using System;
using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Behaviours
{
    public class Building : MonoBehaviour, IBuilding
    {
        public event Action Initialized;
        public event Action Wrecked;
        public Vector2 Position => transform.position;
        public float Width => 2f;
        public float MomentumResistance { get; private set; }
        public float BioEnergyReward { get; private set; }

        public void Initialize(float xPosition, BuildingParameters buildingParameters)
        {
            transform.position = new Vector2(xPosition, 0f);
            MomentumResistance = buildingParameters.MomentumResistance;
            BioEnergyReward = buildingParameters.BioEnergyBonus;
            Initialized?.Invoke();
        }
        
        public void Wreck()
        {
            Wrecked?.Invoke();
        }
        
        public class Pool : MonoMemoryPool<Building> { }
    }
}