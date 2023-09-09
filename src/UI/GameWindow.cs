using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    internal class GameWindow : Window {

        public GameEnvironment Env => env;
        private GameEnvironment env;


        public GameWindow(string name, int x, int y, int width, int height, Color windowColor) : base(name, x, y, width, height, windowColor) {
            env = new GameEnvironment(DrawOrder + 1, this);
            type = UIComponentType.GameWindow;
            DrawOrder = 4;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (isFocused) {
                env.Input();
                env.Update(gameTime);
            }
        }

        public override void LoadContent() {
            base.LoadContent();
            env.LoadContent();
        }

        public override void Destroy() {
            base.Destroy();
            env.Destroy();
        }
    }
}
