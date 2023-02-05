using System;
using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views
{
    public class RootMomentumVisualizer : MonoBehaviour
    {
        private const float FeedbackInterval = 10f;
        
        private ParticleSystem _particles;
        private IRootTip _rootTip;

        private float _lastFeedbackMomentum;

        [Inject]
        public void Initialize(IRootTip rootTip)
        {
            _rootTip = rootTip;
            _particles = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_rootTip.RootMomentum < _lastFeedbackMomentum)
            {
                _lastFeedbackMomentum = 0f;
            }
            else if (_rootTip.RootMomentum - _lastFeedbackMomentum > FeedbackInterval)
            {
                _lastFeedbackMomentum = _rootTip.RootMomentum;
                _particles.Stop();
                _particles.Play();
            }
        }
    }
}