﻿using MediatR;

namespace Shared.Models
{
    public interface IMessage : INotification
    {
        public int? Id { get; set; }
        public long Author { get; set; }
        public long Receiver { get; set; }
        public string Text { get; set; }
    }
}
