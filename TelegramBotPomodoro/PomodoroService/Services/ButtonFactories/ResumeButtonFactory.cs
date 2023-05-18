﻿using Shared.Models.Telegram;
using TelegramCommon.Services.ButtonFactories;

namespace PomodoroService.Services.ButtonFactories
{
    internal class ResumeButtonFactory : IButtonFactory
    {
        public IAnswerButton CreateButton(params object[] args)
        {
            return new AnswerInlineButton
            {
                Text = string.Format("Resume [{0:mm\\:ss}]", args),
                CallbackData = "/resume"
            };
        }
    }
}
