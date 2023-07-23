using Microsoft.Xna.Framework;

namespace Monogaym_Reborn {
    internal class ImageIcon : Icon {
        public ImageIcon(int x = 0, int y = 0, int width = 100, int height = 100) : base("ksp1", "Image Window", x, y, width, height, Color.White) {
            type = UIComponentType.ImageIcon;
            winType = UIComponentType.ImageWindow;
        }
    }
}
