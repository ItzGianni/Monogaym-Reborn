using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    static internal class Utilities {
        public static Texture2D CreateBlankTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) {
                data[i] = color;
            }
            texture.SetData(data);

            return texture;
        }
    }
}
