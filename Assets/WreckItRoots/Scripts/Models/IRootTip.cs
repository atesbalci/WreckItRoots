namespace WreckItRoots.Models
{
    public interface IRootTip : IWorldObject
    {
        PlantState State { get; }
        float Angle { get; }
        float RootMomentum { get; }
        float RootLifetime { get; }

        void Maneuver(float direction);
        void RootDown();
        void PickUpExtraLifetime(float amount);
    }

    public enum PlantState
    {
        Tree,
        Root,
        Dead
    }
}