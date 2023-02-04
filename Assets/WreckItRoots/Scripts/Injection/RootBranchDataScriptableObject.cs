using System;
using UnityEngine;
using WreckItRoots.Views;
using Zenject;

namespace WreckItRoots.Injection
{
    [CreateAssetMenu]
    public class RootBranchDataScriptableObject : ScriptableObjectInstaller<RootBranchDataScriptableObject>, IRootBranchDataProvider
    {
        [SerializeField] private GameObject rootBranchPrefab;
        [SerializeField] private LevelEntry[] _levels;
        
        public override void InstallBindings()
        {
            Container.BindInstance<IRootBranchDataProvider>(this);
            Container.BindMemoryPool<RootBranch, RootBranch.Pool>().WithInitialSize(50)
                .FromComponentInNewPrefab(rootBranchPrefab).AsSingle();
        }

        public float GetLifetime(int level) => _levels[level].Lifetime;

        public float GetVelocity(int level) => _levels[level].Velocity;

        public float GetBranchOutInterval(int level) => _levels[level].BranchOutInterval;

        public float GetBranchOutAngle(int level) => _levels[level].BranchOutAngle;

        [Serializable]
        private struct LevelEntry
        {
            public float Lifetime;
            public float Velocity;
            public float BranchOutInterval;
            public float BranchOutAngle;
        }
    }
}