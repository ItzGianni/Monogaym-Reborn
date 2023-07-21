using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    public class Game1 : Game {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Piper _piper;

        public static int ScreenWidth { get { return _graphics.PreferredBackBufferWidth; } }
        public static int ScreenHeight { get { return _graphics.PreferredBackBufferHeight; } }

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            Resources.Init(Content);
            UIManager.Init(GraphicsDevice);

            base.Initialize();

            UIManager.CreateNewIcon("piper96", "Piper", 25, 25, 75, 75);
            UIManager.CreateNewIcon("piperb96", "Piper Spaghetti", 25, 150, 75, 75);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.AddFont("font", "SpriteFont");
            Resources.AddTexture("piper96", "piper96");
            Resources.AddTexture("piperb96", "piperb96");

            UIManager.LoadContentUI();
        }

        protected void Input() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Update(GameTime gameTime) {
            Input();

            UpdateManager.UpdateAll(gameTime);
            UIManager.UpdateAll(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Cyan);

            DrawManager.DrawAll(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}