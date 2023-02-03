namespace WreckItRoots.Models
{
    public interface IRootDataProvider
    {
        float DefaultRootLifetime { get; }
        
        float GetVelocity(float lifetime);
        float GetManeuverSpeed(float lifetime);
        float GetRootMomentum(float maxDepth);
    }
}