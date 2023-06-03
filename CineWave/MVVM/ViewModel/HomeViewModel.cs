using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CineWave.Helpers;
using Newtonsoft.Json.Linq;

namespace CineWave.MVVM.ViewModel;

public class HomeViewModel : Core.ViewModel
{
    private readonly ObservableCollection<MovieCardViewModel> _movies = new();
    public IEnumerable<MovieCardViewModel> MovieCardViewModels => _movies;

    public HomeViewModel()
    {
        try
        {
            GetMoviesFromApi();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public void GetMoviesFromApi()
    {
        // Fetch movie data from TMDb API
        // Parse the JSON response
        var results = JObject.Parse(TmdbHelper.GetTopMovies())["results"];

        if (results == null) return;
        foreach (var result in results)
        {
            _movies.Add(new MovieCardViewModel(
                $"https://image.tmdb.org/t/p/w500/{result["poster_path"]}",
                result["original_title"]?.ToString(),
                result["overview"]?.ToString()
            ));
        }
    }
}