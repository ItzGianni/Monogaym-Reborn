using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class DrawManager {
        public static List<List<IDrawable>> Items;
        public static Effect DebugBorder;

        public static int MaxIconsDrawOrder {
            get {
                int max = default;
                Items[0].ForEach(item => { max = Math.Max(max, item.DrawOrder); });
                return max;
            }
        }

        public static int MaxWindowsDrawOrder {
            get {
                int max = default;
                Items[1].ForEach(item => { max = Math.Max(max, item.DrawOrder); });
                return max;
            }
        }

        public static int MaxStuffOverWindowsDrawOrder {
            get {
                int max = default;
                Items[2].ForEach(item => { max = Math.Max(max, item.DrawOrder); });
                return max;
            }
        }

        static DrawManager() {
            Items = new List<List<IDrawable>>();
            for (int i = 0; i < 3; i++) {
                Items.Add(new List<IDrawable>());
            }
            // 0 - icons
            // 1 - windows
            // 2 - stuff over windows
        }

        public static void AddItem(IDrawable item, int index) {
            Items[index].Add(item);
            Sort(index);
        }

        public static void RemoveItem(IDrawable item, int index) {
            Items[index].Remove(item);
            Sort(index);
        }

        public static void DrawAll(SpriteBatch spriteBatch) {
            spriteBatch.Begin(effect: DebugBorder);
            for (int i = 0; i < Items.Count; i++) {
                Items[i].ForEach((item) => item.Draw(spriteBatch));
            }
            spriteBatch.End();
        }

        public static void Sort() {
            for (int i = 0; i < Items.Count; i++) {
                Items[i].Sort((item, item2) => item.DrawOrder.CompareTo(item2.DrawOrder));
            }
        }

        public static void Sort(int index) {
            Items[index].Sort((item, item2) => item.DrawOrder.CompareTo(item2.DrawOrder));
        }
    }
}
