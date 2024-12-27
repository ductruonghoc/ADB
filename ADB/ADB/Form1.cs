using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ADB
{
    public class DbContextService
    {
        private static QLNHAHANGEntities1 _context;

        // Use a singleton pattern to share the context
        public static QLNHAHANGEntities1 GetDbContext()
        {

            _context = new QLNHAHANGEntities1();

            return _context;
        }

        public class User
        {
            public int? ID { get; set; }
            public int? Role {  get; set; }
        }

        public class Staff
        {
            public string Name { get; set; }
            public byte? DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public byte? BranchID { get; set; }
            public string BranchName { get; set; }
        }

        public class Table
        {
            public string Name { get; set; }
            public int? ID { get; set; }
            public string Status { get; set; } = "Trong";
            public int? ServedTableID { get; set; } = null;
        }
    }


    public static class SharedResources
    {

        public static DbContextService.User CurrentUser { get; set; }

        // You can also have an instance of DbContext here if you want to share it
        private static QLNHAHANGEntities1 _context;

        public static QLNHAHANGEntities1 GetDbContext()
        {
            if (_context == null)
            {
                _context = DbContextService.GetDbContext();  // Use the previously defined service
            }

            return _context;
        }
    }

    public partial class Form1 : Form
    {
        public delegate void Direction(Panel panel);
        // Variable to store the active panel
        private Panel activePanel;
        // Function to set the active panel (this can also hide the current active panel if needed)
        public void SetActivePanel(Panel panel)
        {
            if (activePanel != null)
            {
                // You can hide the previous panel or keep it visible as per the requirement
                activePanel.Visible = false;  // Hide the previous active panel
                                              //remvoe old panel from control
                this.Controls.Remove(activePanel);
                //release resources
                activePanel.Dispose();
            }

            activePanel = panel;
            activePanel.Visible = true;  // Make the new panel visible
            this.Controls.Add(activePanel);
        }

        public Form1()
        {
            InitializeComponent();
            SetActivePanel(new StaffPanel(SetActivePanel));
        }
    }

    public class LoginPanel : Panel
    {

        private TextBox ID_Input;
        private TextBox Password_Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Button Submit;
        private System.Windows.Forms.Label Warning;
        private QLNHAHANGEntities1 context;
        // Define a delegate type matching the method signature you want to store

        // Declare a delegate variable to store the method reference from ClassA
        private Form1.Direction _direction;
        public LoginPanel(Form1.Direction direction)
        {
            // Create a new panel
            Name = "Đăng nhập";
            Dock = DockStyle.Fill;
            Location = new System.Drawing.Point(0, 0); // Set the location where the panel will appear
            BackColor = System.Drawing.Color.LightBlue;  // Change the background color for better visibility
            //db context create
            context = SharedResources.GetDbContext();
            //set direction
            _direction = direction;
            //visualize component
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // 
            // ID_Input
            // 

            // 
            ID_Input = new TextBox
            {
                Location = new System.Drawing.Point(173, 155),
                Name = "ID_Input",
                Size = new System.Drawing.Size(406, 22),
                TabIndex = 0
            };
            ID_Input.TextChanged += new System.EventHandler(idInput_TextChanged);

            Password_Input = new TextBox
            {
                Location = new System.Drawing.Point(173, 223),
                Name = "Password_Input",
                Size = new System.Drawing.Size(406, 22),
                TabIndex = 1
            };
            // Password_Input
            // 

            Password_Input.TextChanged += new System.EventHandler(this.passwordInput_TextChanged);
            // 

            // label1
            // 

            // 
            label1 = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(168, 59),
                Name = "label1",
                Size = new System.Drawing.Size(117, 25),
                TabIndex = 2,
                Text = "Đăng nhập",
            };
            label1.Click += new System.EventHandler(this.label1_Click);
            // label2
            // 
            label2 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(173, 133),
                Name = "label2",
                Size = new System.Drawing.Size(20, 16),
                TabIndex = 3,
                Text = "ID",
            };
            label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            label3 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(173, 204),
                Name = "label3",
                Size = new System.Drawing.Size(61, 16),
                TabIndex = 4,
                Text = "Mật khẩu"
            };
            label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Submit
            // 
            Submit = new Button
            {
                Location = new System.Drawing.Point(317, 251),
                Name = "Submit",
                Size = new System.Drawing.Size(122, 38),
                TabIndex = 5,
                Text = "Đăng nhập",
                UseVisualStyleBackColor = true

            };
            Submit.Click += new System.EventHandler(this.button1_Click);
            // 
            // Warning
            // 
            Warning = new System.Windows.Forms.Label
            {
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(173, 98),
                Name = "Warning",
                Size = new System.Drawing.Size(193, 16),
                TabIndex = 6,
                Text = "ID hoặc mật khẩu không hợp lệ!",
                Visible = false

            };
            Warning.Click += new System.EventHandler(this.label4_Click);

            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(label3);
            this.Controls.Add(ID_Input);
            this.Controls.Add(Submit);
            this.Controls.Add(Warning);
            this.Controls.Add(Password_Input);
        }
        private void idInput_TextChanged(object sender, EventArgs e)
        {
            bool emptyField = string.IsNullOrEmpty(ID_Input.Text);

            if (emptyField == false)
            {
                Warning.Visible = false;
            }
        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            bool emptyField = string.IsNullOrEmpty(ID_Input.Text);

            if (emptyField == false)
            {
                Warning.Visible = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string id = ID_Input.Text;
            string password = Password_Input.Text;
            string role = id.Substring(0, 1);
            int t1 = 0, t2 = 0;
            id  = id.Substring(1);
            bool injection = id.Contains('-');
            bool invalid =
                string.IsNullOrEmpty(password) ||
                !int.TryParse(role, out t1) ||
                !int.TryParse(id, out t2) ||
                id.Length < 1;
            
            //Invalid
            if (injection || invalid)
            {
                Warning.Visible = true;
            }
            else {
                string query = $"exec str_Login {t1}, {t2}, '{password}'";
                var temp1 = context.Database.SqlQuery<DbContextService.User>(query);
                var temp = temp1.FirstOrDefault();
                //context.str_Login(t1, t2, password).FirstOrDefault();
                SharedResources.CurrentUser = temp;
                //Has value
                if (SharedResources.CurrentUser != null && 
                    SharedResources.CurrentUser.ID.HasValue)
                {
                    SharedResources.CurrentUser.Role = t1;
                    if(SharedResources.CurrentUser.Role.HasValue &&
                        SharedResources.CurrentUser.Role == 1)
                        _direction(new StaffPanel(_direction));
                }
            } 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

    public class StaffPanel : Panel
    {
        public DbContextService.Staff CurrentStaff { get; set; }
        private QLNHAHANGEntities1 context;
        // Define a delegate type matching the method signature you want to store

        // Declare a delegate variable to store the method reference from ClassA
        private Form1.Direction _direction;
        private HeaderPanel headerPanel;
        private TablePanel tablePanel;
        public StaffPanel(Form1.Direction direction)
        {
            //Test
            SharedResources.CurrentUser = new DbContextService.User
            {
                ID = 105,
                Role = 1
            };
            // Create a new panel
            Name = "Nhân viên";
            Size = new System.Drawing.Size(1280, 800);
            Location = new System.Drawing.Point(0, 0); // Set the location where the panel will appear
            BackColor = System.Drawing.Color.Aqua;  // Change the background color for better visibility
            //db context create
            context = SharedResources.GetDbContext();
            //set direction
            _direction = direction;
            //Fetch data
            StaffFetch();
            //visualize component
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            headerPanel = new HeaderPanel(this);
            tablePanel = new TablePanel(this);
            this.Controls.Add(headerPanel);
            this.Controls.Add(tablePanel);
        }

        public void StaffFetch()
        {
            string query = $"str_Staff {SharedResources.CurrentUser.ID}";
            var temp = context.Database.SqlQuery<DbContextService.Staff>(query);
            var staff = temp.FirstOrDefault();
            staff.BranchName.TrimEnd();
            staff.Name.TrimEnd();
            staff.DepartmentName.TrimEnd();

            CurrentStaff = staff;
        }
        public class HeaderPanel : Panel
        {
            private StaffPanel _outside;
            public HeaderPanel(StaffPanel outside)
            {
                _outside = outside;
                Name = "Header";
                BackColor = System.Drawing.Color.White;
                Size = new System.Drawing.Size(1280 - 120, 100);
                Location = new System.Drawing.Point(120, 0); // Set the location where the panel will appear
                
                InitalizeComponent();
            }
            private void InitalizeComponent()
            {
                DbContextService.Staff staff = _outside.CurrentStaff;
                // 
                // label1
                // 
                System.Windows.Forms.Label label1 = new System.Windows.Forms.Label {
                    AutoSize = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(155, 45),
                    Name = "label1",
                    TabIndex = 1,
                    Text = $"Bộ phận {staff.DepartmentName}\nChi nhánh {staff.BranchName}"
                };
                // 
                // label2
                // 
                System.Windows.Forms.Label label2 = new System.Windows.Forms.Label {
                    AutoSize = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(155, 15),
                    Name = "label2",
                    TabIndex = 2,
                    Text = $"Xin chào {staff.Name}"
                };

                this.Controls.Add(label1);
                this.Controls.Add(label2);
            }
        }
        public class TablePanel : Panel
        {
            private StaffPanel _outside;
            private List<DbContextService.Table> tables;
            private int? currentTableIndex = null;

            public TablePanel(StaffPanel outside)
            {
                AutoScroll = true;
                _outside = outside;
                Name = "Tables";
                BackColor = System.Drawing.Color.White;
                Size = new System.Drawing.Size(1160, 800 - _outside.headerPanel.Height);
                Location = new System.Drawing.Point(120, _outside.headerPanel.Height); // Set the location where the panel will appear
                TableFetch();
                InitalizeComponent();
            }

            public void TableFetch()
            {
                int threshold = 20;
                string query = $"exec str_using_Tables {_outside.CurrentStaff.BranchID}";
                var context = _outside.context;
                var temp = context.Database.
                    SqlQuery<DbContextService.Table>(query).
                    ToList();
                var result = new List<DbContextService.Table>();
                result.AddRange(temp);
                threshold -= result.Count;
                query = $"exec str_Tables {_outside.CurrentStaff.BranchID}, {threshold}";
                temp = context.Database.SqlQuery<DbContextService.Table>(query).ToList();
                result.AddRange(temp);
                tables = result;
            }

            private void InitalizeComponent()
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label {
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(30, 0),
                    Name = "label1",
                    AutoSize = true,
                    TabIndex = 1,
                    Text = "Danh sách bàn"
                };

                this.Controls.Add(label);

                int i = 0;
                foreach (var item in tables)
                {
                    // 
                    // button1
                    // 
                    Button button = new Button
                    {
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0))),
                        Location = new System.Drawing.Point(15 + i % 4 * (140), 30 + i / 4 * (100)),
                        Tag = item,
                        Size = new System.Drawing.Size(130, 95),
                        Text = item.Name,
                        BackColor = System.Drawing.Color.Red
                    };
                    if (item.Status == "Trong")
                    { 
                        button.BackColor = System.Drawing.Color.White;
                        button.Click += ThemBanAn;
                    }
                    this.Controls.Add(button);
                    i++;
                }
            }
            private void ThemBanAn(object sender, EventArgs e)
            {
                DbContextService.Table table = (DbContextService.Table)((Button)sender).Tag;

                string query = $"exec str_serveNewTable {table.ID}";
                var context = _outside.context;
                var temp = context.Database.
                    SqlQuery<DbContextService.Table>(query).
                    FirstOrDefault();

                if (temp != null && temp.ServedTableID.HasValue)
                {
                    table.Status = "DangDung";
                    table.ServedTableID = temp.ServedTableID;
                    DbContextService.Table servedTable = tables.Find(x => x.ID == table.ID);
                    servedTable.Status = table.Status;
                    servedTable.ServedTableID = table.ServedTableID;
                    
                    ((Button)sender).Click -= ThemBanAn;
                    ((Button)sender).BackColor = System.Drawing.Color.Red;
                    ((Button)sender).Tag = servedTable;
                }
            }
        }
    }
}
