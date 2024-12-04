using System.Collections.ObjectModel;
using System.Diagnostics;
using WeatherAppMAUI.Models;
using WeatherAppMAUI.Service;

namespace WeatherAppMAUI
{
    public partial class MainPage : ContentPage
    {
        Tempo? previsao;

        public MainPage()
        {
            InitializeComponent();

        }



        private async void pegarPrevisao(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_cidade.Text))
                {
                    previsao = await DataService.GetPrevisaoDoTempo(txt_cidade.Text);
                    Debug.WriteLine("Previsão recebida: " + previsao.DataPesquisa);

                    if (previsao != null)
                    {
                        stack_content.IsVisible = true;

                        int code = int.Parse(previsao.code);

                        switch (code)
                        {
                            case int i when (i >= 200 && i < 300):
                                weatherImage.Source = "one.png";
                                break;
                            case int i when (i >= 300 && i < 400):
                                weatherImage.Source = "two.png";
                                break;
                            case int i when (i >= 500 && i < 600):
                                weatherImage.Source = "three.png";
                                break;
                            case int i when (i >= 600 && i < 700):
                                weatherImage.Source = "four.png";
                                break;
                            case int i when (i >= 700 && i < 800):
                                weatherImage.Source = "five.png";
                                break;
                            case int i when (i == 800):
                                weatherImage.Source = "six.png";
                                break;
                            case int i when (i > 800 && i <= 804):
                                weatherImage.Source = "seven.png";
                                break;
                        }

                        label_descricao.Text = previsao.Weather;
                        label_cidade.Text = previsao.Title;
                        label_temperatura.Text = previsao.Temperature;

                        List<Tempo> salvo = await App.DataBase.SearchEqual(previsao.Title, previsao.DataPesquisa);

                        if (salvo.Count == 0)
                        {
                            button_salvar.IsEnabled = true;
                        } else
                        {
                            button_salvar.IsEnabled = false;
                        }
                    }
                    else
                    {
                        label_erro.Text = "Previsão nula";
                        label_erro.IsVisible = true;
                    }


                }
            } catch(Exception ex)
            {
                Debug.WriteLine("Erro: " + ex.Message);
                label_erro.Text = ex.Message.ToString();
                label_erro.IsVisible = true;
            }
        } // Fim da função de buscar previsão

        private async void salvarPrevisao(object sender, EventArgs e)
        {
            try
            {
                if (previsao != null)
                {
                    button_salvar.IsEnabled = false;
                    await App.DataBase.Insert(previsao);
                }
            } catch (Exception ex)
            {
                label_erro.Text = ex.Message.ToString();
                label_erro.IsVisible = true;
            }
        }

        private void SalvosPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.SalvosPage()); // Navegando até a pagina EditarProduto, enviando o item selecionado
        }
    }

}
