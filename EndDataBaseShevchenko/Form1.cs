using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EndDataBaseShevchenko
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\EndDataBaseShevchenko\EndDataBaseShevchenko\Database1.mdf;Integrated Security=True");
        public int Id;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//добавить запись
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                    !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                    !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                    !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    if (textBox6.Text == "A10" || textBox6.Text == "A11" || textBox6.Text == "A12" || textBox6.Text == "А10"
                        || textBox6.Text == "А11" || textBox6.Text == "А12" || textBox6.Text == "V33" || textBox6.Text == "V99")
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "INSERT INTO [Table1] (Name,Surname,Age,Weight,Type) VALUES (@Name ,@Surname,@Age,@Weight,@Type)";
                        cmd.Parameters.AddWithValue("Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("Surname", textBox3.Text);
                        cmd.Parameters.AddWithValue("Age", textBox4.Text);
                        cmd.Parameters.AddWithValue("Weight", textBox5.Text);
                        cmd.Parameters.AddWithValue("Type", textBox6.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        TableData();

                        MessageBox.Show("Запись успешно записана");
                    }
                    else
                    {
                        MessageBox.Show("Данного кода болезни не существует!");
                    }
                }

                else
                {
                    MessageBox.Show("Не все поля заполнены!");
                }
            }
            else
            {
                MessageBox.Show("Поле Id задаётся автоматически, вы не можете его добавить!");
            }
            //CleanTextBox();
        }

        public void TableData()//отрисовка таблицы1
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [Table1]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.AutoResizeColumns();

            con.Close();
        }

        public void TableData2()//отрисовка таблицы2
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [Table2]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;


            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TableData();
            TableData2();
            CleanTextBox();
        }

        private void button2_Click(object sender, EventArgs e)//удалить запись
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM [Table1] WHERE [Id] = @Id";
                cmd.Parameters.AddWithValue("Id", textBox1.Text);

                cmd.ExecuteNonQuery();
                con.Close();

                TableData();

                MessageBox.Show("Запись успешно удалена");
            }

            else
            {
                MessageBox.Show("Не заполнено поле Id!");
            }

            CleanTextBox();
        }

        private void button3_Click(object sender, EventArgs e)//корректирвать запись
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(textBox2.Text) ||
                    !string.IsNullOrEmpty(textBox3.Text) ||
                    !string.IsNullOrEmpty(textBox4.Text) ||
                    !string.IsNullOrEmpty(textBox5.Text) ||
                    !string.IsNullOrEmpty(textBox6.Text))
                {
                    if (textBox6.Text == "A10" || textBox6.Text == "A11" || textBox6.Text == "A12" || textBox6.Text == "А10"
                        || textBox6.Text == "А11" || textBox6.Text == "А12" || textBox6.Text == "V33" || textBox6.Text == "V99")
                    {
                        if (string.IsNullOrEmpty(textBox2.Text))
                        {
                            textBox2.Text = "—";
                        }
                        if (string.IsNullOrEmpty(textBox3.Text))
                        {
                            textBox3.Text = "—";
                        }
                        if (string.IsNullOrEmpty(textBox4.Text))
                        {
                            textBox4.Text = "—";
                        }
                        if (string.IsNullOrEmpty(textBox5.Text))
                        {
                            textBox5.Text = "—";
                        }
                        if (string.IsNullOrEmpty(textBox6.Text))
                        {
                            textBox6.Text = "—";
                        }
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE [Table1] SET [Name] = @Name ,[Surname] = @Surname,[Age] = @Age,[Weight] = @Weight,[Type] = @Type WHERE [Id] = @Id";

                        cmd.Parameters.AddWithValue("Id", textBox1.Text);
                        cmd.Parameters.AddWithValue("Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("Surname", textBox3.Text);
                        cmd.Parameters.AddWithValue("Age", textBox4.Text);
                        cmd.Parameters.AddWithValue("Weight", textBox5.Text);
                        cmd.Parameters.AddWithValue("Type", textBox6.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        TableData();

                        MessageBox.Show("База данных успешно обновлена");
                    }
                    else
                    {
                        MessageBox.Show("Данного кода болезни не существует!");
                    }
                }

                else
                {
                    MessageBox.Show("Все поля пусты!");
                }
            }

            else
            {
                MessageBox.Show("Не заполнено поле Id!");
            }
            CleanTextBox();
        }

        private void button4_Click(object sender, EventArgs e)//обновить
        {
            TableData();
            CleanTextBox();
        }

        private void button5_Click(object sender, EventArgs e)//поиск записи
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrWhiteSpace(textBox2.Text) &&
                    string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrWhiteSpace(textBox3.Text) &&
                    string.IsNullOrEmpty(textBox4.Text) && string.IsNullOrWhiteSpace(textBox4.Text) &&
                    string.IsNullOrEmpty(textBox5.Text) && string.IsNullOrWhiteSpace(textBox5.Text) &&
                    string.IsNullOrEmpty(textBox6.Text) && string.IsNullOrWhiteSpace(textBox6.Text))
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM [Table1] WHERE [Id] = @Id";
                        
                        cmd.Parameters.AddWithValue("Id", textBox1.Text);
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        CleanTextBox();
                        con.Close();
                        CleanTextBox();
                    }
                    else
                    {
                        MessageBox.Show("Поиск осуществляется только по Id!");
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Не заполнено поле Id!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        public void CleanTextBox()//очистить
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)//Ввод только цифр
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//выбор строки с последующим выводом информации
        {
            try
            {
                con.Open();
                Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [Table1] WHERE Id =" + Id + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    textBox1.Text = dr["Id"].ToString();
                    textBox2.Text = dr["Name"].ToString();
                    textBox3.Text = dr["Surname"].ToString();
                    textBox4.Text = dr["Age"].ToString();
                    textBox5.Text = dr["Weight"].ToString();
                    textBox6.Text = dr["Type"].ToString();
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)//очистить поля
        {
            CleanTextBox();
        }

        private void button7_Click(object sender, EventArgs e)//формирование рецепта
        {
            if(textBox7.Text == "A10" || textBox7.Text == "А10")
            {
                label7.Visible = true;
                label7.Text = "   В течении 10-ти дней принимать по 3\n" +
                              "таблетки анальгина и парацетамола в сутки";
            }
            else if (textBox7.Text == "A11" || textBox7.Text == "А11")
            {
                label7.Visible = true;
                label7.Text = "В течении 20-ти дней принимать по 2\n" +
                              "  таблетки парацетамола в сутки";
            }
            else if (textBox7.Text == "A12" || textBox7.Text == "А12")
            {
                label7.Visible = true;
                label7.Text = "В течении 7-ми дней принимать по 3\n" +
                              "   таблетки ибупрофена в сутки";
            }
            else if (textBox7.Text == "V33")
            {
                label7.Visible = true;
                label7.Text = "В течении 60-ти дней принимать по 2\n" +
                              "   таблетки анальгетиков в сутки";
            }
            else if (textBox7.Text == "N99")
            {
                label7.Visible = true;
                label7.Text = "В течении 5-ти дней после операции принимать по 1\n" +
                              "        таблетке обезболивающего в сутки";
            }

            else if(string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Поле кода болезни пусто!");
            }

            else
            {
                MessageBox.Show("Данной болезни нет в базе данных!");
            }
        }

        private void button8_Click(object sender, EventArgs e)//очистить поля
        {
            label7.Text = "";
            textBox7.Clear();
        }

        private void button9_Click(object sender, EventArgs e)//генерация справки
        {
            label19.Text = "";

            if (!string.IsNullOrEmpty(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text))
            {
                if (textBox6.Text== "A10" || textBox6.Text == "А10")
                {
                    label9.Visible = true;
                    label9.Text = "Данная особа болеет гриппом, в течении\n" +
                                  "   10-ти дней она не работоспособна";
                    label7.Visible = true;
                    label7.Text = "   В течении 10-ти дней принимать по 3\n" +
                                  "таблетки анальгина и парацетамола в сутки";

                    information();
                }
                else if (textBox6.Text == "A11" || textBox6.Text == "А11")
                {
                    label9.Visible = true;
                    label9.Text = "Данная особа болеет ветрянкой, в течении\n" +
                                  "   20-ти дней она не работоспособна";
                    label7.Visible = true;
                    label7.Text = "В течении 20-ти дней принимать по 2\n" +
                                  "  таблетки парацетамола в сутки";

                    information();
                }
                else if (textBox6.Text == "A12" || textBox6.Text == "А12")
                {
                    label9.Visible = true;
                    label9.Text = "Данная особа испытывает сильную мигрень, в течении\n" +
                                  "         7-ми дней она не работоспособна";
                    label7.Visible = true;
                    label7.Text = "В течении 7-ми дней принимать по 3\n" +
                                  "   таблетки ибупрофена в сутки";

                    information();
                }
                else if (textBox6.Text == "V33")
                {
                    label9.Visible = true;
                    label9.Text = "Данная особа больна на остеохондроз, в течении\n" +
                                  "  60-ти дней она частично не работоспособна";
                    label7.Visible = true;
                    label7.Text = "В течении 60-ти дней принимать по 2\n" +
                                  "   таблетки анальгетиков в сутки";

                    information();
                }
                else if (textBox6.Text == "N99")
                {
                    label9.Visible = true;
                    label9.Text = "У данной особы был удалён аппендицит, в течении\n" +
                                  "        14-ти дней она не работоспособна";
                    label7.Visible = true;
                    label7.Text = "В течении 5-ти дней после операции принимать по 1\n" +
                                  "        таблетке обезболивающего в сутки";

                    information();
                }
                else
                {
                    MessageBox.Show("Данной болезни нет в базе данных!");
                }

                clock();

            }
            else
            {
                MessageBox.Show("Особа не выбрана!");
            }
            CleanTextBox();
        }


        public void information()//вывод информации в справке
        {
            label12.Text = textBox2.Text;
            label14.Text = textBox3.Text;
            label16.Text = textBox4.Text;
            label18.Text = textBox5.Text;
            label12.Visible = true;
            label14.Visible = true;
            label16.Visible = true;
            label18.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)//проверка входных данных
        {
            if (!string.IsNullOrEmpty(label9.Text) || !string.IsNullOrWhiteSpace(label9.Text) || label9.Text!="")
            {
                label19.Text = maskedTextBox1.Text;
                label19.Visible = true;
            }
            else
            {
                MessageBox.Show("В справке отсутствуют данные!");
            }
        }

        private void button11_Click(object sender, EventArgs e)//очистить поля
        {
            label12.Text = "";
            label14.Text = "";
            label16.Text = "";
            label18.Text = "";
            label19.Text = "";
            label9.Text = "";
            maskedTextBox1.Clear();
        }

        private void button12_Click(object sender, EventArgs e)//указать сегодняшнюю дату
        {
            if (!string.IsNullOrEmpty(label9.Text) || !string.IsNullOrWhiteSpace(label9.Text) || label9.Text != "")
            {
                clock();
            }
            else
            {
                MessageBox.Show("В справке отсутствуют данные!");
            }
        }

        public void clock()//дата
        {
            string s = DateTime.Now.ToString("dd" + "." + "MM" + "." + "yyyy");
            label19.Text = s;
            label19.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)//очистить рецепт и справку
        {
            label12.Text = "";
            label14.Text = "";
            label16.Text = "";
            label18.Text = "";
            label19.Text = "";
            label9.Text = "";
            maskedTextBox1.Clear();
            label7.Text = "";
            textBox7.Clear();
        }
    }
}