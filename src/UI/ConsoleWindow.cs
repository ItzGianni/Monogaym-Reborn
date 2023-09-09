using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    internal class ConsoleWindow : Window {
        protected InputTextField InputTextField;
        public ConsoleWindow(string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(name, x, y, width, height, windowColor) {
            type = UIComponentType.ConsoleWindow;

            Point InputTextFieldPos = mainRect.Location + new Point(10, 25);
            InputTextField = UIManager.CreateNewInputTextField(this, "InputTextField", InputTextFieldPos.X, InputTextFieldPos.Y);

            DrawOrder = 3;
        }

        public override void Destroy() {
            base.Destroy();
            InputTextField.Destroy();
        }
    }
}
