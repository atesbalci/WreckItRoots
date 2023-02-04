namespace WreckItRoots.Models
{
    public delegate void PlantStateChangeEvent(PlantState oldState, PlantState newState);
    
    public interface IRootTip : IWorldObject
    {
        event PlantStateChangeEvent StateChanged;
        PlantState State { get; }
        float Angle { get; }
        float RootMomentum { get; }
        float RootLifetime { get; }
        float TotalRootLifetime { get; }

        void Maneuver(float direction);
        void RootDown();
        void PickUpExtraLifetime(float amount);
        void Die();
    }

    public enum PlantState
    {
        Tree,
        Root,
        Dead
    }
}