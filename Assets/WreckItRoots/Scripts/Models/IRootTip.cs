namespace WreckItRoots.Models
{
    public interface IRootTip : IWorldObject
    {
        PlantState State { get; }
        float Velocity { get; }
        float Angle { get; }

        void Maneuver(float direction);
        void RootDown();
    }

    public enum PlantState
    {
        Tree,
        Root
    }
}