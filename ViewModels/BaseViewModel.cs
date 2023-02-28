using AppControleFinanceiro.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppControleFinanceiro.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool _isBusy;

        [ObservableProperty]
        public string _title;

        public INavigation Navigation { get; set; }

        [ObservableProperty]
        public Transaction transactionProp;


    }
}
