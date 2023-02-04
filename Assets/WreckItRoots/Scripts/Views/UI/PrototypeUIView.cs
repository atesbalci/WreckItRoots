using TMPro;
using UnityEngine;
using WreckItRoots.Models;
using Zenject;

namespace WreckItRoots.Views.UI
{
    public class PrototypeUIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text bioEnergyText;
        [SerializeField] private TMP_Text rootMomentumText;
        
        private IRootTip _rootTip;

        [Inject]
        public void Initialize(IRootTip rootTip)
        {
            _rootTip = rootTip;
        }

        private void Update()
        {
            bioEnergyText.text = (_rootTip.TotalRootLifetime - _rootTip.RootLifetime).ToString("F1");
            rootMomentumText.text = _rootTip.RootMomentum.ToString("F1");
        }
    }
}