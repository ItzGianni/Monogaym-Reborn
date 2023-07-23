using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    internal class ImageWindow : Window {

        protected int imageIndex;
        protected bool wasRightPressed, wasLeftPressed;

        public ImageWindow(GraphicsDevice graphicsDevice, string imageName, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(graphicsDevice, name, x, y, width, height, windowColor) {
            type = UIComponentType.ImageWindow;

            DrawOrder = 2;
        }

        public override void LoadContent() {
            base.LoadContent();

            Texture2D tex = Resources.GetTexture("ksp2");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp3");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp4");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp5");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp6");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp7");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp8");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp9");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp10");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp11");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp12");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp13");
            texs.Add(tex);
            tex = Resources.GetTexture("ksp14");
            texs.Add(tex);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !wasRightPressed) {
                imageIndex++;
                if (imageIndex > 12) {
                    imageIndex = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !wasLeftPressed) {
                imageIndex--;
                if (imageIndex < 0) {
                    imageIndex = 12;
                }
            }

            wasRightPressed = Keyboard.GetState().IsKeyDown(Keys.Right);
            wasLeftPressed = Keyboard.GetState().IsKeyDown(Keys.Left);
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            base.Draw(_spriteBatch);
            float aspectRatio = (float)texs[3 + imageIndex].Width / texs[3 + imageIndex].Height;
            Point newImageDim = new((int)((mainRect.Height - 25) * aspectRatio), mainRect.Height - 25);
            int halfWindow = mainRect.Width / 2;
            int halfImageW = (int)(newImageDim.X * 0.5f);

            _spriteBatch.Draw(
                texs[3 + imageIndex],
                new Rectangle(mainRect.X + halfWindow - halfImageW, mainRect.Y + 25, newImageDim.X, newImageDim.Y),
                Color.White
            );
        }
    }
}
