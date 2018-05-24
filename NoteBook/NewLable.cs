using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public partial class NewLable: Component
    {
        float Size = 12;

        public NewLable()
        {
            InitializeComponent();
        }

        public NewLable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Hover(object sender, EventArgs e)
        {

        }
    }
}
