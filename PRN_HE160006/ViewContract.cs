using PRN_HE160006.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRN_HE160006
{
    public partial class ViewContract : Form
    {
        int roomId;
        DAO dao = new DAO();
        PrnContext context = new PrnContext();
        public ViewContract()
        {
            InitializeComponent();
        }
        public ViewContract(string text): this()
        {
            roomId = int.Parse(text);
            textBox1.Text = roomId.ToString();
        }


        private void ViewContract_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "RoomId";
            dataGridView1.Columns[2].Name = "CustomerId";
            dataGridView1.Columns[3].Name = "Start Date";
            dataGridView1.Columns[4].Name = "End Date";
            dataGridView1.Columns[5].Name = "Status";

            Contract currentContract = context.Contracts.SingleOrDefault(x => (x.Id == roomId));

            textBox2.Text = currentContract.CustomerId.ToString();
            dateTimePicker1.Value = currentContract.Start;
            dateTimePicker2.Value = currentContract.End;
            textBox5.Text = currentContract.Status == 0 ? "Da tra phong" : "Dang thue";
            //textBox6.Text = currentContract.Id.ToString();
            textBox6.Hide();

            var contracts = dao.getListContractsByRoomId(int.Parse(textBox1.Text));
            foreach ( var contract in contracts )
            {
                string status = (contract.Status == 0) ? "Da tra phong" : "Dang thue";
                dataGridView1.Rows.Add(contract.Id ,contract.RoomId, contract.CustomerId, contract.Start, contract.End, status);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int? index = dataGridView1.CurrentCell.RowIndex;
            string? id = dataGridView1.Rows[(int)index].Cells[0].Value.ToString();
            Contract contract = context.Contracts.SingleOrDefault(c => c.Id == int.Parse(id));

            textBox2.Text = contract.CustomerId.ToString();
            dateTimePicker1.Value = contract.Start;
            dateTimePicker2.Value = contract.End;
            textBox5.Text = contract.Status == 0 ? "Da tra phong" : "Dang thue";
            textBox6.Text = contract.Id.ToString();
            if(contract.Status == 0)
            {
                button2.Text = "View";
            }
            else
            {
                button2.Text = "Edit";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditContract editContract = new EditContract(textBox6.Text);
            editContract.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "RoomId";
            dataGridView1.Columns[2].Name = "CustomerId";
            dataGridView1.Columns[3].Name = "Start Date";
            dataGridView1.Columns[4].Name = "End Date";
            dataGridView1.Columns[5].Name = "Status";

            textBox6.Hide();

            var contracts = dao.getListContractsByRoomId(int.Parse(textBox1.Text));
            foreach (var contract in contracts)
            {
                string status = (contract.Status == 0) ? "Da tra phong" : "Dang thue";
                dataGridView1.Rows.Add(contract.Id, contract.RoomId, contract.CustomerId, contract.Start, contract.End, status);
            }
        }
    }
}
