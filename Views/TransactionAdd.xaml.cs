

using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
	public TransactionAdd()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		Navigation.PopModalAsync();
    }

	private void OnButtonClicked_Save(object sender, EventArgs e)
	{

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

		if (string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out double result))
		{
			sb.AppendLine("O campo 'Valor' é inválido!");
			valid = false;
		}

		if(valid == false)
		{
			LabelError.Text = sb.ToString();
		}

		return valid;
	}
}