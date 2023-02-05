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
            if (newState == PlantState.Root && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }
}