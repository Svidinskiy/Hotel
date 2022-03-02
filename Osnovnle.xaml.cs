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
    /// Логика взаимодействия для Osnovnle.xaml
    /// </summary>
    public partial class Osnovnle : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public Osnovnle()
        {
            InitializeComponent();
            foreach (var room in entities.Rooms)
                ListNumberRooms.Items.Add(room);
            foreach (var rms in entities.Rooms)
               combxNumberClient.Items.Add(rms);
            foreach (var clnt in entities.Clients)
                ClientInfoList.Items.Add(clnt);
        }

        private void ListNumberRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_room = ListNumberRooms.SelectedItem as Rooms;
            if (selected_room != null)
            {
                TextBoxCategory.Text = selected_room.Category;
                TextBoxOpisanie.Text = selected_room.Description;
                TextBoxPrice.Text = selected_room.Price.ToString();
                TextStatusRoom.Text = selected_room.Status;
            }
            else
            {
                TextBoxCategory.Text = "";
                TextBoxOpisanie.Text = "";
                TextBoxPrice.Text = "";
                TextStatusRoom.Text = "";
            }
        }

        private void btnExitOsnovnoe_Click(object sender, RoutedEventArgs e)
        {
            var result2 = MessageBox.Show("Закрыть программу?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result2 == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnBookRoom_Click(object sender, RoutedEventArgs e)
        {
            var save_client2 = ClientInfoList.SelectedItem as Clients;
            if (txtFIOClient.Text == "" || combxNumberClient.SelectedIndex == -1)
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (save_client2 == null)
                {
                    save_client2 = new Clients();
                    entities.Clients.Add(save_client2);
                    ClientInfoList.Items.Add(save_client2);
                }              
                var selected_client4 = combxNumberClient.SelectedItem as Rooms;
                if (selected_client4.Status == "Свободен")
                {
                    save_client2.FIO = txtFIOClient.Text;
                    save_client2.Arrival_date = DatePr.DisplayDate;
                    save_client2.Departure_date = dateOt.DisplayDate;
                    save_client2.Room = (combxNumberClient.SelectedItem as Rooms).ID_room;
                    entities.SaveChanges();
                    ClientInfoList.Items.Refresh();
                    MessageBox.Show("Номер забронирован", "Информация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else MessageBox.Show("Номер занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
        }

        private void btnOsnovnoeBack_Click(object sender, RoutedEventArgs e)
        {
            var result_back1 = MessageBox.Show("Вернуться к авторизации?", "Назад", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result_back1 == MessageBoxResult.Yes)
            {
                Window clnt = new MainWindow();
                clnt.Show();
                this.Close();
            }
        }

        private void ClientInfoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_client3 = ClientInfoList.SelectedItem as Clients;
            var selected_client4 = combxNumberClient.SelectedItem as Rooms;
            if(selected_client4.Status == "Свободен")
            {
                combxNumberClient.SelectedItem = (from p in entities.Rooms where p.ID_room == selected_client3.Room select p).Single<Rooms>();
            }
            else
            {
                combxNumberClient.SelectedItem = -1;
            }
        }

        private void SearchTxtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListNumberRooms.Items.Clear();
            foreach (var item2 in from z in entities.Rooms where z.Number.StartsWith(SearchTxtNumber.Text) select z)
            {
                ListNumberRooms.Items.Add(item2);
            }
        }
    }
}
