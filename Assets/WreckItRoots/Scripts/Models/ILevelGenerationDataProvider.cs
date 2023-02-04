using System;

namespace WreckItRoots.Models
{
    public interface ILevelGenerationDataProvider
    {
        float InterBuildingDistance { get; }
        float GeneratedBuildingsAheadCount { get; }
        BuildingParameters GetNewBuildingParameters();
    }

    [Serializable]
    public class BuildingParameters
    {
        public float MomentumResistance;
        public float BioEnergyBonus;
    }
}