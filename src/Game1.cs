using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    public class Game1 : Game {
        public static GraphicsDeviceManager _graphics;
        public static Color backCol;

        private SpriteBatch _spriteBatch;
        private Piper _piper;


        public static int ScreenWidth => _graphics.PreferredBackBufferWidth;
        public static int ScreenHeight => _graphics.PreferredBackBufferHeight;
        public static Point ScreenSize {
            get { return new(ScreenWidth, ScreenHeight); }
            set {
                _graphics.PreferredBackBufferWidth = value.X;
                _graphics.PreferredBackBufferHeight = value.Y;
                _graphics.ApplyChanges();
            }
        }

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            UIManager.Init(GraphicsDevice);
            Resources.Init(Content);
            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            backCol = Color.Aquamarine;

            UIManager.CreateNewComponent(UIComponentType.ConsoleIcon, "Piper", "piper96", winColor: Color.Teal);
            UIManager.CreateNewComponent(UIComponentType.ConsoleIcon, "Piper Spaghetti", "piperb96", winColor: Color.Thistle);
            UIManager.CreateNewComponent(UIComponentType.ImageIcon, "ImageIcon", "ksp1", winColor: Color.MediumSpringGreen);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.AddFont("font", "SpriteFont");
            Resources.AddFont("consolefont", "ConsoleFont");

            Resources.AddTexture("piper96", "piper96");
            Resources.AddTexture("piperb96", "piperb96");

            Resources.AddTexture("ksp1", "Images/KSP200439-1663945593964879872-20230531_182838-img2");
            Resources.AddTexture("ksp2", "Images/KSP200439-1663945593964879872-20230531_182838-img1");
            Resources.AddTexture("ksp3", "Images/KSP200439-1653731687136899072-20230503_140213-img1");
            Resources.AddTexture("ksp4", "Images/KSP200439-1629180450198687744-20230224_190422-img1");
            Resources.AddTexture("ksp5", "Images/KSP200439-1627780409936478208-20230220_222107-img2");
            Resources.AddTexture("ksp6", "Images/KSP200439-1627073541450649605-20230218_233216-img1");
            Resources.AddTexture("ksp7", "Images/KSP200439-1544741208211464193-20220706_195258-img1");
            Resources.AddTexture("ksp8", "Images/095_kaltespur95");
            Resources.AddTexture("ksp9", "Images/093_kaltespur93");
            Resources.AddTexture("ksp10", "Images/071_kaltespur72");
            Resources.AddTexture("ksp11", "Images/064_kaltespur64");
            Resources.AddTexture("ksp12", "Images/062_kaltespur62");
            Resources.AddTexture("ksp13", "Images/061_kaltespur61");
            Resources.AddTexture("ksp14", "Images/055_kaltespur55");

            Resources.AddTexture("grass", "grass");
            Resources.AddTexture("sand", "sand");

            Resources.AddEffect("test", "effecttest");

            UIManager.LoadContentUI();

            //_piper.LoadContent();
        }

        protected void Input() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                DrawManager.Sort();
            }
        }

        protected override void Update(GameTime gameTime) {
            Input();

            UpdateManager.UpdateAll(gameTime);
            UIManager.UpdateAll(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(backCol);

            DrawManager.DrawAll(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}