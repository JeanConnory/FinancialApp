using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	/*
		 * Publisher - Subscribers
		 * TransactionAdd -> Publisher > Cadastro (Mensagem > Transaction)
		 * Subscriber -> TransactionList (Recebe o Transaction)
	*/
	private readonly ITransactionRepository _repository;

	public TransactionList(ITransactionRepository repository)
	{
		_repository = repository;
		InitializeComponent();
		Reload();

		// Subscribe
		WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reload();
		});

	}

	private void Reload()
	{
		var items = _repository.GetAll();
		CollectionViewTransactions.ItemsSource = _repository.GetAll();

		double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
		double expense = items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
		double balance = income - expense;

		LabelIncome.Text = income.ToString("C");
		LabelExpense.Text = expense.ToString("C");
		LabelBalance.Text = balance.ToString("C");
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs e)
	{
		
		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
		Navigation.PushModalAsync(transactionAdd);
    }

	private void TapGestureRecognizerTapped_To_Edit(object sender, TappedEventArgs e)
	{
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
		Transaction transaction = (Transaction)gesture.CommandParameter;

		var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
		transactionEdit.SetTransactionToEdit(transaction);
		Navigation.PushModalAsync(transactionEdit);
	}

	private async void TapGestureRecognizerTapped_To_Delete(object sender, TappedEventArgs e)
	{
		bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

		if (result) 
		{
			Transaction transaction = (Transaction)e.Parameter;
			_repository.Delete(transaction);

			Reload();
		}
	}
}