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
    public partial class NewContract : Form
    {
        int roomId;
        PrnContext context = new PrnContext();
        public NewContract()
        {
            InitializeComponent();
        }
        public NewContract(string text):this()
        {
            roomId = int.Parse(text);
            textBox1.Text = roomId.ToString();
            textBox1.Enabled = false;
        }

        private void NewContract_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer c = new Customer
            {
                Name = textBox2.Text,
                Address = textBox3.Text,
                Phone = textBox4.Text,
            };
            context.Customers.Add(c);
            context.SaveChanges();
            Contract ct = new Contract
            {
                RoomId = roomId,
                CustomerId = c.Id,
                Start = dateTimePicker1.Value,
                End = dateTimePicker2.Value,
                Status = 1
            };
            context.Contracts.Add(ct);
            context.SaveChanges();

            Room r = context.Rooms.SingleOrDefault(r => r.Id == roomId);
            r.Status = 1;
            context.Rooms.Update(r);
            context.SaveChanges();
        }
    }
}
