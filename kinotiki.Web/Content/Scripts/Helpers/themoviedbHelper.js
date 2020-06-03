// how to use
/*
let theMovieDbHelper = new TheMovieDbHelper({
    testVar: testVarValue,
    testVar2: testVar2Value
});
*/

/* TODO: */
// Need to move it to Backend(C#) in the future
function TheMovieDbHelper(initialValues)
{
    var self = this;
    // Init Properties
    self.apiKey = null;
    self.apiBaseUrl = "https://api.themoviedb.org/3/";
    self.defaultLanguage = "en-US";

    // Load Properties From Params
    for(var key in initialValues) {
        if(initialValues.hasOwnProperty(key)) {
            this[key] = initialValues[key];
        }
    }

    // Init Properties


    //TODO: 
    // Remove it !!!
    // Move All MovieDB API calls to BackEnd
    if (self.apiKey == null) {
        $.ajax(
            "/api/settings/moviedb/key",
            {
                type: 'GET',
                async: false,
                success: function(data) {
                    if (data != null)
                        self.apiKey = data;
                },
                error: function () {
                    console.log("error while getting API Key For MovieDB");
                }
            }
        );
    }


    // Class Functions

    this.GetAPIUrl = function (apiAction) {
        if (apiAction != null) {
            if (apiAction.includes("?"))
                apiAction += "&";
            else
                apiAction += "?";
            return self.apiBaseUrl + apiAction + "api_key=" + self.apiKey
        }
        return "";
    }

    this.ExecCustomMethod = function (apiUrl, type, callback, errorCallback) {
        $.ajax(
            self.GetAPIUrl("trending/all/week"),
            {
                type: type,
                async: true,
                success: function (data) {
                    if (callback != null) {
                        callback(data);
                    }
                },
                error: function (error) {
                    console.log("error while getting trending movies");
                    if (errorCallback != null)
                        errorCallback(errorCallback);
                }
            }
        );
    }
    
    this.GetTrendingAllWeek = function (callback, errorCallback) {
        self.ExecCustomMethod(self.GetAPIUrl("trending/all/week"), 'GET', callback, errorCallback);
    }

    this.GetMovie = function (movieId, language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("movie/" + movieId + "?language=" + language), 'GET', callback, errorCallback);
    }

    this.SearchMovies = function (query, language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("search/movie?query=" + encodeURIComponent(query) + "&language=" + language), 'GET', callback, errorCallback);
    }

    this.GetRecomendations = function(movieId, language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("movie/" + movieId + "/recommendations?language=" + language), 'GET', callback, errorCallback);
    }

    this.GetNowPlaying = function (language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("/movie/now_playing?language=" + language), 'GET', callback, errorCallback);
    }

    this.GetMovieVideos = function (movieId, language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("/movie/" + movieId + "/videos?language=" + language), 'GET', callback, errorCallback);
    }

    this.GetTopRatedMovies = function (language, callback, errorCallback) {
        if (language == null) {
            language = self.defaultLanguage;
        }
        self.ExecCustomMethod(self.GetAPIUrl("/movie/top_rated?language=" + language), 'GET', callback, errorCallback);
    }
}
