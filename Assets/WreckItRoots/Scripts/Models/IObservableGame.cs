using System;

namespace WreckItRoots.Models
{
    public interface IObservableGame
    {
        event Action<IBuilding> BuildingWrecked;
        event Action RootSurfaced;
    }
}