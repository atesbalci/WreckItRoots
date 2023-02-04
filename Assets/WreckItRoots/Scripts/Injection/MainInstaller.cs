using UnityEngine;
using WreckItRoots.Behaviours;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Injection
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private RootTip _rootTip;

        public override void InstallBindings()
        {
            Container.BindInstance<IRootTip>(_rootTip);
            Container.Bind(typeof(IBuildingProvider), typeof(ITickable)).To<LevelGenerator>().AsSingle().NonLazy();
        }
    }
}