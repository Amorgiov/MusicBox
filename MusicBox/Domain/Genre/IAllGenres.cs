using System.Collections.Generic;
using MusicBox.ViewModels;

namespace MusicBox.Domain.Genres
{
    public interface IAllGenres
    {
        public IEnumerable<GenresViewModel> AllGenreses { get; }
    }
}