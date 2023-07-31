using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class ImageWindow : Window {
        protected List<Texture2D> _textures;
        protected int imageIndex;
        protected bool wasRightPressed, wasLeftPressed;

        public ImageWindow(GraphicsDevice graphicsDevice, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(graphicsDevice, name, x, y, width, height, windowColor) {
            type = UIComponentType.ImageWindow;

            DrawOrder = 2;
        }

        public void LoadImages(List<Texture2D> _textures) {
            this._textures = _textures;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !wasRightPressed) {
                imageIndex++;
                if (imageIndex > _textures.Count - 1) {
                    imageIndex = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !wasLeftPressed) {
                imageIndex--;
                if (imageIndex < 0) {
                    imageIndex = _textures.Count - 1;
                }
            }

            wasRightPressed = Keyboard.GetState().IsKeyDown(Keys.Right);
            wasLeftPressed = Keyboard.GetState().IsKeyDown(Keys.Left);
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            base.Draw(_spriteBatch);
            float aspectRatio = (float)_textures[imageIndex].Width / _textures[imageIndex].Height;
            Point newImageDim = new((int)((mainRect.Height - 25) * aspectRatio), mainRect.Height - 25);
            int halfWindow = mainRect.Width / 2;
            int halfImageW = (int)(newImageDim.X * 0.5f);
            _spriteBatch.Draw(
                _textures[imageIndex],
                new Rectangle(mainRect.X + halfWindow - halfImageW, mainRect.Y + 25, newImageDim.X, newImageDim.Y),
                Color.White
            );
        }
    }
}
