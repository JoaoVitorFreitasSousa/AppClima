using System.Collections.ObjectModel;
using WeatherAppMAUI.Models;

namespace WeatherAppMAUI.Views;

public partial class SalvosPage : ContentPage
{

    ObservableCollection<Tempo> lista_salvos = new ObservableCollection<Tempo>();

	public SalvosPage()
	{
		InitializeComponent();
        lst_salvos.ItemsSource = lista_salvos;
	}

    protected async override void OnAppearing() // Função para carregar a lista_salvos com os dados do banco
    {
        lista_salvos.Clear();
        if (lista_salvos.Count == 0)
        {
            List<Tempo> tmp = await App.DataBase.GetAll();
            foreach (Tempo t in tmp)
            {
                lista_salvos.Add(t);
            }
        }
    }

    private void VoltarHome(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) // Função para filtrar as consultas feitas pelo nome da cidade
    {
        try
        {
            string q = e.NewTextValue;
            lista_salvos.Clear(); // Limpando a lista de salvos para carregar denovo apenas com o produtos filtrados

            List<Tempo> tmp = await App.DataBase.Search(q); // Fazendo a busca no banco
            foreach (Tempo t in tmp)
            {
                lista_salvos.Add(t);// Adicionando os itens carregados na lista
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Fechar"); // Mensagem em caso de erro
        }
    }
}