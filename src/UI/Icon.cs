using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    internal class Icon : UIComponent {
        protected Texture2D tex;
        protected UIComponentType winType;
        string texName;
        Color winColor;

        public override int DrawOrder { get; set; }

        public Icon(string texName, string name = "Icon", int x = 0, int y = 0, int width = 100, int height = 100, Color winColor = default) : base(name, x, y, width, height) {
            this.texName = texName;
            this.winColor = winColor;

            type = UIComponentType.ConsoleIcon;
            winType = UIComponentType.ConsoleWindow;

            DrawOrder = 1;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (mainRect.Contains(mousePosition)) {
                if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                    if (!UIManager.ContainsNameComponent(name, winType)) {
                        int x = RandomGenerator.GetRandomIntRange(50, Game1.ScreenWidth - width - 600);
                        int y = RandomGenerator.GetRandomIntRange(50, Game1.ScreenHeight - height - 400);
                        UIManager.CreateNewComponent(winType, name, x: x, y: y, width: 600, height: 400, winColor: winColor);
                    }
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
            Vector2 stringPos = new Vector2(
                mainRect.Center.X - font.MeasureString(name).X * 0.5f,
                mainRect.Bottom + font.MeasureString(name).Y * 0.5f
            );
            _spriteBatch.DrawString(font, name, stringPos, Color.WhiteSmoke);
        }
    }
}
