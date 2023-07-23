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

        public InputTextField(Window win, string name = "Window", int x = 0, int y = 0, int width = 100, int height = 100) : base(name, x, y, width, height) {
            this.win = win;

            inputText = "Type Here";
            outputText = string.Empty;

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
            switch (commands[0]) {
                case "exit":
                    bool valid = true;
                    if (commands.Length > 1) {
                        int i = default;
                        try {
                            i = int.Parse(commands[1]);
                        }
                        catch {
                            outputText = "- Command Invalid: parameter(s) of incorrect format";
                            valid = false;
                        }
                        finally {
                            if (valid) {
                                outputText = $"- Command Succesful: closed window \"{UIManager.uiComponents[i].Name}\"";
                                UIManager.uiComponents[i].Destroy();
                            }
                        }
                    }
                    else {
                        win.Destroy();
                    }
                    break;
                case "info":
                    outputText = "- This project was made by @GianniPapetti\nMade for fun owo";
                    break;
                case "expand":
                    int x = default;
                    int y = default;
                    int index = default;
                    valid = true;   //commit on github, dumbass
                    try {
                        x = int.Parse(commands[1]);
                        y = int.Parse(commands[2]);
                        if (commands.Length == 4)
                            index = int.Parse(commands[3]);
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
                            UIManager.uiComponents[index].mainRect.Inflate(x, y);
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
                                 "owo (Make image icon appear)";
                    break;
                case "owo":
                    UIManager.CreateNewComponent(UIComponentType.ImageIcon, "ImageIcon", "ksp1", 125, 25, 75, 75);
                    break;
                case "setres":
                    x = default;
                    y = default;
                    valid = true;
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
                    int j = 0;
                    for (int i = 0; i < UIManager.uiComponents.Count; i++) {
                        if ((int)UIManager.uiComponents[i].Type >= 2 && (int)UIManager.uiComponents[i].Type <= 3) {
                            j++;
                            s += $"{i} - {UIManager.uiComponents[i].Name}\n";
                        }
                    }
                    outputText = s;
                    break;
                default:
                    outputText = "- Command not recognized\nuse help to get a list of all commands";
                    break;
            }
        }
    }
}
