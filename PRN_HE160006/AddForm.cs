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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            List<string> status = new List<string> { 
                "Trong",
                "Dang thue"
            };
            comboBox1.DataSource= status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrnContext context = new PrnContext();
            Room room = new Room
            {
                Name = textBox1.Text.ToString(),
                Price = (decimal?)double.Parse(textBox4.Text.ToString()),
                Position = textBox3.Text.ToString(),
                Status = (comboBox1.Text == "Trong") ? 0 : 1,
                Description = textBox2.Text.ToString(),
            };
            context.Rooms.Add(room);
            context.SaveChanges();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
