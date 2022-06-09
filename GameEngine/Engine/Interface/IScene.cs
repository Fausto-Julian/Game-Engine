namespace GameEngine.Engine.Interface
{
    public enum SceneId
    {
        Menu,
        Credit,
        Level,
        Level2,
        Level3,
        Defeat,
        Victory,
        Test
    }
    
    public interface IScene
    {
        SceneId Id { get; }

        void Initialize();

        void Update();
    }
}