using AppControleFinanceiro.Libraries;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.ViewModels
{
	public partial class TransactionAddViewModel : ObservableObject
	{
		[ObservableProperty]
		decimal numbertoMoney;
		
	}
}
