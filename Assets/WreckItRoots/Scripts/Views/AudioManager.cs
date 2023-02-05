using System;
using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        
        [Inject]
        public void Initialize(IRootTip rootTip)
        {
            rootTip.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(PlantState oldState, PlantState newState)
        {
            switch (newState)
            {
                case PlantState.Root:
                    musicSource.Play();
                    break;
                default:
                    musicSource.Pause();
                    break;
                    
            }
        }
    }
}