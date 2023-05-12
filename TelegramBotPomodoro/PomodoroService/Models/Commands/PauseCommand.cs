﻿using Shared.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/pause")]
    internal class PauseCommand : CommandBase
    {
        public PauseCommand(IMessage message) : base(message)
        {
        }
    }
}
