using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp_Serenity_Spa
{
    public partial class Form1 : Form
    {
       private string conexaostring = "server=127.0.0.1;uid=root;pwd=root;database=ss";
        public Form1()
        {
            
            InitializeComponent();
        }
    }
}
