using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;

using StarBattle.Contracts.Services;
using StarBattle.Contracts.Views;
using StarBattle.Views;

namespace StarBattle.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;
        private readonly IPersistAndRestoreService _persistAndRestoreService;
        private IShellWindow _shellWindow;

        public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IPersistAndRestoreService persistAndRestoreService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            _persistAndRestoreService = persistAndRestoreService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync();

            // Tasks after activation
            await StartupAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _persistAndRestoreService.PersistData();
            await Task.CompletedTask;
        }

        private async Task InitializeAsync()
        {
            _persistAndRestoreService.RestoreData();
            await Task.CompletedTask;
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;
        }

        private async Task HandleActivationAsync()
        {
            if (!Application.Current.Windows.OfType<IShellWindow>().Any())
            {
                // Default activation that navigates to the apps default page
                _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
                
                Debug.Assert(_shellWindow != null, nameof(_shellWindow) + " != null");

                _navigationService.Initialize(_shellWindow.GetNavigationFrame());
                _shellWindow.ShowWindow();
                _navigationService.NavigateTo(typeof(MainPage));
                await Task.CompletedTask;
            }
        }
    }
}
