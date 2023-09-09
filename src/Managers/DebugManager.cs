using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal static class DebugManager {
        static float interval;
        static SpriteFont font;
        static double deltaTime;
        static Texture2D tex;
        static List<Rectangle> rects;

        static DebugManager() {
            font = Resources.GetFont("font");
            rects = new List<Rectangle>();
        }

        public static void AddItem(Rectangle r) {
            rects.Add(r);
        }

        public static void RemoveItem(Rectangle r) {
            rects.Remove(r);
        }

        public static void Update(GameTime gameTime) {
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void Draw(SpriteBatch _spriteBatch) {
            string s = $"{Math.Round(1 / deltaTime)}";
            Vector2 sPos = new Vector2(Game1.ScreenWidth - 75, 25);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, s, sPos, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}
