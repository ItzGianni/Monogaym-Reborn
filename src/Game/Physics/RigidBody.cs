using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    enum RigidBodyType { Player = 1, PlayerBullet = 2, Enemy = 4, EnemyBullet = 8, Item = 16 }

    class RigidBody {
        protected uint collisionMask;

        public GameObject GameObject;   // Owner

        public bool IsGravityAffected;
        public bool IsCollisionAffected = true;
        public bool IsGrounded;
        public bool IsActive { get { return GameObject.Enabled; } }

        public Point relPos;

        public int maxSpeed;
        public Point velocity;

        public Point Position { get { return GameObject.Position; } set { GameObject.Position = value; } }

        public RigidBodyType Type;

        public RigidBody(GameObject owner) {
            GameObject = owner;
            PhysicsManager.AddItem(this);
            relPos = Position;
        }

        public void Update(GameTime gameTime) {
            if (IsGravityAffected && !IsGrounded) {
                velocity.Y += (int)PhysicsManager.G;
            }
            relPos += velocity;
            Position = GameObject.Env.Bounds.Location + relPos;
        }

        public bool Collides(RigidBody other) {
            return GameObject.Rec.Intersects(other.GameObject.Rec);
        }

        public void AddCollisionType(RigidBodyType type) {
            collisionMask |= (uint)type;    //collisionMask = collisionMask | (uint)type
        }

        public void AddCollisionType(uint type) {
            collisionMask |= type;    //collisionMask = collisionMask | type
        }

        public bool CollisionTypeMatches(RigidBodyType type) {
            return ((uint)type & collisionMask) != 0;
        }

        public void Destroy() {
            GameObject = null;
            PhysicsManager.RemoveItem(this);
        }
    }
}
