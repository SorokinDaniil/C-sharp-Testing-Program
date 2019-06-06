using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingProgram
{
    class TabControlViewModel
    {
        MainTable Admin_ListStudent_TableListStudentEdit = new MainTable() { DataContext = new MainTableViewModel("Admin_ListStudent_TableListStudentEdit") };
        MainTable Admin_ListStudent_TableTestNoEdit = new MainTable() { DataContext = new MainTableViewModel("Admin_ListStudent_TableTestNoEdit") };

      public TabControlViewModel(string selectedvaluegroup ,string selectedvaluechaphter)
        {
          

        }
    }
}
