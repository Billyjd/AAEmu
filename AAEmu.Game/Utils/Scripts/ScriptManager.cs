using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AAEmu.Commons.Utils;
using AAEmu.Game.Models.Game;
using AAEmu.Game.Models.Game.Char;
using NLog;

namespace AAEmu.Game.Utils.Scripts
{
    public class ScriptManager : Singleton<ScriptManager>
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        private Dictionary<string, BaseCommand> commands = new Dictionary<string, BaseCommand>();

        public void LoadCommands()
        {
            commands.Clear();

            var assemblyToScanForCommands = Assembly.GetExecutingAssembly();

            foreach (var type in assemblyToScanForCommands.GetTypes())
            {
                if (type.IsClass == false)
                    continue;
                if (type.BaseType == typeof(BaseCommand))
                {
                    BaseCommand gameCommand = (BaseCommand)Activator.CreateInstance(type);
                    commands.Add(gameCommand.Command.ToLower(), gameCommand);
                }
            }

            _log.Info("Loaded " + commands.Count + " commands!");
        }
        public bool HandleCommand(Character character, string commandLine)
        {
            try
            {
                if (string.IsNullOrEmpty(commandLine))
                    return false;

                string[] commandValues = commandLine.Replace("/", "").Split(' ');
                string command = commandValues[0].ToLower();
                BaseCommand gameCommand = GetCommand(command);

                if (gameCommand == null)
                    return false;

                //Execute the command
                gameCommand.Execute(character, commandValues);

            }
            catch (Exception e)
            {
                if (_log.IsErrorEnabled)
                    _log.Error(e, nameof(HandleCommand), null);
            }
            return true;
        }
        public BaseCommand GetCommand(string command)
        {
            BaseCommand gameCommand = null;
            commands.TryGetValue(command, out gameCommand);

            return gameCommand;
        }
    }
}
