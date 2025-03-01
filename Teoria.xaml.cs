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

namespace Матричный_калькулятор
{
    /// <summary>
    /// Логика взаимодействия для Teoria.xaml
    /// </summary>
    public partial class Teoria : Window
    {
        public Teoria()
        {
            InitializeComponent();
        }

        private void AddMatrices_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FEE0FF"));
            AddMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            SubtractMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            SubtractMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TransposeMatrix_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TransposeMatrix_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrixByScalar_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrixByScalar_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TeoriaFrame.Navigate(new Pages.AddMatrices());

        }

        private void SubtractMatrices_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            AddMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            SubtractMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FEE0FF"));
            SubtractMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TransposeMatrix_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TransposeMatrix_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrixByScalar_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrixByScalar_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TeoriaFrame.Navigate(new Pages.SubtractMatrices());

        }

        private void MultiplyMatrices_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            AddMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            SubtractMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            SubtractMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FEE0FF"));
            MultiplyMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TransposeMatrix_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TransposeMatrix_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrixByScalar_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrixByScalar_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TeoriaFrame.Navigate(new Pages.MultiplyMatrices());

        }

        private void TransposeMatrix_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            AddMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            SubtractMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            SubtractMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TransposeMatrix_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FEE0FF"));
            TransposeMatrix_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrixByScalar_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrixByScalar_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TeoriaFrame.Navigate(new Pages.TransposeMatrices());

        }

        private void MultiplyMatrixByScalar_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            AddMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            SubtractMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            SubtractMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrices_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            MultiplyMatrices_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            TransposeMatrix_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TransposeMatrix_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#BDF9FF"));
            MultiplyMatrixByScalar_Btn.Background = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FEE0FF"));
            MultiplyMatrixByScalar_Btn.Foreground = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#046B99"));
            TeoriaFrame.Navigate(new Pages.MultiplyMatrixByScalar());

        }

        private void Return_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

  
    }
}
