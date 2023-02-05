using WreckItRoots.Models;

namespace WreckItRoots.Views
{
    public class TreesView
    {
        private readonly TreeView.Pool _treePool;
        private readonly IRootTip _rootTip;

        public TreesView(TreeView.Pool treePool, IRootTip rootTip, IObservableGame observableGame)
        {
            _treePool = treePool;
            _rootTip = rootTip;
            observableGame.RootSurfaced += OnRootSurfaced;
            OnRootSurfaced();
        }

        private void OnRootSurfaced()
        {
            _treePool.Spawn().Initialize(_rootTip.Position.x, _rootTip.RootMomentum);
        }
    }
}