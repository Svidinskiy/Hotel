using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для Bronirovanie.xaml
    /// </summary>
    public partial class Bronirovanie : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public Bronirovanie()
        {
            InitializeComponent();
            foreach (var cl in entities.Rooms)
                ComboBoxNumberRoom.Items.Add(cl);
        }

        private void btnSaveBron_Click(object sender, RoutedEventArgs e)
        {           
            var save_client = ComboBoxNumberRoom.SelectedItem as Clients;
            if (TextBoxFIOClienta.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // проверка на заполняемость поля
            else
            {
                if (save_client == null)
                {
                    save_client = new Clients();
                    entities.Clients.Add(save_client);
                    ComboBoxNumberRoom.Items.Add(save_client);
                }
                save_client.FIO = TextBoxFIOClienta.Text; // запись данных в таблицу
                save_client.Arrival_date = DatePr.DisplayDate;
                save_client.Departure_date = DateOt.DisplayDate;
                entities.SaveChanges();              
                MessageBox.Show("Запись сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void ComboBoxNumberRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select_room = ComboBoxNumberRoom.SelectedItem as Clients;            
            if (select_room != null)
            {
                ComboBoxNumberRoom.Text = select_room.Room.ToString();
                ComboBoxNumberRoom.SelectedItem = (from a in entities.Rooms where a.ID_room == select_room.Room select a).Single<Rooms>();
            }     
            else
            {
                ComboBoxNumberRoom.SelectedIndex = -1;
            }
        }
    }
}
