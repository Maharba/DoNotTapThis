using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DoNotTapThis.Models;
using DoNotTapThis.Services.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace DoNotTapThis.Services
{
    public class AzureServiceManager
    {
        private MobileServiceClient _mobileServiceClient;
        private IMobileServiceSyncTable<UserTaps> _userTapsSyncTable;
        private bool _isInitialized;

        public async Task InitializeAsync()
        {
            if (_mobileServiceClient?.SyncContext?.IsInitialized ?? false)
                return;
            _mobileServiceClient = new MobileServiceClient(Keys.AZURE_URL);

            string path = "usertaps.db3";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<UserTaps>();

            await _mobileServiceClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            _userTapsSyncTable = _mobileServiceClient.GetSyncTable<UserTaps>();
        }

        private async Task SyncUserTapsAsync()
        {
            if (CrossConnectivity.Current.IsConnected && await CrossConnectivity.Current.IsRemoteReachable(Keys.AZURE_URL))
            {
                try
                {
                    await _mobileServiceClient.SyncContext.PushAsync();
                    await _userTapsSyncTable.PullAsync("usertaps", _userTapsSyncTable.CreateQuery());
                }
                catch (Exception e)
                {
                    
                    throw;
                }
            }
        }

        public async Task<IEnumerable<UserTaps>> GetUserTapsAsync()
        {
            await InitializeAsync();
            await SyncUserTapsAsync();
            return await _userTapsSyncTable.OrderByDescending(u => u.Taps).ToEnumerableAsync();
        }

        public async Task AddUserTapAsync(string username, int taps)
        {
            await InitializeAsync();
            await _userTapsSyncTable.InsertAsync(new UserTaps()
            {
                Name = username,
                Taps = taps
            });
            await SyncUserTapsAsync();
        }
    }
}
