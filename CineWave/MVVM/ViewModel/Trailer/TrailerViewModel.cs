using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CineWave.MVVM.ViewModel.Trailer;

public partial class TrailerViewModel : BaseViewModel
{
    [ObservableProperty] private Uri? _videoSource;

    [ObservableProperty] private string? _youtubeVideoId;

    public TrailerViewModel()
    {
        _youtubeVideoId = "dLR_D2IJE1M"; // SAMPLE
        LoadYoutubeVideo();
    }

    private void LoadYoutubeVideo()
    {
        // Set the video source
        VideoSource = new Uri($"https://www.youtube.com/embed/{YoutubeVideoId}");
    }
}