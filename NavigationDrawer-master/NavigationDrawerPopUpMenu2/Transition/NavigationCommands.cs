using System.Windows.Input;

namespace TestingProgram
{
    public static class NavigationCommands
    {
        //РЕДАКТОР
        public static RoutedCommand ShowChoiceChaphterCommand = new RoutedCommand();
        public static RoutedCommand ShowAdmin_Editor_TableChaphterEditCommand = new RoutedCommand();
        //ЖУРНАЛ
        public static RoutedCommand ShowChoiceChaphterNoEditAdminCommand = new RoutedCommand();
        public static RoutedCommand ShowUser_ListStudent_TableTestNoEditCommand = new RoutedCommand();
        //ТЕСТЫ
        public static RoutedCommand ShowChoiceChaphterNoEditUserCommand = new RoutedCommand();


   public static RoutedCommand ShowAdmin_ListStudent_TableListStudentEditCommand = new RoutedCommand();
   public static RoutedCommand ShowAdmin_ListStudent_TableTestNoEditCommand = new RoutedCommand();
        public static RoutedCommand ShowAdmin_ListStudent_TablePassedTestNoEditCommand = new RoutedCommand();



        public static RoutedCommand ShowChoiceGroupCommand = new RoutedCommand();

        public static RoutedCommand ShowTabControlCommand = new RoutedCommand();

        public static RoutedCommand ShowMainTableCommand = new RoutedCommand();

        public static RoutedCommand GoBackCommand = new RoutedCommand();
        public static RoutedCommand GoForwardCommand = new RoutedCommand();




        public static RoutedCommand ShowMainWindowCommand = new RoutedCommand();
        public static RoutedCommand ShowTestingEditorCommand = new RoutedCommand();
        public static RoutedCommand ShowThemeEditorCommand = new RoutedCommand();
        public static RoutedCommand ShowTestingWindowCommand = new RoutedCommand();


        public static RoutedCommand GoBackQuestionCommand = new RoutedCommand();
        public static RoutedCommand GoNextQuestionCommand = new RoutedCommand();

    }
}

