using Caliburn.Micro;
using OHMDesktopUI.EventModels;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OHMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<SwitchToLogInEvent>
    {
        private readonly CheckInViewModel _checkInVM;
        private readonly IEventAggregator _events;
        private readonly ILoggedInUser _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(CheckInViewModel checkInVM,
                              IEventAggregator events,
                              ILoggedInUser user,
                              IAPIHelper apiHelper)
        {
            _checkInVM = checkInVM;
            _events = events;
            _user = user;
            _apiHelper = apiHelper;
            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }



        public bool IsLoggedIn
        {
            get
            {
                bool output = false;

                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }

                return output;
            }
        }


        public bool IsLoggedOut
        {
            get
            {
                return !IsLoggedIn;
            }
        }


        public void ExitApplication()
        {
            TryCloseAsync();
        }


        public async Task LogIn()
        {
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            
        }


        public async Task LogOut()
        {
            _user.ResetUser();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => IsLoggedOut);
        }



        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }


        public async Task Room()
        {
            await ActivateItemAsync(IoC.Get<RoomViewModel>(), new CancellationToken());
        }


        public async Task Client()
        {
            await ActivateItemAsync(IoC.Get<ClientViewModel>(), new CancellationToken());
        }


        public async Task CheckIn()
        {
            await ActivateItemAsync(IoC.Get<CheckInViewModel>(), new CancellationToken());
        }


        public async Task CheckOut()
        {
            await ActivateItemAsync(IoC.Get<CheckOutViewModel>(), new CancellationToken());
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            
            await ActivateItemAsync(IoC.Get<BlankViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => IsLoggedOut);
            
        }

        public async Task HandleAsync(SwitchToLogInEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync (_checkInVM, cancellationToken);
            _checkInVM.Client = message.Client;
            _checkInVM.Phone = message.Phone;
        }
    }
}
