using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.ViewModels
{
    public partial class TransactionAddViewModel : BaseViewModel
	{
        private ITransactionRepository _transactionRepository;

        public TransactionAddViewModel(ITransactionRepository repository, INavigation navigation)
		{
            _transactionRepository = repository;
            Navigation = navigation;
        }

        [ObservableProperty]
        bool radioIncome = true;

        [ObservableProperty]
        bool radioExpense;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        DateTime dataTransaction = DateTime.Now;

        [ObservableProperty]
        decimal valueTransaction;

        [ObservableProperty]
        string labelError;

        [ObservableProperty]
        bool labelErrorIsVisible = false;

        [RelayCommand]
        void TapClose()
        {
            KeyboardFixBugs.HideKeyboard();
            Navigation.PopModalAsync();
        }

        [RelayCommand]
		void Add()
		{
            if (IsValidData() == false)
                return;

            SaveTransactionInDatabase();

            KeyboardFixBugs.HideKeyboard();
            Navigation.PopModalAsync();

            WeakReferenceMessenger.Default.Send<string>(string.Empty);
        }

        private void SaveTransactionInDatabase()
        {
            Transaction transaction = new Transaction()
            {
                Type = RadioIncome ? TransactionType.Income : TransactionType.Expense,
                Name = Name,
                Date = DataTransaction.Date,
                Value = double.Parse(ValueTransaction.ToString().Trim())
            };
            _transactionRepository.Add(transaction);
        }

        private bool IsValidData()
        {
            bool valid = true;
            StringBuilder sb = new StringBuilder();


            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                sb.AppendLine("O campo 'Nome' deve ser preenchido!");
                valid = false;
            }
            if (string.IsNullOrEmpty(ValueTransaction.ToString()) || string.IsNullOrWhiteSpace(ValueTransaction.ToString()))
            {
                sb.AppendLine("O campo 'Valor' deve ser preenchido!");
                valid = false;
            }

            if (!string.IsNullOrEmpty(ValueTransaction.ToString()) && !double.TryParse(ValueTransaction.ToString(), out double result))
            {
                sb.AppendLine("O campo 'Valor' é inválido!");
                valid = false;
            }

            if (valid == false)
            {
                LabelErrorIsVisible = true;
                LabelError = sb.ToString();
            }

            return valid;
        }

        [RelayCommand]
        void CursorEntry(Entry entryValue)
        {
            entryValue.CursorPosition = entryValue?.Text?.Length ?? 0;
        }
    }
}
