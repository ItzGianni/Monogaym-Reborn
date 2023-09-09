using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogaym_Reborn.src.Game;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class GameEnvironment {

        public Rectangle Bounds => _bounds;
        private Rectangle _bounds;
        public Piper Player => _player;
        private Piper _player;

        SpriteFont font;
        private Point ammoCountPos;

        private List<Item> items;

        public int DrawOrder => drawOrder;
        private int drawOrder;

        public Window AttachedWindow;

        private bool wasMouseLeftButtonReleased;

        test1 a;
        test2 b;

        public GameEnvironment(int drawOrder) {
            _bounds = new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight);
            _player = new Piper((int)(Game1.ScreenSize.X * 0.5f), (int)(Game1.ScreenSize.Y * 0.5f), drawOrder, this);
            ammoCountPos = new Point(_bounds.Right - 100, _bounds.Bottom - 50);
            items = new List<Item>() {
                new AmmoItem(this, 500, 500),
                new MagazineItem(this, 800, 300)
            };
        }

        public GameEnvironment(int drawOrder, Window win) {
            _bounds = win.usableRect;
            _player = new Piper((int)(100), (int)(100), drawOrder + 1, this);
            AttachedWindow = win;
            items = new List<Item>() {
                new AmmoItem(this, 300, 100),
                new MagazineItem(this, 25, 200)
            };
            a = new test1(this, "icon_placeholder", 0, 50, 0, 0, 64, 64);
            b = new test2(this, "icon_placeholder", 0, 50, a, 0, 0, 64, 64);
        }

        public void Input() {

        }

        public void LoadContent() {
            _player.LoadContent();
            items.ForEach(item => item.LoadContent());
            font = Resources.GetFont("font");
            a.LoadContent();
            b.LoadContent();
        }

        public void Update(GameTime gameTime) {
            MouseState mState = Mouse.GetState();
            _bounds = AttachedWindow.usableRect;
            drawOrder = AttachedWindow.DrawOrder + 1;
            if (AttachedWindow != null) {
                if (AttachedWindow.usableRect.Contains(mState.Position)) {
                    if (mState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                        ((GameWindow)AttachedWindow).Env.Player.Shoot(mState.Position);
                    }
                }
            }

            wasMouseLeftButtonReleased = mState.LeftButton == ButtonState.Released;

            PhysicsManager.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch) {
            //_spriteBatch.DrawString(font, _player.)
        }

        public void Destroy() {
            _player.Destroy();
            AttachedWindow = null;
            items.ForEach(item => { item.Destroy(); item = null; });
        }
    }
}
