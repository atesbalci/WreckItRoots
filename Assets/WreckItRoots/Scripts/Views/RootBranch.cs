using System;
using System.Collections;
using UnityEngine;
using WreckItRoots.Behaviours;
using Zenject;
using Random = UnityEngine.Random;

namespace WreckItRoots.Views
{
    public class RootBranch : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        [Inject] private IRootBranchDataProvider _rootBranchDataProvider;
        
        private int _level;
        private float _angle;
        private float _birthTime;
        private Pool _pool;
        private int _branchCount;

        public void Initialize(int level, Vector2 position, float angle, Pool pool)
        {
            _level = level;
            _pool = pool;
            _birthTime = Time.time;
            _angle = angle;
            _branchCount = 0;
            _lineRenderer = GetComponent<LineRenderer>();
            StopAllCoroutines();
            _lineRenderer.SetPositions(new Vector3[] { position, position });
            StartCoroutine(UpdateCoroutine());
            if (_rootBranchDataProvider.GetBranchOutInterval(_level) > 0.001f)
            {
                StartCoroutine(BranchOutCoroutine());
            }
        }

        private IEnumerator UpdateCoroutine()
        {
            while (Time.time - _birthTime < _rootBranchDataProvider.GetLifetime(_level) &&
                   _lineRenderer.GetPosition(1).y < _rootBranchDataProvider.MaxBranchOutHeight)
            {
                _lineRenderer.SetPosition(1,
                    _lineRenderer.GetPosition(1) + _angle.ToRootAngularDirection() *
                    (_rootBranchDataProvider.GetVelocity(_level) * Time.deltaTime));
                yield return null;
            }
            
            Die();
        }

        private void Die()
        {
            StopAllCoroutines();
        }

        private IEnumerator BranchOutCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_rootBranchDataProvider.GetBranchOutInterval(_level));
                var newAngle = _angle +
                               (_branchCount++ % 2 == 0 ? 1 : -1) * _rootBranchDataProvider.GetBranchOutAngle(_level);
                _pool.Spawn().Initialize(_level + 1, _lineRenderer.GetPosition(1), newAngle, _pool);
            }
        }

        public class Pool : MonoMemoryPool<RootBranch> { }
    }

    public interface IRootBranchDataProvider
    {
        float MaxBranchOutHeight { get; }
        float GetLifetime(int level);
        float GetVelocity(int level);
        float GetBranchOutInterval(int level);
        float GetBranchOutAngle(int level);
    }
}