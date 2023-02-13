using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
	private ITransactionRepository _transactionRepository;

	public TransactionAdd(ITransactionRepository repository, TransactionAddViewModel vm)
	{
		_transactionRepository = repository;
		InitializeComponent();
		BindingContext = vm;
	}

	private void TapGestureRecognizerTapped_To_Close(object sender, TappedEventArgs e)
	{
		KeyboardFixBugs.HideKeyboard();
		Navigation.PopModalAsync();
	}

	private void OnButtonClicked_Save(object sender, EventArgs e)
	{
		if (IsValidData() == false)
			return;

		SaveTransactionInDatabase();

		KeyboardFixBugs.HideKeyboard();
		Navigation.PopModalAsync();

		//Publisher
		WeakReferenceMessenger.Default.Send<string>(string.Empty);
	}

	private void SaveTransactionInDatabase()
	{
		Transaction transaction = new Transaction()
		{
			Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
			Name = EntryName.Text,
			Date = DatePickerDate.Date,
			Value = double.Parse(EntryValue.Text.Substring(2).Trim())
		};

		_transactionRepository.Add(transaction);
	}

	private bool IsValidData()
	{
		bool valid = true;
		StringBuilder sb = new StringBuilder();


		if(string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
		{
			sb.AppendLine("O campo 'Nome' deve ser preenchido!");
			valid = false;
		}
		if (string.IsNullOrEmpty(EntryValue.Text.Substring(2).Trim()) || string.IsNullOrWhiteSpace(EntryValue.Text.Substring(2).Trim()))
		{
			sb.AppendLine("O campo 'Valor' deve ser preenchido!");
			valid = false;
		}

		if (!string.IsNullOrEmpty(EntryValue.Text.Substring(2).Trim()) && !double.TryParse(EntryValue.Text.Substring(2).Trim(), out double result))
		{
			sb.AppendLine("O campo 'Valor' é inválido!");
			valid = false;
		}

		if(valid == false)
		{
			LabelError.IsVisible = true;
			LabelError.Text = sb.ToString();
		}

		return valid;
	}

	private void EntryValue_TextChanged_LastCharCursor(object sender, TextChangedEventArgs e)
	{
		EntryValue.CursorPosition = EntryValue?.Text?.Length ?? 0;
	}
}