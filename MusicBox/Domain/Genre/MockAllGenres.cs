using System.Collections.Generic;
using MusicBox.ViewModels;

namespace MusicBox.Domain.Genres
{
    public class MockAllGenres : IAllGenres
    {
        public IEnumerable<GenresViewModel> AllGenreses
        {
            get
            {
                return new List<GenresViewModel>
                {
                    new GenresViewModel {Name = "AlternativeRock"},
                    new GenresViewModel {Name = "Ambient"},
                    new GenresViewModel {Name = "Classical"},
                    new GenresViewModel {Name = "Country"},
                    new GenresViewModel {Name = "Dance"},
                    new GenresViewModel {Name = "DanceHall"},
                    new GenresViewModel {Name = "DeepHouse"},
                    new GenresViewModel {Name = "Disco"},
                    new GenresViewModel {Name = "DrumBass"},
                    new GenresViewModel {Name = "DubStep"},
                    new GenresViewModel {Name = "Electronic"},
                    new GenresViewModel {Name = "Folk"},
                    new GenresViewModel {Name = "HipHop"},
                    new GenresViewModel {Name = "House"},
                    new GenresViewModel {Name = "Indie"},
                    new GenresViewModel {Name = "Jazz"},
                    new GenresViewModel {Name = "Metal"},
                    new GenresViewModel {Name = "Piano"},
                    new GenresViewModel {Name = "Pop"},
                    new GenresViewModel {Name = "Soul"},
                    new GenresViewModel {Name = "Reggae"},
                    new GenresViewModel {Name = "ReggaeTon"},
                    new GenresViewModel {Name = "Rock"},
                    new GenresViewModel {Name = "SoundTrack"},
                    new GenresViewModel {Name = "Techno"},
                    new GenresViewModel {Name = "Trance"},
                    new GenresViewModel {Name = "Trap"},
                    new GenresViewModel {Name = "TripHop"},
                    new GenresViewModel {Name = "World"},
                };
            }
        }
    }
}
