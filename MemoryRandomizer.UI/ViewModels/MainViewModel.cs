using MemoryRandomizer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemoryRandomizer.UI
{
    public class MainViewModel : IRandomizerViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GameManager gameManager;

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

        public ICommand AttachCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public MainViewModel()
        {
            this.gameManager = new GameManager();
            this.DresssphereViewModel = new DresssphereViewModel();
            this.GarmentGridViewModel = new GarmentGridViewModel();

            this.DresssphereViewModel.PropertyChanged += (o, e) => { this.PropertyChanged?.Invoke(this, e); };
            this.GarmentGridViewModel.PropertyChanged += (o, e) => { this.PropertyChanged?.Invoke(this, e); };

            this.AttachCommand = new AsyncDelegateCommand(this.Attach, _ => true);
            this.DeleteCommand = new AsyncDelegateCommand(this.DeleteSave, _ => true);
        }

        #region Command Implementations
        public async Task Attach(object _)
        {
            await Task.Run(() => this.gameManager.Startup(this.DresssphereViewModel.Randomize, this.GarmentGridViewModel.Randomize, this.Randomize));
        }

        public async Task DeleteSave(object _)
        {
            await this.DresssphereViewModel.DeleteSave(_);
            await this.GarmentGridViewModel.DeleteSave(_);
        }
        #endregion
    }
}
