namespace AppControleFinanceiro
{
	public static class AppSettings
	{
		private static string DatabaseName = "database.db";
		private static string DatabaseDirectory = FileSystem.AppDataDirectory;
		public static string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
	}
}
