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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static ADB.StaffPanel;

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
            public int? Role { get; set; }
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
            public string Location { get; set; } = null;
            public string SeatTime { get; set; } = null;
            public int NumberOfPeople { get; set; } = 0;
        }

        public class Menu
        {
            public string Name { get; set; }
            public int? ID { get; set; }
        }

        public class Menu_Category
        {
            public string Name { get; set; }
            public int? ID { get; set; }
            public int? MenuID { get; set; }
        }

        public class Food
        {
            public string Name { get; set; }
            public int? ID { get; set; }
            public Double? Price { get; set; } = 0;
            public int Quantity { get; set; } = 1;
            public int? CategoryID { get; set; }
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

        private System.Windows.Forms.TextBox ID_Input;
        private System.Windows.Forms.TextBox Password_Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Submit;
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
            ID_Input = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(173, 155),
                Name = "ID_Input",
                Size = new System.Drawing.Size(406, 22),
                TabIndex = 0
            };
            ID_Input.TextChanged += new System.EventHandler(idInput_TextChanged);

            Password_Input = new System.Windows.Forms.TextBox
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
            Submit = new System.Windows.Forms.Button
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
        public QLNHAHANGEntities1 context;
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

    }
    public class TablePanel : Panel
    {
        public StaffPanel _outside;
        public List<DbContextService.Table> tables { get; set; }
        public int? currentTableIndex { get; set; } = 0;
        public DbContext context;
        public OrderPanel orderPanel;
        public TableListPanel tableListPanel;

        public TablePanel(StaffPanel outside)
        {
            _outside = outside;
            context = _outside.context;
            Name = "Tables";
            BackColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(1160, 700);
            Location = new System.Drawing.Point(120, 100); // Set the location where the panel will appear
            TableFetch();
            InitalizeComponent();
        }

        public void TableFetch()
        {
            string query = $"exec str_Tables {_outside.CurrentStaff.BranchID}";
            var context = _outside.context;
            var temp = context.Database.
                SqlQuery<DbContextService.Table>(query).
                ToList();
            var result = new List<DbContextService.Table>();
            result.AddRange(temp);

            //update status using
            query = $"exec str_using_Tables {_outside.CurrentStaff.BranchID}";
            temp = context.Database.
                SqlQuery<DbContextService.Table>(query).
                ToList();
            foreach (var item in temp)
            {
                var table = result.FirstOrDefault(x => x.ID == item.ID);
                if (table != null)
                {
                    table.Status = item.Status;
                    table.ServedTableID = item.ServedTableID;
                    table.SeatTime = item.SeatTime;
                }
            }

            //update tables
            tables = result;
        }

        private void InitalizeComponent()
        {
            orderPanel = new OrderPanel(this);
            tableListPanel = new TableListPanel(this);
            this.Controls.Add(orderPanel);
            this.Controls.Add(tableListPanel);
        }

        public void SyncTables()
        {
            orderPanel.SyncTable();
            orderPanel.UpdateLayout();
        }

        public void SyncCurrentTable(int index)
        {
            currentTableIndex = index;
            orderPanel.SyncTable();
            orderPanel.UpdateLayout();
        }
    }

    public class OrderPanel : Panel
    {
        public TablePanel _outside;
        private System.Windows.Forms.Label 
            TableNameLabel,
            TableLocationLabel,
            TableSeatTimeLabel;
        public DbContextService.Table currentTable;
        private List<DbContextService.Menu> menus;
        private List<DbContextService.Food> foods;
        private List<DbContextService.Menu_Category> categories;
        private int currentMenuIndex = 0;
            private int? currentCategoryIndex = 0;
        private List<DbContextService.Menu_Category> categoriesByMenu;
        private List<DbContextService.Food> foodByCategory;
        private System.Windows.Forms.ComboBox menuBox, categoryBox, foodBox;
        public List<DbContextService.Food> orderedFood { get; set; } = new List<DbContextService.Food>();
        private TableFoodPanel tableFoodPanel;
           
        
        public OrderPanel(TablePanel outside)
        {
            _outside = outside;

            
            Name = "Order";
            BackColor = System.Drawing.Color.GhostWhite;
            Size = new System.Drawing.Size(500, _outside.Height);
            Location = new System.Drawing.Point(1260 - 500, 0); // Set the location where the panel will appear
            //Menus fetch
            MenuFetch();
            //Menu Category fetch
            CategoryFetch();
            //Foods fetch
            FoodFetch();
            SyncMenuCategory();
            InitalizeComponent();
            SyncTable();

            UpdateLabels();
            UpdateMenuCategoryBox();
            tableFoodPanel.DisplayByOrderedFood();
        }
        public void OrderedFoodFetch()
        {
            string query = $"select ma.ID as ID, bm.SoLuong as Quantity, km.TenMonAn as Name, ma.Gia as Price " +
                            $"from BANAN_MONAN bm " +
                            $"inner join MONAN ma on ma.ID = bm.MaMon " +
                            $"inner join KHOMONAN km on km.MaMonAn = ma.MaMonAn " +
                            $"where bm.MaBanAn = {currentTable.ServedTableID} ";
            var context = _outside.context;
            var temp = context.Database.
                SqlQuery<DbContextService.Food>(query).
                ToList();
            var result = new List<DbContextService.Food>();
            result.AddRange(temp);
            orderedFood = result;
        }

        public void ServeFood (object Sender,EventArgs e)
        {
            var selectedFood = foodByCategory[foodBox.SelectedIndex];
            //Add food to served list
            orderedFood.Add(selectedFood);

            //Remove food from category food list
            foodByCategory.RemoveAt(foodBox.SelectedIndex);
            UpdateFoodBox();
            tableFoodPanel.SyncOrderedFood();
            tableFoodPanel.DisplayByOrderedFood();

            //Add food to db
            if(selectedFood.ID.HasValue)
                InsertTableFoodDB(selectedFood.ID.Value);
        }

        public void SyncTable()
        {
            currentTable = _outside.tables[_outside.currentTableIndex.Value];
            SyncTableFood();
        }

        public void SyncMenuCategory()
        {
            int menuID = currentMenuIndex;
            var temp = categories.FindAll(x => x.MenuID == menus[menuID].ID);
            categoriesByMenu = temp;
            if(categoriesByMenu.Count > 0)
            {
                currentCategoryIndex = 0;
            }   
            else
            {
                currentCategoryIndex = null;
            }    
            SyncFood();
        }

        public void SyncFood()
        {
            int? categoryID = currentCategoryIndex;
            if(categoryID == null)
            {
                foodByCategory = new List<DbContextService.Food>();
                return;
            }
            var temp = foods.FindAll(x => x.CategoryID == categoriesByMenu[categoryID.Value].ID);
            foodByCategory = temp;
        }

        public void UpdateMenuCategoryBox()
        {
            categoryBox.DataSource = null;
            categoryBox.DataSource = categoriesByMenu;
            categoryBox.DisplayMember = "Name";
            categoryBox.ValueMember = "ID";
            UpdateFoodBox();
        }

        public void UpdateFoodBox()
        {
            foodBox.DataSource = null;
            foodBox.DataSource = foodByCategory;
            foodBox.DisplayMember = "Name";
            foodBox.ValueMember = "ID";
        }

        private void MenuBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentMenuIndex = menuBox.SelectedIndex;
            SyncMenuCategory();
            UpdateMenuCategoryBox();
        }

        private void CategoryMenuBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoriesByMenu.Count > 0)
            {
                currentCategoryIndex = categoryBox.SelectedIndex;
            }
            else
            {
                currentCategoryIndex = null;
            }
            SyncFood();
            UpdateFoodBox();
        }
        private void MenuFetch()
        {
            string query = $"select MaTD as ID, TenThucDon as Name from THUCDON where MaCN = {_outside._outside.CurrentStaff.BranchID} and DangDung = 1";
            var context = _outside.context;
            var temp = context.Database.
                SqlQuery<DbContextService.Menu>(query).
                ToList();
            var result = new List<DbContextService.Menu>();
            result.AddRange(temp);
            this.menus = result;
        }

        private void CategoryFetch()
        {
            string query = $"select MaMuc as ID, TenMuc as Name, td.MaTD as MenuID " +
                $"from MUCTHUCDON mt " +
                $"inner join THUCDON td on td.MaTD = mt.MaTD " +
                $"where td.MaCN = {_outside._outside.CurrentStaff.BranchID}" +
                $"and mt.DangDung = 1" +
                $"and td.DangDung = 1";
            var context = _outside.context;
            var temp = context.Database.
                SqlQuery<DbContextService.Menu_Category>(query).
                ToList();
            var result = new List<DbContextService.Menu_Category>();
            result.AddRange(temp);
            
            categories = result;
        }

        private void FoodFetch()
        {
            string query = $"select ma.ID as ID, km.TenMonAn as Name, ma.Gia as Price, mt.MaMuc as CategoryID " +
                $"from MONAN ma " +
                $"inner join MUCTHUCDON mt on mt.MaMuc = ma.MaMuc " +
                $"inner join THUCDON td on mt.MaTD = td.MaTD " +
                $"inner join KHOMONAN km on km.MaMonAn = ma.MaMonAn " +
                $"where td.MaCN = {_outside._outside.CurrentStaff.BranchID} " +
                $"and ma.DangDung = 1 " +
                $"and td.DangDung = 1 " +
                $"and mt.DangDung = 1 ";
            var context = _outside.context;
            var temp = context.Database.
                SqlQuery<DbContextService.Food>(query).
                ToList();
            var result = new List<DbContextService.Food>();
            result.AddRange(temp);
            foods = result;
        }

        public void UpdateLabels()
        {
            if (currentTable != null)
            {
                TableNameLabel.Text = $"Tên bàn: {currentTable.Name}";
                TableLocationLabel.Text = $"Vị trí: {currentTable.Location}";
                TableSeatTimeLabel.Text = $"Giờ vào: {currentTable.SeatTime}";
            }
        }

        public void SyncTableFood()
        {
            if (currentTable != null)
            {
                OrderedFoodFetch();
                tableFoodPanel.SyncOrderedFood();
                UnsuggestedOrderedFood();
            }
        }

        public void UnsuggestedOrderedFood()
        {
            //Remove item in food category that already ordered
            foreach (var item in orderedFood)
            {
                var food = foodByCategory.FirstOrDefault(x => x.ID == item.ID);
                if (food != null)
                {
                    foodByCategory.Remove(food);
                }
            }
        }
        public void UpdateLayout()
        {
            UpdateLabels();
            UpdateTableFoodPanel();
        }
        public void UpdateTableFoodPanel()
        {
            tableFoodPanel.DisplayByOrderedFood();
        }
        public void InsertTableFoodDB(int id)
        {
            string query = "insert into BANAN_MONAN (MaBanAn, MaMon, SoLuong) " +
                $"values ({currentTable.ServedTableID}, {id}, 1)";
            var context = _outside.context;
            context.Database.ExecuteSqlCommand(query);
        }
        private void InitalizeComponent()
        {
            // comboBox1
            // 
            System.Windows.Forms.ComboBox comboBox1 = new System.Windows.Forms.ComboBox
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(25, 110),
                Name = "comboBox1",
                Size = new System.Drawing.Size(120, 25),
                TabIndex = 1,
                DropDownStyle = ComboBoxStyle.DropDown
                
            };
            comboBox1.SelectedIndexChanged += new System.EventHandler(this.MenuBox_SelectedIndexChanged);
            
            menuBox = comboBox1;
            
            // 
            // comboBox2
            // 
            System.Windows.Forms.ComboBox comboBox2 = new System.Windows.Forms.ComboBox
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(195, 110),
                Name = "comboBox2",
                Size = new System.Drawing.Size(120, 25),
                TabIndex = 2
            };
            comboBox2.SelectedIndexChanged += new System.EventHandler(this.CategoryMenuBox_SelectedIndexChanged);
            categoryBox = comboBox2;
            
            // 
            // comboBox3
            // 
            System.Windows.Forms.ComboBox comboBox3 = new System.Windows.Forms.ComboBox
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(30, 160),
                Name = "comboBox3",
                Size = new System.Drawing.Size(290, 25),
                TabIndex = 3
            };
            foodBox = comboBox3;
            // 
            // button1
            // 
            System.Windows.Forms.Button button1 = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(135, 190),
                Name = "button1",
                Size = new System.Drawing.Size(75, 25),
                TabIndex = 4,
                BackColor = System.Drawing.Color.Ivory,
                Text = "Thêm"
            };
            button1.Click += new System.EventHandler(this.ServeFood);
            // 
            // label1
            // 
            System.Windows.Forms.Label label1 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(25, 90),
                Name = "label1",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 5,
                Text = "Thực đơn: "
            };
            // 
            // label2
            // 
            System.Windows.Forms.Label label2 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(195, 90),
                Name = "label2",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 6,
                Text = "Mục: "
            };
            // 
            // label3
            // 
            System.Windows.Forms.Label label3 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(25, 140),
                Name = "label3",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 7,
                Text = "Món ăn: "
            };

            // 
            // button3
            // 
            System.Windows.Forms.Button button3 = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(30, 500),
                Name = "button3",
                Size = new System.Drawing.Size(75, 25),
                TabIndex = 13,
                Text = "Hủy bàn",
                BackColor = System.Drawing.Color.Salmon,
            };
            // button4
            // 
            System.Windows.Forms.Button button4 = new System.Windows.Forms.Button
            {
                Location = new System.Drawing.Point(135, 500),
                Name = "button4",
                Size = new System.Drawing.Size(75, 25),
                TabIndex = 14,
                AutoSize = true,
                BackColor = System.Drawing.Color.Ivory,
                Text = "Lập hóa đơn",
            };

            // 
            // label7
            // 
            System.Windows.Forms.Label label7 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(25, 30),
                Name = "label7",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 15,
                Text = "Tên bàn:"
            };
            TableNameLabel = label7;
            // 
            // label8
            // 
            System.Windows.Forms.Label label8 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(25, 60),
                Name = "label8",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 15,
                Text = "Giờ vào: "
            };
            TableSeatTimeLabel = label8;
            // 
            // label9
            // 
            System.Windows.Forms.Label label9 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(195, 30),
                Name = "label9",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 17,
                Text = "Vị trí: "
            };
            TableLocationLabel = label9;

            tableFoodPanel = new TableFoodPanel(this);


            this.Controls.Add(menuBox);
            this.Controls.Add(comboBox2);
            this.Controls.Add(comboBox3);
            this.Controls.Add(button1);
            this.Controls.Add(label1);
            this.Controls.Add(label2);

            this.Controls.Add(label3);
            this.Controls.Add(button3);
            this.Controls.Add(button4);
            this.Controls.Add(label7);
            this.Controls.Add(label8);
            this.Controls.Add(label9);

            this.Controls.Add(tableFoodPanel);

            menuBox.DataSource = null;
            menuBox.DataSource = new List<DbContextService.Menu>(menus);
            menuBox.DisplayMember = "Name";
            menuBox.ValueMember = "ID";
        }
    }

    public class TableListPanel : Panel
    {
        private TablePanel _outside;
        private List<DbContextService.Table> tables;
        public TableListPanel(TablePanel outside)
        {
            _outside = outside;
            tables = _outside.tables;
            Name = "Table List";
            AutoScroll = true;
            BackColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(1260 - 500, _outside.Height);
            Location = new System.Drawing.Point(0, 0); // Set the location where the panel will appear
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label
            {
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(30, 0),
                Name = "label1",
                AutoSize = true,
                TabIndex = 1,
                Text = "Danh sách bàn"
            };

            this.Controls.Add(label);


            int i = 0;
            foreach (var item in _outside.tables)
            {
                // 
                // button1
                // 
                System.Windows.Forms.Button button = new System.Windows.Forms.Button
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
                    button.Click += ServeTable;
                }
                else
                {
                    button.Click += SyncOutsideCurrentTable;
                   
                }
                button.MouseUp += RenameTable;
                this.Controls.Add(button);
                i++;
            }
        }

        private void SyncOutsideCurrentTable(object sender, EventArgs e)
        {
            int index = tables.IndexOf((DbContextService.Table)((( System.Windows.Forms.Button)sender).Tag));
            _outside.SyncCurrentTable(index);
        }

        private void RenameTable(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Prompt for new button text
                // Create a simple input dialog to change the button text
                using (Form inputDialog = new Form
                {
                    AutoSize = true,
                    Size = new System.Drawing.Size(400, 150)

                })
                {
                    inputDialog.Text = "Đổi tên bàn";

                    // Create a TextBox for user input
                    System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox
                    {
                        Text = ((System.Windows.Forms.Button)sender).Text, // Pre-fill with current button text
                        Dock = DockStyle.Fill,
                        Size = new System.Drawing.Size(200, 100),
                        MaxLength = 10,
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)))

                    };

                    // Create an OK button to submit the new text
                    System.Windows.Forms.Button okButton = new System.Windows.Forms.Button
                    {
                        Text = "OK",
                        Dock = DockStyle.Bottom
                    };
                    okButton.Click += (s, args) =>
                    {
                        ((System.Windows.Forms.Button)sender).Text = textBox.Text;
                        var button = (System.Windows.Forms.Button)sender;
                        var item = (DbContextService.Table)button.Tag;
                        var itemInTable = tables.FirstOrDefault(table => table.ID == item.ID);
                        itemInTable.Name = textBox.Text;
                        if(itemInTable.ID.HasValue)
                            UpdateTableNameDB(itemInTable.Name, itemInTable.ID.Value);
                        UpdateTableOutside();
                        inputDialog.Close();
                    };

                    // Add TextBox and OK Button to the dialog
                    inputDialog.Controls.Add(textBox);
                    inputDialog.Controls.Add(okButton);

                    // Show the input dialog
                    inputDialog.ShowDialog();
                }
            }
        }

        private void UpdateTableOutside()
        {
            _outside.tables = tables;
            _outside.SyncTables();
        }
        private void UpdateTableNameDB(string Name, int ID)
        {
            if (_outside.currentTableIndex.HasValue == false)
                return;
            var currentTable = tables[_outside.currentTableIndex.Value];
            string query = "update BAN " +
                $"set TenBan = '{Name}' " +
                $"where MaBan = {ID}";
            var context = _outside.context;
            context.Database.ExecuteSqlCommand(query);
        }
        private void ServeTable(object sender, EventArgs e)
        {
            DbContextService.Table table = (DbContextService.Table)(( System.Windows.Forms.Button)sender).Tag;

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
                //sync internal external
                int index = tables.IndexOf(servedTable);
                _outside.SyncCurrentTable(index);
                (( System.Windows.Forms.Button)sender).Click -= ServeTable;
                (( System.Windows.Forms.Button)sender).Click += SyncOutsideCurrentTable;
                
                (( System.Windows.Forms.Button)sender).BackColor = System.Drawing.Color.Red;
                (( System.Windows.Forms.Button)sender).Tag = servedTable;
            }
        }
    }
    public class TableFoodPanel : Panel
    {
        private OrderPanel _outside;
        private List<DbContextService.Food> orderedFood;
        public TableFoodPanel(OrderPanel outside)
        {
            _outside = outside;
            orderedFood = _outside.orderedFood;
            Name = "Table List";
            AutoScroll = true;
            BackColor = System.Drawing.Color.White;
            Size = new System.Drawing.Size(1260 - 500, 280);
            Location = new System.Drawing.Point(0, 220); // Set the location where the panel will appear
            InitializeComponent();
        }

        private void InitializeComponent()
        {


            DisplayByOrderedFood();

            
        }

        public void DisplayByOrderedFood()
        {
            int i = 0;
            this.Controls.Clear();
            // 
            // label5
            // 
            System.Windows.Forms.Label label5 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(30, 0),
                Name = "label5",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 10,
                Text = "Tên món"
            };
            // 
            // label6
            // 
            System.Windows.Forms.Label label6 = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(135, 0),
                Name = "label6",
                Size = new System.Drawing.Size(45, 15),
                TabIndex = 11,
                Text = "Số lượng"
            };
            this.Controls.Add(label5);
            this.Controls.Add(label6);
            foreach (var item in orderedFood)
            {
                // 
                // label4
                // 
                System.Windows.Forms.Label label4 = new System.Windows.Forms.Label
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(30, 30 * (i + 1)),
                    Name = "label4",
                    Size = new System.Drawing.Size(45, 15),
                    TabIndex = 8,
                    Text = $"{item.Name}"
                };
                // 
                // numericUpDown1
                // 
                NumericUpDown numericUpDown1 = new NumericUpDown
                {
                    Location = new System.Drawing.Point(135, 30 * (i + 1)),
                    Name = "numericUpDown1",
                    Size = new System.Drawing.Size(120, 20),
                    TabIndex = 9,
                    Tag = item.ID
                };
                numericUpDown1.DataBindings.Add("Value", item, "Quantity", true, DataSourceUpdateMode.OnPropertyChanged);
                numericUpDown1.ValueChanged += Quantity_ValueChanged;
                // 
                // button2
                // 
                System.Windows.Forms.Button button2 = new System.Windows.Forms.Button
                {
                    Location = new System.Drawing.Point(270, 30 * (i + 1)),
                    Name = "button2",
                    Size = new System.Drawing.Size(45, 25),
                    TabIndex = 12,
                    BackColor = System.Drawing.Color.Salmon,
                    Text = "Xóa",
                    Tag = item
                };
                button2.Click += RemoveOrderedFoodOnClick;
                i++;
                this.Controls.Add(label4);
                this.Controls.Add(numericUpDown1);
                this.Controls.Add(button2);
            }
        }

        public void SyncOrderedFood()
        {
            orderedFood = _outside.orderedFood;
        }

        public void RemoveOrderedFoodOnClick (object sender, EventArgs e)
        {
            var food = (DbContextService.Food)((System.Windows.Forms.Button)sender).Tag;
            var index = orderedFood.IndexOf(food);
            RemoveOrderedFoodFromList(index);
            _outside.orderedFood = orderedFood;
            _outside.UnsuggestedOrderedFood();
            _outside.UpdateFoodBox();
            DisplayByOrderedFood();
            if(food.ID.HasValue)
                RemoveOrderedFoodDB(food.ID.Value);
        }

        public void RemoveOrderedFoodFromList(int index)
        {
            orderedFood.RemoveAt(index);
        }

        public void RemoveOrderedFoodDB(int foodID)
        {
            var table = _outside.currentTable;
            string query = "delete from BANAN_MONAN " +
                $"where MaBanAn = {table.ServedTableID} " +
                $"and MaMon = {foodID} ";
            var context = _outside._outside.context;
            context.Database.ExecuteSqlCommand(query);
        }

        // Event handler for ValueChanged event
        private void Quantity_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            var food = ((NumericUpDown)sender).Tag;
            Quantity_UpdateDB((int)food, (int)numericUpDown.Value);
        }

        private void Quantity_UpdateDB (int foodID, int quantity)
        {
            var table = _outside.currentTable;
            string query = "update BANAN_MONAN " +
                $"set SoLuong = {quantity}" +
                $"where MaBanAn = {table.ServedTableID} " +
                $"and MaMon = {foodID} ";
            var context = _outside._outside.context;
            context.Database.ExecuteSqlCommand(query);
        }
    }
}
