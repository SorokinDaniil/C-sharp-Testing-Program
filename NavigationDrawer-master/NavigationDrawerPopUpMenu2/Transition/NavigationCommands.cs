using System.Windows.Input;

namespace TestingProgram
{
    public static class NavigationCommands
    {
        public static RoutedCommand ShowRaceCommand = new RoutedCommand();
        public static RoutedCommand ShowSeasonCommand = new RoutedCommand();
        public static RoutedCommand GoBackCommand = new RoutedCommand();
    }
}