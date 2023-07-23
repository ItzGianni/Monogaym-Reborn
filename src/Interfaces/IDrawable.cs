using Microsoft.Xna.Framework.Graphics;

namespace Monogaym_Reborn {
    public interface IDrawable {

        int DrawOrder { get; }

        void Draw(SpriteBatch _spriteBatch);
    }
}
