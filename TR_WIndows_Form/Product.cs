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
    public partial class Product : Form
    {

        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Work\Old\TR\TR_WIndows_Form\App_Data\Work_Test_Old.mdf;Integrated Security=True;Connect Timeout=30";
        string sql = "SELECT * FROM Product";

        public Product()
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
                dataGridView1.Columns["Name"].HeaderText = "Наименование";
                dataGridView1.Columns["Type"].HeaderText = "Тип";
                dataGridView1.Columns["Code"].HeaderText = "Код";
                dataGridView1.Columns["Qty_Storage"].HeaderText = "Кол-во на складе";
                dataGridView1.Columns["Qty_In_Transit"].HeaderText = "Кол-во в транзите";
                dataGridView1.Columns["Exp_Date"].HeaderText = "Срок годности";

            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();

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
                adapter.InsertCommand = new SqlCommand("Save_Product", conn);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, "Name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar, 0, "Type"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 50, "Code"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Qty_Storage", SqlDbType.Int, 0, "Qty_Storage"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Qty_In_Transit", SqlDbType.Int, 50, "Qty_In_Transit"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Exp_Date", SqlDbType.Date, 0, "Exp_Date"));

                adapter.Update(ds);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
