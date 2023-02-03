using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Behaviours
{
    public class RootTip : MonoBehaviour, IRootTip
    {
        public Vector2 Position => transform.position;
        public PlantState State { get; private set; }
        public float Angle { get; private set; }
        public float RootMomentum => _rootDataProvider.GetRootMomentum(_maxDepth);
        public float RootLifetime => Time.time - _lastRootTime;
        public float TotalRootLifetime { get; private set; }

        private IRootDataProvider _rootDataProvider;
        private float _maneuverDirection;
        private float _lastRootTime;
        private float _maxDepth;

        [Inject]
        public void Initialize(IRootDataProvider rootDataProvider)
        {
            _rootDataProvider = rootDataProvider;
            Surface();
        }
        
        public void Maneuver(float direction)
        {
            _maneuverDirection = direction;
        }

        public void RootDown()
        {
            if (State == PlantState.Tree)
            {
                State = PlantState.Root;
                _lastRootTime = Time.time;
            }
        }

        public void PickUpExtraLifetime(float amount)
        {
            TotalRootLifetime += amount;
        }

        private void Update()
        {
            if (State == PlantState.Root)
            {
                Angle += _maneuverDirection * _rootDataProvider.GetManeuverSpeed(RootLifetime) * Time.deltaTime;
                _maneuverDirection = 0;
                var angleRad = Angle * Mathf.Deg2Rad;
                var effectiveVelocity = _rootDataProvider.GetVelocity(RootLifetime) * Time.deltaTime;
                transform.position += new Vector3(Mathf.Sin(angleRad) * effectiveVelocity,
                    -Mathf.Cos(angleRad) * effectiveVelocity, 0f);
                _maxDepth = Mathf.Max(_maxDepth, -transform.position.y);
                if (Position.y > 0.001f)
                {
                    Surface();
                }
                else if (RootLifetime > TotalRootLifetime)
                {
                    Die();
                }
            }
        }

        private void Surface()
        {
            var pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            State = PlantState.Tree;
            Angle = 0;
            _maxDepth = 0;
            TotalRootLifetime = _rootDataProvider.DefaultRootLifetime;
        }

        private void Die()
        {
            State = PlantState.Dead;
        }
    }
}