using TMPro;
using UnityEngine;
using WreckItRoots.Models;

namespace WreckItRoots.Views
{
    public class BuildingView : MonoBehaviour
    {
        private const float MomentumResistanceHeightMultiplier = 0.15f;
        
        [SerializeField] private Transform modelPivot;
        [SerializeField] private GameObject infoObject;
        [SerializeField] private TMP_Text momentumResistanceText;
        [SerializeField] private TMP_Text bioEnergyText;
        [SerializeField] private BreakableObject[] breakableObjects;

        private IBuilding _building;
        private BreakableObject _currentBuilding;
        
        private void Start()
        {
            _building = GetComponent<IBuilding>();
            OnInitialized();
            _building.Initialized += OnInitialized;
            _building.Wrecked += OnWrecked;

            var buildingIndex = Random.Range(0, breakableObjects.Length);
            for (int i = 0; i < breakableObjects.Length; i++)
            {
                breakableObjects[i].gameObject.SetActive(buildingIndex == i);
            }

            _currentBuilding = breakableObjects[buildingIndex];
            _currentBuilding.ResetPieces();
            infoObject.SetActive(true);
        }

        private void OnWrecked()
        {
            _currentBuilding.Break();
            infoObject.SetActive(false);
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