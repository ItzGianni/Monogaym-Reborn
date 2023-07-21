using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class DrawManager {
        public static List<IDrawable> Items;

        static DrawManager() {
            Items = new List<IDrawable>();
        }

        public static void AddItem(IDrawable item) {
            Items.Add(item);
        }

        public static void RemoveItem(IDrawable item) {
            Items.Remove(item);
        }

        public static void DrawAll(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            foreach (IDrawable item in Items) {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
