using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogaym_Reborn.src.Game {
    internal class test2 : GameObject {
        test1 a;
        public test2(GameEnvironment env, string texName, int x, int y, test1 a,
            int texOffsetX = 0, int texOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) : 
            base(env, texName, x, y, texOffsetX, texOffsetY, spriteWidth, spriteHeight) {
            DrawOrder = 1000001;
            this.a = a;
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(tex, rec, Color.Blue);
        }

        public override void OnCollide(RigidBody other) {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime) {
            //RB.relPos = a.RB.relPos;
            RB.velocity = a.RB.velocity;
        }
    }
}
