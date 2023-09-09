using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    abstract class Window : UIComponent {

        protected Color windowColor;
        protected List<Texture2D> texs;

        public Rectangle usableRect;
        protected Rectangle barRect;
        protected Rectangle resizeRect;
        protected Rectangle xRect;

        protected bool isFocused;

        protected bool isFullSized;
        protected Rectangle originalRec;
        protected Point prevMousePos;
        protected bool canMoveWindow;
        protected bool isDragging;

        public override int DrawOrder { get; set; }

        public Window(string name, int x, int y, int width, int height, Color windowColor) : base(name, x, y) {
            this.windowColor = windowColor;

            this.width = width == 0 ? UIManager.DefaultWindowSize.X : width;
            this.height = height == 0 ? UIManager.DefaultWindowSize.Y : height;

            mainRect = new Rectangle(x, y, this.width, this.height);

            xRect = new Rectangle() {
                X = mainRect.Right - 20,
                Y = mainRect.Top + 5,
                Width = 15,
                Height = 15
            };
            barRect = new Rectangle(mainRect.Location, mainRect.Size) {
                Height = 25
            };
            resizeRect = new Rectangle(xRect.Location, xRect.Size) {
                X = xRect.X - 23
            };
            usableRect = new Rectangle() {
                X = mainRect.X,
                Y = mainRect.Y + barRect.Height,
                Width = mainRect.Width,
                Height = mainRect.Height - barRect.Height
            };

            texs = new List<Texture2D> {
                Utilities.CreateBlankTexture(width, height, windowColor),   //main
                Utilities.CreateBlankTexture(width, 30, Color.SlateGray),   //bar
                Utilities.CreateBlankTexture(15, 15, Color.OrangeRed),      //x
                Utilities.CreateBlankTexture(15, 15, Color.LightGray)       //resize
            };

            isFocused = true;
            DrawManager.AddItem(this, 1);
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
            resizeRect = new Rectangle(xRect.Location, xRect.Size) {
                X = xRect.X - 23
            };
            usableRect = new Rectangle() {
                X = mainRect.X,
                Y = mainRect.Y + barRect.Height,
                Width = mainRect.Width,
                Height = mainRect.Height - barRect.Height
            };


            if (mainRect.Contains(mousePosition)) {
                if (wasMouseRightButtonReleased) {
                    canMoveWindow = true;       //check to let dragging window pressing right mouse only from window itself
                }
                if (mouseState.RightButton == ButtonState.Pressed && canMoveWindow) {
                    isFocused = true;
                    isDragging = true;
                    bool valid = false;
                    foreach (var item in UIManager.UiComponents) {
                        if (item.GetHashCode() == GetHashCode())
                            continue;
                        if ((int)item.Type >= 2 && (int)item.Type <= 3) {    //if it's a window
                            if (item.mainRect.Contains(mousePosition)) {    //if mouse is over 2 windows
                                if (DrawOrder < item.DrawOrder) {           //if this window is lower than the other
                                    isDragging = false;
                                }
                            }
                            else {
                                if (wasMouseRightButtonReleased) {      //if it's trying to drag an alone window
                                    valid = true;
                                }
                            }
                        }
                    }
                    if (valid && isDragging) {
                        if (DrawOrder < DrawManager.MaxWindowsDrawOrder) {
                            DrawOrder = DrawManager.MaxWindowsDrawOrder + 1;       //get selected window to top
                            DrawManager.Sort(1);
                        }
                    }
                }
                if (xRect.Contains(mousePosition)) {
                    if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                        Destroy();      //if click on x, close window
                    }
                }
                else if (resizeRect.Contains(mousePosition)) {
                    if (mouseState.LeftButton == ButtonState.Pressed && wasMouseLeftButtonReleased) {
                        if (isFullSized) {
                            mainRect = originalRec;
                        }
                        else {
                            originalRec = mainRect;
                            mainRect.Location = Game1.ScreenSize / new Point(2) - (mainRect.Center - mainRect.Location);
                            int wDelta = Game1.ScreenWidth - mainRect.Right;
                            int hDelta = Game1.ScreenHeight - mainRect.Bottom;

                            mainRect.Inflate(wDelta, hDelta);
                        }
                        isFullSized = !isFullSized;
                    }
                }
            }
            else {
                canMoveWindow = false;
                if (!isDragging && mouseState.RightButton == ButtonState.Pressed) {
                    isFocused = false;
                }
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
            Color c = isFocused ? Color.White : Color.Gray;
            _spriteBatch.Draw(texs[0], mainRect, c);
            _spriteBatch.Draw(texs[1], barRect, c);
            _spriteBatch.Draw(texs[2], xRect, c);
            _spriteBatch.Draw(texs[3], resizeRect, c);
            _spriteBatch.DrawString(font, $"{Type} - {name} - {DrawOrder}", mainRect.Location.ToVector2() + new Vector2(5, 2), c);
        }

        public override void Destroy() {
            base.Destroy();
            DrawManager.RemoveItem(this, 1);
        }
    }
}
