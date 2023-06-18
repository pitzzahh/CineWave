using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public class ManageMoviesViewModel : BaseViewModel
{
    private readonly ObservableCollection<MovieInfoCardViewModel> _movieInfoCardViewModels = new();
    private readonly IUnitOfWork _unitOfWork;

    public ManageMoviesViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<MovieInfoCardViewModel> MovieInfoCardViewModels => _movieInfoCardViewModels;

    public async Task CreateMovieInfoCards()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _movieInfoCardViewModels.Clear();
            foreach (var movie in _unitOfWork.MoviesRepository.GetAll())
                _movieInfoCardViewModels.Add(
                    new MovieInfoCardViewModel(
                        movie.MovieName,
                        movie.MoviePrice,
                        movie.Runtime,
                        movie.ReleaseDate,
                        movie.ScreeningDateTime
                    )
                );
            _unitOfWork.Complete();
        });
    }
}