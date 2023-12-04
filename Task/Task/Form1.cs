using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Task
{
    public partial class Form1 : Form
    {
        Task obj;
        public Form1()
        {
            InitializeComponent();
            obj = new Task();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text_all = textBox1.Text;
            MatchCollection matches;

            matches = obj.Module1(text_all);

            textBox2.Text = obj.Module2(matches);
            foreach (Match match in matches)
            {
                listBox1.Items.Add(match.Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obj.Module3(listBox1.Items);
        }
    }

    public class Task
    {
        public MatchCollection Module1(string text_all)
        {
            const string MatchEmailPattern =
              @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
              + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";

            Regex rx = new Regex(
              MatchEmailPattern,
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.Matches(text_all);
        }

        public string Module2(MatchCollection matches)
        {
            return matches.Count.ToString();
        }

        public void Module3(ListBox.ObjectCollection list)
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("sheetName");
            var ws = workbook.Worksheet("sheetName");

            ws.Column("A").Width = 50;

            int row = 1;
            foreach (object item in list)
            {
                ws.Cell("A" + row.ToString()).Value = item.ToString();
                row++;
            }

            workbook.SaveAs("Список email.xlsx");
        }
    }
}
