using System.Collections.Generic;
using GameEngine.Engine.PhysicsEngine;

namespace GameEngine.Engine
{
    public class GameObject
    {
        public string Id { get; }

        public Transform Transform { get; } = new Transform();

        public Vector2 RealSize => new Vector2(_spriteRenderer.Sprite.Width * Transform.Scale.X,
            _spriteRenderer.Sprite.Height * Transform.Scale.Y);

        public bool DontDestroyOnLoad { get; }

        public bool IsActive { get; private set; }

        public List<Component> Components { get; set; } = new List<Component>();

        private readonly SpriteRenderer _spriteRenderer;

        public GameObject(string id, string pathImage, int layer, Vector2 startPosition, Vector2 scale, float angle = 0f, bool dontDestroyOnLoad = false)
        {
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Angle = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;

            _spriteRenderer = new SpriteRenderer(this, pathImage, layer);
            
            Components.Add(_spriteRenderer);

            SetActive(true);

            EngineController.AddGameObject(this);
        }
        
        public GameObject(string id, string pathImage, int layer, Vector2 startPosition, Vector2 scale, TypeCollision typeCollision, float mass = 1f, bool isKinematic = false, bool isStatic = false, bool isTrigger = false, float angle = 0, bool dontDestroyOnLoad = false)
        {
            Id = id;
            Transform.Position = startPosition;
            Transform.Scale = scale;
            Transform.Angle = angle;
            DontDestroyOnLoad = dontDestroyOnLoad;

            _spriteRenderer = new SpriteRenderer(this, pathImage, layer);
            
            Components.Add(_spriteRenderer);
            
            switch (typeCollision)
            {
                case TypeCollision.Box:
                    Components.Add(Body.CreateBoxBody(this, Transform, RealSize, mass, 1f, 0.5f, isKinematic, isStatic,
                        isTrigger));
                    break;
                case TypeCollision.Circle:
                    Components.Add(Body.CreateCircleBody(this, Transform, RealSize.X / 2, mass, 1f, 0.5f, isKinematic,
                        isStatic, isTrigger));
                    break;
                default:
                    Components.Add(Body.CreateBoxBody(this, Transform, RealSize, mass, 1f, 0.5f, isKinematic, isStatic,
                        isTrigger));
                    break;
            }
            
            EngineController.World.AddBody(GetComponent<Body>());
            
            SetActive(true);

            EngineController.AddGameObject(this);
        }
        
        public void Destroy(GameObject gameObject)
        {
            EngineController.World.RemoveBody(gameObject.GetComponent<Body>());
            EngineController.RemoveSpriteRender(gameObject.GetComponent<SpriteRenderer>());
            EngineController.RemoveGameObject(gameObject);
        }
        
        protected void Destroy()
        {
            EngineController.World.RemoveBody(GetComponent<Body>());
            EngineController.RemoveSpriteRender(_spriteRenderer);
            EngineController.RemoveGameObject(this);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public static GameObject FindWithTag(string tag)
        { 
            return EngineController.FindGameObjectWithTag(tag);
        }

        public T GetComponent<T>()
        {
            for (var i = 0; i < Components.Count; i++)
            {
                if (Components[i] is T value)
                {
                    return value;
                }
            }

            return default;
        }
        
        public bool CompareTag(string tag)
        {
            return Id == tag;
        }

        public virtual void Update()
        {
            
        }
    }
}