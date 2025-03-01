using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Матричный_калькулятор
{
    /// <summary>
    /// Логика взаимодействия для ResultMatrix.xaml
    /// </summary>
    public partial class ResultMatrix : Window
    {
        private Dictionary<string, double[,]> matrixDictionary;
        private string operation;
        private double[,] resultMatrix;
        public ResultMatrix(Dictionary<string, double[,]> matrixDictionary, string operation, double[,] resultMatrix)
        {
            InitializeComponent();
            this.matrixDictionary = matrixDictionary;
            this.operation = operation;
            this.resultMatrix = resultMatrix;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var data = new
            {
                Matrices = this.matrixDictionary,
                Operation = this.operation,
                ResultMatrix = this.resultMatrix
            };

            List<object> dataList;
            if (File.Exists("data.json"))
            {
                // Считывание существующего массива из файла
                string existingJson = File.ReadAllText("data.json");
                dataList = JsonConvert.DeserializeObject<List<object>>(existingJson);

                // Если dataList равен null, создать новый список
                if (dataList == null)
                {
                    dataList = new List<object>();
                }
            }
            else
            {
                // Создание нового списка, если файл не существует
                dataList = new List<object>();
            }

                // Добавление новых данных в список
                dataList.Add(data);

            // Запись обновленного списка обратно в файл
            string json = JsonConvert.SerializeObject(dataList);
            File.WriteAllText("data.json", json);
            MessageBox.Show("Успешное сохранение!");
        }
    }
}
