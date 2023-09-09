using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Monogaym_Reborn {
    static internal class Utilities {

        public static Texture2D CreateBlankTexture(int width, int height, Color color = default) {
            width = width == 0 ? 1 : width;
            height = height == 0 ? 1 : height;
            Texture2D texture = new Texture2D(UIManager.GraphicsDevice, width, height);
            if (color == default)
                color = Color.White;

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) {
                data[i] = color;
            }
            texture.SetData(data);

            return texture;
        }

        public static Texture2D CreateNoiseTexture(int width, int height) {
            Texture2D tex = new Texture2D(UIManager.GraphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) {
                int x = RandomGenerator.GetRandomInt(255);
                data[i] = new Color(x, x, x);
            }
            tex.SetData(data);

            return tex;
        }

        public static void ProcessCommand(string commandText, ref string outputText, ref Window uiComponent) {
            string[] commandParts = commandText.Split("  ");
            string commandName = commandParts[0].ToLower();

            Command command = Resources.Commands.Find(c => c.Name == commandName);

            if (command != null) {
                switch (command.Action.ToLower()) {
                    case "display_text":
                        string output = command.OutputText;
                        outputText = output;
                        break;
                    case "close_window":
                        if (commandParts.Length == 2) {
                            if (int.TryParse(commandParts[1], out int i)) {
                                UIManager.UiComponents[i].Destroy();
                            }
                            else {
                                outputText = "- Command Invalid: parameter(s) of incorrect format";
                            }
                        }
                        else if (commandParts.Length > 1) {
                            outputText = "- Command Invalid: too many arguments";
                        }
                        else {
                            uiComponent.Destroy();
                        }
                        break;
                    case "resize_window":
                        if (commandParts.Length == 1) {
                            outputText = "- resize []";
                        }
                        else if (commandParts.Length == 3) {
                            if (int.TryParse(commandParts[1], out int x) && int.TryParse(commandParts[2], out int y)) {
                                uiComponent.mainRect.Inflate(x, y);
                            }
                        }
                        else if (commandParts.Length == 4) {
                            if (int.TryParse(commandParts[1], out int x) && int.TryParse(commandParts[2], out int y) && int.TryParse(commandParts[3], out int i)) {
                                UIManager.UiComponents[i].mainRect.Inflate(x, y);
                            }
                        }
                        else {
                            outputText = "- Command Invalid: incorrect amount of arguments";
                        }
                        break;
                    case "change_game_window_resolution":
                        if (commandParts.Length == 3) {
                            if (int.TryParse(commandParts[1], out int x) && int.TryParse(commandParts[2], out int y)) {
                                Game1.ScreenSize = new(x, y);
                            }
                        }
                        else {
                            outputText = "- Command Invalid: incorrect amount of arguments";
                        }
                        break;
                    case "show_list":
                        if (commandParts.Length == 1) {
                            string s = string.Empty;
                            string[] uiTypes = Enum.GetNames(typeof(UIComponentType));
                            for (int i = 0; i < uiTypes.Length; i++) {
                                s += $"- {uiTypes[i]}\n";
                            }
                            outputText = s;
                        }
                        else if (commandParts.Length == 2) {
                            string s = string.Empty;
                            string[] uiTypes = Enum.GetNames(typeof(UIComponentType));
                            for (int i = 0; i < uiTypes.Length; i++) {
                                if (commandParts[1] == uiTypes[i].ToLower()) {    //if argument is one of ui types
                                    for (int j = 0; j < UIManager.UiComponents.Count; j++) {
                                        if (UIManager.UiComponents[j].Type.ToString().Equals(uiTypes[i])) {
                                            s += $"{j} - {UIManager.UiComponents[j].Name}\n";
                                        }
                                    }
                                }
                            }
                            outputText = s;
                        }
                        else {
                            outputText = "- Command Invalid: incorrect amount of arguments";
                        }
                        break;
                    case "spawn_uicomponent":
                        if (commandParts.Length == 1) {
                            string s = string.Empty;
                            string[] uiTypes = Enum.GetNames(typeof(UIComponentType));
                            for (int i = 0; i < uiTypes.Length; i++) {
                                s += $"- {uiTypes[i]}\n";
                            }
                            outputText = s;
                        }
                        else if (commandParts.Length == 2) {
                            string s = string.Empty;
                            string[] uiTypes = Enum.GetNames(typeof(UIComponentType));
                            for (int i = 0; i < uiTypes.Length; i++) {
                                if (commandParts[1] == uiTypes[i].ToLower()) {    //if argument is one of ui types
                                    switch ((UIComponentType)Enum.Parse(typeof(UIComponentType), uiTypes[i])) {
                                        case UIComponentType.ConsoleIcon:
                                            UIManager.CreateNewComponent((UIComponentType)Enum.Parse(typeof(UIComponentType), uiTypes[i]), name: uiTypes[i], texName: "piper96");
                                            break;
                                        case UIComponentType.ImageIcon:
                                            UIManager.CreateNewComponent((UIComponentType)Enum.Parse(typeof(UIComponentType), uiTypes[i]), name: uiTypes[i], texName: "piperb96");
                                            break;
                                        case UIComponentType.GameIcon:
                                            UIManager.CreateNewComponent((UIComponentType)Enum.Parse(typeof(UIComponentType), uiTypes[i]), name: uiTypes[i], texName: "piper32");
                                            break;
                                        default:
                                            UIManager.CreateNewComponent((UIComponentType)Enum.Parse(typeof(UIComponentType), uiTypes[i]), name: uiTypes[i]);
                                            break;
                                    }
                                }
                            }
                            outputText = s;
                        }
                        else {
                            outputText = "- Command Invalid: incorrect amount of arguments";
                        }
                        break;
                    case "show_commands":
                        string str = string.Empty;
                        foreach (XmlNode item in Resources.xmlDocument.SelectNodes("commands/command")) {
                            str += $"- {item.SelectSingleNode("name").InnerText}\n";
                        }
                        outputText = str;
                        break;
                }
            }
            else {
                outputText = "- Command not recognized";
            }
        }

    }
}