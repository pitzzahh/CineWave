using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;
using CineWave.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.ManageMovies;

public partial class ManageMoviesViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<MovieInfoCardViewModel> _movieInfoCardViewModels = new();
    private readonly IUnitOfWork _unitOfWork;

    public ManageMoviesViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateMovieInfoCards()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            MovieInfoCardViewModels.Clear();
            foreach (var movie in _unitOfWork.MoviesRepository.GetAll())
                MovieInfoCardViewModels.Add(
                    new MovieInfoCardViewModel(
                        movie.Name ?? StringHelper.MovieNotFound,
                        movie.Price,
                        movie.Runtime,
                        movie.ReleaseDate,
                        movie.ScreeningDateTime
                    )
                );
            _unitOfWork.Complete();
        });
    }
}