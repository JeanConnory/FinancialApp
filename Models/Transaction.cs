using LiteDB;

namespace AppControleFinanceiro.Models
{
	public class Transaction
	{
		[BsonId]
		public int Id { get; set; }

		public TransactionType Type { get; set; }

		public string Name { get; set; }

		public DateTimeOffset Date { get; set; } //DatetimeOffset independe do fuso horário

		public decimal Value { get; set; }
	}

	public enum TransactionType
	{
		Income,
		Expenses
	}
}
