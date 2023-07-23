using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class Window : UIComponent {

        protected Color windowColor;
        protected List<Texture2D> texs;

        protected Rectangle barRect;
        protected Rectangle xRect;

        protected Point prevMousePos;
        protected bool canMoveWindow;
        protected bool isDragging;

        public override int DrawOrder { get; set; }

        public Window(GraphicsDevice graphicsDevice, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100, Color windowColor = default) : base(name, x, y, width, height) {
            this.windowColor = windowColor;

            texs = new List<Texture2D> {
                Utilities.CreateBlankTexture(graphicsDevice, width, height, windowColor),
                Utilities.CreateBlankTexture(graphicsDevice, width, 30, Color.SlateGray),
                Utilities.CreateBlankTexture(graphicsDevice, 15, 15, Color.OrangeRed)
            };
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            xRect = new Rectangle() {
                X = mainRect.Right - 20,
                Y = mainRect.Top + 5,
                Width = 15,
                Height = 15
            };
            barRect = new Rectangle(mainRect.Location, mainRect.Size) {
                Height = 25
            };


            if (mainRect.Contains(mousePosition)) {
                if (wasMouseRightButtonReleased) {
                    canMoveWindow = true;       //check to let dragging window pressing right mouse only from window itself
                }
                if (mouseState.RightButton == ButtonState.Pressed && canMoveWindow) {
                    isDragging = true;
                    bool valid = false;
                    foreach (var item in UIManager.uiComponents) {
                        if (item.GetHashCode() == GetHashCode()) continue;
                        if ((int)item.Type >= 2 && (int)item.Type <= 3) {    //if it's a window
                            if (item.mainRect.Contains(mousePosition)) {    //if mouse is over 2 windows
                                if (DrawOrder < item.DrawOrder) {           //if this window is lower than the other
                                    isDragging = false;
                                }
                            }
                            else {
                                if (wasMouseRightButtonReleased) {//if it's trying to drag an alone window
                                    valid = true;
                                }
                            }
                        }
                    }
                    if (valid && isDragging) {
                        if (DrawOrder < DrawManager.MaxDrawOrder) {
                            DrawOrder = DrawManager.MaxDrawOrder + 1;       //get selected window to top
                            DrawManager.Sort();
                        }
                    }
                }
                if (xRect.Contains(mousePosition)) {
                    if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                        Destroy();      //if click on x, close window
                    }
                }
            }
            else {
                canMoveWindow = false;
            }

            if (isDragging) {
                Point dist = mousePosition - mainRect.Location;
                Point mouseDelta = prevMousePos - mousePosition;
                mainRect.Location = mousePosition - dist - mouseDelta;      //dragging the window
                if (mouseState.RightButton == ButtonState.Released) {
                    isDragging = false;
                }
            }

            wasMouseRightButtonReleased = mouseState.RightButton == ButtonState.Released;
            wasMouseLeftButtonReleased = mouseState.LeftButton == ButtonState.Released;

            prevMousePos = mousePosition;
        }

        public override void LoadContent() {
            font = Resources.GetFont("font");
        }

        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(texs[0], mainRect, Color.White);
            _spriteBatch.Draw(texs[1], barRect, Color.White);
            _spriteBatch.Draw(texs[2], xRect, Color.White);
            _spriteBatch.DrawString(font, $"{Type} - {name} - {DrawOrder}", mainRect.Location.ToVector2() + new Vector2(5, 2), Color.White);
        }
    }
}
