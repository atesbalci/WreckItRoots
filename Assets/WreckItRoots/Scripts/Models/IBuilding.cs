using System;

namespace WreckItRoots.Models
{
    public interface IBuilding : IWorldObject
    {
        event Action Initialized;
        float Width { get; }
        float MomentumResistance { get; }
        float BioEnergyReward { get; }
    }
}