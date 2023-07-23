using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class DrawManager {
        public static List<IDrawable> Items;

        public static int MaxDrawOrder {
            get {
                int max = default;
                DrawManager.Items.ForEach(item => { max = Math.Max(max, item.DrawOrder); });
                return max;
            }
        }

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
            Items.ForEach((item) => item.Draw(spriteBatch));
            spriteBatch.End();
        }

        public static void Sort() {
            Items.Sort((item, item2) => item.DrawOrder.CompareTo(item2.DrawOrder));
        }
    }
}
