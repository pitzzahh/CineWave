using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.DB.Core;

namespace CineWave.MVVM.ViewModel.Reservations;

public class ReservationsViewModel : BaseViewModel
{
    private readonly ObservableCollection<RMovieInfoCardViewModel> _movieInfoCardViewModels = new();
    public IEnumerable<RMovieInfoCardViewModel> MovieInfoCardViewModels => _movieInfoCardViewModels;
    private readonly IUnitOfWork _unitOfWork;

    public ReservationsViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateMovieInfoCards()
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _movieInfoCardViewModels.Clear();
            foreach (var movie in _unitOfWork.MoviesRepository.GetAll())
            {
                _movieInfoCardViewModels.Add(
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