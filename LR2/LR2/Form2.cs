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
    public partial class Form2 : Form
    {
        public static CarShop carShop;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            carShop = new CarShop(
                Convert.ToInt32(textBox1.Text.Trim()),
                Convert.ToInt32(textBox2.Text.Trim()),
                textBox3.Text.Trim(),
                textBox4.Text.Trim(),
                Convert.ToInt32(textBox5.Text.Trim()));
            
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                main.comboBox1.Items.Add(textBox3.Text.Trim());
                main.labelShopName.Text = textBox3.Text.Trim();
                main.listBox1.Items.Clear();
                main.listBox1.Items.Add("Кількість відділів:  " + carShop.Property_NumberOfDepartments);
                main.listBox1.Items.Add("Кількість працівників:  " + carShop.Property_NumberOfEmployers);
                main.listBox1.Items.Add("Назва магазину:  " + carShop.Property_ShopName);
                main.listBox1.Items.Add("Адреса магазину:  " + carShop.Property_StoreAddres);
                main.listBox1.Items.Add("Середній дохід магазину на місяць:  " + carShop.Property_AvgIncome);
                main.listBox1.Items.Add("Загальна заробітна плата співробітників:  " + carShop.Property_Salary);
                main.listBox1.Items.Add("Загальні витрати на кіпівлю товарів:  " + carShop.Property_Expenses);
                main.listBox1.Items.Add("Кількість найменувань товарів:  " + carShop.Property_ProductsQuantity);
            }
            this.Close();
            
        }
    }
}
