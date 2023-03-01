using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.ViewModels;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
	public TransactionEdit(ITransactionRepository repository, Transaction transaction)
	{
		InitializeComponent();
		BindingContext = new TransactionEditViewModel(repository, Navigation, transaction);
	}
}