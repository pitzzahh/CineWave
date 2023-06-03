using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CineWave.Helpers;
using Newtonsoft.Json.Linq;

namespace CineWave.MVVM.ViewModel;

public class HomeViewModel : Core.ViewModel
{
    private readonly ObservableCollection<MovieCardViewModel> _movies;
    public IEnumerable<MovieCardViewModel> MovieCardViewModels => _movies;
    public HomeViewModel()
    {
        _movies = new ObservableCollection<MovieCardViewModel>();
        // Fetch movie data from TMDb API
        var movieData = TmdbHelper.GetTopMovies();

        // Parse the JSON response
        var jsonObject = JObject.Parse(movieData);
        var results = jsonObject["results"];
        
        if (results == null) return;
        foreach (var result in results)
        {
            var title = result["original_title"]?.ToString();
            var overview = result["overview"]?.ToString();
            var posterPath = result["poster_path"]?.ToString();
            var imageUrl = $"https://image.tmdb.org/t/p/w500/{posterPath}";

            _movies.Add(new MovieCardViewModel(imageUrl, title, overview));
        }
    }

}