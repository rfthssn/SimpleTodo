using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTodo.Maui.Models
{
    public partial class TodoItem : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private bool isCompleted;
    }

}
