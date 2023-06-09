using System.Diagnostics;
using System.Linq;
using CineWave.Components;
using CineWave.DB.Core;
using CineWave.Messages.SeatsBooking;
using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CineWave.MVVM.ViewModel.SeatsBooking;

public partial class SeatCardViewModel : BaseViewModel
{
    [ObservableProperty] private bool _isSeatAvailable;
    [ObservableProperty] private string? _seatNumber;
    private readonly IUnitOfWork _unitOfWork;
    public SeatCardViewModel(string seatNumber, bool isTaken, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        SeatNumber = seatNumber;
        IsSeatAvailable = !isTaken;
    }

    [RelayCommand]
    public void OpenForm()
    {
        if (!IsSeatAvailable) return;
        Debug.Assert(App.ServiceProvider != null, "App.ServiceProvider != null");
        var seatBookingRegistrationForm = App.ServiceProvider.GetRequiredService<SeatBookingRegistrationForm>();
        if (seatBookingRegistrationForm.IsVisible)
        {
            seatBookingRegistrationForm.Hide();
        }
        else
        {
            seatBookingRegistrationForm.Show();
            var currentMovie = _unitOfWork.MoviesRepository.GetAll().FirstOrDefault(m => m.NowShowing);
            WeakReferenceMessenger.Default.Send(new GetSeatInfoMessage(new ReservationInfo(currentMovie?.MovieName ?? "", currentMovie?.MoviePrice?? 0, SeatNumber ?? "")));
        }
    }
}