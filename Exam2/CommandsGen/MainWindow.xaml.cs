using CommandsGen.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ObservableCollection<string> _debugData = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            icCommands.ItemsSource = _debugData;
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

        /// <summary>
        /// Stars tracking mouse move events
        /// </summary>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _prevPosition = e.GetPosition(gPad);
            gPad.MouseMove += Grid_MouseMove;
        }

        /// <summary>
        /// Stops tracking mouse move events and sends stop command
        /// </summary>
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gPad.MouseMove -= Grid_MouseMove;
            _client.SendCommands(new MouseCommandBase[] { new StopCommand() });

            AddDebugData("STOP");
        }

        /// <summary>
        /// Tracks mouse move events and sends mouse move commands
        /// </summary>
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            var commands = new List<MouseMoveCommand>();

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

            var sb = new StringBuilder("MOVE ");
            foreach (var item in commands)
                sb.Append($" {item.CommandName}+{item.Quantity}");
            AddDebugData(sb.ToString());
        }

        /// <summary>
        /// Tracks wheel events and sends zoom commands
        /// </summary>
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            WheelCommand command = null;

            var delta = e.Delta;
            if (delta > 0)
                command = new WheelCommand { CommandName = "IN", Quantity = delta };
            else 
                command = new WheelCommand { CommandName = "OUT", Quantity = Math.Abs(delta) };

            _client.SendCommands(new MouseCommandBase[] { command });

            AddDebugData($"WHEEL {command.CommandName} {command.Quantity}");
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

        private void AddDebugData(string data)
        {
            _debugData.Insert(0, $"{DateTime.Now.ToString("mm:ss")}\t{data}");
        }
    }
}
