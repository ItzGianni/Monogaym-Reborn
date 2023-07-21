using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class UIManager {
        static GraphicsDevice graphicsDevice;
        static List<UIComponent> uiComponents;

        public static void Init(GraphicsDevice gd) {
            uiComponents = new List<UIComponent>();
            graphicsDevice = gd;
        }

        public static void CreateNewWindow(string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) {
            Window win = new Window(graphicsDevice, name, x, y, width, height, windowColor);
            win.LoadContent();
            uiComponents.Add(win);
        }

        public static void CreateNewIcon(string texName, string name = "Icon", int x = 0, int y = 0, int width = 100, int height = 100) {
            Icon icon = new Icon(texName, name, x, y, width, height);
            icon.LoadContent();
            uiComponents.Add(icon);
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
    }
}
