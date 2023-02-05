using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views
{
    public class AudioManager : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource[] treeSoundSources;
        [SerializeField] private AudioSource buzzSoundSource;
        
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
                case PlantState.Tree:
                    musicSource.Pause();
                    foreach (var source in treeSoundSources)
                    {
                        source.Play();
                    }
                    break;
                default:
                    musicSource.Stop();
                    break;
            }
        }

        public void PlayBuzzSound()
        {
            buzzSoundSource.Stop();
            buzzSoundSource.Play();
        }
    }

    public interface IAudioPlayer
    {
        void PlayBuzzSound();
    }
}