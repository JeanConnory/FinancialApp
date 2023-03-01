using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppControleFinanceiro.ViewModels
{
    public partial class TransactionListViewModel : BaseViewModel
    {
        private ITransactionRepository _repository;

        public TransactionListViewModel(ITransactionRepository repository, INavigation navigation)
        {
            _repository = repository;
            Navigation = navigation;
            Items = new ObservableCollection<Transaction>();
            Reload();

            //Subscribe
            WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
            {
                Reload();
            });
        }

        [ObservableProperty]
        ObservableCollection<Transaction> items;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        string labelBalance;

        [ObservableProperty]
        string labelIncome;

        [ObservableProperty]
        string labelExpense;

        private void Reload()
        {
            Items = new ObservableCollection<Transaction>(_repository.GetAll());

            double income = Items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
            double expense = Items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
            double balance = income - expense;

            LabelIncome = income.ToString("C");
            LabelExpense = expense.ToString("C");
            LabelBalance = balance.ToString("C");
        }

        [RelayCommand]
        void Add()
        {
            var transactionAdd = new TransactionAdd(_repository);
            Navigation.PushModalAsync(transactionAdd);
        }

        [RelayCommand]
        async void Delete(Border parametro) //passei a borda como parametro pra conseguir executar a animação
        {
            if (parametro == null)
                return;

            var transaction = (Transaction)parametro.BindingContext;
            await AnimationBorder(parametro, true);
            bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

            if (result)
            {
                _repository.Delete(transaction);
                Reload();
            }
            else
                await AnimationBorder(parametro, false);
        }

        [RelayCommand]
        void Edit(Transaction transaction)
        {
            var transactionEdit = new TransactionEdit(_repository, transaction);
            Navigation.PushModalAsync(transactionEdit);
        }

        private Color _borderOroginalBackgroundColor;
        private string _labelOriginalText;

        public async Task AnimationBorder(Border border, bool IsDeleteAnimation)
        {
            var label = (Label)border.Content;

            if (IsDeleteAnimation)
            {
                _borderOroginalBackgroundColor = border.BackgroundColor;
                _labelOriginalText = label.Text;

                await border.RotateYTo(90, 500);

                border.BackgroundColor = Colors.Red;
                label.TextColor = Colors.White;
                label.Text = "X";

                await border.RotateYTo(180, 500);
            }
            else
            {
                await border.RotateYTo(90, 500);

                border.BackgroundColor = _borderOroginalBackgroundColor;
                label.TextColor = Colors.Black;
                label.Text = _labelOriginalText;

                await border.RotateYTo(0, 500);
            }
        }
    }
}