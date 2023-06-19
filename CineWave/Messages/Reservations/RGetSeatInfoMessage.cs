using CineWave.MVVM.Model.SeatsBooking;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CineWave.Messages.Reservations;

public class RGetSeatInfoMessage : ValueChangedMessage<BookMovieInfo>
{
    public RGetSeatInfoMessage(BookMovieInfo value) : base(value)
    {
    }
}