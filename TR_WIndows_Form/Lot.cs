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
    public partial class Lot : Form
    {
        DataSet ds = new DataSet();
        DataSet ds_CB = new DataSet();
        DataSet ds_CB1 = new DataSet();
        DataSet ds_Name = new DataSet();

        SqlDataAdapter adapter;
        SqlDataAdapter adapter_CB;
        SqlDataAdapter adapter_Name;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Work\Old\TR\TR_WIndows_Form\App_Data\Work_Test_Old.mdf;Integrated Security=True;Connect Timeout=30";
        string sql = "SELECT top 100 * FROM Lot order by Last_Tran_Date Desc";
        string sql_CB = "SELECT Max(Id) as Id,Name FROM Storage where Name IS not NULL group by Name";

        public Lot()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapter_Name = new SqlDataAdapter("SELECT Max(Id) as Id,Name FROM Product where Name IS not NULL group by Name", connection);
                adapter_Name.Fill(ds_Name);
                comboBox4.DataSource = ds_Name.Tables[0];
                comboBox4.ValueMember = "Id";
                comboBox4.DisplayMember = "Name";

                adapter_CB = new SqlDataAdapter(sql_CB, connection);
                adapter_CB.Fill(ds_CB);
                adapter_CB.Fill(ds_CB1);

                comboBox2.DataSource = ds_CB.Tables[0];
                comboBox2.ValueMember = "Id";
                comboBox2.DisplayMember = "Name";

                comboBox3.DataSource = ds_CB1.Tables[0]; //Разные элементы
                comboBox3.ValueMember = "Id";
                comboBox3.DisplayMember = "Name";

                adapter = new SqlDataAdapter(sql, connection);

                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["Id"].ReadOnly = true;
                dataGridView1.Columns["Code"].HeaderText = "Код";
                dataGridView1.Columns["Name"].HeaderText = "Наименование";
                dataGridView1.Columns["Create_Date"].HeaderText = "Дата создания";
                dataGridView1.Columns["Last_Tran_Date"].HeaderText = "Последняя транзакция";
                dataGridView1.Columns["End_Date"].HeaderText = "Дата доставки";
                dataGridView1.Columns["Status"].HeaderText = "Статус";
                dataGridView1.Columns["Storage_From"].HeaderText = "Пункт отправки";
                dataGridView1.Columns["Storage_To"].HeaderText = "Пункт доставки";

                dataGridView1.Columns["Name"].ReadOnly = true;
                dataGridView1.Columns["Code"].ReadOnly = true;
                dataGridView1.Columns["Create_Date"].ReadOnly = true;
                dataGridView1.Columns["Last_Tran_Date"].ReadOnly = true;
                dataGridView1.Columns["End_Date"].ReadOnly = true;
                dataGridView1.Columns["Status"].ReadOnly = true;
                dataGridView1.Columns["Storage_From"].ReadOnly = true;
                dataGridView1.Columns["Storage_To"].ReadOnly = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = comboBox4.Text.ToString();
            row["Status"] = comboBox1.Text.ToString();
            row["End_Date"] = dateTimePicker_EndDate.Value;
            row["Storage_From"] = comboBox2.Text.ToString();
            row["Storage_To"] = comboBox3.Text.ToString();

            ds.Tables[0].Rows.Add(row);
        }


        private void Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                adapter = new SqlDataAdapter(sql, conn);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("Save_Lot", conn);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@End_Date", SqlDbType.Date, 50, "End_Date"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50, "Status"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Storage_From", SqlDbType.NVarChar, 50, "Storage_From"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Storage_To", SqlDbType.NVarChar, 50, "Storage_To"));


                adapter.Update(ds);
            }
        }
    }
}
