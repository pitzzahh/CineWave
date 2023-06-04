using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Helpers;
using Newtonsoft.Json.Linq;

namespace CineWave.MVVM.ViewModel.Gallery;

public class HomeViewModel : Core.ViewModel
{
    private readonly ObservableCollection<MovieCardViewModel> _movies = new();
    public IEnumerable<MovieCardViewModel> MovieCardViewModels => _movies;

    public HomeViewModel()
    {
        try
        {
            Task.Run(GetMoviesFromApi); // Run the method on a separate thread
        }
        catch (Exception e)
        {
            Debug.Print(e.StackTrace);
        }
    }

    public async Task GetMoviesFromApi()
    {
        // Fetch movie data from TMDb API
        // Parse the JSON response
        var results = JObject.Parse(await TmdbHelper.GetTopMovies()).GetValue("results");

        if (results == null) return;

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            foreach (var result in results)
            {
                _movies.Add(new MovieCardViewModel(
                    $"https://image.tmdb.org/t/p/w500/{result["poster_path"]}",
                    result["original_title"]?.ToString(),
                    result["overview"]?.ToString()
                ));
            }
        });
    }
}