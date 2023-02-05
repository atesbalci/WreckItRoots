using WreckItRoots.Models;

namespace WreckItRoots.Behaviours
{
    public class GameManager
    {
        private readonly IRootTip _rootTip;
        private readonly IBuildingProvider _buildingProvider;

        public GameManager(IRootTip rootTip, IBuildingProvider buildingProvider)
        {
            _rootTip = rootTip;
            _buildingProvider = buildingProvider;
            _rootTip.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(PlantState oldState, PlantState newState)
        {
            if (newState == PlantState.Surfacing)
            {
                foreach (var building in _buildingProvider.Buildings)
                {
                    // We're already past this building, no need to check the previous ones
                    if (_rootTip.Position.x > building.Position.x + building.Width * 0.5f)
                    {
                        break;
                    }
                    // We're under a building
                    else if (_rootTip.Position.x > building.Position.x - building.Width * 0.5f)
                    {
                        WreckOrFail(building);
                        return;
                    }
                }
                
                _rootTip.Surface();
            }
        }

        private void WreckOrFail(IBuilding building)
        {
            if (_rootTip.RootMomentum > building.MomentumResistance)
            {
                building.Wreck();
                _rootTip.PickUpExtraLifetime(building.BioEnergyReward);
                _rootTip.Surface();
            }
            else
            {
                _rootTip.Die();
            }
        }
    }
}