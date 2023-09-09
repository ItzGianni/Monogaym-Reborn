using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    public struct Magazine {
        public int MaxAmmo;
        public int CurrentAmmo;
        public BulletType BulletType;
    }
    public enum BulletType {
        Normal
    }

    internal static class BulletManager {
        static Dictionary<Actor, Bullet[]> Bullets;
        static int maxBulletsOnScreen;

        static BulletManager() {
            maxBulletsOnScreen = 30;
            Bullets = new Dictionary<Actor, Bullet[]>();
        }

        public static void Init(Actor owner) {
            Bullets[owner] = new Bullet[maxBulletsOnScreen];
            for (int i = 0; i < maxBulletsOnScreen; i++) {
                Bullets[owner][i] = new Bullet(owner);
            }
        }

        public static void Update(GameTime gameTime) {
            foreach (Bullet[] bArr in Bullets.Values) {
                foreach (Bullet b in bArr) {
                    if (b.IsAlive) {
                        b.Update(gameTime);
                    }
                }
            }
        }

        public static Bullet GetBullet(Actor owner) {
            foreach (Bullet b in Bullets[owner]) {
                if (!b.IsAlive && owner.Magazines[owner.CurrentMagIndex].CurrentAmmo > 0) {
                    owner.ChangeAmmoCount(-1);
                    Console.WriteLine($"Bullets: {b.Owner.CurrentMagazine.CurrentAmmo}");
                    return b;
                }
            }
            return null;
        }
    }
}
