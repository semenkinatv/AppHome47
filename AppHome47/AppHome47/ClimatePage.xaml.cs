using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private void Alarm_Click(object sender, EventArgs e)
        {
            stackLayout = new StackLayout { Padding = new Thickness(60) };

            Content = stackLayout;
            
            // Создаем виджет выбора даты
            var datePicker = new DatePicker
            {
                Format = "D",
                // Диапазон дат: +/- неделя
                MaximumDate = DateTime.Now.AddDays(7),
                MinimumDate = DateTime.Now.AddDays(-7),
            };
            var datePickerText = new Label { Text = "Дата запуска будильника:", Margin = new Thickness(0, 30, 0, 0) };
           
            stackLayout.Children.Add(datePickerText);
            stackLayout.Children.Add(datePicker);
           
            // Виджет выбора времени.
            var timePickerText = new Label { Text = "Время запуска будильника:", Margin = new Thickness(0, 30, 0, 0) };
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(13, 0, 0)
            };

            stackLayout.Children.Add(timePickerText);
            stackLayout.Children.Add(timePicker);

            Slider slider = new Slider
            {
                Minimum = 0,
                Maximum = 10,
                Value = 8.0,
                ThumbColor = Color.DodgerBlue,
                MinimumTrackColor = Color.DodgerBlue,
                MaximumTrackColor = Color.Gray
            };
            var sliderText = new Label { Text = $"Громкость сигнала будильника: {slider.Value}", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 30, 0, 0) };
            stackLayout.Children.Add(sliderText);
            stackLayout.Children.Add(slider);

            // Создаем заголовок для переключателя
            var switchHeader = new Label { Text = "Возможность повторения сигнала каждый день:", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 30, 0, 0) };
            stackLayout.Children.Add(switchHeader);

            // Создаем переключатель
            Switch switchControl = new Switch
            {
                IsToggled = false,
                HorizontalOptions = LayoutOptions.Center,
                ThumbColor = Color.DodgerBlue,
                OnColor = Color.LightSteelBlue,
            };
            stackLayout.Children.Add(switchControl);

            var button = new Button();
            button.Text = "Сохранить";
            button.BackgroundColor = Color.Silver;
            button.Margin = new Thickness(10, 35, 0, 0);
            button.CornerRadius = 15;

            stackLayout.Children.Add(button); 

            button.Clicked += (sender1, e1) => button_Click(sender1, e1, datePicker.Date.ToString("dd.MM.yyyy"),timePicker.Time.ToString("00:00"));

           // Регистрируем обработчик события выбора даты
            datePicker.DateSelected += (sender1, e1) => DateSelectedHandler(sender1, e1, datePickerText);
            // Регистрируем обработчик события выбора времени
            timePicker.PropertyChanged += (sender1, e1) => TimeChangedHandler(sender1, e1, timePickerText, timePicker);
            // Регистрируем обработчик события на громкость
            slider.ValueChanged += (sender1, e1) => VolumeChangedHandler(sender1, e1, sliderText);
            //slider.ValueChanged += (sender1, e1) => { sliderText.Text = $"Громкость сигнала будильника: {e1.NewValue.ToString("0")}"; };
            
            // Регистрируем обработчик события переключения
            switchControl.Toggled += (sender1, e1) => SwitchHandler(sender1, e1, switchHeader);
        }
        public void VolumeChangedHandler(object sender, ValueChangedEventArgs e, Label sliderText)
        {
            // При срабатывании выбора - будет меняться информационное сообщение.
            sliderText.Text = $"Громкость сигнала будильника: {e.NewValue.ToString("0")}";
        }

        // Регистрируем обработчик события выбора даты
        public void button_Click(object sender, EventArgs e, string date, string time)
        {
            stackLayout = new StackLayout { Padding = new Thickness(60) };

            Content = stackLayout;

            var setAlarm = new Label { Text = $"Будильник установлен на:"+ date + " " + time, FontSize = 17,  VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center , Margin = new Thickness(50) };
            stackLayout.Children.Add(setAlarm);
        }

        public void DateSelectedHandler(object sender, DateChangedEventArgs e, Label datePickerText)
        {
            // При срабатывании выбора - будет меняться информационное сообщение.
            datePickerText.Text = "Будильник запустится " + e.NewDate.ToString("dd/MM/yyyy");
        }
        public void TimeChangedHandler(object sender, PropertyChangedEventArgs e, Label timePickerText, TimePicker timePicker)
        {
            // Обновляем текст сообщения, когда появляется новое значение времени
            if (e.PropertyName == "Time")
                timePickerText.Text = "В " + timePicker.Time;
        }
        public void SwitchHandler(object sender, ToggledEventArgs e, Label header)
        {
            if (!e.Value)
            {
                header.Text = "Не повторять каждый день";
                return;
            }

            header.Text = "Повторять каждый день";
        }
    }
}

