using CommandsGen.HandlerService;
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
            var commands = new MouseCommand[2];

            var newPosition = e.GetPosition(gPad);

            var deltaX = newPosition.X - _prevPosition.X;
            if (deltaX > 0)
                commands[0] = new MouseMoveCommand { CommandName = "RIGHT", Quantity = deltaX };
            else if (deltaX < 0)
                commands[0] = new MouseMoveCommand { CommandName = "LEFT", Quantity = Math.Abs(deltaX) };


            var deltaY = newPosition.Y - _prevPosition.Y;
            if (deltaY > 0)
                commands[1] = new MouseMoveCommand { CommandName = "UP", Quantity = deltaY };
            else if (deltaY < 0)
                commands[1] = new MouseMoveCommand { CommandName = "DOWN", Quantity = Math.Abs(deltaY) };

            _prevPosition = newPosition;

            _client.SendCommands(commands);
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // TODO: Add wheel event
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
