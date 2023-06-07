using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;

namespace CineWave.MVVM.ViewModel.Gallery;

public partial class HomeViewModel : BaseViewModel
{
    private readonly ObservableCollection<MovieCardBaseViewModel> _movies = new();
    public IEnumerable<MovieCardBaseViewModel> MovieCardViewModels => _movies;
    private readonly List<string> _trailers = new();
    [ObservableProperty] private string? _currentMovieId;

    public async Task GetMoviesFromApi()
    {
        // Fetch movie data from TMDb API
        // Parse the JSON response
        var results = JObject.Parse(await TmdbHelper.GetTopMovies()).GetValue("results");
        if (results == null) return;
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _movies.Clear();
            foreach (var result in results)
            {
                _movies.Add(new MovieCardBaseViewModel(
                    $"https://image.tmdb.org/t/p/w500/{result["poster_path"]}",
                    result["original_title"]?.ToString(),
                    result["overview"]?.ToString()
                ));
            }
        });
    }

    public async Task GetRandomTrailer()
    {
        var results = JObject.Parse(await TmdbHelper.GetTrailers()).GetValue("results");
        if (results == null) return;
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            _trailers.Clear();
            foreach (var result in results)
            {
                var id = result["id"];
                if (id == null) continue;
                _trailers.Add(id.ToString());
            }

            CurrentMovieId = _trailers[new Random().Next(_trailers.Count)];
        });
    }
}