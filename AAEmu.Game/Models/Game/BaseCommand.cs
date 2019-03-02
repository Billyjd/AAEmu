using System;
using System.Collections.Generic;
using System.Text;
using AAEmu.Game.Models.Game.Char;

namespace AAEmu.Game.Models.Game
{
    public abstract class BaseCommand
    {
        public abstract string Command { get; }
        public abstract string Description { get; }
        public abstract string Syntax { get; }
        public abstract void Execute(Character character, string[] args);

        public void DisplaySyntax(Character character)
        {
            character.SendMessage("/"+Command + " - " +Description + "\n" + "Usage: " + Syntax);
        }
        public void DisplayError(Character character, string message)
        {
            character.SendMessage("Command error: "+message);
        }
    }
}
