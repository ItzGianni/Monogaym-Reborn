using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class Window : UIComponent {

        protected Color windowColor;
        protected List<Texture2D> texs;

        protected Rectangle barRect;
        protected Rectangle xRect;

        protected bool canMoveWindow;

        public Window(GraphicsDevice graphicsDevice, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(name, x, y, width, height) {
            this.windowColor = windowColor;

            texs = new List<Texture2D> {
                Utilities.CreateBlankTexture(graphicsDevice, width, height, windowColor),
                Utilities.CreateBlankTexture(graphicsDevice, width, 30, Color.SlateGray),
                Utilities.CreateBlankTexture(graphicsDevice, 15, 15, Color.OrangeRed)
            };
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            xRect = new Rectangle() {
                X = mainRect.Right - 20,
                Y = mainRect.Top + 5,
                Width = 15,
                Height = 15
            };
            barRect = new Rectangle(mainRect.Location, mainRect.Size) {
                Height = 25
            };


            if (mainRect.Contains(mousePosition)) {
                if (wasMouseRightButtonReleased) {
                    canMoveWindow = true;
                }
                if (mouseState.RightButton == ButtonState.Pressed && canMoveWindow) {
                    Point dist = mousePosition - mainRect.Location;
                    //mainRect.Location = mousePosition - dist;
                    mainRect.Location = mousePosition - new Point((int)(mainRect.Width * 0.5f), (int)(mainRect.Height * 0.5f));
                }
                if (xRect.Contains(mousePosition)) {
                    if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                        Destroy();
                    }
                }
            }
            else {
                canMoveWindow = false;
            }

            wasMouseRightButtonReleased = mouseState.RightButton == ButtonState.Released;
            wasMouseLeftButtonReleased = mouseState.LeftButton == ButtonState.Released;
        }

        public override void LoadContent() {
            font = Resources.GetFont("font");
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(texs[0], mainRect, Color.White);
            _spriteBatch.Draw(texs[1], barRect, Color.White);
            _spriteBatch.Draw(texs[2], xRect, Color.White);
            _spriteBatch.DrawString(font, name, mainRect.Location.ToVector2() + new Vector2(5, 2), Color.White);
        }
    }
}
