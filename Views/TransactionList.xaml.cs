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

	private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
	{
		var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
		Navigation.PushModalAsync(transactionEdit);
	}
}