using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogaym_Reborn {
    public class Game1 : Game {
        public static GraphicsDeviceManager _graphics;
        public static Color backCol;

        private SpriteBatch _spriteBatch;

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
            UIManager.CreateNewComponent(UIComponentType.GameIcon, "GameIcon", "minipiper2", winColor: Color.DimGray);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Resources.AddFont("font", "SpriteFont");
            Resources.AddFont("consolefont", "ConsoleFont");

            Resources.AddTexture("piper32", "Images/piper");
            Resources.AddTexture("piper64", "Images/piper64");
            Resources.AddTexture("piper96", "Images/piper96");
            Resources.AddTexture("piperb96", "Images/piperb96");
            Resources.AddTexture("minipiper2", "Images/minipiper_2");
            Resources.AddTexture("minipiper3", "Images/minipiper_3");

            Resources.AddTexture("icon_placeholder", "Images/Astral Requin");

            Resources.AddTexture("ksp1", "Images/ImageWindow/KSP200439-1663945593964879872-20230531_182838-img2");
            Resources.AddTexture("ksp2", "Images/ImageWindow/KSP200439-1663945593964879872-20230531_182838-img1");
            Resources.AddTexture("ksp3", "Images/ImageWindow/KSP200439-1653731687136899072-20230503_140213-img1");
            Resources.AddTexture("ksp4", "Images/ImageWindow/KSP200439-1629180450198687744-20230224_190422-img1");
            Resources.AddTexture("ksp5", "Images/ImageWindow/KSP200439-1627780409936478208-20230220_222107-img2");
            Resources.AddTexture("ksp6", "Images/ImageWindow/KSP200439-1627073541450649605-20230218_233216-img1");
            Resources.AddTexture("ksp7", "Images/ImageWindow/KSP200439-1544741208211464193-20220706_195258-img1");
            Resources.AddTexture("ksp8", "Images/ImageWindow/095_kaltespur95");
            Resources.AddTexture("ksp9", "Images/ImageWindow/093_kaltespur93");
            Resources.AddTexture("ksp10", "Images/ImageWindow/071_kaltespur72");
            Resources.AddTexture("ksp11", "Images/ImageWindow/064_kaltespur64");
            Resources.AddTexture("ksp12", "Images/ImageWindow/062_kaltespur62");
            Resources.AddTexture("ksp13", "Images/ImageWindow/061_kaltespur61");
            Resources.AddTexture("ksp14", "Images/ImageWindow/055_kaltespur55");

            Resources.AddTexture("pika", "Images/pika");
            Resources.AddTexture("ammo", "Images/ammo");
            Resources.AddTexture("magazine", "Images/magazine");

            Resources.AddTexture("ball", "Images/ball");

            Resources.AddEffect("FadingRepeat", "Shaders/FadingRepeat");
            Resources.AddEffect("DebugBorders", "Shaders/DebugBorders");
            Resources.AddEffect("test", "Shaders/test");

            UIManager.LoadContentUI();
        }

        protected void Input() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Update(GameTime gameTime) {
            Input();

            UIManager.UpdateAll(gameTime);
            UpdateManager.UpdateAll(gameTime);
            BulletManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(backCol);

            DrawManager.DrawAll(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}

// give everyone the same velocity rather than telling others to be in the same position as the parent,
// since it'll create a delay on high velocities

//fix on window destroy