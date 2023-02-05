using System.Collections;
using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views
{
    public class RootVisuals : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private IRootTip _rootTip;
        private RootBranch.Pool _branchPool;
        
        private float _lastBranchOutTime;

        private float _branchOutInterval;
        private float _branchOutAngle;
        private float _maxBranchOutHeight;
        private int _branchCount;

        [Inject]
        public void Initialize(IRootTip rootTip, IRootBranchDataProvider rootBranchDataProvider, RootBranch.Pool branchPool)
        {
            _rootTip = rootTip;
            _branchPool = branchPool;
            _branchOutInterval = rootBranchDataProvider.GetBranchOutInterval(0);
            _branchOutAngle = rootBranchDataProvider.GetBranchOutAngle(0);
            _maxBranchOutHeight = rootBranchDataProvider.MaxBranchOutHeight;
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, _rootTip.Position);
            _lineRenderer.positionCount = 1;
            StartCoroutine(SegmentPlacementRoutine());
        }

        private void Update()
        {
            if (_rootTip.State == PlantState.Root)
            {
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _rootTip.Position);
                if (Time.time - _lastBranchOutTime > _branchOutInterval && _rootTip.Position.y < _maxBranchOutHeight)
                {
                    _lastBranchOutTime = Time.time;
                    var newAngle = _rootTip.Angle + (_branchCount++ % 2 == 0 ? 1 : -1) * _branchOutAngle;
                    _branchPool.Spawn().Initialize(1, _rootTip.Position, newAngle, _branchPool);
                }
            }
            else
            {
                _lastBranchOutTime = Time.time;
            }
        }

        private IEnumerator SegmentPlacementRoutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => _rootTip.State == PlantState.Root);
                _lineRenderer.SetPosition((++_lineRenderer.positionCount) - 1, _rootTip.Position);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}