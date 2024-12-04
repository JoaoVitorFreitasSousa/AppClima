using DotNetEnv;
using WeatherAppMAUI.Helpers;

namespace WeatherAppMAUI
{
    public partial class App : Application
    {

        static SQLiteDatabaseHelper database;

        public static SQLiteDatabaseHelper DataBase
        {
            get
            {
                if (database == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "arquivo.db3");
                    database = new SQLiteDatabaseHelper(path);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            // Carrega as variáveis de ambiente uma vez na inicialização do aplicativo
            DotNetEnv.Env.Load();

            MainPage = new AppShell();
        }
    }
}
