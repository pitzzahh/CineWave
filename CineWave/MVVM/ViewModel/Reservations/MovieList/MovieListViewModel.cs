using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Reservations.MovieList;

public partial class MovieListViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<RMovieInfoCardViewModel> _movieInfoCardViewModels = new();
    private readonly IUnitOfWork _unitOfWork;

    public MovieListViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateMovieInfoCards()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            MovieInfoCardViewModels.Clear();
            foreach (var movie in _unitOfWork.MoviesRepository.GetAll())
            {
                MovieInfoCardViewModels.Add(
                    new RMovieInfoCardViewModel(
                        movie.MovieName,
                        movie.MoviePrice,
                        movie.Runtime,
                        movie.ReleaseDate,
                        movie.ScreeningDateTime
                    )
                );
            }
            _unitOfWork.Complete();
        });
    }
}