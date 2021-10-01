using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppHome47
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClimatePage : ContentPage
    {
        private Grid grid;
        public ClimatePage()
        {
            InitializeComponent();


        }
        public void AddBlock(Grid grid, Color color, string textName, string textTemp, int x, int y)
        {

            grid.Children.Add(
                // Создаем прямоугольник заданного цвета
                new BoxView { Color = color },
                // Задаем его местоположение и размеры
                x, y);
            grid.Children.Add(
               new Label
               {
                   Text = textName,
                   VerticalTextAlignment = TextAlignment.Start,
                   HorizontalTextAlignment = TextAlignment.Center,
                   FontSize = 36,
                   TextColor = Color.Black
               },
               x, y);
            grid.Children.Add(
               new Label
               {
                   Text = textTemp,
                   VerticalTextAlignment = TextAlignment.Center,
                   HorizontalTextAlignment = TextAlignment.Center,
                   FontSize = 60,
                   TextColor = Color.Black
               }, x, y);


        }
        private void Weather_Click(object sender, EventArgs e)
        {
            grid = new Grid
            {
                // Набор строк 
                RowDefinitions =
              {
                  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                  new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
               }
            };

            Content = grid;
            grid.BackgroundColor = Color.Black;

            AddBlock(grid, Color.LightYellow, "Внутри", "+ 26 °C", 0, 0);
            AddBlock(grid, Color.LightSkyBlue, "Снаружи", "- 15 °C", 0, 1);
            AddBlock(grid, Color.LightSeaGreen, "Давление", "760 мм", 0, 2);

        }

    }
}

