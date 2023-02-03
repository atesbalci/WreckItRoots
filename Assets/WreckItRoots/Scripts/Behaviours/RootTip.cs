using UnityEngine;
using WreckItRoots.Models;

namespace WreckItRoots.Behaviours
{
    public class RootTip : MonoBehaviour, IRootTip
    {
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float velocityMultiplier;
        [SerializeField] private AnimationCurve maneuverSpeedCurve;
        [SerializeField] private float maneuverSpeedMultiplier;
        
        public Vector2 Position => transform.position;
        public PlantState State { get; private set; }
        public float Angle { get; private set; }

        private float _maneuverDirection;
        private float _lastRootTime;
        
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

        private void Update()
        {
            if (State == PlantState.Root)
            {
                Angle += _maneuverDirection * ManeuverSpeed * Time.deltaTime;
                _maneuverDirection = 0;
                var angleRad = Angle * Mathf.Deg2Rad;
                var effectiveVelocity = Velocity * Time.deltaTime;
                transform.position += new Vector3(Mathf.Sin(angleRad) * effectiveVelocity,
                    -Mathf.Cos(angleRad) * effectiveVelocity, 0f);
                if (Position.y > 0.001f)
                {
                    Surface();
                }
            }
        }

        public float Velocity
        {
            get
            {
                if (State != PlantState.Root) return 0f;
                return velocityCurve.Evaluate(Time.time - _lastRootTime) * velocityMultiplier;
            }
        }

        private float ManeuverSpeed
        {
            get
            {
                if (State != PlantState.Root) return 0f;
                return maneuverSpeedCurve.Evaluate(Time.time - _lastRootTime) * maneuverSpeedMultiplier;
            }
        }

        private void Surface()
        {
            var pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            State = PlantState.Tree;
            Angle = 0;
        }
    }
}