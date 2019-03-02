using System.Collections.Generic;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Core.Managers.Id;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Game.Models.Game.Items.Actions;

namespace AAEmu.Game.Scripts.Commands
{
    public class AddItem : BaseCommand
    {
        public override string Command => "add_item";

        public override string Description => "Adds an item to your inventory";

        public override string Syntax => "/add_item <itemId> <count?> <grade?>";

        public override void Execute(Character character, string[] args)
        {
            if (args.Length == 1)
            {
                DisplaySyntax(character);
                return;
            }

            if (!uint.TryParse(args[0], out uint itemId))
            {
                DisplayError(character,"You must enter a valid item id");
                return;
            }


            var count = 1;
            byte grade = 0;

            if (args.Length > 1)
            {
                if (!int.TryParse(args[1], out count))
                {
                    DisplayError(character, "You must enter a valid count");
                    return;
                }
            }
            if (args.Length > 2)
            {
                if (!byte.TryParse(args[1], out grade))
                {
                    DisplayError(character, "You must enter a valid grade");
                    return;
                }
            }



            var item = ItemManager.Instance.Create(itemId, count, grade, true);
            if (item == null)
            {
                DisplayError(character, "Item cannot be created");
                return;
            }

            var res = character.Inventory.AddItem(item);
            if (res == null)
            {
                ItemIdManager.Instance.ReleaseId((uint)item.Id);
                return;
            }

            var tasks = new List<ItemTask>();
            if (res.Id != item.Id)
                tasks.Add(new ItemCountUpdate(res, item.Count));
            else
                tasks.Add(new ItemAdd(item));
            character.SendPacket(new SCItemTaskSuccessPacket(ItemTaskType.AutoLootDoodadItem, tasks, new List<ulong>()));
        }
    }
}
