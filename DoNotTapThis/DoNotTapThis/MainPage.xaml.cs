using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DoNotTapThis.Helpers;
using Xamarin.Forms;

namespace DoNotTapThis
{
    public partial class MainPage : ContentPage
    {
        private TapGestureRecognizer _tap;
        private UserTapsManager _userTapsManager;
        public MainPage()
        {
            InitializeComponent();

            _tap = new TapGestureRecognizer();
            _userTapsManager = new UserTapsManager();

            _tap.Tapped += (sender, args) =>
            {
                try
                {
                    _userTapsManager.AddTap();
                    lblTaps.Text = _userTapsManager.GetFormattedTaps();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    UserDialogs.Instance.ShowError(ex.Message);
                }
            };
            lblTaps.GestureRecognizers.Add(_tap);
        }
    }
}
