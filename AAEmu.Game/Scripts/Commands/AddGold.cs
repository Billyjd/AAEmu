using System.Collections.Generic;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Items.Actions;

namespace AAEmu.Game.Scripts.Commands
{
    public class AddGold : BaseCommand
    {
        public override string Command => "add_gold";

        public override string Description => "Adds the specified amount of gold to character";

        public override string Syntax => "/add_gold <amount>";

        public override void Execute(Character character, string[] args)
        {
            if (args.Length == 1)
            {
                DisplaySyntax(character);
                return;
            }

            if (!int.TryParse(args[1], out int amount))
            {
                DisplayError(character, "You must enter a valid gold amount");
                return;
            }

            character.Money += amount;
            character.SendPacket(new SCItemTaskSuccessPacket(ItemTaskType.AutoLootDoodadItem, new List<ItemTask> { new MoneyChange(amount) }, new List<ulong>()));
        }
    }
}
