using System.Drawing;

namespace GameEngine.Engine
{

    public class Sprite
    {
        public int Width { get; }
        public int Height { get; }
        
        public Image Image { get; }

        public Sprite(string path)
        {
            Image = Image.FromFile($"Texture/{path}.png");
            
            Width = Image.Width;
            Height = Image.Height;
        }
    }

    public class SpriteRenderer : Component
    {
        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                _bitmap = new Bitmap(_sprite.Image);
            }
        }

        public int Layer { get; }

        private Bitmap _bitmap;
        
        private readonly Transform _transform;
        private Sprite _sprite;

        public SpriteRenderer(GameObject gameObject, string path, int layer = 0)
            : base(gameObject)
        {
            Sprite = new Sprite(path);

            _bitmap = new Bitmap(Sprite.Image);

            _transform = GameObject.Transform;

            Layer = layer;
            
            EngineController.AddSpriteRender(this);
        }
        
        public SpriteRenderer(string path, Transform transform, int layer = 0)
            : base(null)
        {
            Sprite = new Sprite(path);

            _bitmap = new Bitmap(Sprite.Image);

            _transform = transform;
            
            Layer = layer;
            
            EngineController.AddSpriteRender(this);
        }
        
        public SpriteRenderer(GameObject gameObject, Sprite sprite, int layer = 0)
            : base(gameObject)
        {
            Sprite = sprite;

            _bitmap = new Bitmap(Sprite.Image);

            _transform = GameObject.Transform;
            
            Layer = layer;
            
            EngineController.AddSpriteRender(this);
        }
        
        public SpriteRenderer(Sprite sprite, Transform transform, int layer = 0)
            : base(null)
        {
            Sprite = sprite;

            _bitmap = new Bitmap(Sprite.Image);

            _transform = transform;
            
            Layer = layer;
            
            EngineController.AddSpriteRender(this);
        }
        
        public void Render(ref Graphics graphics)
        {
            if (GameObject != null)
            {
                if (GameObject.IsActive)
                {
                    graphics.DrawImage(_bitmap, _transform.Position.X, _transform.Position.Y, Sprite.Width * _transform.Scale.X, Sprite.Height * _transform.Scale.Y);
                }
            }
            else
            {
                graphics.DrawImage(_bitmap, _transform.Position.X, _transform.Position.Y, Sprite.Width * _transform.Scale.X, Sprite.Height * _transform.Scale.Y);
            }
        }
    }
}