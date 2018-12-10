using Prism.Commands;
using Prism.Mvvm;
using ServerApp.Helpers;
using ServerApp.Managers;
using ServerApp.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ServerApp.ViewModel
{
    public class MainGridViewModel : BindableBase
    {
        private readonly CommunicationManager _communicationManager;

        public ObservableCollection<ClientModel> Data { get; private set; }

        #region DeleteItemCommand

        public DelegateCommand<ClientModel> DeleteItemCommand { get; private set; }

        private void DeleteItemCommandExecute(ClientModel e)
        {
            if (e == null || Data == null)
                return;

           
            MessageBoxResult result =
                MessageBox.Show("Are you sure to delete row?", "", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Data.Remove(e);
                DeleteItemCommand.RaiseCanExecuteChanged();
                _communicationManager.ItemDeleted(e);
            }
        }

        private bool DeleteItemCommandCanExecute(ClientModel e)
        {
            return Data != null && Data.Any();
        }

        #endregion

        public MainGridViewModel(CommunicationManager communicationManager)
        {
            _communicationManager = communicationManager;

            var data = StaticData.GetData();
            Data = new ObservableCollection<ClientModel>(data);

            DeleteItemCommand = new DelegateCommand<ClientModel>(DeleteItemCommandExecute, DeleteItemCommandCanExecute);

            _communicationManager.ItemAdding += CommunicationManagerItemAdding;
        }


        private void CommunicationManagerItemAdding(object sender, ClientModel e)
        {
            if (e == null)
                return;

            if (Data == null)
                Data = new ObservableCollection<ClientModel>();

            Data.Add(e);
        }
    }
}
