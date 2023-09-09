namespace Monogaym_Reborn {
    internal class AmmoItem : Item {
        public AmmoItem(GameEnvironment env, int x, int y) : base(env, x, y, "ammo", 0, 0, 32, 32) {

        }

        public override void OnCollide(RigidBody other) {
            env.Player.BulletCount += RandomGenerator.GetRandomIntRange(5, 15);
            base.OnCollide(other);
        }
    }
}
