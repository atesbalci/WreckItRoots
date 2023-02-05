using System;
using UnityEngine;
using WreckItRoots.Views;
using Zenject;
using Random = UnityEngine.Random;

namespace WreckItRoots.Injection
{
    [CreateAssetMenu]
    public class RootBranchDataScriptableObject : ScriptableObjectInstaller<RootBranchDataScriptableObject>, IRootBranchDataProvider
    {
        [SerializeField] private float maxBranchOutHeight;
        [SerializeField] private GameObject rootBranchPrefab;
        [SerializeField] private LevelEntry[] _levels;
        
        public override void InstallBindings()
        {
            Container.BindInstance<IRootBranchDataProvider>(this);
            Container.BindMemoryPool<RootBranch, RootBranch.Pool>().WithInitialSize(50)
                .FromComponentInNewPrefab(rootBranchPrefab);
        }

        public float GetLifetime(int level) => _levels[level].Lifetime;

        public float GetVelocity(int level) => _levels[level].Velocity;

        public float GetBranchOutInterval(int level) => _levels[level].BranchOutInterval;

        public float GetBranchOutAngle(int level)
        {
            var l = _levels[level];
            return l.BranchOutAngle + Random.Range(-l.BranchOutAngleVariance, l.BranchOutAngleVariance);
        }

        public float MaxBranchOutHeight => maxBranchOutHeight;

        [Serializable]
        private struct LevelEntry
        {
            public float Lifetime;
            public float Velocity;
            public float BranchOutInterval;
            public float BranchOutAngle;
            public float BranchOutAngleVariance;
        }
    }
}