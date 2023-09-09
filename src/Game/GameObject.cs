using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogaym_Reborn {
    abstract class GameObject : IUpdateable, IDrawable {
        protected Texture2D tex;
        public Rectangle Rec => rec;
        protected Rectangle rec;
        public RigidBody RB;

        protected int maxSpeed;

        protected int texOffsetX, texOffsetY;
        protected int spriteW, spriteH;

        private string texName;

        public GameEnvironment Env => env;
        protected GameEnvironment env;

        public bool Enabled { get; protected set; }
        public int UpdateOrder { get; protected set; } = 2;
        public int DrawOrder { get; protected set; }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public int HalfHeight => (int)(rec.Width * 0.5f);
        public int HalfWidth => (int)(rec.Height * 0.5f);
        public Point Position { get { return rec.Location; } set { rec.Location = value; } }

        public GameObject(GameEnvironment env, string texName, int x, int y,
                int texOffsetX = 0, int texOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) {
            spriteW = spriteWidth;
            spriteH = spriteHeight;

            this.texOffsetX = texOffsetX;
            this.texOffsetY = texOffsetY;

            this.texName = texName;

            this.env = env;
            Position = new Point(x, y);

            Enabled = true;
            RB = new RigidBody(this);

            DrawManager.AddItem(this, 2);
            UpdateManager.AddItem(this);
        }

        public virtual void LoadContent() {
            tex = Resources.GetTexture(texName);
            spriteW = spriteW > 0 ? spriteW : tex.Width;
            spriteH = spriteH > 0 ? spriteH : tex.Height;
            rec.Size = new Point(spriteW, spriteH);
        }

        public abstract void OnCollide(RigidBody other);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch _spriteBatch);

        public virtual void Destroy() {
            tex.Dispose();

            UpdateManager.RemoveItem(this);

            if (RB != null) {
                RB.Destroy();
                RB = null;
            }
        }
    }
}
