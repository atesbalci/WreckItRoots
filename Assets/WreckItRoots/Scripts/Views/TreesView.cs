using WreckItRoots.Models;

namespace WreckItRoots.Views
{
    public class TreesView
    {
        private readonly TreeView.Pool _treePool;
        private readonly IRootTip _rootTip;

        public TreesView(TreeView.Pool treePool, IRootTip rootTip)
        {
            _treePool = treePool;
            _rootTip = rootTip;
            _rootTip.StateChanged += OnStateChanged;
            OnStateChanged(rootTip.State, rootTip.State);
        }

        private void OnStateChanged(PlantState oldState, PlantState newState)
        {
            if (newState == PlantState.Tree)
            {
                _treePool.Spawn().Initialize(_rootTip.Position.x, _rootTip.RootMomentum);
            }
        }
    }
}