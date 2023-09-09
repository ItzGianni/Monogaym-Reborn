using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    internal class CalculatorWindow : Window {
        protected Button[][] buttons;

        public CalculatorWindow(string name, int x, int y, int width, int height, Color windowColor) : base(name, x, y, width, height, windowColor) {
            buttons[0][0] = (Button)UIManager.CreateNewCustomUIComponent(new Button("close_window", this));
        }
    }
}
