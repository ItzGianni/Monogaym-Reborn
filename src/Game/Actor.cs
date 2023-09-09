using System.Collections.Generic;

namespace Monogaym_Reborn {
    abstract class Actor : GameObject {

        protected int bulletSpeed;
        public int MagCount => Magazines.Count;
        public List<Magazine> Magazines;
        public byte CurrentMagIndex;
        public int BulletCount;
        public Magazine CurrentMagazine => Magazines[CurrentMagIndex];

        protected Actor(GameEnvironment env, int x, int y, int drawOrder, string texturePath, 
            int texOffsetX = 0, int texOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) : 
                base(env, texturePath, x, y, texOffsetX, texOffsetY, spriteWidth, spriteHeight) {

            DrawOrder = drawOrder;
        }

        public void ChangeAmmoCount(int amount) {
            Magazine mag = Magazines[CurrentMagIndex];  //I fucking hate having to do this :D
            mag.CurrentAmmo += amount;
            Magazines[CurrentMagIndex] = mag;
        }

        public void Reload() {
            if (BulletCount > 0) {
                int difference = CurrentMagazine.MaxAmmo - CurrentMagazine.CurrentAmmo;
                if (BulletCount >= difference) {
                    BulletCount -= difference;
                    ChangeAmmoCount(difference);
                }
                else {
                    ChangeAmmoCount(BulletCount);
                    BulletCount = 0;
                }
            }
        }

        public void Unload() {
            BulletCount += CurrentMagazine.CurrentAmmo;
            ChangeAmmoCount(-CurrentMagazine.CurrentAmmo);
        }

        public override void Destroy() {
            base.Destroy();
            DrawManager.RemoveItem(this, 2);
            UpdateManager.RemoveItem(this);
            Magazines = null;
        }
    }
}
