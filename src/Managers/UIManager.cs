using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class UIManager {
        static GraphicsDevice graphicsDevice;
        public static List<UIComponent> uiComponents;

        public static void Init(GraphicsDevice gd) {
            uiComponents = new List<UIComponent>();
            graphicsDevice = gd;
        }

        public static UIComponent CreateNewComponent(UIComponentType type, string name = "Window", string texName = "", int x = 0, int y = 0, int width = 100, int height = 100, Color winColor = default) {
            UIComponent component = default;
            switch (type) {
                case UIComponentType.ConsoleIcon:
                    component = new ConsoleIcon(texName, name, x, y, width, height, winColor);
                    break;
                case UIComponentType.ImageIcon:
                    component = new ImageIcon(x, y, width, height);
                    break;
                case UIComponentType.ConsoleWindow:
                    component = new ConsoleWindow(graphicsDevice, name, x, y, width, height, winColor);
                    break;
                case UIComponentType.ImageWindow:
                    component = new ImageWindow(graphicsDevice, "ImageIcon", name, x, y, width, height, winColor);
                    break;
            }
            component.LoadContent();
            uiComponents.Add(component);
            DrawManager.Sort();
            return component;
        }

        public static InputTextField CreateNewInputTextField(Window win, string name = "Icon", int x = 0, int y = 0, int width = 100, int height = 100) {
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
    }
}
