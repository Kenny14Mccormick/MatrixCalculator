using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Матричный_калькулятор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Main_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Pages.Window1 wd1 = new Pages.Window1();
            wd1.Show();
        }

        private void Btn_Info_Click(object sender, RoutedEventArgs e)
        {
            new Pages.InfroProgramm().Show();
        }

        private void Btn_Spravka_Click(object sender, RoutedEventArgs e)
        {
            new Pages.Spravka().Show();

        }

        private void F1Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            new Pages.Spravka().Show();
        }

        private void Btn_Teoria_Click(object sender, RoutedEventArgs e)
        {
            new Teoria().Show();
        }

        private void F2Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            new Pages.InfroProgramm().Show();
        }
    }
}
