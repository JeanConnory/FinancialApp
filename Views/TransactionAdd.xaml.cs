using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
	private ITransactionRepository _transactionRepository;

	public TransactionAdd(ITransactionRepository repository)
	{
		_transactionRepository = repository;
		InitializeComponent();
	}

	private void TapGestureRecognizerTapped_To_Close(object sender, TappedEventArgs e)
	{
		Navigation.PopModalAsync();
    }

	private void OnButtonClicked_Save(object sender, EventArgs e)
	{
		if (IsValidData() == false)
			return;

		SaveTransactionInDatabase();

		Navigation.PopModalAsync();

		//Publisher
		WeakReferenceMessenger.Default.Send<string>(string.Empty);

		var count = _transactionRepository.GetAll().Count;
		App.Current.MainPage.DisplayAlert("Mensagem!", $"Existe(m) {count} registro(s) no banco!", "OK");
	}

	private void SaveTransactionInDatabase()
	{
		Transaction transaction = new Transaction()
		{
			Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
			Name = EntryName.Text,
			Date = DatePickerDate.Date,
			Value = double.Parse(EntryValue.Text)
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
		if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
		{
			sb.AppendLine("O campo 'Valor' deve ser preenchido!");
			valid = false;
		}

		if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out double result))
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
}