using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    static internal class Resources {
        public static Dictionary<string, SpriteFont> Fonts;
        public static Dictionary<string, Texture2D> Textures;
        static ContentManager ContentManager;

        public static void Init(ContentManager cm) {
            Fonts = new Dictionary<string, SpriteFont>();
            Textures = new Dictionary<string, Texture2D>();

            ContentManager = cm;
        }

        public static void AddFont(string fontName, string fontPath) {
            Fonts.Add(fontName, ContentManager.Load<SpriteFont>(fontPath));
        }

        public static SpriteFont GetFont(string key) {
            return Fonts[key];
        }

        public static void AddTexture(string texName, string texPath) {
            Textures.Add(texName, ContentManager.Load<Texture2D>(texPath));
        }

        public static Texture2D GetTexture(string key) {
            return Textures[key];
        }
    }
}
