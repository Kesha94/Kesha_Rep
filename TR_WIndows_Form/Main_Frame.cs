using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TR_WIndows_Form
{
    public partial class Main_Frame : Form
    {
        DataSet ds = new DataSet();
        DataSet ds_all = new DataSet();
        DataSet ds_name = new DataSet();

        SqlDataAdapter adapter;
        SqlDataAdapter adapter_all;
        SqlDataAdapter adapter_Name;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Work\Old\TR\TR_WIndows_Form\App_Data\Work_Test_Old.mdf;Integrated Security=True;Connect Timeout=30";
        

        public Main_Frame()
        {
            InitializeComponent();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapter_Name = new SqlDataAdapter("Select max(Id) as Id,Name from Product where Name is not null group by Name", connection);
                adapter_Name.Fill(ds_name);

                comboBox1.DataSource = ds_name.Tables[0];
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Name";

                connection.Close();
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Верхняя панель
        private void аптекиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var storage = new Product();
            storage.Show();
        }

        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var product = new Storage();
            product.Show();
        }

        private void партияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Lot = new Lot();
            Lot.Show();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ds.Clear();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[i]); //Чистка таблицы
            }

            string sql = "SELECT * FROM Goods_In_Stock where Name = " + "'" + comboBox1.Text.ToString() + "'" + " and Storage= '" + textBox1.Text + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapter = new SqlDataAdapter(sql, connection);
                adapter.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["Id"].ReadOnly = true;

                dataGridView1.Columns["Name"].HeaderText = "Наименование";
                dataGridView1.Columns["Code"].HeaderText = "Код";
                dataGridView1.Columns["Storage"].HeaderText = "Помещение";
                dataGridView1.Columns["Type"].HeaderText = "Тип помещения";
                dataGridView1.Columns["Qty_In_Stock"].HeaderText = "В наличии";
                dataGridView1.Columns["Expected"].HeaderText = "Ожидается к поступлению";

                dataGridView1.Columns["Name"].ReadOnly = true;
                dataGridView1.Columns["Code"].ReadOnly = true;
                dataGridView1.Columns["Storage"].ReadOnly = true;
                dataGridView1.Columns["Type"].ReadOnly = true;
                dataGridView1.Columns["Qty_In_Stock"].ReadOnly = true;
                dataGridView1.Columns["Expected"].ReadOnly = true;

                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds_all.Clear();

            for (int z = 0; z < dataGridView1.Rows.Count; z++)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[z]); //Чистка таблицы
            }
            string sql_all = "SELECT * FROM Goods_In_Stock where Name = " + "'" + comboBox1.Text.ToString()+"'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapter_all = new SqlDataAdapter(sql_all, connection);
                adapter_all.Fill(ds_all);

                dataGridView1.DataSource = ds_all.Tables[0];
                dataGridView1.Columns["Id"].ReadOnly = true;

                dataGridView1.Columns["Name"].HeaderText = "Наименование";
                dataGridView1.Columns["Code"].HeaderText = "Код";
                dataGridView1.Columns["Storage"].HeaderText = "Помещение";
                dataGridView1.Columns["Type"].HeaderText = "Тип помещения";
                dataGridView1.Columns["Qty_In_Stock"].HeaderText = "В наличии";
                dataGridView1.Columns["Expected"].HeaderText = "Ожидается к поступлению";

                dataGridView1.Columns["Name"].ReadOnly = true;
                dataGridView1.Columns["Code"].ReadOnly = true;
                dataGridView1.Columns["Storage"].ReadOnly = true;
                dataGridView1.Columns["Type"].ReadOnly = true;
                dataGridView1.Columns["Qty_In_Stock"].ReadOnly = true;
                dataGridView1.Columns["Expected"].ReadOnly = true;

                connection.Close();

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
