
namespace Monogaym_Reborn {
    internal class MagazineItem : Item {
        public MagazineItem(GameEnvironment env, int x, int y) : base(env, x, y, "magazine", 0, 0, 32, 32) {

        }

        public override void OnCollide(RigidBody other) {
            env.Player.Magazines.Add(new Magazine() { BulletType = BulletType.Normal, CurrentAmmo = 17, MaxAmmo = 17 });
            base.OnCollide(other);
        }
    }
}
