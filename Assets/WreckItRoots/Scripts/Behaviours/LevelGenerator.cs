using System.Collections.Generic;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Behaviours
{
    public class LevelGenerator : IBuildingProvider, ITickable
    {
        public Stack<IBuilding> Buildings { get; }

        private readonly Building.Pool _buildingPool;
        private readonly IRootTip _rootTip;
        private readonly ILevelGenerationDataProvider _levelGenerationDataProvider;
        
        private float _lastSpawnedXCoord;

        public LevelGenerator(Building.Pool buildingPool, IRootTip rootTip, ILevelGenerationDataProvider levelGenerationDataProvider)
        {
            Buildings = new Stack<IBuilding>();
            _buildingPool = buildingPool;
            _rootTip = rootTip;
            _levelGenerationDataProvider = levelGenerationDataProvider;
        }

        public void Tick()
        {
            var requiredXCoordSpawn = _rootTip.Position.x +
                (_levelGenerationDataProvider.GeneratedBuildingsAheadCount * _levelGenerationDataProvider.InterBuildingDistance);
            while (_lastSpawnedXCoord < requiredXCoordSpawn)
            {
                _lastSpawnedXCoord += _levelGenerationDataProvider.InterBuildingDistance;
                var building = _buildingPool.Spawn();
                building.Initialize(_lastSpawnedXCoord, _levelGenerationDataProvider.GetNewBuildingParameters());
                Buildings.Push(building);
            }
        }
    }
}