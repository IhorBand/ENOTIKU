// how to use
/*
let theMovieDbHelper = new TheMovieDbHelper({
    testVar: testVarValue,
    testVar2: testVar2Value
});
*/

function TheMovieDbHelper(initialValues)
{
    var self = this;
    // Init Properties
    self.apiKey = null;
    self.apiBaseUrl = "https://api.themoviedb.org/3/";

    // Load Properties From Params
    for(var key in initialValues) {
        if(initialValues.hasOwnProperty(key)) {
            this[key] = initialValues[key];
        }
    }

    // Init Properties
    self.curUrl = self.apiBaseUrl;


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
    
    this.GetTrendingAllWeek = function (callback) {
        $.ajax(
            self.GetAPIUrl("trending/all/week"),
            {
                type: 'GET',
                async: true,
                success: function (data) {
                    if (callback != null) {
                        callback(data);
                    }
                },
                error: function () {
                    console.log("error while getting trending movies");
                }
            });
    }
}