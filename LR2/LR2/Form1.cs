using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LR2
{
    public partial class Form1 : Form
    {
        public CarShop GCarShop;
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InactiveButtons();
            comboBox1.Items.Add("Mexico Motors");
            comboBox1.Items.Add("Tokyo Auto");
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        GCarShop = new CarShop(12, 115, "Mexico Motors", "м. Мехіко", 150);
                        labelShopName.Text = GCarShop.Property_ShopName.Trim();
                        ActiveButtons();
                        ToString(GCarShop);
                        break;
                    }
                case 1:
                    {
                        GCarShop = new CarShop(20,300, "Tokyo Auto", "м. Токіо",500);
                        labelShopName.Text = GCarShop.Property_ShopName.Trim();
                        ActiveButtons();
                        ToString(GCarShop);
                        break;
                    }
                default:
                    break;
            }
        }
        private void InactiveButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }
        private void ActiveButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }
        private void ToString(CarShop carShop)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Кількість відділів:  " + carShop.Property_NumberOfDepartments);
            listBox1.Items.Add("Кількість працівників:  " + carShop.Property_NumberOfEmployers);
            listBox1.Items.Add("Назва магазину:  " + carShop.Property_ShopName);
            listBox1.Items.Add("Адреса магазину:  " + carShop.Property_StoreAddres);
            listBox1.Items.Add("Середній дохід магазину на місяць:  " + carShop.Property_AvgIncome);
            listBox1.Items.Add("Загальна заробітна плата співробітників:  " + carShop.Property_Salary);
            listBox1.Items.Add("Загальні витрати на кіпівлю товарів:  " + carShop.Property_Expenses);
            listBox1.Items.Add("Кількість найменувань товарів:  " + carShop.Property_ProductsQuantity);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Дохід за місяць: {0}$", (GCarShop.Property_AvgIncome.ToString("###,###"))),"Розрахунок");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Дохід за рік: {0}$", (GCarShop.Property_AvgIncome *12).ToString("###,###")), "Розрахунок");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Річний податок(17%): {0}$", ((GCarShop.Property_AvgIncome * 12)*0.17).ToString("###,###")), "Податок");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToString(++GCarShop);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ToString(--GCarShop);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActiveButtons();
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.ShowDialog();
            GCarShop = Form2.carShop;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = GCarShop.CompareTo(new CarShop(12, 115, "Mexico Motors", "м. Мехіко", 150));

            if (i == 0) MessageBox.Show(GCarShop.Property_ShopName + " приносить менші доходи.");
            if (i == 1) MessageBox.Show(GCarShop.Property_ShopName + " приносить більші доходи.");
            if (i == 2) MessageBox.Show("Магазини приносять однакові доходи.");
        }
    }

    public class CarShop : IComparable
    {
        private uint NumberOfDepartments;       //Кількість відділів
        private uint NumberOfEmployers;         //Кількість співробітників
        private string ShopName = "";                //Назва магазину
        private string StoreAddres;             //Адреса магазину
        private uint AvgIncome;                 //Середній дохід магазину на місяць 
        private uint Salary;                    //Загальна заробітна плата співробітників
        private uint Expenses;                  //Загальні витрати на кіпівлю товарів
        private uint ProductsQuantity;          //Кількість найменувань товарів


        //Properties 
        public uint Property_NumberOfDepartments { get { return NumberOfDepartments; } set { NumberOfDepartments = value; } }
        public uint Property_NumberOfEmployers { get { return NumberOfEmployers; } set { NumberOfEmployers = value; } }
        public string Property_ShopName { get { return ShopName; } set { ShopName = value; } }
        public string Property_StoreAddres { get { return StoreAddres; } set { StoreAddres = value; } }
        public uint Property_AvgIncome { get { return AvgIncome; } set { AvgIncome = value; } }
        public uint Property_Salary { get { return Salary; } set { Salary = value; } }
        public uint Property_Expenses { get { return Expenses; } set { Expenses = value; } }
        public uint Property_ProductsQuantity { get { return ProductsQuantity; } set { ProductsQuantity = value; } }

        public CarShop() {}

        public CarShop(int _NumberOfDepartments, int _NumberOfEmployers, string _ShopName, string _StoreAddres,
            int _ProductsQuantity)
        {
            NumberOfDepartments = (uint)_NumberOfDepartments;
            NumberOfEmployers = (uint)_NumberOfEmployers;
            ShopName = _ShopName;
            StoreAddres = _StoreAddres;
            ProductsQuantity = (uint)_ProductsQuantity;
            Expenses = (uint) ExpensesSum(this);
            Salary = (uint) SalarySum(this);
            AvgIncome = (uint) AvgSum(this);
        }

        private int ExpensesSum(CarShop carShop)
        {
            int sum = 0;
            //Індексатор
            Products[] pr = Products.CreateProducts(carShop);
            for (int j = 0; j < pr.Length; j++)
            {
                sum += pr[j].Property_Cost;
            }
            return sum;
        }
        private int SalarySum(CarShop carShop)
        {
            int sum = 0;
            Employees[] emp = Employees.SetSalaryforEmployee(carShop);
            for(int j = 0; j < emp.Length; j++)
            {
                sum += emp[j].Property_Salary;
            }
            return sum;
        }
        private int AvgSum(CarShop carShop)
        {
            return ((int)carShop.NumberOfDepartments*50000)-(int)carShop.Expenses - (int)carShop.Salary;
        }

        public int CompareTo(Object obj)
        {
            CarShop cs = (CarShop) obj;
            if(this.Property_AvgIncome > cs.Property_AvgIncome) return 1;
            if (this.Property_AvgIncome == cs.Property_AvgIncome) return 2;
            else return 0;
        }

        public static CarShop operator ++ (CarShop carShop)
        {
            carShop.NumberOfEmployers = (uint)carShop.NumberOfEmployers + 1;
            return carShop;
        }

        public static CarShop operator -- (CarShop carShop)
        {
            carShop.NumberOfEmployers = (uint)carShop.NumberOfEmployers - 1;
            return carShop;
        }
    }

    public class Products
    {
        private string name;
        private int cost;
        public int Property_Cost { get { return cost; } set { cost = value; } }
        public Products() { }
        public Products(string _name, int _cost) 
        {
            name = _name;
            cost = _cost;
        }

        static Products[] products = new Products[1];

        //Indexator
        public Products this[int index]
        {
            get { return products[index]; }
            set { products[index] = value; }
        }

        public static Products[] CreateProducts(CarShop cs)
        {
            //Random rnd = new Random();
            products = new Products[(int)cs.Property_ProductsQuantity];
            for (int i = 0; i < products.Length; i++)
            {
                products[i] = new Products("wheel", 800);
            }
            return products;
        }
    }
    public class Employees
    {
        int salary;

        public int Property_Salary { get { return salary; } set { salary = value; } }

        static Employees[] employees = new Employees[1];
        //Indexator
        public Employees this[int index]
        {
            get { return employees[index]; }
            set { employees[index] = value; }
        }

        Employees(int _salary)
        {
            this.salary = _salary;
        }

        public static Employees[] SetSalaryforEmployee(CarShop cs)
        {
            employees = new Employees[(int) cs.Property_NumberOfEmployers];
            for (int i = 0; i < employees.Length; i++)
            {
                employees[i] = new Employees(800);
            }
            return employees;
        }
    }
}
