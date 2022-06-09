using GameEngine.Engine;

namespace GameEngine
{
    public abstract class Component
    {
        public GameObject GameObject { get; }
        protected Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}