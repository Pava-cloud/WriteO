# WriteO

WriteO is the OOP-based successor to WriteC, a C# console application designed for messaging and file management with a modular, maintainable architecture.

## Features

- **Multi-Language Support**: Choose your preferred language (currently EN and DE supported).
- **Messaging System**: Scalable messaging capabilities.
- **File Server**: Robust file management and sharing.
- **Secure Data Handling**: Integrated text encoding/decoding.
- **Modern CLI**: Beautiful console interface powered by [Spectre.Console](https://spectreconsole.net/).

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Pava-cloud/WriteO.git
   ```
2. Navigate to the project directory:
   ```bash
   cd WriteO/WriteO
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Usage

When you first launch WriteO, you will be prompted to:
1. Enter your name.
2. Select your preferred language (EN or DE).
3. Specify a path for the server logs and files.

Once initialized, you can navigate through the menu using your keyboard.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [Spectre.Console](https://github.com/spectreconsole/spectre.console) for the amazing terminal UI components.
- Alex on StackOverflow for the `ClearAll` implementation (CC BY-SA 4.0).
