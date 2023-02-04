using UnityEngine;
using WreckItRoots.Models;

namespace WreckItRoots.Views
{
    public class BuildingView : MonoBehaviour
    {
        private const float MomentumResistanceHeightMultiplier = 0.25f;
        
        [SerializeField] private Transform modelPivot;
        
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
        }
    }
}