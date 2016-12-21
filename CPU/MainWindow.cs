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

namespace CPU
{
    public partial class MainWindow : Form
    {
        public Microprocessor mp { get; set; }
        private int step;

        public MainWindow()
        {
            InitializeComponent();
            mp = new Microprocessor();
            step = 1;
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
                try
                {
                    mp.Command(cmd);
                }
                catch (Exception ex)
                {
                    var mb = MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                registersTextBox.Text = mp.PrintRegistersToString();
            }
            step = 1;
            stepBox.Text = step.ToString();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            mp.Clear();
            registersTextBox.Text = mp.PrintRegistersToString();
        }

        private void runStepButton_Click(object sender, EventArgs e)
        {
            if (codeTextBox.Lines.Length < step)
            {
                step = 1;
                stepBox.Text = step.ToString();
                return;
            }
            string[] commands = codeTextBox.Lines[step-1].Split(';').Select(cmd => cmd.Trim()).ToArray();
            foreach (string cmd in commands)
            {
                if (cmd.Equals("")) continue;
                Console.WriteLine("Executing: '{0}'", cmd);
                try
                {
                    mp.Command(cmd);
                }
                catch (Exception ex)
                {
                    var mb = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                registersTextBox.Text = mp.PrintRegistersToString();
            }
            step++;
            stepBox.Text = step.ToString();
        }

        private void stepBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(stepBox.Text, out step)) ;
            else step = 1;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "code";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.ShowDialog();
            
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                codeTextBox.SaveFile(fs, RichTextBoxStreamType.UnicodePlainText);

                fs.Close();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            StreamReader myStreamReader = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStreamReader = new StreamReader(openFileDialog1.OpenFile(), Encoding.Unicode)) != null)
                    {
                        using (myStreamReader)
                        {
                            codeTextBox.Text = myStreamReader.ReadToEnd();
                            AddLineNumbers();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void clearCodeButton_Click(object sender, EventArgs e)
        {
            codeTextBox.Text = "";
            AddLineNumbers();
            step = 1;
            stepBox.Text = step.ToString();
        }
    }
}
