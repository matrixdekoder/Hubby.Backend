using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.ChooseGenres
{
    public class ChooseGenresCommand: INotification
    {
        public ChooseGenresCommand(string buddyId, IEnumerable<string> genreIds)
        {
            BuddyId = buddyId;
            GenreIds = genreIds;
        }

        public string BuddyId { get; }
        public IEnumerable<string> GenreIds { get; }
    }
}
