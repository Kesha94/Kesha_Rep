using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TR_WIndows_Form
{
    public partial class Storage : Form
    {

        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Work\Old\TR\TR_WIndows_Form\App_Data\Work_Test_Old.mdf;Integrated Security=True;Connect Timeout=30";
        string sql = "SELECT * FROM Storage";


        public Storage()
        {

            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Name"].HeaderText = "Название";
                dataGridView1.Columns["Adress"].HeaderText = "Адрес";
                dataGridView1.Columns["Town"].HeaderText = "Город";
                dataGridView1.Columns["Type"].HeaderText = "Тип";
                dataGridView1.Columns["Id_Storage"].HeaderText = "ID помещения";
                dataGridView1.Columns["Phone_Num"].HeaderText = "Контакт";

            }

        }

        private void Product_Load(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                adapter = new SqlDataAdapter(sql, conn);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("Save_Storage", conn);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Adress", SqlDbType.NVarChar, 0, "Adress"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Town", SqlDbType.NVarChar, 50, "Town"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 0, "Type"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Id_Storage", SqlDbType.Int, 50, "Id_Storage"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Phone_Num", SqlDbType.Int, 0, "Phone_Num"));

                adapter.Update(ds);
            }
        }
    }
}
