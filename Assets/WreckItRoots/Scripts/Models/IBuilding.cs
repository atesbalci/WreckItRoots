using System;

namespace WreckItRoots.Models
{
    public interface IBuilding : IWorldObject
    {
        event Action Initialized;
        event Action Wrecked;
        float Width { get; }
        float MomentumResistance { get; }
        float BioEnergyReward { get; }

        public void Wreck();
    }
}