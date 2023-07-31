using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    internal class ConsoleWindow : Window {
        protected InputTextField InputTextField;
        public ConsoleWindow(GraphicsDevice graphicsDevice, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(graphicsDevice, name, x, y, width, height, windowColor) {
            type = UIComponentType.ConsoleWindow;

            Point InputTextFieldPos = mainRect.Location + new Point(10, 25);
            InputTextField = UIManager.CreateNewInputTextField(this, "InputTextField", InputTextFieldPos.X, InputTextFieldPos.Y);

            DrawOrder = 3;
        }

        public override void LoadContent() {
            base.LoadContent();


        }

        public override void Destroy() {
            base.Destroy();
            InputTextField.Destroy();
        }
    }
}
