using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CebuContactTracing.ViewModels
{
    class AccountPageViewModel : ViewModelBase
    {
        private string barcodeValue;

        public string BarcodeValue
        {
            get { return barcodeValue; }
            set { barcodeValue = value; RaisePropertyChanged(() => BarcodeValue); }
        }

        public AccountPageViewModel()
        {

        }
        public override Task InitializeAsync(object navigationData)
        {
            BarcodeValue = "BUL50A41";
            return base.InitializeAsync(navigationData);
        }
    }
}
