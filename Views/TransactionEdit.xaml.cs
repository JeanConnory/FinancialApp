using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{

	public TransactionEdit(ITransactionRepository repository, Transaction transaction)
	{
		InitializeComponent();
		BindingContext = new TransactionEditViewModel(repository, Navigation, transaction);
	}

	//public void SetTransactionToEdit(Transaction transaction)
	//{
	//	_transaction = transaction;

	//	if(transaction.Type == TransactionType.Income)
	//		RadioIncome.IsChecked = true;
	//	else
	//		RadioExpense.IsChecked = true;

	//	EntryName.Text = transaction.Name;
	//	DatePickerDate.Date = transaction.Date.Date;
	//	EntryValue.Text = transaction.Value.ToString("C");
	//}

	//private void TapGestureRecognizerTapped_To_Close(object sender, TappedEventArgs e)
	//{
	//	KeyboardFixBugs.HideKeyboard();
	//	Navigation.PopModalAsync();
	//}

	//private void OnButtonClicked_Save(object sender, EventArgs e)
	//{
	//	if (IsValidData() == false)
	//		return;

	//	SaveTransactionInDatabase();

	//	KeyboardFixBugs.HideKeyboard();
	//	Navigation.PopModalAsync();

	//	//Publisher
	//	WeakReferenceMessenger.Default.Send<string>(string.Empty);
	//}

	//private void SaveTransactionInDatabase()
	//{
	//	Transaction transaction = new Transaction()
	//	{
	//		Id = _transaction.Id,
	//		Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
	//		Name = EntryName.Text,
	//		Date = DatePickerDate.Date,
	//		Value = double.Parse(EntryValue.Text.Substring(2).Trim())
	//	};

	//	_transactionRepository.Update(transaction);
	//}

	//private bool IsValidData()
	//{
	//	bool valid = true;
	//	StringBuilder sb = new StringBuilder();


	//	if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
	//	{
	//		sb.AppendLine("O campo 'Nome' deve ser preenchido!");
	//		valid = false;
	//	}
	//	if (string.IsNullOrEmpty(EntryValue.Text.Substring(2).Trim()) || string.IsNullOrWhiteSpace(EntryValue.Text.Substring(2).Trim()))
	//	{
	//		sb.AppendLine("O campo 'Valor' deve ser preenchido!");
	//		valid = false;
	//	}

	//	if (!string.IsNullOrEmpty(EntryValue.Text.Substring(2).Trim()) && !double.TryParse(EntryValue.Text.Substring(2).Trim(), out double result))
	//	{
	//		sb.AppendLine("O campo 'Valor' é inválido!");
	//		valid = false;
	//	}

	//	if (valid == false)
	//	{
	//		LabelError.IsVisible = true;
	//		LabelError.Text = sb.ToString();
	//	}

	//	return valid;
	//}

	//private void EntryValue_TextChanged_LastCharCursor(object sender, TextChangedEventArgs e)
	//{
	//	EntryValue.CursorPosition = EntryValue?.Text?.Length ?? 0;
	//}
}