using UnityEngine;
using WreckItRoots.Behaviours;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Injection
{
    [CreateAssetMenu]
    public class LevelGenerationDataScriptableObject : ScriptableObjectInstaller<LevelGenerationDataScriptableObject>, ILevelGenerationDataProvider
    {
        [SerializeField] private GameObject buildingPrefab;
        [SerializeField] private float interBuildingDistance;
        [SerializeField] private float generatedBuildingsAheadCount;
        [SerializeField] private BuildingParameters[] buildingEntries;

        public float InterBuildingDistance => interBuildingDistance;
        public float GeneratedBuildingsAheadCount => generatedBuildingsAheadCount;
        
        public BuildingParameters GetNewBuildingParameters()
        {
            return buildingEntries[Random.Range(0, buildingEntries.Length)];
        }

        public override void InstallBindings()
        {
            Container.BindInstance<ILevelGenerationDataProvider>(this).AsSingle();
            Container.BindMemoryPool<Building, Building.Pool>().WithInitialSize(50)
                .FromComponentInNewPrefab(buildingPrefab);
        }
    }
}