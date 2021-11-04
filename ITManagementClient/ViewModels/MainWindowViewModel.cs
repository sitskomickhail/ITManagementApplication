using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.UserControls;

namespace ITManagementClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>()); }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        public MainWindowViewModel()
        {
            PageViewModels.Add(new LoginControlViewModel());
            PageViewModels.Add(new RegisterControlViewModel());

            CurrentPageViewModel = PageViewModels[0];

            Mediator.Subscribe("OnGo1Screen", OnGo1Screen);
            Mediator.Subscribe("OnGo2Screen", OnGo2Screen);
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if(!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGo1Screen(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }
        
        private void OnGo2Screen(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }
    }
}