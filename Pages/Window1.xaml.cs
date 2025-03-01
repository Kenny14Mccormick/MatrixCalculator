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
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

//******************************************************************************************
//* Название программы: "Матричный калькулятор"                                            *
//*                                                                                        *
//* Назначение программы: программа предназначена для выполнения операций над матрицами.   *
//*                                                                                        *
//* Разработчик: студент группы ПР-330/б Хортов С.В.                                       *
//*                                                                                        *
//* Версия: 1.0                                                                            *
//******************************************************************************************

namespace Матричный_калькулятор.Pages
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int MatrixCount { get; set; }
        public Window1()
        {
            InitializeComponent();
        }
        private Dictionary<string, TextBox[,]> textBoxArrays = new Dictionary<string, TextBox[,]>();
        MatrixOperations matrixOps = new MatrixOperations();
        Dictionary<string, Grid> matrixGrids = new Dictionary<string, Grid>();
        private Grid matrixGrid;
        public int[] matrixRowCount;
        public int[] matrixColumnCount;
        TextBox rowsTextBox;
        TextBox columnsTextBox;

        private void CreateMatricesButton_Click(object sender, RoutedEventArgs e)
        {
            CreateMatricesGrid.Children.Clear();
            CreateMatricesGrid.RowDefinitions.Clear();

            try
            {
                MatrixCount = int.Parse(MatrixCountTextBox.Text);
                if (MatrixCount > 10 | MatrixCount <= 0)
                {
                    throw new Exception("Указано некорректное количество матриц!");
                }
                matrixRowCount = new int[MatrixCount];
                matrixColumnCount = new int[MatrixCount];
                int count = SameSizeCheckBox.IsChecked.Value ? 1 : MatrixCount;
                string s = "";


                for (int j = 0; j < count; j++)
                {
                    RowDefinition rowDefinition1 = new RowDefinition();
                    CreateMatricesGrid.RowDefinitions.Add(rowDefinition1);



                    // TextBlock для "Матрица i"
                    TextBlock textBlock = new TextBlock();
                    if (SameSizeCheckBox.IsChecked == true)
                    {
                        s = "Матрицы";
                    }
                    else s = $"Матрица {(char)('A' + j)}";
                    textBlock.Text = s;
                    textBlock.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;

                    Grid.SetRow(textBlock, j * 3);
                    Grid.SetColumnSpan(textBlock, 2);
                    CreateMatricesGrid.Children.Add(textBlock);

                    // TextBlock для "Количество строк"
                    TextBlock RowsText = new TextBlock();
                    RowsText.Text = "Введите количество строк";
                    RowsText.HorizontalAlignment = HorizontalAlignment.Center;
                    RowsText.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(RowsText, 0);
                    Grid.SetRow(RowsText, j * 3 + 1);
                    CreateMatricesGrid.Children.Add(RowsText);

                    // TextBlock для "Количество столбцов"
                    TextBlock ColText = new TextBlock();
                    ColText.Text = "Введите количество столбцов";
                    ColText.VerticalAlignment = VerticalAlignment.Center;
                    ColText.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(ColText, 0);
                    Grid.SetRow(ColText, j * 3 + 2);
                    CreateMatricesGrid.Children.Add(ColText);
                    RowDefinition rowDefinition3 = new RowDefinition();
                    CreateMatricesGrid.RowDefinitions.Add(rowDefinition3);

                    for (int k = 0; k < 1; k++)
                    {
                        RowDefinition rowDefinition = new RowDefinition();
                        CreateMatricesGrid.RowDefinitions.Add(rowDefinition);

                        // TextBox для ввода количества строк
                        rowsTextBox = new TextBox();
                        rowsTextBox.Name = "RowsTextBox_" + j + "_" + k; // Уникальное имя
                        rowsTextBox.Height = 48;
                        rowsTextBox.BorderBrush = Brushes.Black;
                        rowsTextBox.BorderThickness = new Thickness(2);
                        rowsTextBox.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);

                        if (CreateMatricesGrid.FindName(rowsTextBox.Name) is TextBox)
                        {
                            //Если имя уже зарегестрировано, удаляем
                            CreateMatricesGrid.UnregisterName(rowsTextBox.Name);
                        }
                        CreateMatricesGrid.RegisterName(rowsTextBox.Name, rowsTextBox);
                        Grid.SetColumn(rowsTextBox, 1);
                        Grid.SetRow(rowsTextBox, j * 3 + k * 3 + 1);

                        // TextBox для ввода количества столбцов
                        columnsTextBox = new TextBox();
                        columnsTextBox.Name = "ColumnsTextBox_" + j + "_" + k; // Уникальное имя
                        columnsTextBox.Height = 48;
                        columnsTextBox.BorderBrush = Brushes.Black;
                        columnsTextBox.BorderThickness = new Thickness(2);
                        columnsTextBox.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);

                        if (CreateMatricesGrid.FindName(columnsTextBox.Name) is TextBox)
                        {
                            //Если имя уже зарегестрировано, удаляем
                            CreateMatricesGrid.UnregisterName(columnsTextBox.Name);
                        }
                        CreateMatricesGrid.RegisterName(columnsTextBox.Name, columnsTextBox);
                        Grid.SetColumn(columnsTextBox, 1);
                        Grid.SetRow(columnsTextBox, j * 3 + k * 3 + 2);


                        CreateMatricesGrid.Children.Add(rowsTextBox);
                        CreateMatricesGrid.Children.Add(columnsTextBox);

                    }

                }


                InputPanel.Visibility = Visibility.Collapsed;
                Sizes.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            bool allDimensionsValid = true;
            int rowCount = 0;
            int columnCount = 0;
            if (SameSizeCheckBox.IsChecked.Value)
            {
                // Если выбран чекбокс, берем размеры только из первых текстбоксов
                if (!int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_0_0"))?.Text, out rowCount) ||
                    !int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_0_0"))?.Text, out columnCount))
                {
                    allDimensionsValid = false;
                }
            }
            else
            {
                // Проверка размеров для каждой матрицы
                for (int matrixIndex = 0; matrixIndex < MatrixCount; matrixIndex++)
                {
                    if (!int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_{matrixIndex}_0"))?.Text, out rowCount) ||
                        !int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_{matrixIndex}_0"))?.Text, out columnCount))
                    {
                        allDimensionsValid = false;
                        break;
                    }
                }
            }


            if (!allDimensionsValid)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для размеров всех матриц.");
                return; // Прервать выполнение при некорректных значениях
            }

            // Проверка всех размеров перед основным циклом
            int count = SameSizeCheckBox.IsChecked.Value ? 1 : MatrixCount;
            for (int matrixIndex = 0; matrixIndex < count; matrixIndex++)
            {
                if (!int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_{matrixIndex}_0"))?.Text, out _) ||
                    !int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_{matrixIndex}_0"))?.Text, out _))
                {
                    allDimensionsValid = false;
                    break;
                }
            }
            if (!allDimensionsValid)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для размеров всех матриц.");
                return; // Прервать выполнение при некорректных значениях
            }

            MatricesStackPanel.Children.Clear();
            try
            {
                for (int matrixIndex = 0; matrixIndex < MatrixCount; matrixIndex++)
                {
                    // Парсинг размеров для каждой матрицы внутри цикла
                    if (!SameSizeCheckBox.IsChecked.Value)
                    {
                        if (!int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_{matrixIndex}_0"))?.Text, out rowCount) ||
                            !int.TryParse(((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_{matrixIndex}_0"))?.Text, out columnCount))
                        {
                            MessageBox.Show("Пожалуйста, введите корректные значения для размеров всех матриц.");
                            return; // Прервать выполнение при некорректных значениях
                        }
                    }

                    double[,] matrix = new double[rowCount, columnCount];


                    // Создание нового StackPanel для каждой матрицы
                    StackPanel matrixStackPanel = new StackPanel();
                    matrixStackPanel.Orientation = Orientation.Vertical;
                    MatricesStackPanel.Children.Add(matrixStackPanel);

                    // Создание текстблока для отображения номера матрицы
                    TextBlock matrixLabel = new TextBlock();
                    matrixLabel.Text = $"Матрица {(char)('A' + matrixIndex)}";
                    matrixLabel.FontWeight = FontWeights.Bold;
                    matrixLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    matrixStackPanel.Children.Add(matrixLabel);

                    // Создание ScrollViewer для текущей матрицы
                    var matrixScrollViewer = new ScrollViewer
                    {
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled
                    };


                    // Создание вложенной сетки для текущей матрицы
                    matrixGrid = new Grid();
                    string key = $"Matrix_{matrixIndex}";
                    if (matrixGrids.ContainsKey(key))
                    {
                        matrixGrids.Remove(key);
                    }

                    matrixGrids.Add($"Matrix_{matrixIndex}", matrixGrid);
                    matrixScrollViewer.Content = matrixGrid; // Добавление сетки в ScrollViewer
                    matrixStackPanel.Children.Add(matrixScrollViewer);
                    matrixScrollViewer.Margin = new Thickness(left: 50, right: 50, top: 0, bottom: 0);

                    // Добавление строк и столбцов во вложенную сетку
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        matrixGrid.RowDefinitions.Add(new RowDefinition());
                    }
                    for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                    {
                        matrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    // Создание массива textBoxes перед внутренними циклами
                    TextBox[,] textBoxes = new TextBox[rowCount, columnCount];

                    if (rowCount > 20)
                    {
                        MessageBox.Show("Количество строк не может быть больше 20");
                        return;
                    }
                    else if (columnCount > 20)
                    {
                        MessageBox.Show("Количество столбцов не может быть больше 20");
                        return;
                    }
                    // Добавление элементов TextBox в сетку
                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                        {
                            TextBox textBox = new TextBox();
                            // Примерно определенный размер (можно настроить)
                            textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
                            textBox.MinHeight = 60;
                            textBox.MinWidth = 60;
                            textBox.Name = $"MatrixTextBox_{matrixIndex}_{rowIndex}_{columnIndex}";
                            if (matrixGrid.FindName(textBox.Name) is TextBox)
                            {
                                // Если имя уже зарегистрировано, отменить его регистрацию
                                matrixGrid.UnregisterName(textBox.Name);
                            }
                            matrixGrid.RegisterName(textBox.Name, textBox);
                            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                            textBox.VerticalContentAlignment = VerticalAlignment.Center;

                            // Добавление textBox в массив
                            textBoxes[rowIndex, columnIndex] = textBox;

                            matrixGrid.Children.Add(textBox);

                            // Установка строки и столбца для каждого текстблока
                            Grid.SetRow(textBox, rowIndex);
                            Grid.SetColumn(textBox, columnIndex);
                            TextBox textBox1 = (TextBox)matrixGrid.FindName($"MatrixTextBox_{matrixIndex}_{rowIndex}_{columnIndex}");
                            if (double.TryParse(textBox.Text, out double value))
                            {
                                matrix[rowIndex, columnIndex] = value;
                            }
                            else
                            {
                                // Обработка ошибки ввода для текущего текстбокса
                            }
                        }
                    }
                    Button TransposeButton = new Button();
                    Button FillWithIntegers = new Button();
                    Button FillWithFractional = new Button();
                    TransposeButton.FontSize = 13;
                    FillWithIntegers.FontSize = 13;
                    FillWithFractional.FontSize = 13;
                    TransposeButton.Width = 150;
                    TransposeButton.Height = 25;
                    FillWithIntegers.Width = 150;
                    FillWithIntegers.Height = 25;
                    FillWithFractional.Width = 150;
                    FillWithFractional.Height = 25;
                    TransposeButton.Content = "Транспонировать";
                    FillWithIntegers.Content = "Заполнить целыми";
                    FillWithFractional.Content = "Заполнить дробными";
                    TransposeButton.Margin = new Thickness(left: 0, right: 0, top: 10, bottom: 0);
                    FillWithIntegers.Margin = new Thickness(left: 0, right: 0, top: 5, bottom: 5);
                    TransposeButton.Tag = $"Matrix_{matrixIndex}";
                    FillWithIntegers.Tag = $"Matrix_{matrixIndex}";
                    FillWithFractional.Tag = $"Matrix_{matrixIndex}";
                    TransposeButton.Click += TransposeButton_Click;
                    FillWithIntegers.Click += FillWithIntegers_Click;
                    FillWithFractional.Click += FillWithFractional_Click;

                    if (textBoxArrays.ContainsKey(key))
                    {
                        textBoxArrays.Remove(key);
                    }
                    textBoxArrays.Add($"Matrix_{matrixIndex}", textBoxes);
                    matrixStackPanel.Children.Add(TransposeButton);
                    matrixStackPanel.Children.Add(FillWithIntegers);
                    matrixStackPanel.Children.Add(FillWithFractional);

                    if (matrixDictionary.ContainsKey(key))
                    {
                        matrixDictionary.Remove(key);
                    }
                    // Добавление массива textBoxes в словарь после внутренних циклов
                    matrixDictionary.Add($"Matrix_{matrixIndex}", matrix);
                    LoadMatrices.Visibility = Visibility.Collapsed;
                    Sizes.Visibility = Visibility.Collapsed;
                    MatricesGrid.Visibility = Visibility.Visible;

                }
            }
            catch when (rowCount <= 0 | columnCount <= 0)
            {
                MessageBox.Show("Введите корректные размеры");
            }

        }

        private void TransposeButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение имени матрицы из свойства Tag кнопки
            string matrixName = (sender as Button).Tag.ToString();

            int matrixIndex = int.Parse((sender as Button).Tag.ToString().Split('_')[1]);

            // Получение текущей матрицы из словаря
            TextBox[,] currentMatrix = textBoxArrays[$"Matrix_{matrixIndex}"];

            // Удаление старых текстовых блоков из словаря
            foreach (var textBox in currentMatrix)
            {
                matrixGrid.UnregisterName(textBox.Name);
            }

            // Создание новой матрицы для хранения транспонированных значений
            TextBox[,] transposedMatrix = new TextBox[currentMatrix.GetLength(1), currentMatrix.GetLength(0)];

            // Транспонирование матрицы
            for (int i = 0; i < currentMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < currentMatrix.GetLength(1); j++)
                {
                    transposedMatrix[j, i] = new TextBox();
                    transposedMatrix[j, i].Text = currentMatrix[i, j].Text;
                    transposedMatrix[j, i].PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
                    transposedMatrix[j, i].MinHeight = 60;
                    transposedMatrix[j, i].MinWidth = 60;
                    // Создание нового имени
                    string newName = $"MatrixTextBox_{matrixIndex}_{j}_{i}";

                    // Проверка, существует ли старое имя
                    if (matrixGrid.FindName(newName) is TextBox oldTextBox)
                    {
                        // Если существует, удалить старое имя
                        matrixGrid.UnregisterName(oldTextBox.Name);
                    }

                    // Зарегистрировать новое имя
                    transposedMatrix[j, i].Name = newName;
                    matrixGrid.RegisterName(newName, transposedMatrix[j, i]);
                    transposedMatrix[j, i].HorizontalContentAlignment = HorizontalAlignment.Center;
                    transposedMatrix[j, i].VerticalContentAlignment = VerticalAlignment.Center;
                }
            }

            // Замена старой матрицы на транспонированную в словаре
            textBoxArrays[matrixName] = transposedMatrix;
            UpdateMatrixDisplay(matrixName, transposedMatrix);

            // Обновление размеров матрицы в текстовых полях
            if (SameSizeCheckBox.IsChecked.Value)
            {
                ((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_0_0")).Text = currentMatrix.GetLength(1).ToString();
                ((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_0_0")).Text = currentMatrix.GetLength(0).ToString();
            }
            else
            {
                ((TextBox)this.CreateMatricesGrid.FindName($"RowsTextBox_{matrixIndex}_0")).Text = currentMatrix.GetLength(1).ToString();
                ((TextBox)this.CreateMatricesGrid.FindName($"ColumnsTextBox_{matrixIndex}_0")).Text = currentMatrix.GetLength(0).ToString();
            }

            // Создание новой матрицы для хранения транспонированных значений
            double[,] transposedMatrixValues = new double[currentMatrix.GetLength(1), currentMatrix.GetLength(0)];

            // Копирование значений из transposedMatrix в transposedMatrixValues
            for (int i = 0; i < transposedMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < transposedMatrix.GetLength(1); j++)
                {
                    if (double.TryParse(transposedMatrix[i, j].Text, out double value))
                    {
                        transposedMatrixValues[i, j] = value;
                    }
                }
            }

            // Замена старой матрицы на транспонированную в словаре
            matrixDictionary[matrixName] = transposedMatrixValues;
        }


        private void UpdateMatrixDisplay(string matrixName, TextBox[,] matrix)
        {
            // Получение Grid, содержащего текущую матрицу
            Grid matrixGrid = matrixGrids[matrixName];

            // Очистка старых строк, столбцов и дочерних элементов
            matrixGrid.RowDefinitions.Clear();
            matrixGrid.ColumnDefinitions.Clear();
            matrixGrid.Children.Clear();

            // Добавление новых строк и столбцов
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrixGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Добавление новых TextBox в Grid
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    TextBox textBox = matrix[i, j];
                    matrixGrid.Children.Add(textBox);
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                }
            }
        }
        private void FillWithIntegers_Click(object sender, RoutedEventArgs e)
        {
            // Получение имени матрицы из свойства Tag кнопки
            string matrixName = (sender as Button).Tag.ToString();
            TextBox[,] currentMatrix = textBoxArrays[matrixName];

            // Создание генератора случайных чисел
            Random random = new Random();

            // Заполнение матрицы случайными целыми числами
            for (int rowIndex = 0; rowIndex < currentMatrix.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < currentMatrix.GetLength(1); columnIndex++)
                {
                    currentMatrix[rowIndex, columnIndex].Text = random.Next(-100, 100).ToString();  // Генерация случайного числа в диапазоне от -100 до 100
                }
            }
        }

        private void FillWithFractional_Click(object sender, RoutedEventArgs e)
        {
            // Получение имени матрицы из свойства Tag кнопки
            string matrixName = (sender as Button).Tag.ToString();
            TextBox[,] currentMatrix = textBoxArrays[matrixName];

            // Создание генератора случайных чисел
            Random random = new Random();

            // Заполнение матрицы случайными дробными числами
            for (int rowIndex = 0; rowIndex < currentMatrix.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < currentMatrix.GetLength(1); columnIndex++)
                {
                    currentMatrix[rowIndex, columnIndex].Text = (random.NextDouble() * 200 - 100).ToString("F2");  // Генерация случайного числа в диапазоне от -100 до 100 с двумя знаками после запятой
                }
            }
        }



        private Dictionary<string, double[,]> matrixDictionary = new Dictionary<string, double[,]>();

        private void CalculateMatrices_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MatrixCount; i++)
            {
                int rowCount = 0, columnCount = 0;

                // Получение значений из соответствующих текстбоксов
                string rowsTextBoxName = SameSizeCheckBox.IsChecked.Value ? "RowsTextBox_0_0" : $"RowsTextBox_{i}_0";
                string columnsTextBoxName = SameSizeCheckBox.IsChecked.Value ? "ColumnsTextBox_0_0" : $"ColumnsTextBox_{i}_0";

                if (!int.TryParse(((TextBox)this.CreateMatricesGrid.FindName(rowsTextBoxName))?.Text, out rowCount) ||
               !int.TryParse(((TextBox)this.CreateMatricesGrid.FindName(columnsTextBoxName))?.Text, out columnCount))
                {
                    MessageBox.Show("Пожалуйста, введите корректные значения для размеров всех матриц.");
                    return; // Прервать выполнение при некорректных значениях
                }

                // Проверка, есть ли матрица с таким ключом в словаре
                if (matrixDictionary.ContainsKey($"Matrix_{i}"))
                {
                    // Если есть, удаляем старую матрицу
                    matrixDictionary.Remove($"Matrix_{i}");
                }

                double[,] matrix = new double[rowCount, columnCount];

                for (int j = 0; j < rowCount; j++)
                {
                    for (int k = 0; k < columnCount; k++)
                    {
                        string textBoxName = $"MatrixTextBox_{i}_{j}_{k}";
                        TextBox textBox = (TextBox)matrixGrid.FindName(textBoxName);

                        if (textBox == null)
                        {
                            MessageBox.Show($"Ошибка в текстбоксе {textBoxName}");
                            return; // Прервать выполнение при некорректных значениях
                        }

                        if (i == 0 || i == 1)
                        {
                            if (double.TryParse(textBox.Text, out double value))
                            {
                                matrix[j, k] = value;
                            }
                            else
                            {
                                // Обработка ошибки ввода для текущего текстбокса
                                MessageBox.Show($"Заполните все ячейки матриц цифрами");
                                return; // Прервать выполнение при некорректных значениях
                            }
                        }
                    }
                }

                matrixDictionary.Add($"Matrix_{i}", matrix);

                // Обновление размеров матрицы в текстовых полях
                ((TextBox)this.CreateMatricesGrid.FindName(rowsTextBoxName)).Text = matrix.GetLength(0).ToString();
                ((TextBox)this.CreateMatricesGrid.FindName(columnsTextBoxName)).Text = matrix.GetLength(1).ToString();

            }




            string operation = OperationTextBox.Text;
            if (operation == "")
            {
                MessageBox.Show("Введите математическое выражение");
                return;
            }
            // Создайте матрицу для хранения результата перед циклом
            double[,] resultMatrix = null;

            double scalar = 1;
            char operationSymbol = '+';

            string scalarString = "";
            for (int i = 0; i < operation.Length; i++)
            {
                if (char.IsLetter(operation[i]))
                {
                    // Это матрица
                    string matrixName = "Matrix_" + (operation[i] - 'A');
                    if (!matrixDictionary.ContainsKey(matrixName))
                    {
                        MessageBox.Show($"Матрица {operation[i]} не существует. Пожалуйста, убедитесь, что вы правильно ввели ее.");
                        return;
                    }

                    double[,] matrix = matrixDictionary[matrixName];

                    // Если есть скаляр, умножьте матрицу на скаляр
                    if (!string.IsNullOrEmpty(scalarString))
                    {
                        scalar = double.Parse(scalarString);
                        matrix = matrixOps.MultiplyMatrixByScalar(matrix, scalar);
                        scalarString = "";  // Сбросьте строку скаляра
                        scalar = 1;  // Сбросьте скаляр обратно на 1
                    }

                    // Умножьте матрицу на скаляр
                    matrix = matrixOps.MultiplyMatrixByScalar(matrix, scalar);

                    // Если это первая матрица, скопируйте ее в resultMatrix
                    if (resultMatrix == null)
                    {
                        resultMatrix = (double[,])matrix.Clone();

                    }
                    else
                    {
                        // Выполните операцию с resultMatrix и текущей матрицей
                        if (operationSymbol == '+')
                        {
                            resultMatrix = matrixOps.AddMatrices(resultMatrix, matrix);
                        }
                        else if (operationSymbol == '-')
                        {
                            resultMatrix = matrixOps.SubtractMatrices(resultMatrix, matrix);
                        }
                        else if (operationSymbol == '*')
                        {
                            resultMatrix = matrixOps.MultiplyMatrices(resultMatrix, matrix);
                        }
                    }

                    // Сбросьте скаляр обратно на 1
                    scalar = 1;
                }
                else if (char.IsDigit(operation[i]))
                {
                    // Это скаляр, добавьте его к строке скаляра
                    scalarString += operation[i];
                }
                else
                {
                    // Это символ операции, сохраните его для следующего цикла
                    operationSymbol = operation[i];
                }
            }
            // Выведите результат
            if (resultMatrix != null)
            {
                ResultMatrix result = new ResultMatrix(matrixDictionary, OperationTextBox.Text, resultMatrix);

                // Очистка старых дочерних элементов
                result.stackPanel.Children.Clear();

                // Создание вложенной сетки для отображения результата
                Grid resultGrid = new Grid();
                result.stackPanel.Children.Add(resultGrid);
                resultGrid.Margin = new Thickness(left: 50, right: 50, top: 0, bottom: 0);

                // Добавление строк и столбцов во вложенную сетку (может потребоваться настроить)
                for (int rowIndex = 0; rowIndex < resultMatrix.GetLength(0); rowIndex++)
                {
                    resultGrid.RowDefinitions.Add(new RowDefinition());
                }
                for (int columnIndex = 0; columnIndex < resultMatrix.GetLength(1); columnIndex++)
                {
                    resultGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                // Добавление элементов TextBox в сетку для отображения результата
                for (int rowIndex = 0; rowIndex < resultMatrix.GetLength(0); rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < resultMatrix.GetLength(1); columnIndex++)
                    {
                        TextBox resultTextBox = new TextBox
                        {
                            Text = $"{resultMatrix[rowIndex, columnIndex]}",
                            MinHeight = 60,
                            MinWidth = 60,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            IsReadOnly = true
                        };

                        resultGrid.Children.Add(resultTextBox);

                        // Установка строки и столбца для каждого текстблока
                        Grid.SetRow(resultTextBox, rowIndex);
                        Grid.SetColumn(resultTextBox, columnIndex);
                    }
                }


                var data = new
                {
                    Matrices = matrixDictionary,
                    Operation = OperationTextBox.Text,
                    ResultMatrix = resultMatrix
                };
                string json = JsonConvert.SerializeObject(data);

                // Отображение окна
                result.Show();
            }

        }

        private void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox != null)
            {
                textBox.Focus();
                textBox.SelectAll();
                e.Handled = true; // Предотвращаем передачу события дальше, чтобы не сбросить выделение
            }
        }

        private void F2Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            new Pages.InfroProgramm().Show();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            new Pages.InfroProgramm().Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new MainWindow().Show();
        }

        private void BackToCount_Click(object sender, RoutedEventArgs e)
        {
            Sizes.Visibility = Visibility.Collapsed;
            InputPanel.Visibility = Visibility.Visible;
            MatrixCountTextBox.Text = "";

        }

        private void Btn_Teoria_Click(object sender, RoutedEventArgs e)
        {
            new Teoria().Show();
        }

        private void BackToSizes_Click(object sender, RoutedEventArgs e)
        {
            Sizes.Visibility = Visibility.Visible;
            MatricesGrid.Visibility = Visibility.Hidden;
        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            LoadMatrices.Visibility = Visibility.Collapsed;
            InputPanel.Visibility = Visibility.Visible;
        }

        private void Btn_Load_Click(object sender, RoutedEventArgs e)
        {
           
            
                int rowCount = 0;
                int columnCount = 0;
                CreateMatricesGrid.Children.Clear();
                // Считать данные из файла
                string json = File.ReadAllText("data.json");

                // Десериализовать JSON в список объектов
                List<JObject> dataList = JsonConvert.DeserializeObject<List<JObject>>(json);

            if (MatricesCmbBx.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите операцию из списка");
                return;
            }

            // Получить выбранную матрицу
            JObject selectedMatrixData = dataList[MatricesCmbBx.SelectedIndex];

                var data = JsonConvert.DeserializeObject<List<Dictionary<string, dynamic>>>(json);
                var matricesData = data[0]["Matrices"];
                int matrixIndex = 0;

                // Получить матрицы из выбранных данных
                JObject matrices = (JObject)selectedMatrixData["Matrices"];
                foreach (var matrixData in matrices)
                {
                    JArray matrix = (JArray)matrixData.Value;
                    rowCount = matrix.Count;
                    columnCount = ((JArray)matrix[0]).Count;

                    OperationTextBox.Text = $"{selectedMatrixData["Operation"]}";
                    MatrixCount = matrices.Count;
                    MatrixCountTextBox.Text = $"{MatrixCount}";
                    string s = "";

                    for (int k = 0; k < 1; k++)
                    {
                        RowDefinition rowDefinition1 = new RowDefinition();
                        CreateMatricesGrid.RowDefinitions.Add(rowDefinition1);
                        RowDefinition rowDefinition3 = new RowDefinition();
                        CreateMatricesGrid.RowDefinitions.Add(rowDefinition3);

                        // TextBlock для "Матрица i"
                        TextBlock textBlock = new TextBlock();
                        if (SameSizeCheckBox.IsChecked == true)
                        {
                            s = "Матрицы";
                        }
                        else s = $"Матрица {(char)('A' + matrixIndex)}";
                        textBlock.Text = s;
                        textBlock.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;

                        Grid.SetRow(textBlock, matrixIndex * 3);
                        Grid.SetColumnSpan(textBlock, 2);
                        CreateMatricesGrid.Children.Add(textBlock);

                        // TextBlock для "Количество строк"
                        TextBlock RowsText = new TextBlock();
                        RowsText.Text = "Введите количество строк";
                        RowsText.HorizontalAlignment = HorizontalAlignment.Center;
                        RowsText.VerticalAlignment = VerticalAlignment.Center;
                        Grid.SetColumn(RowsText, 0);
                        Grid.SetRow(RowsText, matrixIndex * 3 + 1);
                        CreateMatricesGrid.Children.Add(RowsText);

                        // TextBlock для "Количество столбцов"
                        TextBlock ColText = new TextBlock();
                        ColText.Text = "Введите количество столбцов";
                        ColText.VerticalAlignment = VerticalAlignment.Center;
                        ColText.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(ColText, 0);
                        Grid.SetRow(ColText, matrixIndex * 3 + 2);
                        CreateMatricesGrid.Children.Add(ColText);

                        RowDefinition rowDefinition = new RowDefinition();
                        CreateMatricesGrid.RowDefinitions.Add(rowDefinition);

                        // TextBox для ввода количества строк
                        TextBox rowsTextBox = new TextBox();
                        rowsTextBox.Name = "RowsTextBox_" + matrixIndex + "_" + k; // Уникальное имя
                        rowsTextBox.Height = 48;
                        rowsTextBox.BorderBrush = Brushes.Black;
                        rowsTextBox.BorderThickness = new Thickness(2);
                        rowsTextBox.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);
                        rowsTextBox.Text = $"{rowCount}";

                        if (CreateMatricesGrid.FindName(rowsTextBox.Name) is TextBox)
                        {
                            // Если имя уже зарегистрировано, удаляем
                            CreateMatricesGrid.UnregisterName(rowsTextBox.Name);
                        }
                        CreateMatricesGrid.RegisterName(rowsTextBox.Name, rowsTextBox);
                        Grid.SetColumn(rowsTextBox, 1);
                        Grid.SetRow(rowsTextBox, matrixIndex * 3 + k * 3 + 1);

                        // TextBox для ввода количества столбцов
                        TextBox columnsTextBox = new TextBox();
                        columnsTextBox.Name = "ColumnsTextBox_" + matrixIndex + "_" + k; // Уникальное имя
                        columnsTextBox.Height = 48;
                        columnsTextBox.BorderBrush = Brushes.Black;
                        columnsTextBox.BorderThickness = new Thickness(2);
                        columnsTextBox.Margin = new Thickness(left: 0, top: 10, right: 0, bottom: 0);
                        columnsTextBox.Text = $"{columnCount}";

                        if (CreateMatricesGrid.FindName(columnsTextBox.Name) is TextBox)
                        {
                            // Если имя уже зарегистрировано, удаляем
                            CreateMatricesGrid.UnregisterName(columnsTextBox.Name);
                        }
                        CreateMatricesGrid.RegisterName(columnsTextBox.Name, columnsTextBox);
                        Grid.SetColumn(columnsTextBox, 1);
                        Grid.SetRow(columnsTextBox, matrixIndex * 3 + k * 3 + 2);
                        CreateMatricesGrid.Children.Add(rowsTextBox);
                        CreateMatricesGrid.Children.Add(columnsTextBox);
                    }

                    matrixIndex++;
                }
                CreateMatrixButton_Click(sender, e);

                matrixIndex = 0;
                foreach (var matrixData in matrices)
                {
                    JArray matrix = (JArray)matrixData.Value;
                    rowCount = matrix.Count;
                    columnCount = ((JArray)matrix[0]).Count;

                    for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                        {
                            double value = matrix[rowIndex][columnIndex].ToObject<double>();

                            string textBoxName = $"MatrixTextBox_{matrixIndex}_{rowIndex}_{columnIndex}";
                            TextBox textBox = (TextBox)matrixGrid.FindName(textBoxName);
                            textBox.Text = value.ToString();
                        }
                    }
                    matrixIndex++;
                }
      

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            // Считать данные из файла
            string json = File.ReadAllText("data.json");
            MatricesCmbBx.Items.Clear();
            MatricesCmbBx.SelectedIndex = -1;

            // Десериализовать JSON в список объектов
            List<JObject> dataList = JsonConvert.DeserializeObject<List<JObject>>(json);

            // Добавить объекты в ComboBox
            foreach (var data in dataList)
            {
                // Форматировать строку для отображения операции
                string displayString = $"{data["Operation"]} =\n";

                // Преобразовать каждую матрицу в строку
                foreach (var row in data["ResultMatrix"])
                {
                    foreach (var value in row)
                    {
                        // Добавить значение и табуляцию к строке
                        displayString += $"{value}\t";
                    }

                    // Добавить новую строку в конце каждой строки матрицы
                    displayString += "\n";
                }

                // Добавить строку в ComboBox
                MatricesCmbBx.Items.Add(displayString);
            }
            InputPanel.Visibility = Visibility.Collapsed;
            LoadMatrices.Visibility = Visibility.Visible;
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MatricesCmbBx.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную операцию?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int selectedIndex = MatricesCmbBx.SelectedIndex;

                    // Считываем данные из файла
                    string json = File.ReadAllText("data.json");

                    // Десериализуем JSON в список объектов
                    List<JObject> dataList = JsonConvert.DeserializeObject<List<JObject>>(json);

                    if (selectedIndex >= 0 && selectedIndex < dataList.Count)
                    {
                        // Удаляем выбранный элемент из списка
                        dataList.RemoveAt(selectedIndex);

                        // Сериализуем обновленный список и сохраняем в файл
                        string updatedJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);
                        File.WriteAllText("data.json", updatedJson);

                        // Удаляем выбранный элемент из ComboBox
                        MatricesCmbBx.Items.RemoveAt(selectedIndex);
                        MatricesCmbBx.SelectedIndex = -1;

                        MessageBox.Show("Операция успешно удалена.");
                    }
                    else
                    {
                        MessageBox.Show("Выбранная операция не найдена в данных.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите операцию для удаления.");
            }
        }

    }
}