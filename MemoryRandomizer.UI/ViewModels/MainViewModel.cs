using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MemoryRandomizer.UI
{
    public class MainViewModel : INotifyPropertyChanged, IRandomizerViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Errors { get; set; }
        public string Path { get; set; }

        public bool Randomize
        {
            get
            {
                return this.DresssphereViewModel.Randomize;// && this.GarmentGridViewModel.Randomize;
            }
            set
            {
                this.DresssphereViewModel.Randomize = value;
                //this.GarmentGridViewModel.Randomize = value;
            }
        }
        public bool LoadSave
        {
            get
            {
                return this.DresssphereViewModel.LoadSave;// && this.GarmentGridViewModel.LoadSave;
            }
            set
            {
                this.DresssphereViewModel.LoadSave = value;
                //this.GarmentGridViewModel.LoadSave = value;
            }
        }

        public DresssphereViewModel DresssphereViewModel { get; set; }
        public GarmentGridViewModel GarmentGridViewModel { get; set; }

        public MainViewModel()
        {
            this.DresssphereViewModel = new DresssphereViewModel();
            this.GarmentGridViewModel = new GarmentGridViewModel();

            this.DresssphereViewModel.PropertyChanged += (o, e) => { this.PropertyChanged?.Invoke(this, e); };
            this.GarmentGridViewModel.PropertyChanged += (o, e) => { this.PropertyChanged?.Invoke(this, e); };
        }
    }
}
