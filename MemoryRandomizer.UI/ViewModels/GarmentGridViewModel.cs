using MemoryRandomizer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemoryRandomizer.UI
{
    public class GarmentGridViewModel : NotifyPropertyChanged, IRandomizerViewModel
    {
        private string errors;
        private bool randomize;
        private bool loadSave;

        public string Errors { get => errors; private set => this.Set(ref errors, value); }
        public bool Randomize { get => randomize; set => this.Set(ref randomize, value); }
        public bool LoadSave { get => loadSave; set => this.Set(ref loadSave, value); }

        public ICommand DeleteCommand { get; private set; }

        public MainViewModel Parent { get; set; }

        public GarmentGridViewModel()
        {
            this.DeleteCommand = new AsyncDelegateCommand(this.DeleteSave, _ => true);
        }

        public Task DeleteSave(object _)
        {
            File.Delete(this.Parent.Path + SaveManager.GGSaveFileName);
            return Task.CompletedTask;
        }
    }
}
