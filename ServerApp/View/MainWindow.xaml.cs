using System;
using ServerApp;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using Prism.Ioc;
using Prism.Regions;
using ServerApp.View;

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;
        private IRegion _region;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var _view = _container.Resolve<MainGridView>();

            _region = _regionManager.Regions["ContentRegion"];
            _region.Add(_view);
        }
    }
}