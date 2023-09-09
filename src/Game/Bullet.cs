using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    internal class Bullet : IDrawable {
        protected Texture2D tex;
        protected float maxSpeed;
        public bool IsAlive => isAlive;
        private bool isAlive;
        public Rectangle Rec => rec;

        public int DrawOrder { get; private set; }

        private Rectangle rec;
        private Vector2 relPos;
        private Vector2 dir;
        public Actor Owner => owner;
        private Actor owner;


        public Bullet(Actor owner) {
            this.owner = owner;
            tex = Resources.GetTexture("minipiper3");
            rec.Size = new Point(25, 25);
            DrawManager.AddItem(this, 2);
            maxSpeed = 5;
        }

        public void Update(GameTime gameTime) {
            relPos += Vector2.Multiply(dir, maxSpeed);
            rec.Location = new Point(
                (int)(relPos.X),
                (int)(relPos.Y)
            );
            if (rec.Right > owner.Env.Bounds.Right || rec.Left < owner.Env.Bounds.Left ||
                rec.Top < owner.Env.Bounds.Top || rec.Bottom > owner.Env.Bounds.Bottom) {
                isAlive = false;
            }
            DrawOrder = ((Piper)owner).DrawOrder + 1;
        }

        public void Draw(SpriteBatch _spriteBatch) {
            if (isAlive)
                _spriteBatch.Draw(tex, rec, Color.White);
        }

        public void Shoot(Point pos, Vector2 dir) {
            relPos = pos.ToVector2();
            isAlive = true;
            this.dir = dir;
            this.dir.Normalize();
            DrawOrder = owner.Env.DrawOrder + 1;
            DrawManager.Sort(1);
        }
    }
}
