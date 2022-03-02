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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            bool a = false;
            foreach (var login in entities.Users)
            {
                if (userLogin.Text.Length > 0) //проверка не пустое ли поле логина
                {
                    if (userPass.Password.Length > 0) // проверка не пустое ли поле пароля
                    {
                        if (login.ID_user == 1 && login.Login == userLogin.Text && login.Password == userPass.Password)
                        {
                            MessageBox.Show("Выполнен вход администратором","Авторизация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            var n = new AdminWindow();
                            n.Show();
                            this.Close();
                            a = true;
                        }
                        else if (login.Login == userLogin.Text && login.Password == userPass.Password) // сравнение введенных данных с данными из БД
                        {
                            MessageBox.Show("Вход выполнен","Авторизация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            var w = new Osnovnle();
                            w.Show();
                            this.Close();
                            a = true;
                        }                      
                    }
                    else MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (a != true)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void registrBtn_Click(object sender, RoutedEventArgs e)
        {
            Window A = new RegistrWindow();
            this.Close();
            A.ShowDialog();            
        }
    }
}
