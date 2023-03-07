using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.ViewModels
{
    public class FlyoutMenuViewModel : BaseViewModel
    {
        private ObservableCollection<string> menuLabels;
        public ObservableCollection<string> MenuLabels
        {
            get { return menuLabels; }
            set { menuLabels = value; NotifyPropertyChanged(); }
        }

        public FlyoutMenuViewModel(List<string> menuLabels)
        {
            MenuLabels = new ObservableCollection<string>(menuLabels);
        }
    }
}
