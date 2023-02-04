using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Injection
{
    [CreateAssetMenu]
    public class RootDataScriptableObject : ScriptableObjectInstaller<RootDataScriptableObject>, IRootDataProvider
    {
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float velocityMultiplier;
        [SerializeField] private float velocityGainTimespan;
        [SerializeField] private AnimationCurve maneuverSpeedCurve;
        [SerializeField] private float maneuverSpeedMultiplier;
        [SerializeField] private float maneuverabilityGainTimespan;
        [SerializeField] private float rootMomentumPerDepth;
        [SerializeField] private float defaultRootLifetime;
        
        public override void InstallBindings()
        {
            Container.BindInstance<IRootDataProvider>(this);
        }

        public float DefaultRootLifetime => defaultRootLifetime;

        public float GetVelocity(float lifetime)
        {
            return velocityCurve.Evaluate(lifetime / velocityGainTimespan) * velocityMultiplier;
        }

        public float GetManeuverSpeed(float lifetime)
        {
            return maneuverSpeedCurve.Evaluate(lifetime / maneuverabilityGainTimespan) * maneuverSpeedMultiplier;
        }

        public float GetRootMomentum(float maxDepth)
        {
            return maxDepth * rootMomentumPerDepth;
        }
    }
}