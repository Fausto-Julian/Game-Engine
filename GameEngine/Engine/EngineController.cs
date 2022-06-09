using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GameEngine.Engine.PhysicsEngine;
using GameEngine.Engine.Interface;
using Color = System.Drawing.Color;

namespace GameEngine.Engine
{
    public class Canvas : Form
    {
        public Canvas()
        {
            DoubleBuffered = true;
        }
    }

    public class EngineController
    {
        public static float DeltaTime { get; private set; }
        public static float RealDeltaTime { get; private set; }
        public static int ScaleTime
        {
            get => _scaleTime;
            set
            {
                if (value < 0) _scaleTime = 0;
                else if (value > 1) _scaleTime = 1;
                else _scaleTime = value;
            }
        }

        private static int _scaleTime = 1;

        public static Vector2 ScreenSize { get; private set; }
        public static Canvas Windows = null;

        public static World World = null;
        
        private readonly Thread _gameLoopThread = null;

        private static List<GameObject> _gameObjects = null;

        private static List<SpriteRenderer> _renderers = null;
        private static DateTime _startTime;
        private static float _lastFrameTime;

        public EngineController(Vector2 screenSize, string title)
        {
            ScreenSize = screenSize;
            Windows = new Canvas();
            Windows.Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y);
            Windows.StartPosition = FormStartPosition.Manual;
            Windows.Text = title;
            Windows.Paint += Renderer;

            World = new World();
            _gameObjects = new List<GameObject>();
            _renderers = new List<SpriteRenderer>();
            
            Input.Initialize();
            
            var testScene = new TestScene();
            
            SceneManager.Instance.AddScene(testScene);
            
            SceneManager.Instance.InitializeGame(SceneId.Test);
            
            _gameLoopThread = new Thread(GameLoop);
            _gameLoopThread.SetApartmentState(ApartmentState.STA);
            _gameLoopThread.Start();

            Application.Run(Windows);
            
            Debug.Info("Game Engine Initialize.");
        }
        
        public static GameObject FindGameObjectWithTag(string id)
        {
            for (var i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].CompareTag(id))
                {
                    return _gameObjects[i];
                }
            }

            return null;
        }
        
        public static void AddGameObject(GameObject gameObject)
        {
            if (gameObject != null)
            {
                _gameObjects.Add(gameObject);
                Debug.Info($"{gameObject.Id} object was added successfully.");
            }
            else
            {
                Debug.Error("The object you want to add is null");
            }
        }
        
        public static void RemoveGameObject(GameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
            {
                _gameObjects.Remove(gameObject);
            }
            else
            {
                Debug.Error("This Game Object does not exist");
            }
        }
        
        public static void RemoveAllGameObject()
        {
            for (var i = _gameObjects.Count - 1; i > 0 ; i--)
            {
                if (!_gameObjects[i].DontDestroyOnLoad)
                {
                    _gameObjects.RemoveAt(i);
                }
            }
        }
        
        public static void AddSpriteRender(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer != null)
            {
                _renderers.Add(spriteRenderer);
                _renderers = _renderers.OrderBy(x => x.Layer).ToList();
                Debug.Info($"{spriteRenderer.GameObject?.Id} Sprite Render was added successfully.");
            }
            else
            {
                Debug.Error("The Sprite Render you want to add is null");
            }
        }
        
        public static void RemoveSpriteRender(SpriteRenderer spriteRenderer)
        {
            if (_renderers.Contains(spriteRenderer))
            {
                _renderers.Remove(spriteRenderer);
            }
            else
            {
                Debug.Error("This Sprite Render does not exist");
            }
        }
        
        public static void RemoveAllSpriteRender()
        {
            for (var i = _renderers.Count - 1; i > 0; i--)
            {
                if (_renderers[i].GameObject != null)
                {
                    if (!_renderers[i].GameObject.DontDestroyOnLoad)
                    {
                        _renderers.RemoveAt(i);
                    }
                }
                else
                {
                    _renderers.RemoveAt(i);
                }
            }
        }

        private void GameLoop()
        {
            _startTime = DateTime.Now;
            while (_gameLoopThread.IsAlive)
            {
                var currentTime = (float)(DateTime.Now - _startTime).TotalSeconds;
                DeltaTime = currentTime - _lastFrameTime;
                RealDeltaTime = DeltaTime;
                DeltaTime *= _scaleTime;
                
                try
                {
                    Windows.BeginInvoke((MethodInvoker)delegate { Windows.Refresh(); });
                }
                catch
                {
                    Debug.Error("The engine is not initialized.");
                }
                
                SceneManager.Instance.Update();
                
                for (var i = 0; i < _gameObjects.Count; i++)
                {
                    if (_gameObjects[i].IsActive)
                    {
                        _gameObjects[i].Update();
                    }
                }
                
                World.Step(DeltaTime, 128);

                
                Input.Update();
                _lastFrameTime = currentTime;
                Thread.Sleep(1);
            }
        }

        private static void Renderer(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            
            g.Clear(Color.Black);

            for (var i = 0; i < _renderers.Count; i++)
            {
                _renderers[i].Render(ref g);
            }
        }
    }
}