using UnityEngine;
using WreckItRoots.Behaviours;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views
{
    public class RootTipView : MonoBehaviour
    {
        private const float TipLength = 0.1f;
        
        private LineRenderer _lineRenderer;
        private IRootTip _rootTip;
        
        [Inject]
        public void Initialize(IRootTip rootTip)
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _rootTip = rootTip;
        }

        private void Update()
        {
            _lineRenderer.enabled = _rootTip.State == PlantState.Root;
            if (_lineRenderer.enabled)
            {
                var vectorDir = _rootTip.Angle.ToRootAngularDirection() * TipLength;
                _lineRenderer.SetPosition(0, -vectorDir);
                _lineRenderer.SetPosition(1, vectorDir);
            }
        }
    }
}