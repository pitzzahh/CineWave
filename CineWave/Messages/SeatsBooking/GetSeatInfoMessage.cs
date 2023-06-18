using CineWave.MVVM.Model.SeatsBooking;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CineWave.Messages.SeatsBooking;

public class GetSeatInfoMessage : ValueChangedMessage<BookMovieInfo>
{
    public GetSeatInfoMessage(BookMovieInfo value) : base(value)
    {
    }
}