namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs e)
	{
		App.Current.MainPage = new TransactionAdd();
    }
}