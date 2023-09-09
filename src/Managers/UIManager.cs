using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class UIManager {

        public static GraphicsDevice GraphicsDevice;
        static List<UIComponent> uiComponents;

        public static List<UIComponent> UiComponents { get => uiComponents; }

        public static Point DefaultIconSize;
        public static Point DefaultWindowSize;
        public static Point DefaultInputTextSize;
        public static Point DefaultButtonSize;
        public static Color DefaultColor;

        public static Point[,] IconPos;
        public static int iconSpacing;


        public static void Init(GraphicsDevice gd) {
            uiComponents = new List<UIComponent>();
            GraphicsDevice = gd;

            DefaultIconSize = new Point(75, 75);
            DefaultWindowSize = new Point(600, 400);
            DefaultInputTextSize = new Point(100, 15);
            DefaultButtonSize = new Point(150, 75);
            iconSpacing = 25;

            IconPos = new Point[
                Game1.ScreenWidth / (DefaultIconSize.X + iconSpacing),                               //x
                Game1.ScreenHeight / (DefaultIconSize.Y + DefaultInputTextSize.Y + iconSpacing)      //y
            ];
        }

        public static UIComponent CreateNewComponent(UIComponentType type, string name = "UIComponent", string texName = "icon_placeholder", int x = 0, int y = 0, int width = 0, int height = 0, Color winColor = default) {
            UIComponent component = default;
            if (winColor == default)
                winColor = DefaultColor;
            switch (type) {
                case UIComponentType.ConsoleIcon:
                    for (int i = 0; i < IconPos.GetLength(0); i++) {
                        for (int j = 0; j < IconPos.GetLength(1); j++) {
                            if (IconPos[i, j] == Point.Zero) {
                                Point p = new Point(
                                    iconSpacing + (DefaultIconSize.X + iconSpacing) * i,
                                    iconSpacing + (DefaultIconSize.Y + DefaultInputTextSize.Y + iconSpacing) * j
                                );
                                IconPos[i, j] = p;
                                component = new ConsoleIcon(texName, name, p.X, p.Y, width, height, winColor);
                                goto ConsoleEndLoop;
                            }
                        }
                    }
                ConsoleEndLoop:
                    break;
                case UIComponentType.ImageIcon:
                    for (int i = 0; i < IconPos.GetLength(0); i++) {
                        for (int j = 0; j < IconPos.GetLength(1); j++) {
                            if (IconPos[i, j] == Point.Zero) {
                                Point p = new Point(
                                    iconSpacing + (DefaultIconSize.X + iconSpacing) * i,
                                    iconSpacing + (DefaultIconSize.Y + DefaultInputTextSize.Y + iconSpacing) * j
                                );
                                IconPos[i, j] = p;
                                component = new ImageIcon(texName, name, p.X, p.Y, width, height, winColor);
                                goto ImageEndLoop;
                            }
                        }
                    }
                ImageEndLoop:
                    break;
                case UIComponentType.GameIcon:
                    for (int i = 0; i < IconPos.GetLength(0); i++) {
                        for (int j = 0; j < IconPos.GetLength(1); j++) {
                            if (IconPos[i, j] == Point.Zero) {
                                Point p = new Point(
                                    iconSpacing + (DefaultIconSize.X + iconSpacing) * i,
                                    iconSpacing + (DefaultIconSize.Y + DefaultInputTextSize.Y + iconSpacing) * j
                                );
                                IconPos[i, j] = p;
                                component = new GameIcon(texName, name, p.X, p.Y, width, height, winColor);
                                goto GameEndLoop;
                            }
                        }
                    }
                GameEndLoop:
                    break;
                case UIComponentType.ConsoleWindow:
                    component = new ConsoleWindow(name, x, y, width, height, winColor);
                    break;
                case UIComponentType.ImageWindow:
                    component = new ImageWindow(name, x, y, width, height, winColor);
                    List<Texture2D> images = new List<Texture2D>();
                    #region le a bit furry images
                    //Texture2D tex = Resources.GetTexture("ksp2");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp3");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp4");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp5");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp6");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp7");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp8");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp9");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp10");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp11");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp12");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp13");
                    //images.Add(tex);
                    //tex = Resources.GetTexture("ksp14");
                    //images.Add(tex);
                    //Texture2D tex = Utilities.CreateNoiseTexture(graphicsDevice, 250, 250);
                    #endregion

                    Texture2D tex = PerlinNoise.GenerateGrayScaleImageWithNoise(250, 250, 6);
                    //images.Add(tex);
                    tex = PerlinNoise.MapGradient(Color.Teal, Color.Black, PerlinNoise.GeneratePerlinNoise(250, 250, 6));
                    images.Add(tex);
                    tex = PerlinNoise.MapGradient(Color.Teal, Color.Black, PerlinNoise.GeneratePerlinNoise(250, 250, 6));
                    images.Add(tex);
                    tex = PerlinNoise.MapGradient(Color.Teal, Color.Black, PerlinNoise.GeneratePerlinNoise(250, 250, 6));
                    images.Add(tex);
                    tex = PerlinNoise.MapGradient(Color.Teal, Color.Black, PerlinNoise.GeneratePerlinNoise(250, 250, 6));
                    images.Add(tex);
                    ((ImageWindow)component).LoadImages(images);
                    break;
                case UIComponentType.GameWindow:
                    component = new GameWindow("GameWindow", x, y, width, height, winColor);
                    break;
                case UIComponentType.Button:
                    //component = new Button();
                    break;
            }
            component.LoadContent();
            uiComponents.Add(component);
            uiComponents.Sort((component, component2) => component.Type.CompareTo(component2.Type));
            DrawManager.Sort();
            return component;
        }

        public static UIComponent CreateNewCustomUIComponent(UIComponent uiComponent) {
            UIComponent component = uiComponent;
            component.LoadContent();
            uiComponents.Add(component);
            return component;
        }

        public static InputTextField CreateNewInputTextField(Window win, string name = "InputTextField", int x = 0, int y = 0, int width = 0, int height = 0) {
            InputTextField inpuTextField = new InputTextField(win, name, x, y, width, height);
            inpuTextField.LoadContent();
            uiComponents.Add(inpuTextField);
            return inpuTextField;
        }

        public static void LoadContentUI() {
            for (int i = 0; i < uiComponents.Count; i++) {
                uiComponents[i].LoadContent();
            }
        }

        public static void UpdateAll(GameTime gameTime) {
            for (int i = 0; i < uiComponents.Count; i++) {
                uiComponents[i].Update(gameTime);
            }
        }

        public static bool ContainsNameComponent(string name, UIComponentType type) {
            for (int i = 0; i < uiComponents.Count; i++) {
                if (uiComponents[i].Name.Equals(name) && uiComponents[i].Type == type) {
                    return true;
                }
            }
            return false;
        }

        public static void RemoveUIComponent(UIComponent comp) {
            uiComponents.Remove(comp);
        }

        //public void AddUITextOnScreen()

        public void Draw(SpriteBatch _spriteBatch) {

        }
    }
}
