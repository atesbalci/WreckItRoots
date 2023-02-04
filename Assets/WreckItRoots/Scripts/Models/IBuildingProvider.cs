using System.Collections.Generic;

namespace WreckItRoots.Models
{
    public interface IBuildingProvider
    {
        public Stack<IBuilding> Buildings { get; }
    }
}