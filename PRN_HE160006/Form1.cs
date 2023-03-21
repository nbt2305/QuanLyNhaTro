using PRN_HE160006.DataAccess;
using System.Windows.Forms;

namespace PRN_HE160006
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void LoadFormRoom()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Price";
            dataGridView1.Columns[3].Name = "Position";
            dataGridView1.Columns[4].Name = "Status";
            dataGridView1.Columns[5].Name = "Descriprion";

            List<string> searchByStatus = new List<string>()
            {
                "Tat ca",
                "Trong",
                "Dang thue"
            };
            comboBox1.DataSource = searchByStatus;

            DAO dao = new DAO();
            var rooms = dao.getListRooms(); 
            foreach (var room in rooms)
            {
                var status = room.Status == 0 ? "Trong" : "Dang thue";
                dataGridView1.Rows.Add(room.Id, room.Name, room.Price, room.Position, status, room.Description);
            }
        }

        public void LoadFormSearchRoom()
        {
            dataGridView1.Rows.Clear();
            var nameSearch = textBox6.Text;
            var statusSearch = comboBox1.Text;
            List<Room> rooms = new List<Room>();
            DAO dao = new DAO();
            rooms = dao.getListSearchRooms(nameSearch, statusSearch);
            foreach (var room in rooms)
            {
                var status = room.Status == 0 ? "Trong" : "Dang thue";
                dataGridView1.Rows.Add(room.Id, room.Name, room.Price, room.Position, status, room.Description);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Price";
            dataGridView1.Columns[3].Name = "Position";
            dataGridView1.Columns[4].Name = "Status";
            dataGridView1.Columns[5].Name = "Descriprion";

            List<string> searchByStatus = new List<string>()
            {
                "Tat ca",
                "Trong",
                "Dang thue"
            };
            comboBox1.DataSource = searchByStatus;
            button5.Hide();
            button6.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrnContext context= new PrnContext();
            DAO dao = new DAO();
            var rooms = dao.getListRooms();
            int? index = dataGridView1.CurrentCell.RowIndex;
            string? id = dataGridView1.Rows[(int)index].Cells[0].Value.ToString();
            Room room = context.Rooms.SingleOrDefault(r => r.Id == int.Parse(id));
            textBox1.Text = room.Id.ToString();
            textBox2.Text = room.Name.ToString();
            textBox3.Text = room.Price.ToString();
            textBox4.Text = room.Position.ToString();
            textBox5.Text = room.Description.ToString();
            radioButton1.Checked = room.Status == 0;
            radioButton2.Checked = room.Status == 1;


            textBox1.Enabled = false;
            if (room.Status == 0)
            {
                button6.Show(); 
                button5.Hide();
            }
            else
            {
                button6.Hide();
                button5.Show();
            }
        }

        // Button Add
        private void button1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            LoadFormSearchRoom();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFormSearchRoom();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrnContext context = new PrnContext();
            var id = int.Parse(textBox1.Text);
            Room room = context.Rooms.SingleOrDefault(r => r.Id == id);
            DialogResult dr = MessageBox.Show("Do you want to delete this room ?", "Delete room", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
                LoadFormSearchRoom();
            }
            else
            {
                LoadFormSearchRoom();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PrnContext context = new PrnContext();
                var id = int.Parse(textBox1.Text);
                Room room = context.Rooms.SingleOrDefault(r => r.Id == id);
                room.Name = textBox2.Text.ToString();
                room.Price = (decimal?)double.Parse(textBox3.Text.ToString());
                room.Position = textBox4.Text.ToString();
                room.Description = textBox5.Text.ToString();
                room.Status = radioButton1.Checked ? 0 : 1;

                DialogResult dr = MessageBox.Show("Do you want to edit this room ?", "Delete room", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    context.Rooms.Update(room);
                    context.SaveChanges();
                    LoadFormSearchRoom();
                }
                else
                {
                    LoadFormSearchRoom();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Price";
            dataGridView1.Columns[3].Name = "Position";
            dataGridView1.Columns[4].Name = "Status";
            dataGridView1.Columns[5].Name = "Descriprion";

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            List<string> searchByStatus = new List<string>()
            {
                "Tat ca",
                "Trong",
                "Dang thue"
            };
            comboBox1.DataSource = searchByStatus;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewContract viewContract = new ViewContract(textBox1.Text);
            viewContract.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NewContract newContract = new NewContract(textBox1.Text);
            newContract.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}