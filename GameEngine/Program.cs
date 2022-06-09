using GameEngine.Engine;

namespace GameEngine
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Initialize();
        }

        private static void Initialize()
        {
            var engine = new EngineController(new Vector2(1920, 1080), "Hola");
        }
    }
}