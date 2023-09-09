using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogaym_Reborn {
    internal class Item : GameObject {
        protected string texName;

        public Item(GameEnvironment env, int x, int y, string texName, 
            int texOffsetX = 0, int texOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) : 
                base(env, texName, x, y, texOffsetX, texOffsetY, spriteWidth, spriteHeight) {
            RB.Type = RigidBodyType.Item;
            RB.AddCollisionType(RigidBodyType.Player);
        }


        public override void Update(GameTime gameTime) {
            DrawOrder = env.DrawOrder + 1;
        }


        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(tex, rec, Color.White);
        }
        public override void OnCollide(RigidBody other) {
            Console.WriteLine("Item Picked Up");
            Destroy();
        }

        public override void Destroy() {
            Enabled = false;
            DrawManager.RemoveItem(this, 2);
            UpdateManager.RemoveItem(this);
            tex.Dispose();
            env = null;
        }

    }
}
