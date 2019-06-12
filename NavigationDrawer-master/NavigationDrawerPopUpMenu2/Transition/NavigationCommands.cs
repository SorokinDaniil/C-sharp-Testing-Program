using System.Windows.Input;

namespace TestingProgram
{
    public static class NavigationCommands
    {
       
        public static RoutedCommand ShowChoiceGroupCommand = new RoutedCommand();
        public static RoutedCommand ShowChoiceChaphterCommand = new RoutedCommand();
        public static RoutedCommand ShowChoiceChaphterNoEditCommand = new RoutedCommand();
public static RoutedCommand ShowAdmin_Editor_TableChaphterEditCommand = new RoutedCommand();
        public static RoutedCommand ShowTabControlCommand = new RoutedCommand();

        public static RoutedCommand ShowMainTableCommand = new RoutedCommand();

        public static RoutedCommand GoBackCommand = new RoutedCommand();
        public static RoutedCommand GoForwardCommand = new RoutedCommand();




        public static RoutedCommand ShowMainWindowCommand = new RoutedCommand();
        public static RoutedCommand ShowTestingEditorCommand = new RoutedCommand();
        public static RoutedCommand ShowThemeEditorCommand = new RoutedCommand();
        public static RoutedCommand ShowTestingWindowCommand = new RoutedCommand();
    }
}

