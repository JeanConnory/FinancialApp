using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.ViewModels;

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
		BindingContext = new TransactionListViewModel(repository, Navigation);
	}
}