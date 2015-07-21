using BXP_MobileApp_WindowsPhone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BXP_MobileApp_WindowsPhone.ViewModel
{
    class ListeeViewModel
    {
        private ObservableCollection<ToDo> colTodoList;
        public ObservableCollection<ToDo> pColTodolist
        {
            get { return colTodoList; }
            set { colTodoList = value; }
        }


        public async Task fnparsingListeeList()
        {

        }
    }
}
