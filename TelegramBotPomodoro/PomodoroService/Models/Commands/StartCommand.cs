﻿using Shared.Models;

namespace PomodoroService.Models.Commands
{
    [CommandString("/start")]
    internal class StartCommand : CommandBase
    {
        public StartCommand(IMessage message) : base(message)
        {
        }
    }
}
