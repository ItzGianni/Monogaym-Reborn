using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class UpdateManager {
        public static List<IUpdateable> Items;

        static UpdateManager() {
            Items = new List<IUpdateable>();
        }

        public static void AddItem(IUpdateable item) {
            Items.Add(item);
        }

        public static void RemoveItem(IUpdateable item) {
            Items.Remove(item);
        }

        public static void UpdateAll(GameTime gameTime) {
            for (int i = 0; i < Items.Count; i++) {
                Items[i].Update(gameTime);
            }
        }
    }
}
