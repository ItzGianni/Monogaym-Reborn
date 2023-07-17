using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    public class Game1 : Game {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Piper _ball;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize() {
            _ball = new Piper();

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _ball.LoadContent(Content);
        }

        protected void Input(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _ball.Input();
        }

        protected override void Update(GameTime gameTime) {
            Input(gameTime);

            Window.Title = $"{1 / gameTime.ElapsedGameTime.TotalSeconds}";

            UpdateManager.UpdateAll();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Cyan);

            DrawManager.DrawAll(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}