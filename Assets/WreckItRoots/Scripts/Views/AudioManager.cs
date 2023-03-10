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
                    if (!musicSource.isPlaying)
                    {
                        musicSource.Play();
                    }

                    break;
                case PlantState.Tree:
                    foreach (var source in treeSoundSources)
                    {
                        source.Play();
                    }
                    break;
                default:
                    musicSource.Pause();
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