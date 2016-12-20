using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU
{
    public partial class MainWindow : Form
    {
        public Microprocessor mp { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mp = new Microprocessor();
            registersTextBox.Text = mp.PrintRegistersToString();
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = codeTextBox.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)codeTextBox.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)codeTextBox.Font.Size;
            }
            else
            {
                w = 50 + (int)codeTextBox.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = codeTextBox.GetCharIndexFromPosition(pt);
            int First_Line = codeTextBox.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = codeTextBox.GetCharIndexFromPosition(pt);
            int Last_Line = codeTextBox.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            codeTextBox.Location = new Point(getWidth(), 0);
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = codeTextBox.Font;
            codeTextBox.Select();
            AddLineNumbers();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = codeTextBox.GetPositionFromCharIndex(codeTextBox.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (codeTextBox.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = codeTextBox.Font;
            codeTextBox.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            codeTextBox.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            string[] commands = codeTextBox.Text.Split(';').Select(cmd => cmd.Trim()).ToArray();
            foreach (string cmd in commands)
            {
                if (cmd.Equals("")) continue;
                Console.WriteLine("Executing: '{0}'", cmd);
                mp.Command(cmd);
                registersTextBox.Text = mp.PrintRegistersToString();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            mp.Clear();
            registersTextBox.Text = mp.PrintRegistersToString();
        }
    }
}
