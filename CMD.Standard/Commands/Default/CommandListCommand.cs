﻿using Core.Attributes;

namespace Core.Commands
{
    [AutoRegistrate]
    [Description("Prints out all available commands.")]
    public class CommandListCommand : Command
    {
        public CommandListCommand() : base("clist") { }

        protected override ExecutionResult Execute()
        {
            var commands = ExecutionService.GetAllCommandsIDs();            
            return ExecutionResult.Success(string.Join("; ", commands));
        }
    }
}