using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using ServerApp.Managers;
using ServerApp.View;
using ServerApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// Register types in DI container
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CommunicationManager>();
        }

        /// <summary>
        /// Configure mappings between Views and ViewModels
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<MainGridView, MainGridViewModel>();
        }
    }
}