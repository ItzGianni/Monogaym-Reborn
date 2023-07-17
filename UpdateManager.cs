using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class UpdateManager {
        public static List<IUpdatable> Items;

        static UpdateManager() {
            Items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item) {
            Items.Add(item);
        }

        public static void UpdateAll() {
            foreach (IUpdatable item in Items) {
                item.Update();
            }
        }
    }
}
