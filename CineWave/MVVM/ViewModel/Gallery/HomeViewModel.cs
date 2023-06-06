using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CineWave.Helpers;
using Newtonsoft.Json.Linq;

namespace CineWave.MVVM.ViewModel.Gallery;

public partial class HomeViewModel : BaseViewModel
{
    private readonly ObservableCollection<MovieCardBaseViewModel> _movies = new();
    public IEnumerable<MovieCardBaseViewModel> MovieCardViewModels => _movies;

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
}