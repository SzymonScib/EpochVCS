# EpochVCS

EpochVCS is a lightweight version control system designed to manage and track changes in files and directories. This project aims to provide essential version control functionalities similar to popular systems while maintaining simplicity and ease of use.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- Commit changes to a repository
- Push and pull updates
- Manage branches
- View commit history
- Rollback to previous versions

## Getting Started

To get started with EpochVCS, follow these steps:

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/EpochVCS.git
   ```

2. Navigate to the project directory:
   ```
   cd EpochVCS
   ```

3. Build the solution:
   ```
   dotnet build src/EpochVCS/EpochVCS.sln
   ```

4. Run the application:
   ```
   dotnet run --project src/EpochVCS/EpochVCS.csproj
   ```

## Usage

After running the application, you can use the following commands:

- `commit <message>`: Commit changes with a message.
- `push`: Push changes to the remote repository.
- `pull`: Pull updates from the remote repository.
- `history`: View the commit history.
- `rollback <commit_id>`: Rollback to a specific commit.

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes and commit them (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.