using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    internal class ImageIcon : Icon {
        public ImageIcon(string texName, string name = "Icon", int x = 0, int y = 0, int width = 0, int height = 0, Color winColor = default) :
                base(texName, name, x, y, width, height, winColor) {
            type = UIComponentType.ImageIcon;
            winType = UIComponentType.ImageWindow;
        }
    }
}
