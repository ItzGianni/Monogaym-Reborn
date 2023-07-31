using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogaym_Reborn {
    internal class InputTextField : UIComponent {

        protected Dictionary<Keys, bool> keysState;  //key, canInput
        protected bool isSelected;
        protected string inputText;
        protected string outputText;
        protected Color color;
        protected Window win;

        public override int DrawOrder { get => win.DrawOrder; set => win.DrawOrder = value; }
        public string InputText { get => inputText; set => inputText = value; }

        public InputTextField(Window win, string name = "Window", int x = 0, int y = 0, int width = 0, int height = 0) : base(name, x, y, width, height) {
            this.win = win;

            this.width = width == 0 ? UIManager.DefaultInputTextSize.X : width;
            this.height = height == 0 ? UIManager.DefaultInputTextSize.Y : height;
            mainRect = new Rectangle(x, y, this.width, this.height);

            inputText = "Type Here";
            outputText = string.Empty;
            type = UIComponentType.InputText;
            color = Color.White;
        }

        public override void LoadContent() {
            font = Resources.GetFont("consolefont");
            keysState = new Dictionary<Keys, bool>();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (mainRect.Contains(mousePosition)) {
                    isSelected = true;
                }
                else {
                    isSelected = false;
                }
            }

            if (isSelected) {
                color = Color.White;
                if (wasMouseLeftButtonReleased && inputText == "Type Here") {
                    inputText = string.Empty;
                }
                Keys[] pressedKeys = keyboardState.GetPressedKeys();

                foreach (Keys key in pressedKeys) {
                    if (!keysState.ContainsKey(key)) {
                        keysState.Add(key, true);
                    }
                }

                foreach (var key in keysState) {
                    if (key.Value && keyboardState.IsKeyDown(key.Key)) {
                        if (font.MeasureString(inputText + key.Key.ToString() + key.Key.ToString()).X > win.mainRect.Width) {
                            inputText += "\n";
                        }

                        keysState[key.Key] = false;
                        if ((int)key.Key >= 65 && (int)key.Key <= 90) {
                            string a = !keyboardState.CapsLock ? key.Key.ToString().ToLower() : key.Key.ToString().ToUpper();
                            inputText += a;
                        }
                        else if ((int)key.Key >= 48 && (int)key.Key <= 57) {
                            string a = key.Key.ToString()[1..];

                            inputText += a;
                        }
                        else if ((int)key.Key == 13) {
                            //enter pressed
                            ExecuteCommand(inputText);
                            //Utilities.ProcessCommand(inputText, ref outputText, ref win);
                            inputText = "";
                        }
                        else if ((int)key.Key == 27) {
                            //escape pressed
                            isSelected = false;
                        }
                        else if ((int)key.Key == 0x20) {
                            //space pressed
                            inputText += "  ";
                        }
                        else if ((int)key.Key == 8) {
                            //delete pressed
                            if (inputText.Length > 0)
                                inputText = inputText.Remove(inputText.Length - 1);
                        }
                        else if ((int)key.Key == 109 || (int)key.Key == 189) {
                            //- pressed
                            inputText += "-";
                        }
                        //else {  //debugging
                        //    inputText += key.Key.ToString();
                        //}
                    }
                    else {
                        if (keyboardState.IsKeyUp(key.Key)) {
                            keysState[key.Key] = true;
                        }
                    }
                }
            }
            else {
                color = Color.Gray;
            }

            wasMouseRightButtonReleased = mouseState.RightButton == ButtonState.Released;
            wasMouseLeftButtonReleased = mouseState.LeftButton == ButtonState.Released;

            mainRect.Location = win.mainRect.Location + new Point(10, 30);
        }
        public override void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.DrawString(font, "> " + inputText, mainRect.Location.ToVector2(), color);
            _spriteBatch.DrawString(font, outputText, mainRect.Location.ToVector2() + new Vector2(0, 15), color);
        }

        protected void ExecuteCommand(string command) {
            string[] commands = command.Split("  ");
            bool valid = true;
            int i, x, y, r, g, b;
            switch (commands[0]) {
                case "exit":
                    i = default;
                    if (commands.Length > 1) {
                        try {
                            i = int.Parse(commands[1]);
                        }
                        catch {
                            outputText = "- Command Invalid: parameter(s) of incorrect format";
                            valid = false;
                        }
                        finally {
                            if (valid) {
                                outputText = $"- Command Succesful: closed window \"{UIManager.UiComponents[i].Name}\"";
                                UIManager.UiComponents[i].Destroy();
                            }
                        }
                    }
                    else {
                        win.Destroy();
                    }
                    break;
                case "info":
                    outputText = "- This project was made by @GianniPapetti\nMade for fun o((-w-))o";
                    break;
                case "expand":
                    i = default;
                    x = default;
                    y = default;
                    try {
                        x = int.Parse(commands[1]);
                        y = int.Parse(commands[2]);
                        if (commands.Length == 4)
                            i = int.Parse(commands[3]);
                    }
                    catch (IndexOutOfRangeException) {
                        outputText = "- Command Invalid: insufficient parameters";
                        valid = false;
                    }
                    catch (Exception) {
                        outputText = "- Command Invalid: parameter(s) of incorrect format";
                        valid = false;
                    }
                    if (valid) {
                        if (commands.Length == 4)
                            UIManager.UiComponents[i].mainRect.Inflate(x, y);
                        else
                            win.mainRect.Inflate(x, y);
                        outputText = string.Empty;
                    }
                    break;
                case "help":
                    outputText = "exit [1] (Closes the window of index [1], if none will close itself)\n" +
                                 "info (Shows all the program info)\n" +
                                 "expand [1] [2] [3] (Expands the size of the window of index [3], if none, itself)\n" +
                                 "setres [1] [2] (Changes the program window resolution)" +
                                 "winlist (Shows a list of all the currently open windows and their indexes)" +
                                 "image (Make image icon appear)";
                    break;
                case "image":
                    UIManager.CreateNewComponent(UIComponentType.ImageIcon, "ImageIcon", "ksp1", winColor: Color.MediumSpringGreen);
                    break;
                case "setres":
                    x = default;
                    y = default;
                    try {
                        x = int.Parse(commands[1]);
                        y = int.Parse(commands[2]);
                    }
                    catch (IndexOutOfRangeException) {
                        outputText = "- Command Invalid: insufficient or too many parameters";
                        valid = false;
                    }
                    catch (Exception) {
                        outputText = "- Command Invalid: parameter(s) of incorrect format";
                        valid = false;
                    }
                    if (valid)
                        Game1.ScreenSize = new(x, y);
                    break;
                case "winlist":
                    string s = string.Empty;
                    for (i = 0; i < UIManager.UiComponents.Count; i++) {
                        if ((int)UIManager.UiComponents[i].Type >= 2 && (int)UIManager.UiComponents[i].Type <= 3) {
                            s += $"{i} - {UIManager.UiComponents[i].Name}\n";
                        }
                    }
                    outputText = s;
                    break;
                case "setbackgroundcolor":
                    r = default;
                    g = default;
                    b = default;
                    try {
                        r = int.Parse(commands[2]);
                        g = int.Parse(commands[3]);
                        b = int.Parse(commands[4]);
                    }
                    catch (IndexOutOfRangeException) {
                        outputText = "- Command Invalid: insufficient or too many parameters";
                        valid = false;
                    }
                    finally {
                        Color c = new(r, g, b);
                        Game1.backCol = c;
                        outputText = "- Background color succesfully changed";
                    }
                    break;
                default:
                    outputText = "- Command not recognized\nuse help to get a list of all commands";
                    break;
            }
        }
    }
}
