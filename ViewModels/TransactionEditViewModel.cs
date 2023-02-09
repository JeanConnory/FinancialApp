using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.ViewModels
{
	public partial class TransactionEditViewModel : ObservableObject
	{
		[ObservableProperty]
		decimal numbertoMoney;
	}
}
