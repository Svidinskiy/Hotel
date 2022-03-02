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
    /// Логика взаимодействия для HousemaidWindow.xaml
    /// </summary>
    public partial class HousemaidWindow : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public HousemaidWindow()
        {
            InitializeComponent();
            foreach (var hm in entities.Housemaid)
                ListHousemaid.Items.Add(hm);
        }

        private void ListHousemaid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_house = ListHousemaid.SelectedItem as Housemaid;
            if (selected_house != null)
            {
                TxtHouesemName.Text = selected_house.FIO;
                txtHousemPhone.Text = selected_house.Telephone;
            }
            else
            {
                TxtHouesemName.Text = "";
                txtHousemPhone.Text = "";
            }
        }

        private void btnSaveHouse_Click(object sender, RoutedEventArgs e)
        {
            var house = ListHousemaid.SelectedItem as Housemaid;
            if (TxtHouesemName.Text == "" || txtHousemPhone.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (house == null)
                {
                    house = new Housemaid();
                    entities.Housemaid.Add(house);
                    ListHousemaid.Items.Add(house);
                }
                house.FIO = TxtHouesemName.Text;
                house.Telephone = txtHousemPhone.Text;
                entities.SaveChanges();
                ListHousemaid.Items.Refresh();
                MessageBox.Show("Запись сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void btnNewHouse_Click(object sender, RoutedEventArgs e)
        {
            TxtHouesemName.Text = "";
            txtHousemPhone.Text = "";
            TxtHouesemName.Focus();
        }

        private void btnDeleteHouse_Click(object sender, RoutedEventArgs e)
        {
            var del = MessageBox.Show("Выдествительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (del == MessageBoxResult.No)
            {
                return;
            }
            var delete_house = ListHousemaid.SelectedItem as Housemaid;
            if (delete_house != null)
            {
                try // проверка соотвествия условиям удаления
                {
                    var exist_house = (from rms in entities.Rooms where rms.C__housemaid == delete_house.ID_housemaid select rms).First();
                    MessageBox.Show("Запись удалить нельзя\n Горничная назначена в номер", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    entities.Housemaid.Remove(delete_house);
                    entities.SaveChanges();
                    TxtHouesemName.Clear();
                    txtHousemPhone.Clear();
                    ListHousemaid.Items.Remove(delete_house);
                }
            }
            else
                MessageBox.Show("Нет удаляемых записей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnBackHouse_Click(object sender, RoutedEventArgs e)
        {
            Window win = new AdminWindow();
            win.Show();
            this.Close();          
        }
    }
}
