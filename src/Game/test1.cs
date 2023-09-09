using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogaym_Reborn.src.Game {
    internal class test1 : GameObject {
        public test1(GameEnvironment env, string texName, int x, int y, int texOffsetX = 0, int texOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) : base(env, texName, x, y, texOffsetX, texOffsetY, spriteWidth, spriteHeight) {
            DrawOrder = 1000000;
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(tex, rec, Color.White);
        }

        public override void OnCollide(RigidBody other) {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            RB.velocity = new Point(10, 0);
        }
    }
}
