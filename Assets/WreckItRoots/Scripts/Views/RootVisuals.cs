using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        private Vector3[] _vectorArray;
        private IList<Vector3> _vectorList;
        private float _lastBranchOutTime;

        private float _branchOutInterval;
        private float _branchOutAngle;

        [Inject]
        public void Initialize(IRootTip rootTip, IRootBranchDataProvider rootBranchDataProvider, RootBranch.Pool branchPool)
        {
            _rootTip = rootTip;
            _branchPool = branchPool;
            _branchOutInterval = rootBranchDataProvider.GetBranchOutInterval(0);
            _branchOutAngle = rootBranchDataProvider.GetBranchOutAngle(0);
            _lineRenderer = GetComponent<LineRenderer>();
            _vectorList = new List<Vector3>();
            _vectorList.Add(_rootTip.Position);
            StartCoroutine(SegmentPlacementRoutine());
        }

        private void Update()
        {
            if (_rootTip.State == PlantState.Root)
            {
                _vectorList[^1] = _rootTip.Position;
                _vectorArray[^1] = _rootTip.Position;
                _lineRenderer.SetPositions(_vectorArray);
                if (Time.time - _lastBranchOutTime > _branchOutInterval)
                {
                    _lastBranchOutTime = Time.time;
                    var newAngle = _rootTip.Angle + (Random.value > 0.5f ? 1 : -1) * _branchOutAngle;
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
                _vectorList.Add(_rootTip.Position);
                _vectorArray = _vectorList.ToArray();
                _lineRenderer.positionCount = _vectorArray.Length;
                _lineRenderer.SetPositions(_vectorArray);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}