using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.UI
{
    public class GarmentGridViewModel : NotifyPropertyChanged, IRandomizerViewModel
    {
        private bool randomize;
        private bool loadSave;

        public bool Randomize { get => randomize; set => this.Set(ref randomize, value); }
        public bool LoadSave { get => loadSave; set => this.Set(ref loadSave, value); }
    }
}
