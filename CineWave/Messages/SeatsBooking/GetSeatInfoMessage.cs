using CineWave.MVVM.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CineWave.Messages.SeatsBooking;

public class GetSeatInfoMessage : ValueChangedMessage<ReservationInfo>
{
    public GetSeatInfoMessage(ReservationInfo value) : base(value)
    {
    }
}