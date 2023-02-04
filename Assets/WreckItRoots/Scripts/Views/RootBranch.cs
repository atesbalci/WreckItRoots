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
        private Vector3[] _vertexes;
        private float _birthTime;
        private Pool _pool;

        public void Initialize(int level, Vector2 position, float angle, Pool pool)
        {
            _level = level;
            _pool = pool;
            _birthTime = Time.time;
            _angle = angle;
            _lineRenderer = GetComponent<LineRenderer>();
            StopAllCoroutines();
            _vertexes = new Vector3[] { position, position };
            _lineRenderer.SetPositions(_vertexes);
            StartCoroutine(UpdateCoroutine());
            if (_rootBranchDataProvider.GetBranchOutInterval(_level) > 0.001f)
            {
                StartCoroutine(BranchOutCoroutine());
            }
        }

        private IEnumerator UpdateCoroutine()
        {
            while (Time.time - _birthTime < _rootBranchDataProvider.GetLifetime(_level))
            {
                _vertexes[^1] += _angle.ToRootAngularDirection() *
                                 (_rootBranchDataProvider.GetVelocity(_level) * Time.deltaTime);
                _lineRenderer.SetPositions(_vertexes);
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
                               (Random.value > 0.5f ? 1 : -1) * _rootBranchDataProvider.GetBranchOutAngle(_level);
                _pool.Spawn().Initialize(_level + 1, _vertexes[^1], newAngle, _pool);
            }
        }

        public class Pool : MonoMemoryPool<RootBranch> { }
    }

    public interface IRootBranchDataProvider
    {
        float GetLifetime(int level);
        float GetVelocity(int level);
        float GetBranchOutInterval(int level);
        float GetBranchOutAngle(int level);
    }
}