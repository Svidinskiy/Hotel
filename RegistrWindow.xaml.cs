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
    /// Логика взаимодействия для RegistrWindow.xaml
    /// </summary>
    public partial class RegistrWindow : Window
    {
        hotelEntities7 entities = new hotelEntities7();
        public RegistrWindow()
        {
            InitializeComponent();
        }

        private void registrBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = textPass.Password;
            var user = new Users();
            if (textLogin.Text.Length > 0) // проверка логина
            {
                if (textPass.Password.Length > 0) // проверка пароля
                {
                    if (textPassCopy.Password.Length > 0) // проверка вторго пароля
                    {
                        if (textPass.Password.Length >= 4) // проверка заполняемости поля пароль
                        {
                            if (textPass.Password.Length >= 4)
                            {
                                if (textPass.Password == textPassCopy.Password) // проверка на совпадение паролей
                                {
                                    entities.Users.Add(user);
                                    user.Login = textLogin.Text;
                                    user.Password = pass;
                                    entities.SaveChanges();
                                    MessageBox.Show("Пользователь зарегистрирован", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    Window Aa = new MainWindow();
                                    Aa.Show();
                                    this.Close();
                                }
                                else MessageBox.Show("Пароли не совподают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else MessageBox.Show("Пароль слишком короткий, минимум 4 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Повторите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Укажите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Укажите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {          
            Window B = new MainWindow();
            B.Show();
            this.Close();
        }
    }
}

