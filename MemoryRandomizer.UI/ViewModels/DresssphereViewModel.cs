using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemoryRandomizer.UI
{
    public class DresssphereViewModel : NotifyPropertyChanged, IRandomizerViewModel
    {
        private string errors;
        private bool randomize;
        private bool loadSave;

        public string Errors { get => errors; private set => this.Set(ref errors, value); }
        public bool Randomize { get => randomize; set => this.Set(ref randomize, value); }
        public bool LoadSave { get => loadSave; set => this.Set(ref loadSave, value); }

        public MainViewModel Parent { get; set; }

        public async Task DeleteSave(object _)
        {

        }
    }
}
