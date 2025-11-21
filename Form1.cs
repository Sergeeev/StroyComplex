
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1

{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=127.0.0.1;Database=СтройКомплекс;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";

                if (comboBox1.SelectedIndex == 0)
                    query = "SELECT Проекты.НазваниеПроекта, Задачи.НазваниеЗадачи, Сотрудники.Имя, Сотрудники.Фамилия FROM Задачи JOIN Проекты ON Задачи.ПроектID = Проекты.ПроектID JOIN Сотрудники ON Задачи.Назначена = Сотрудники.СотрудникID";
                else if (comboBox1.SelectedIndex == 1)
                    query = "SELECT Задачи.НазваниеЗадачи, Задачи.Статус, Сотрудники.Имя, Сотрудники.Фамилия FROM Задачи JOIN Сотрудники ON Задачи.Назначена = Сотрудники.СотрудникID WHERE Задачи.ПроектID = 1";
                else if (comboBox1.SelectedIndex == 2)
                    query = "SELECT Сотрудники.Имя, Сотрудники.Фамилия, COUNT(Задачи.ЗадачаID) AS КоличествоЗадач FROM Сотрудники LEFT JOIN Задачи ON Сотрудники.СотрудникID = Задачи.Назначена GROUP BY Сотрудники.СотрудникID";
                else if (comboBox1.SelectedIndex == 3)
                    query = "SELECT * FROM Материалы WHERE ЦенаЗаЕдиницу > 50";
                else if (comboBox1.SelectedIndex == 4)
                    query = "SELECT Задачи.НазваниеЗадачи, Проекты.НазваниеПроекта FROM Задачи JOIN Проекты ON Задачи.ПроектID = Проекты.ПроектID WHERE Задачи.Статус = 'в работе'";
                else if (comboBox1.SelectedIndex == 5)
                    query = "SELECT Проекты.НазваниеПроекта, Задачи.НазваниеЗадачи FROM Проекты LEFT JOIN Задачи ON Проекты.ПроектID = Задачи.ПроектID";
                else if (comboBox1.SelectedIndex == 6)
                    query = "SELECT Сотрудники.Имя, Сотрудники.Фамилия FROM Сотрудники LEFT JOIN Задачи ON Сотрудники.СотрудникID = Задачи.Назначена WHERE Задачи.ЗадачаID IS NULL";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}