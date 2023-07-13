using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CSV_Checker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader jsonReader = new StreamReader("C:\\Users\\SPAN_CHAITANYA\\Desktop\\jsonObj.json");
            StreamReader csvReader = new StreamReader("C:\\Users\\SPAN_CHAITANYA\\Desktop\\PANASH CREAM 30GM UZ 2007938.csv");
            StreamWriter csvWriter = new StreamWriter("C:\\out.csv");

            string json = jsonReader.ReadToEnd();
            List<string> JsonIds = new List<string>(); 
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach(var item in array)
            {
                JsonIds.Add(item._id.Value);
            }

            int h = 1;
            while (!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                if (h == 1)
                {
                    csvWriter.WriteLine(line);
                    h = 0;
                }
                else
                {
                    var values = line.Split('=');
                    if (JsonIds.Contains(this.getFormatedstring(values[3])))
                    {
                        csvWriter.WriteLine(line);
                        csvWriter.Flush();
                    }
                }
            }
        }

        private string getFormatedstring(string str)
        {
            return str.Substring(1, 13);
        }
    }
}
