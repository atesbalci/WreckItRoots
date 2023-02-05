using UnityEngine;
using WreckItRoots.Behaviours;
using WreckItRoots.Models;
using WreckItRoots.Views;
using Zenject;

namespace WreckItRoots.Injection
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private RootTip rootTip;
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private AudioManager audioManager;

        public override void InstallBindings()
        {
            Container.BindInstance<IRootTip>(rootTip);
            Container.Bind(typeof(IBuildingProvider), typeof(ITickable)).To<LevelGenerator>().AsSingle().NonLazy();
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.BindMemoryPool<TreeView, TreeView.Pool>().WithInitialSize(20)
                .FromComponentInNewPrefab(treePrefab);
            Container.Bind<TreesView>().AsSingle().NonLazy();
            Container.BindInstance<IAudioPlayer>(audioManager).AsSingle();
        }
    }
}