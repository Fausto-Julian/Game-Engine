using System.Windows.Forms;
using GameEngine.Engine;
using GameEngine.Engine.PhysicsEngine;

namespace GameEngine
{
    public class PlayerController : GameObject
    {
        private readonly Body _checkGround;
        private Vector2 _desiredSpeed = Vector2.Zero;
        private Vector2 _differenceSpeed = Vector2.Zero;
        private Vector2 _force = Vector2.Zero;
        private readonly Vector2 _speed;
        
        private readonly float _moveForce;
        private readonly float _maxForce;

        private bool _jump;

        private readonly Body _body;
        
        public PlayerController(string id, string pathImage, Vector2 startPosition, Vector2 scale, Vector2 speed, float moveForce, float maxForce, TypeCollision typeCollision) 
            : base(id, pathImage, 2, startPosition, scale, typeCollision, 1f)
        {
            _checkGround = Body.CreateBoxBody(this, Transform, RealSize + new Vector2(0f, 2f), 1f, 1f, 0f, true, false, true);
            EngineController.World.AddBody(_checkGround);
            _checkGround.OnTrigger += OnTriggerGround;

            _body = GetComponent<Body>();
            
            _speed = speed;
            
            _moveForce = moveForce;
            _maxForce = maxForce;

            _jump = false;
        }

        private void OnTriggerGround(GameObject obj)
        {
            if (obj.CompareTag("Floor"))
            {
                _jump = true;
            }
        }

        public override void Update()
        {

            if (Input.GetKeyPress(Keys.D) || Input.GetKeyPress(Keys.A) || Input.GetKeyPress(Keys.W) || Input.GetKeyPress(Keys.S))
            {
                if (Input.GetKeyPress(Keys.D))
                {
                    _desiredSpeed.X = _speed.X;
                }

                if (Input.GetKeyPress(Keys.A))
                {
                    _desiredSpeed.X = -_speed.X;
                }
            }
            else
            {
                _desiredSpeed = new Vector2(0f, _desiredSpeed.Y);
            }
            
            _differenceSpeed = new Vector2(_desiredSpeed.X - _body.LinearVelocity.X, _desiredSpeed.Y);
            
            _force = new Vector2(Mathf.Clamp(_differenceSpeed.X * _moveForce, -_maxForce, _maxForce), Mathf.Clamp(_differenceSpeed.Y * _moveForce, -_maxForce, _maxForce));
            
            if (Input.GetKeyDown(Keys.W) && _jump)
            {
                _force += Vector2.Up * -50f;
                _jump = false;
            }
            
            _body.LinearVelocity += _force;
            
            base.Update();
        }

        public void Death()
        {
            EngineController.World.RemoveBody(_checkGround);
            Destroy();
        }
    }
}