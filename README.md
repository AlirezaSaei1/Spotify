# Spotify Clone

Welcome to the Spotify Clone project! This application aims to provide a music streaming platform where users can discover, follow artists, and enjoy their favorite music.

## Features

- **User Authentication**: Users can create accounts, confirm email, log in, and enjoy a personalized music experience.
- **Artist Profiles**: Artists can create profiles, upload, and manage their music. Email confirmation is required for artist registration.
- **Follow/Unfollow**: Users can follow their favorite artists to stay updated with their latest releases.
- **Music Storage**: Music files are stored securely in an object storage service.
- **Download and Save**: Users can download and save their favorite tracks to enjoy offline.

## Getting Started

### Prerequisites

- [ASP.NET Core](https://dotnet.microsoft.com/download)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Object Storage Service](#) - (Replace with your chosen object storage provider)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/AlirezaSaei1/Spotify.git
    ```

2. Navigate to the project directory:

    ```bash
    cd Spotify
    ```

3. Restore dependencies:

    ```bash
    dotnet restore
    ```

4. Update appsettings.json:

    Update the `ConnectionStrings` section in the `appsettings.json` file with your database and object storage credentials.

5. Apply migrations:

    ```bash
    dotnet ef database update
    ```

6. Run the application:

    ```bash
    dotnet run
    ```

Visit `http://localhost:PORT` in your browser to access the application.

## Usage

- Create a user account or log in if you already have one. Confirm your email to activate your account.
- Explore artists, follow/unfollow them, and discover new music.
- Artists can upload music files, and users can download and save their favorite tracks.
- Enjoy your personalized music experience!

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to enhance the project.

## License

This project is licensed under the [MIT License](LICENSE).
