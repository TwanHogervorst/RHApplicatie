﻿using System;
using System.Windows.Forms;

namespace NESessionList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void addRow(string name, string id)
        {
            Console.WriteLine($"add user: {name} | id: {id}");
            string[] row0 = { name, id };
            dataGridView1.Rows.Add(row0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
