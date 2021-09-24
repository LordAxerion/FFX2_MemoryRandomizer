using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MemoryRandomizer.UI
{
    interface IRandomizerViewModel : INotifyPropertyChanged
    {
        public bool Randomize { get; set; }
        public bool LoadSave { get; set; }
    }
}
