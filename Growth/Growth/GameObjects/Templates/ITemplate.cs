using Growth.GameObjects.Entities;

namespace Growth.GameObjects.Templates
{
    public interface ITemplate
    {
        Entity Make();
    }
}