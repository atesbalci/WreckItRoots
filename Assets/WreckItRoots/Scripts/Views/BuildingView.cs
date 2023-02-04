using TMPro;
using UnityEngine;
using WreckItRoots.Models;

namespace WreckItRoots.Views
{
    public class BuildingView : MonoBehaviour
    {
        private const float MomentumResistanceHeightMultiplier = 0.15f;
        
        [SerializeField] private Transform modelPivot;
        [SerializeField] private TMP_Text momentumResistanceText;
        [SerializeField] private TMP_Text bioEnergyText;
        
        private IBuilding _building;
        
        private void Start()
        {
            _building = GetComponent<IBuilding>();
            OnInitialized();
            _building.Initialized += OnInitialized;
        }

        private void OnInitialized()
        {
            modelPivot.localScale =
                new Vector3(_building.Width, MomentumResistanceHeightMultiplier * _building.MomentumResistance, 1f);
            momentumResistanceText.text = Mathf.RoundToInt(_building.MomentumResistance).ToString();
            bioEnergyText.text = Mathf.RoundToInt(_building.BioEnergyReward).ToString();
        }
    }
}