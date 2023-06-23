using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Reservations.MovieList;

public partial class MovieListViewModel : BaseViewModel
{
    private readonly IUnitOfWork _unitOfWork;
    [ObservableProperty] private ObservableCollection<RMovieInfoCardViewModel> _rMovieInfoCardViewModels = new();

    public MovieListViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateMovieInfoCards()
    {
        var availableMoviesForReservation = _unitOfWork.MoviesRepository.GetAvailableMoviesForReservation();
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            RMovieInfoCardViewModels.Clear();
            foreach (var movie in availableMoviesForReservation)
            {
                RMovieInfoCardViewModels.Add(
                    new RMovieInfoCardViewModel(
                        movie.Name ?? StringHelper.MovieNotFound,
                        movie.Price,
                        movie.Runtime,
                        movie.ReleaseDate,
                        movie.ScreeningDateTime
                    )
                );
            }
        });
    }
}