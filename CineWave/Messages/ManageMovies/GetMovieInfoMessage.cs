using CineWave.MVVM.Model.Movies;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CineWave.Messages.ManageMovies;

public class GetMovieInfoMessage : ValueChangedMessage<EditMovieInfo>
{
    public GetMovieInfoMessage(EditMovieInfo value) : base(value)
    {
    }
}