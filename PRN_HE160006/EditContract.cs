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

namespace PRN_HE160006
{
    public partial class EditContract : Form
    {
        DAO dao = new DAO();
        PrnContext context = new PrnContext();
        int contractId;
        public EditContract()
        {
            InitializeComponent();
        }

        public EditContract(string text): this()
        {
            contractId = int.Parse(text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EditContract_Load(object sender, EventArgs e)
        {
            Contract contract = context.Contracts.SingleOrDefault(x => x.Id == contractId);
            Customer customer = context.Customers.SingleOrDefault(c => c.Id == contract.CustomerId);
            textBox1.Text = contract.Id.ToString();
            textBox2.Text = contract.RoomId.ToString();
            textBox3.Text = contract.CustomerId.ToString();
            dateTimePicker1.Value = contract.Start;
            dateTimePicker2.Value = contract.End;
            if (contract.Status == 1)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            textBox7.Text = customer.Name.ToString();
            textBox8.Text = customer.Address.ToString();
            textBox9.Text = customer.Phone.ToString();
            if (contract.Status == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contract contract = context.Contracts.SingleOrDefault(x => x.Id == contractId);
            Customer customer = context.Customers.SingleOrDefault(c => c.Id == contract.CustomerId);
            Room room = context.Rooms.SingleOrDefault(r => r.Id == contract.RoomId);

            contract.Start = dateTimePicker1.Value;
            contract.End = dateTimePicker2.Value;
            contract.Status = (radioButton1.Checked? 1 : 0);
            if(contract.Status == 0)
            {
                contract.End = DateTime.Now;
                room.Status = 0;
            }
            context.Contracts.Update(contract);
            context.SaveChanges();



            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewContract viewContract = new ViewContract(textBox2.Text);
            viewContract.Show();
        }
    }
}
