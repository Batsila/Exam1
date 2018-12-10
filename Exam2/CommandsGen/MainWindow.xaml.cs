using CommandsGen.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommandsGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReceiverClient _client;
        private Point _prevPosition;

        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
        }

        private void InitializeClient()
        {
             if (_client != null)
            {
                try
                {
                    _client.Close();
                }
                catch
                {
                    _client.Abort();
                }
                finally
                {
                    _client = null;
                }
            }

            _client = new ReceiverClient();
            _client.Open();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _prevPosition = e.GetPosition(gPad);
            gPad.MouseMove += Grid_MouseMove;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gPad.MouseMove -= Grid_MouseMove;
            _client.SendCommands(new MouseCommand[] { new StopCommand() });
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            var commands = new List<MouseCommand>();

            var newPosition = e.GetPosition(gPad);

            var deltaX = newPosition.X - _prevPosition.X;
            if (deltaX > 0)
                commands.Add(new MouseMoveCommand { CommandName = "RIGHT", Quantity = deltaX });
            else if (deltaX < 0)
                commands.Add(new MouseMoveCommand { CommandName = "LEFT", Quantity = Math.Abs(deltaX) });


            var deltaY = newPosition.Y - _prevPosition.Y;
            if (deltaY > 0)
                commands.Add(new MouseMoveCommand { CommandName = "DOWN", Quantity = deltaY });
            else if (deltaY < 0)
                commands.Add(new MouseMoveCommand { CommandName = "UP", Quantity = Math.Abs(deltaY) });

            _prevPosition = newPosition;

            _client.SendCommands(commands.ToArray());
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta;
            // change type to ZoomCommand
            if (delta > 0)
                _client.SendCommands(new MouseCommand[] { new MouseMoveCommand { CommandName = "IN", Quantity = delta } });
            else if (delta < 0)
                _client.SendCommands(new MouseCommand[] { new MouseMoveCommand { CommandName = "OUT", Quantity = Math.Abs(delta) } });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_client != null)
            {
                try
                {
                    _client.Close();
                }
                catch
                {
                    _client.Abort();
                }
            }
            base.OnClosing(e);
        }
    }
}
