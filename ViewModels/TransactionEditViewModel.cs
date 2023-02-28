using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.ViewModels
{
    public partial class TransactionEditViewModel : BaseViewModel
    {
        private ITransactionRepository _transactionRepository;

        public TransactionEditViewModel(ITransactionRepository repository, INavigation navigation, Transaction transactionEdit)
        {
            _transactionRepository = repository;
            Navigation = navigation;
            transactionProp = transactionEdit;
            SetTransactionToEdit(transactionEdit);
        }

        [ObservableProperty]
        bool radioIncome;

        [ObservableProperty]
        bool radioExpense;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        DateTime dataTransaction;

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

        public void SetTransactionToEdit(Transaction transaction)
        {
            if (transaction.Type == TransactionType.Income)
                RadioIncome = true;
            else
                RadioExpense = true;

            Name = transaction.Name;
            DataTransaction = transaction.Date.Date;
            ValueTransaction = (decimal)transaction.Value;
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
                Id = TransactionProp.Id,
                Type = RadioIncome ? TransactionType.Income : TransactionType.Expense,
                Name = Name,
                Date = DataTransaction.Date,
                Value = double.Parse(ValueTransaction.ToString().Trim())
            };
            _transactionRepository.Update(transaction);
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
