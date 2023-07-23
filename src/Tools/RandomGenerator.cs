using System;

namespace Monogaym_Reborn {
    static internal class RandomGenerator {
        static Random random;

        static RandomGenerator() {
            random = new Random();
        }

        public static int GetRandomInt(int value) {
            return random.Next(value);
        }

        public static int GetRandomIntRange(int min, int max) {
            return random.Next(min, max);
        }
    }
}
