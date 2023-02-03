# Roketin-movie-festival

### Warning
As of 03/02/2023 when creating this document, I cannot test the project because the download process of the required NuGet packages timed out. This happened from 11:00. This is not caused by my bad settings or bad connections. This is caused at problems on the Microsoft's side.

### Set-up
1. Open Cmd at this file's location, then run `cd MovieFestival`.
2. Run `dotnet ef migrations add InitialCreate` to add migration.
3. Run `dotnet ef database update`.
4. Run `dotnet run`.

### Admin Endpoints
#### POST api/admin/Create
Create a new movie
Request Body Params:
	Title: string
	Description: string
	Artists: string[]
	Genres: string[]
	WatchUrl: string
	
#### PUT api/admin/Update/{id}
Update an existing movie
Request Body Params:
	Title: string
	Description: string
	Artists: string[]
	Genres: string[]
	WatchUrl: string
	
#### GET api/admin/GetMostViewedMovie
Returns the most viewed movie
	
#### GET api/admin/GetMostViewedGenre
Returns the most viewed genre

### All Users Endpoints
#### GET api/GetList?PageNumber={pageNumber}&PageSize={pageSize}
List all movies with pagination
Request Query Params:
pageNumber: the current page number (default: 1)
pageSize: the current page size (default: 10)

#### GET api/Search?searchTerm={searchTerm}
Search movies by title/description/artists/genres
Request Query Params:
searchTerm: the search term to be used in searching movies

#### GET api/TrackViewership?MovieId={movieId}
Track movie viewership
Request Query Params:
movieId: the movie ID to get the current viewer count


