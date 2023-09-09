using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class Piper : Actor {

        Effect fx;
        protected SpriteFont font;

        public Piper(int x, int y, int drawOrder, GameEnvironment env) : base(env, x, y, drawOrder, "piper96", 0, 0, 64, 64) {
            maxSpeed = 5;
            Magazines = new List<Magazine>();
            for (int i = 0; i < 2; i++) {
                Magazines.Add(new Magazine() { MaxAmmo = 17, CurrentAmmo = 17, BulletType = BulletType.Normal });
            }
            //RB.IsGravityAffected = true;
            RB.Type = RigidBodyType.Player;
            RB.AddCollisionType(RigidBodyType.Item);
            BulletManager.Init(this);
        }

        public override void LoadContent() {
            base.LoadContent();
            fx = Resources.GetEffect("test");
            font = Resources.GetFont("font");
        }

        public void Input() {
            KeyboardState kState = Keyboard.GetState();
            #region movement
            if (kState.IsKeyDown(Keys.W) && rec.Top > env.Bounds.Top) {
                RB.velocity.Y = -maxSpeed;
            }
            else if (kState.IsKeyDown(Keys.S) && rec.Bottom < env.Bounds.Bottom) {
                RB.velocity.Y = maxSpeed;
            }
            else {
                RB.velocity.Y = 0;
            }
            if (kState.IsKeyDown(Keys.A) && rec.Left > env.Bounds.Left) {
                RB.velocity.X = -maxSpeed;
            }
            else if (kState.IsKeyDown(Keys.D) && rec.Right < env.Bounds.Right) {
                RB.velocity.X = maxSpeed;
            }
            else {
                RB.velocity.X = 0;
            }
            #endregion
            #region item selection
            if (kState.IsKeyDown(Keys.D1)) {
                CurrentMagIndex = 0;
            }
            else if (kState.IsKeyDown(Keys.D2)) {
                CurrentMagIndex = 1;
            }
            else if (kState.IsKeyDown(Keys.D3)) {
                CurrentMagIndex = 2;
            }
            else if (kState.IsKeyDown(Keys.D4)) {
                CurrentMagIndex = 3;
            }
            else if (kState.IsKeyDown(Keys.D5)) {
                CurrentMagIndex = 4;
            }
            else if (kState.IsKeyDown(Keys.D6)) {
                CurrentMagIndex = 5;
            }
            else if (kState.IsKeyDown(Keys.D7)) {
                CurrentMagIndex = 6;
            }
            else if (kState.IsKeyDown(Keys.D8)) {
                CurrentMagIndex = 7;
            }
            else if (kState.IsKeyDown(Keys.D9)) {
                CurrentMagIndex = 8;
            }
            CurrentMagIndex = (byte)Math.Min(CurrentMagIndex, Magazines.Count - 1);
            #endregion
            #region other commands
            if (kState.IsKeyDown(Keys.R)) {
                Reload();
            }
            else if (kState.IsKeyDown(Keys.U)) {
                Unload();
            }
            #endregion
        }

        public override void Update(GameTime gameTime) {
            Input();

            DrawOrder = env.DrawOrder + 1;

            //fx.Parameters["ElapsedTime"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
        }

        public override void Draw(SpriteBatch sb) {
            //sb.End();
            //sb.Begin();
            sb.Draw(tex, rec, Color.White);
            //sb.End();
            //sb.Begin();
            Vector2 winBounds = new Vector2(env.Bounds.Right, env.Bounds.Bottom);
            sb.DrawString(font, "- ",
                winBounds + new Vector2(-160, -70 + (15 * CurrentMagIndex)), Color.White);
            sb.DrawString(font, $"Bulletcount: {BulletCount}",
                winBounds + new Vector2(-150, -85), Color.White);
            for (int i = 0; i < MagCount; i++)
                sb.DrawString(font, $"Magazine {i} ammo: {Magazines[i].CurrentAmmo}",
                    winBounds + new Vector2(-150, -70 + (15 * i)), Color.White);
        }

        public void Shoot(Point p) {
            Vector2 dir = p.ToVector2() - rec.Location.ToVector2();

            Bullet b = BulletManager.GetBullet(this);
            b?.Shoot(rec.Location, dir);
        }

        public override void OnCollide(RigidBody other) {
            
        }
    }
}

// make magazines instead of bullets