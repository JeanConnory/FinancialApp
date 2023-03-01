using AppControleFinanceiro.Repositories;
using AppControleFinanceiro.ViewModels;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
		BindingContext = new TransactionAddViewModel(repository, Navigation);
	}
}