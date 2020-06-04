
let theMovieDbHelper = new TheMovieDbHelper();

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

/* Show User Recomended Movies */
function RenderUserRecomended(movies) {
    let size = 4;
    if (movies.length < size)
        size = movies.length;
    let container = jQuery('.js--user-recomended-container');

    for (let i = 0; i < size; i++) {
        var movie = movies[i];
        if (movie != null) {
            var itemPrototype = container.find('.js--user-recomended-item.item-prototype').clone();
            itemPrototype.removeClass('item-prototype');
            let movieName = movie.name != null ? movie.name : movie.title;
            let fullName = movieName;
            if (movieName.length > 15) {
                movieName = movieName.slice(0, 16) + "...";
            }
            itemPrototype.find('.js--movie-img img').attr("src", theMovieDbHelper.posterUrl + movie.poster_path);
            itemPrototype.find('.js--movie-rating').text(movie.vote_average);
            let txtMovieName = itemPrototype.find('.js--movie-name');
            txtMovieName.text(movieName);
            txtMovieName.attr("title", fullName);
            if (movie.genre_ids != null && movie.genre_ids.length != null && movie.genre_ids.length > 0) {
                let randIndex = getRandomInt(0, movie.genre_ids.length - 1);
                let genre = theMovieDbHelper.GetGenreById(movie.genre_ids[randIndex],
                    function (error) {
                        console.log(error);
                    });
                if (genre != null) {
                    itemPrototype.find('.js--movie-genre').text(genre.name);
                } else {
                    itemPrototype.find('.js--movie-genre').hide();
                }
            }
            else {
                itemPrototype.find('.js--movie-genre').hide();
            }
            container.append(itemPrototype);
        }
    }
}

function RenderTrendingMovies(movies) {
    let size = 9;
    if (movies.length < size)
        size = movies.length;
    let container = jQuery('.js--trending-container');

    for (let i = 0; i < size; i++) {
        var movie = movies[i];
        if (movie != null) {
            var itemPrototype = container.find('.js--trending-item.item-prototype').clone();
            itemPrototype.removeClass('item-prototype');
            let movieName = movie.name != null ? movie.name : movie.title;
            let fullName = movieName;
            if (movieName.length > 15) {
                movieName = movieName.slice(0, 16) + "...";
            }
            itemPrototype.find('.js--movie-img img').attr("src", theMovieDbHelper.posterUrl + movie.poster_path);
            itemPrototype.find('.js--movie-rating').text(movie.vote_average);
            let txtMovieName = itemPrototype.find('.js--movie-name');
            txtMovieName.text(movieName);
            txtMovieName.attr("title", fullName);
            container.append(itemPrototype);
        }
    }
}

function RenderTopMovies(movies) {
    let size = 15;
    if (movies.length < size)
        size = movies.length;
    let container = jQuery('.js--top-movies-container');

    jQuery('.js--first-popular-img').attr("src", theMovieDbHelper.posterUrl + movies[0].poster_path);

    for (let i = 0; i < size; i++) {
        var movie = movies[i];
        if (movie != null) {
            var itemPrototype = container.find('.js--top-movies-item.item-prototype').clone();
            itemPrototype.removeClass('item-prototype');
            let movieName = movie.name != null ? movie.name : movie.title;
            let fullName = movieName;
            if (movieName.length > 50) {
                movieName = movieName.slice(0, 16) + "...";
            }
            let topNumber = i >= 0 && i < 9 ? '0' + (i + 1) : (i + 1);

            itemPrototype.find('.js--top-number').text(topNumber);
            itemPrototype.find('.js--views-number').text(movie.popularity);
            let txtMovieName = itemPrototype.find('.js--movie-name');
            txtMovieName.text(movieName);
            txtMovieName.attr("title", fullName);
            container.append(itemPrototype);
        }
    }
}


function GetTopRatedMovies() {
    theMovieDbHelper.GetTopRatedMovies(null,
        function (data) {
            if (data != null && data.results != null && data.results.length != null && data.results.length > 0) {
                let topRatedMovies = data.results;
                RenderUserRecomended(topRatedMovies);
            }
        },
        function (error) {
            console.log(error);
        }
    );
}



function GetTrendingMovies() {
    theMovieDbHelper.GetTrendingAllWeek(
        function (data) {
            if (data != null && data.results != null && data.results.length != null && data.results.length > 0) {
                let movies = data.results;
                RenderTrendingMovies(movies);

                GetVideosForMovies(movies);
            }
        },
        function (error) {
            console.log(error);
        }
    );
}

function GetPopularMovies() {
    theMovieDbHelper.GetPopularMovies(null,
        function (data) {
            if (data != null && data.results != null && data.results.length != null && data.results.length > 0) {
                let movies = data.results;
                RenderTopMovies(movies);
            }
        },
        function (error) {
            console.log(error);
        }
    );
}



function GetVideosForMovies(movies) {
    let size = 4;
    if (movies.length < size)
        size = movies.length;

    for (let i = 0; i < size; i++) {
        let _movie = movies[i];
        theMovieDbHelper.GetMovieVideos(_movie.id, null,
            function (data) {
                if (data != null && data.results != null && data.results.length != null && data.results.length > 0) {
                    let trailer = GetTrailer(data.results);
                    if (trailer != null) {
                        addMovieTrailerToContainer(_movie, trailer);
                    }
                }
            },
            function (error) {
                console.log(error);
            }
        );
    }
}

function GetTrailer(movieVideos) {
    let trailer = null;
    for (let i = 0; i < movieVideos.length; i++) {
        let video = movieVideos[i];
        if (video != null && video.type != null && video.type.toLowerCase() == "trailer") {
            trailer = video;
            break;
        }
    }
    if (trailer == null)
        trailer = movieVideos[0];
    return trailer;
}

function addMovieTrailerToContainer(movie, trailer) {
    let container = jQuery('.js--trailer-container');
    let itemPrototype = jQuery('.item-prototype.js--trailer-item').clone();
    itemPrototype.removeClass('item-prototype');
    let url = "";
    if (trailer.site.toLowerCase() == 'youtube')
        url = "https://www.youtube.com/embed/" + trailer.key;
    else
        url = "https://vimeo.com/" + trailer.key;
    let iframe = jQuery('<iframe class="embed-responsive-item" width="560" height="315" src="' + url + '" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>');
    itemPrototype.find('.js--trailer-wrapper').prepend(iframe);
    let txtMovieName = itemPrototype.find('.js--movie-name');
    let movieName = movie.name != null ? movie.name : movie.title;
    let fullName = movieName;
    if (movieName.length > 15) {
        movieName = movieName.slice(0, 16) + "...";
    }
    txtMovieName.text(movieName);
    txtMovieName.attr("title", fullName);
    container.append(itemPrototype);
}


jQuery(document).ready(function () {
    //UserRecomended
    GetTopRatedMovies();

    GetTrendingMovies();

    GetPopularMovies();
});



document.getElementById('s1t1').addEventListener("click", function () {
    document.getElementById('slide1').style.display = "none";
    document.getElementById('slide2').style.display = "block";
});
document.getElementById('s1t2').addEventListener("click", function () {
    document.getElementById('slide1').style.display = "none";
    document.getElementById('slide3').style.display = "block";
});
document.getElementById('s1t3').addEventListener("click", function () {
    document.getElementById('slide1').style.display = "none";
    document.getElementById('slide4').style.display = "block";
});
document.getElementById('s1t4').addEventListener("click", function () {
    document.getElementById('slide1').style.display = "none";
    document.getElementById('slide5').style.display = "block";
});



document.getElementById('s2t1').addEventListener("click", function () {
    document.getElementById('slide2').style.display = "none";
    document.getElementById('slide1').style.display = "block";
});
document.getElementById('s2t2').addEventListener("click", function () {
    document.getElementById('slide2').style.display = "none";
    document.getElementById('slide3').style.display = "block";
});
document.getElementById('s2t3').addEventListener("click", function () {
    document.getElementById('slide2').style.display = "none";
    document.getElementById('slide4').style.display = "block";
});
document.getElementById('s2t4').addEventListener("click", function () {
    document.getElementById('slide2').style.display = "none";
    document.getElementById('slide5').style.display = "block";
});



document.getElementById('s3t1').addEventListener("click", function () {
    document.getElementById('slide3').style.display = "none";
    document.getElementById('slide1').style.display = "block";
});
document.getElementById('s3t2').addEventListener("click", function () {
    document.getElementById('slide3').style.display = "none";
    document.getElementById('slide2').style.display = "block";
});
document.getElementById('s3t3').addEventListener("click", function () {
    document.getElementById('slide3').style.display = "none";
    document.getElementById('slide4').style.display = "block";
});
document.getElementById('s3t4').addEventListener("click", function () {
    document.getElementById('slide3').style.display = "none";
    document.getElementById('slide5').style.display = "block";
});



document.getElementById('s4t1').addEventListener("click", function () {
    document.getElementById('slide4').style.display = "none";
    document.getElementById('slide1').style.display = "block";
});
document.getElementById('s4t2').addEventListener("click", function () {
    document.getElementById('slide4').style.display = "none";
    document.getElementById('slide2').style.display = "block";
});
document.getElementById('s4t3').addEventListener("click", function () {
    document.getElementById('slide4').style.display = "none";
    document.getElementById('slide3').style.display = "block";
});
document.getElementById('s4t4').addEventListener("click", function () {
    document.getElementById('slide4').style.display = "none";
    document.getElementById('slide5').style.display = "block";
});



document.getElementById('s5t1').addEventListener("click", function () {
    document.getElementById('slide5').style.display = "none";
    document.getElementById('slide1').style.display = "block";
});
document.getElementById('s5t2').addEventListener("click", function () {
    document.getElementById('slide5').style.display = "none";
    document.getElementById('slide2').style.display = "block";
});
document.getElementById('s5t3').addEventListener("click", function () {
    document.getElementById('slide5').style.display = "none";
    document.getElementById('slide3').style.display = "block";
});
document.getElementById('s5t4').addEventListener("click", function () {
    document.getElementById('slide5').style.display = "none";
    document.getElementById('slide4').style.display = "block";
});
