using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    internal class Icon : UIComponent {
        Texture2D tex;
        string texName;

        public Icon(string texName, string name = "Icon", int x = 0, int y = 0, int width = 100, int height = 100) : base(name, x, y, width, height) {
            this.texName = texName;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (mainRect.Contains(mousePosition)) {
                if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                    Destroy();
                    int x = RandomGenerator.GetRandomIntRange(100, Game1.ScreenWidth - width - 100);
                    int y = RandomGenerator.GetRandomIntRange(100, Game1.ScreenHeight - height - 100);
                    UIManager.CreateNewWindow(name, x, y, 150, 100, Color.Teal);
                }
            }

            wasMouseLeftButtonReleased = mouseState.LeftButton == ButtonState.Released;
        }

        public override void LoadContent() {
            tex = Resources.GetTexture(texName);
            font = Resources.GetFont("font");
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(tex, mainRect, Color.White);
            _spriteBatch.DrawString(font, name, mainRect.Location.ToVector2() + new Vector2(15, mainRect.Height + 5), Color.Gray);
        }

        protected override void Destroy() {
            base.Destroy();
        }
    }
}
