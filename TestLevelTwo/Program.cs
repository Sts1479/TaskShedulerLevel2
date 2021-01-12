namespace TaskList
{
    class MainClass {
        static ConsoleView _ConsoleView = new ConsoleView();
        static ParseInput _ParseInput = new ParseInput();
        static CommandResponse _CommandResponse = new CommandResponse();
        static CommandType _CommandType;

        public static void Main(string[] args) {
            _ConsoleView.StartView();
            _CommandResponse.TryToGetDataFromFile();
            while (true) {
                _CommandType = _ParseInput.GetInputCommand();
                _CommandResponse.Response(_CommandType);
                if (_CommandType == CommandType.Exit) {
                    _CommandResponse.TryToSaveFile();
                    break;
                }
            }
        }
    }
}
