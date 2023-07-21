using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    internal class Button : UIComponent {
        public Button(GraphicsDevice graphicsDevice, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(name, x, y, width, height) {

        }

        public override void Draw(SpriteBatch _spriteBatch) {
            throw new System.NotImplementedException();
        }

        public override void LoadContent() {
            throw new System.NotImplementedException();
        }
    }
}
