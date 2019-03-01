using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Buddy.Application.CommandService.Genre.Create
{
    public class CreateGenreCommand: INotification
    {
        public CreateGenreCommand(string name)
        {
            Name = name;
        }


        public string Name { get; }
    }
}
