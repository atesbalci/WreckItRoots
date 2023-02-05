using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views.UI
{
    public class FinalUIView : MonoBehaviour
    {
        [SerializeField] private Slider bioEnergyBar;
        [SerializeField] private TMP_Text bioEnergyText;
        [SerializeField] private TMP_Text momentumText;
        [SerializeField] private TMP_Text screenPrompt;
        
        private IRootTip _rootTip;

        [Inject]
        public void Initialize(IRootTip rootTip)
        {
            _rootTip = rootTip;
            _rootTip.StateChanged += OnStateChanged;
            OnStateChanged(_rootTip.State, _rootTip.State);
        }

        private void OnStateChanged(PlantState oldState, PlantState newState)
        {
            screenPrompt.gameObject.SetActive(newState != PlantState.Root);
            switch (newState)
            {
                case PlantState.Tree:
                    screenPrompt.text = "Press Space to root down";
                    break;
                case PlantState.Dead:
                    screenPrompt.text = "Game Over\nPress R to Restart";
                    break;
            }
        }

        private void Update()
        {
            var remainingBioEnergy = _rootTip.TotalRootLifetime - _rootTip.RootLifetime;
            bioEnergyBar.value = remainingBioEnergy / _rootTip.TotalRootLifetime;
            bioEnergyText.text = remainingBioEnergy.ToString("F1");
            momentumText.text = _rootTip.RootMomentum.ToString("F1");
        }
    }
}