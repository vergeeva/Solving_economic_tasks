using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Амортизация
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Linear = new Dictionary<int, List<double>>();
            Decreasing_balance = new Dictionary<int, List<double>>();
            Sum_of_the_number_of_years = new Dictionary<int, List<double>>();
        }
        //Словари для каждого метода
        Dictionary <int,List <double>> Linear;
        Dictionary<int, List<double>> Decreasing_balance;
        Dictionary<int, List<double>> Sum_of_the_number_of_years;

        public void Fill_dgv_from_dict(Dictionary<int, List<double>> dict, DataGridView dataGridView)
        {//Функция для заполнения таблицы, принимает объект-словарь и таблицу для заполнения
            dataGridView.RowCount = dict.Count; //Количество строк = количество лет+1
            int i = 0; //Нужен счетчик
            foreach (KeyValuePair <int, List<double>> element in dict) //Для работы с каждым элементом словаря
            {
                dataGridView.Rows[i].Cells[0].Value = element.Key; //Записываем год в первый столбец
                for (int j = 0; j < element.Value.Count; j++) //Для работы с массивом внутри словаря
                {
                    dataGridView.Rows[i].Cells[j+1].Value = element.Value[j]; //Заполняем ячейку в таблице
                }
                i++; //Увеличиваем счетчик на 1
            }
        }
        public Dictionary<int, List<double>> Linear_method(double Cost, int years)
        {
            Dictionary<int, List<double>> dict = new Dictionary<int, List<double>>();//Словарь для хранения данных
            double depreciation_rate = 1 / (double)years; //Годовая Норма амортизации
            double Annual_depreciation_amount = Cost * depreciation_rate;//Ежегодная сумма амортизации
            double Monthly_depreciation_rate = Annual_depreciation_amount / 12; //Ежемесячные отчисления амортизации
            double Remains = Cost; //Остаток
            for (int i = 1; i <= years; i++)  //Цикл повторится столько, сколько задано лет
            {
                List<double> temp = new List<double>(); //Создаем массив для хранения вычисленных данных
                Remains -= Annual_depreciation_amount;//Вычисляем остаток на конец года
                if (i == years && Remains != 0) //если
                {//Разница в последний год не равна нулю
                    Annual_depreciation_amount = Annual_depreciation_amount + Remains;//Прибавляем к платежу за год
                    Monthly_depreciation_rate = Annual_depreciation_amount / 12;//Считаем за месяц заново
                    Remains = 0;
                }
                //Добавляем все вычисленные элементы в массив
                temp.Add(Math.Round(depreciation_rate * 100, 2));
                temp.Add(Math.Round(Annual_depreciation_amount, 2));
                temp.Add(Math.Round(Monthly_depreciation_rate,2));
                temp.Add(Math.Round(Remains, 2));
                //Добавляем номер года и масссив в словарь
                dict.Add(i, temp);
            }
            return dict;
        }
        public Dictionary<int, List<double>> Sum_of_the_number_of_years_method(double Cost, int years)
        {
            Dictionary<int, List<double>> dict = new Dictionary<int, List<double>>();//Словарь для хранения данных
            int year = years; //Переменная для того, чтобы не портить исходное значение
            int sum_of_years = 0;
            while (year !=0) //пока не ноль
            {
                sum_of_years += year; //Прибавляем все числа по убыванию
                year--; //вычитаем по одному
            }

            double Remains = Cost; //Остаток
            for (int i = 1; i <= years; i++)  //Цикл повторится столько, сколько задано лет
            {
                List<double> temp = new List<double>(); //Создаем массив для хранения вычисленных данных
                double depreciation_rate = (years - i + 1) / (double)sum_of_years; //Годовая Норма амортизации
                double Annual_depreciation_amount = Cost * depreciation_rate;//Годовая сумма амортизации
                double Monthly_depreciation_rate = Annual_depreciation_amount / 12; //Ежемесячные отчисления амортизации
                Remains -= Annual_depreciation_amount;//Вычисляем остаток на конец года

                if (i == years && Remains != 0) //если
                {//Разница в последний год не равна нулю
                    Annual_depreciation_amount += Remains;//Прибавляем к платежу за пятый год
                    Monthly_depreciation_rate = Annual_depreciation_amount / 12;//Считаем за месяц заново
                    Remains = 0;
                }
                //Добавляем все вычисленные элементы в массив
                temp.Add(Math.Round(depreciation_rate * 100, 2));
                temp.Add(Math.Round(Annual_depreciation_amount, 2));
                temp.Add(Math.Round(Monthly_depreciation_rate, 2));
                temp.Add(Math.Round(Remains, 2));
                //Добавляем номер года и масссив в словарь
                dict.Add(i, temp);
            }
            return dict;
        }

        public Dictionary<int, List<double>> Decreasing_balance_method(double Cost, int years, double Acceleration_coefficient)
        {
            Dictionary<int, List<double>> dict = new Dictionary<int, List<double>>();//Словарь для хранения данных
            double depreciation_rate = (1 / (double)years) * Acceleration_coefficient; //Годовая Норма амортизации

            double Remains = Cost; //Остаток
            for (int i = 1; i <= years; i++)  //Цикл повторится столько, сколько задано лет
            {
                List<double> temp = new List<double>(); //Создаем массив для хранения вычисленных данных

                double Annual_depreciation_amount = Remains * depreciation_rate;//Годовая сумма амортизации
                double Monthly_depreciation_rate = Annual_depreciation_amount / 12; //Ежемесячные отчисления амортизации
                Remains -= Annual_depreciation_amount;//Вычисляем остаток на конец года

                if (i == years && Remains != 0) //если
                {//Разница в последний год не равна нулю
                    Annual_depreciation_amount += Remains;//Прибавляем к платежу за пятый год
                    Monthly_depreciation_rate = Annual_depreciation_amount / 12;//Считаем за месяц заново
                    Remains = 0;
                }
                //Добавляем все вычисленные элементы в массив
                temp.Add(Math.Round(depreciation_rate * 100, 2));
                temp.Add(Math.Round(Annual_depreciation_amount, 2));
                temp.Add(Math.Round(Monthly_depreciation_rate, 2));
                temp.Add(Math.Round(Remains, 2));
                //Добавляем номер года и масссив в словарь
                dict.Add(i, temp);
            }
            return dict;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Убираем лишние цифры с оси Х
            chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            chart2.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            chart3.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Кнопка рассчитать для первой страницы
            //Линейный метод
            if (textBox6.Text != "" && textBox5.Text != "") //Проверяем, заполнил ли пользователь поля
            {
                try //Обработчик ошибок
                {
                    Linear = Linear_method(Convert.ToDouble(textBox6.Text), Convert.ToInt32(textBox5.Text));
                    Fill_dgv_from_dict(Linear, dataGridView2);
                    //Построение диаграммы
                    this.ClientSize = new System.Drawing.Size(980, 629);
                    chart1.Series.Clear();
                    int i = 0;
                    foreach (KeyValuePair<int,List <double>> item in Linear)
                    {
                        chart1.Series.Add("Series" + Convert.ToString(i));
                        chart1.Series[i].LegendText = item.Key.ToString() + " год";
                        chart1.Series[i].Points.AddXY(i, item.Value[1]);
                        chart1.Series[i]["PixelPointWidth"] = "120";
                        chart1.Update(); //Обновить диаграмму
                        i++;
                    }
                }
                catch(Exception)
                {
                    //если с данными будет что-то не так
                    MessageBox.Show("Значения введены некорректно", "Предупреждение");
                }
            }
            else MessageBox.Show("Заполните все необходимые поля", "Предупреждение");
            //Если не заполнил, то предупреждаем и просим заполнить


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Кнопка рассчитать для второй страницы
            //Уменьшаемого остатка
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "") //Проверяем, заполнил ли пользователь поля
            {
                try //Обработчик ошибок
                {
                    Decreasing_balance = Decreasing_balance_method(Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToDouble(textBox3.Text));
                    Fill_dgv_from_dict(Decreasing_balance, dataGridView1);
                    //Построение диаграммы
                    this.ClientSize = new System.Drawing.Size(980, 629); //форма увеличивает размер
                    chart2.Series.Clear(); //Диаграмма чистит старые данные
                    int i = 0;//Индекс
                    foreach (KeyValuePair<int, List<double>> item in Decreasing_balance)
                    {
                        chart2.Series.Add("Series" + Convert.ToString(i)); //Добавляем элементы(столбцы)
                        chart2.Series[i].LegendText = item.Key.ToString() + " год";//даем названия, которые будут отображаться
                        chart2.Series[i].Points.AddXY(i, item.Value[1]); //Добавляем значения
                        chart2.Series[i]["PixelPointWidth"] = "120"; //Столбцы делаем потолще для красоты
                        chart2.Update(); //Обновить диаграмму
                        i++;//Увеличиваем индекс на 1
                    }
                }
                catch (Exception)
                {
                    //если с данными будет что-то не так
                    MessageBox.Show("Значения введены некорректно", "Предупреждение");
                }
            }
            else MessageBox.Show("Заполните все необходимые поля", "Предупреждение");
            //Если не заполнил, то предупреждаем и просим заполнить
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Кнопка рассчитать для третьей страницы
            //Суммы числа лет
            if (textBox7.Text != "" && textBox4.Text != "") //Проверяем, заполнил ли пользователь поля
            {
                try //Обработчик ошибок
                {
                    Sum_of_the_number_of_years = Sum_of_the_number_of_years_method(Convert.ToDouble(textBox7.Text), Convert.ToInt32(textBox4.Text));
                    Fill_dgv_from_dict(Sum_of_the_number_of_years, dataGridView3);
                    //Построение диаграммы
                    this.ClientSize = new System.Drawing.Size(980, 629); //форма увеличивает размер
                    chart3.Series.Clear(); //Диаграмма чистит старые данные
                    int i = 0;//Индекс
                    foreach (KeyValuePair<int, List<double>> item in Sum_of_the_number_of_years)
                    {
                        chart3.Series.Add("Series" + Convert.ToString(i)); //Добавляем элементы(столбцы)
                        chart3.Series[i].LegendText = item.Key.ToString() + " год";//даем названия, которые будут отображаться
                        chart3.Series[i].Points.AddXY(i, item.Value[1]); //Добавляем значения
                        chart3.Series[i]["PixelPointWidth"] = "120"; //Столбцы делаем потолще для красоты
                        chart3.Update(); //Обновить диаграмму
                        i++;//Увеличиваем индекс на 1
                    }
                }
                catch (Exception)
                {
                    //если с данными будет что-то не так
                    MessageBox.Show("Значения введены некорректно", "Предупреждение");
                }
            }
            else MessageBox.Show("Заполните все необходимые поля", "Предупреждение");
            //Если не заполнил, то предупреждаем и просим заполнить
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Ограничения на ввод в текстовое поле, разрешены:
            //Цифры + запятая + работающий BackSpace
            if ((Char.IsNumber(e.KeyChar) | (e.KeyChar == Convert.ToChar(",")) | e.KeyChar == '\b')) return;
            else
                e.Handled = true;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 1 && textBox6.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox6.Text = "";
            }    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox1.Text = "";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length == 1 && textBox7.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox7.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(Convert.ToDouble(textBox3.Text) >= 1 && Convert.ToDouble(textBox3.Text) <= 3))
                {//Если пользователь пишет ноль первым, программа его стирает + если значение вне диапазона допустимого
                    textBox3.Text = "";
                    MessageBox.Show("Ограничение: для коэффициента допустимы значения от 1 до 3-х", "Предупреждение");
                } 
            }
            catch (Exception)
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 1 && textBox2.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox2.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 1 && textBox5.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox5.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 1 && textBox4.Text == "0")
            {//Если пользователь пишет ноль первым, программа его стирает
                textBox4.Text = "";
            }
        }

        //private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ClientSize = new System.Drawing.Size(980, 304);
        //}
    }
}
