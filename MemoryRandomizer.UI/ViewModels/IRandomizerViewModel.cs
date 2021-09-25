using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemoryRandomizer.UI
{
    public interface IRandomizerViewModel : INotifyPropertyChanged
    {
        public string Errors { get; }
        public bool Randomize { get; set; }
        public bool LoadSave { get; set; }

        public ICommand DeleteCommand { get; }

        public Task DeleteSave(object _);
    }
}
