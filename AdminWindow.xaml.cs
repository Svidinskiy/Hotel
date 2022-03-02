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
using Word = Microsoft.Office.Interop.Word;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public AdminWindow()
        {
            InitializeComponent();
            foreach (var roomsadm in entities.Rooms)
                ListRoomsAdmin.Items.Add(roomsadm);
            foreach (var house in entities.Housemaid)
                ComboHousemaidAdm.Items.Add(house);
            foreach (var clientadm in entities.Clients)
                ListClientsAdmin.Items.Add(clientadm);
            foreach (var clientroom in entities.Rooms)
                ComboClientAdm.Items.Add(clientroom);
        }

        private void ListRoomsAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_rooms = ListRoomsAdmin.SelectedItem as Rooms;
            if (selected_rooms != null)
            {
                TextNumberAdmin.Text = selected_rooms.Number;
                TextCategAdmin.Text = selected_rooms.Category;
                TextPriceAdmin.Text = selected_rooms.Price.ToString();
                TextOpisAdmin.Text = selected_rooms.Description;
                TextStatusAdmin.Text = selected_rooms.Status;
                ComboHousemaidAdm.SelectedItem = (from h in entities.Housemaid where h.ID_housemaid == selected_rooms.C__housemaid select h).Single<Housemaid>();

            }
            else
            {
                TextNumberAdmin.Text = "";
                TextCategAdmin.Text = "";
                TextPriceAdmin.Text = "";
                TextOpisAdmin.Text = "";
                TextStatusAdmin.Text = "";
                ComboHousemaidAdm.SelectedIndex = -1;
            }
        }

        private void btnSaveAdmin_Click(object sender, RoutedEventArgs e)
        {
            var save_rooms = ListRoomsAdmin.SelectedItem as Rooms;
            if (TextNumberAdmin.Text == "" || ComboHousemaidAdm.SelectedIndex == -1 || TextCategAdmin.Text == "" || TextPriceAdmin.Text == "" || TextOpisAdmin.Text == "" || TextStatusAdmin.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (save_rooms == null)
                {
                    save_rooms = new Rooms();
                    entities.Rooms.Add(save_rooms);
                    ListRoomsAdmin.Items.Add(save_rooms);
                }
                save_rooms.Category = TextCategAdmin.Text;
                save_rooms.Description = TextOpisAdmin.Text;
                save_rooms.Price = Int32.Parse(TextPriceAdmin.Text); ;
                save_rooms.Number = TextNumberAdmin.Text;
                save_rooms.Status = TextStatusAdmin.Text;
                save_rooms.C__housemaid = (ComboHousemaidAdm.SelectedItem as Housemaid).ID_housemaid;
                entities.SaveChanges();
                ListRoomsAdmin.Items.Refresh();
                MessageBox.Show("Информация о номере добавлена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void NewAdmin_Click(object sender, RoutedEventArgs e)
        {
            TextNumberAdmin.Text = "";
            TextCategAdmin.Text = "";
            TextPriceAdmin.Text = "";
            TextOpisAdmin.Text = "";
            TextStatusAdmin.Text = "";
            ComboHousemaidAdm.SelectedIndex = -1;
            ListRoomsAdmin.SelectedIndex = -1;
            TextNumberAdmin.Focus();
        }

        private void btnDeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            var delete = MessageBox.Show("Выдествительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (delete == MessageBoxResult.No)
            {
                return;
            }
            var delete_rooms = ListRoomsAdmin.SelectedItem as Rooms;
            if (delete_rooms != null)
            {
                try // проверка соотвествия условиям удаления
                {
                    var exist_room = (from rms in entities.Clients where rms.Room == delete_rooms.ID_room select rms).First();
                    MessageBox.Show("Номер удалить нельзя\n Зарегистрирован постоялец", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    entities.Rooms.Remove(delete_rooms);
                    entities.SaveChanges();
                    TextNumberAdmin.Clear();
                    TextCategAdmin.Clear();
                    TextPriceAdmin.Clear();
                    TextStatusAdmin.Clear();
                    TextOpisAdmin.Clear();
                    ListRoomsAdmin.Items.Remove(delete_rooms);
                    ComboHousemaidAdm.SelectedItem = -1;
                    MessageBox.Show("Запись удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
                MessageBox.Show("Нет удаляемых записей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddHousemaid_Click(object sender, RoutedEventArgs e)
        {
            Window m = new HousemaidWindow();
            m.Show();
            this.Close();
        }

        private void ListClientsAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_client = ListClientsAdmin.SelectedItem as Clients;
            if (selected_client != null)
            {
                txtNameClientAdm.Text = selected_client.FIO;
                datePR.SelectedDate = selected_client.Arrival_date;
                dateOTR.SelectedDate = selected_client.Departure_date;
                //TextNumberAdmin.Text = selected_rooms.Number;
                ComboClientAdm.Text = selected_client.Rooms.ToString();
                ComboClientAdm.SelectedItem = (from r in entities.Rooms where r.ID_room == selected_client.Room select r).Single<Rooms>();
            }
            else
            {
                txtNameClientAdm.Text = "";
                ComboClientAdm.SelectedIndex = -1;
                datePR.SelectedDate = null;
                dateOTR.SelectedDate = null;
            }
        }
        private void ExitBtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Закрыть программу?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnClientSave_Click(object sender, RoutedEventArgs e)
        {
            var save_client = ListClientsAdmin.SelectedItem as Clients;
            if (ComboClientAdm.SelectedIndex == -1 ) //|| datePR.SelectedDate = null || dateOTR. )
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (save_client == null)
                {
                    save_client = new Clients();
                    entities.Clients.Add(save_client);
                    ListRoomsAdmin.Items.Add(save_client);
                }
                save_client.FIO = txtNameClientAdm.Text;
                save_client.Room = (ComboClientAdm.SelectedItem as Rooms).ID_room;
                
                save_client.Arrival_date = datePR.DisplayDate;
                save_client.Departure_date = dateOTR.DisplayDate;
                entities.SaveChanges();
                ListClientsAdmin.Items.Refresh();
                MessageBox.Show("Информация о клиенте изменена", "Изменение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void btnBackAdmin_Click(object sender, RoutedEventArgs e)
        {
            var result_back = MessageBox.Show("Вернуться к авторизации?", "Назад", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result_back == MessageBoxResult.Yes)
            {
                Window avt = new MainWindow();
                avt.Show();
                this.Close();
            }
        }

        private void BtnClientDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete1 = MessageBox.Show("Выдествительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (delete1 == MessageBoxResult.No)
            {
                return;
            }
            var delete_client1 = ListClientsAdmin.SelectedItem as Clients;
            if (delete_client1 != null)
            {

                entities.Clients.Remove(delete_client1);
                entities.SaveChanges();
                txtNameClientAdm.Clear();
                datePR.SelectedDate = null;
                dateOTR.SelectedDate = null;
                ListClientsAdmin.Items.Remove(delete_client1);
                ComboClientAdm.SelectedItem = -1;
                MessageBox.Show("Запись удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
            else
                MessageBox.Show("Нет удаляемых записей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnPrintWord_Click(object sender, RoutedEventArgs e)
        {
            Word.Application wordclient = new Word.Application();
            wordclient.Visible = true;
            Word.Document worddoc;
            object wordobj = System.Reflection.Missing.Value;
            worddoc = wordclient.Documents.Add(ref wordobj);
            wordclient.Selection.TypeText(TextOpisAdmin.Text);           
            //wordclient.Selection.TypeText(ComboClientAdm.Text);
            wordclient = null;
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListRoomsAdmin.Items.Clear();
            foreach (var item in from z in entities.Rooms where z.Number.StartsWith(SearchTxt.Text) select z)
            {
                ListRoomsAdmin.Items.Add(item);
            }
        }

        private void SearchTxtClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListClientsAdmin.Items.Clear();
            foreach (var item1 in from n in entities.Clients where n.FIO.StartsWith(SearchTxtClient.Text) select n)
            {
                ListClientsAdmin.Items.Add(item1);
            }
        }
    }
}
