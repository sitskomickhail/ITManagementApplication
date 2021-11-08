using System;
using System.Collections.Generic;
using System.Linq;
using ITManagementClient.Navigation;
using ITManagementClient.ViewModels.Base;
using ITManagementClient.ViewModels.Interfaces;
using ITManagementClient.ViewModels.UserControls;

namespace ITManagementClient.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IPageViewModel _currentPageViewModel;
        private Dictionary<string, IPageViewModel> _pageViewModels;

        public Dictionary<string, IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new Dictionary<string, IPageViewModel>());

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
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(IPageViewModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var viewModel in types)
            {
                var viewModelInstance = (IPageViewModel)viewModel.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
                if (viewModelInstance != null)
                {
                    PageViewModels.Add(viewModel.Name, viewModelInstance);
                    Mediator.Subscribe(viewModel.Name, ChangeViewModel);
                }
            }

            CurrentPageViewModel = PageViewModels[nameof(LoginControlViewModel)];
        }

        private void ChangeViewModel(object obj)
        {
            var viewModelName = (string)obj;

            if (!PageViewModels.Keys.Contains(viewModelName))
                throw new NullReferenceException($"{viewModelName} View Model was not found");

            CurrentPageViewModel = PageViewModels[viewModelName];
        }
    }
}