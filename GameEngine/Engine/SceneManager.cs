using System.Collections.Generic;
using GameEngine.Engine.Interface;

namespace GameEngine.Engine
{
    public class SceneManager
    {
        private static SceneManager _instance;
        public static SceneManager Instance => _instance ?? (_instance = new SceneManager());

        private readonly List<IScene> _scenes = new List<IScene>();

        private IScene CurrentScene { get; set; }
        
        public void InitializeGame(SceneId sceneIdId)
        {
            ChangeScene(sceneIdId);
        }

        public void AddScene(IScene sceneAdd)
        {
            _scenes.Add(sceneAdd);
        }

        public void Update()
        {
            CurrentScene.Update();
        }
        
        public void ChangeScene(SceneId id)
        {
            var scene = GetScene(id);

            if (scene != null)
            {
                EngineController.World.RemoveAllBody();
                EngineController.RemoveAllGameObject();
                EngineController.RemoveAllSpriteRender();
                CurrentScene = scene;
                CurrentScene.Initialize();
                Debug.Info($"Scene change made: Changed to {CurrentScene.Id}");
            }
        }

        private IScene GetScene(SceneId id)
        {
            for (var i = 0; i < _scenes.Count; i++)
            {
                if (_scenes[i].Id == id)
                {
                    return _scenes[i];
                }
            }
            return null;
        }
    }
}