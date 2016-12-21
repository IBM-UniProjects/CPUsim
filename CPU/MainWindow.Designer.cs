namespace CPU
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.codeTextBox = new System.Windows.Forms.RichTextBox();
            this.LineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.registersTextBox = new System.Windows.Forms.RichTextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.runStepButton = new System.Windows.Forms.Button();
            this.stepBox = new System.Windows.Forms.TextBox();
            this.stepLabel = new System.Windows.Forms.Label();
            this.legendLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.clearCodeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // codeTextBox
            // 
            this.codeTextBox.BackColor = System.Drawing.Color.White;
            this.codeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextBox.Location = new System.Drawing.Point(33, 0);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(525, 647);
            this.codeTextBox.TabIndex = 0;
            this.codeTextBox.Text = "";
            this.codeTextBox.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            this.codeTextBox.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            this.codeTextBox.FontChanged += new System.EventHandler(this.richTextBox1_FontChanged);
            this.codeTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // LineNumberTextBox
            // 
            this.LineNumberTextBox.BackColor = System.Drawing.Color.White;
            this.LineNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineNumberTextBox.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.LineNumberTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.LineNumberTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LineNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.LineNumberTextBox.Location = new System.Drawing.Point(0, 0);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.ReadOnly = true;
            this.LineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNumberTextBox.Size = new System.Drawing.Size(117, 647);
            this.LineNumberTextBox.TabIndex = 1;
            this.LineNumberTextBox.Text = "";
            // 
            // registersTextBox
            // 
            this.registersTextBox.BackColor = System.Drawing.Color.White;
            this.registersTextBox.Enabled = false;
            this.registersTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registersTextBox.Location = new System.Drawing.Point(565, 410);
            this.registersTextBox.Name = "registersTextBox";
            this.registersTextBox.ReadOnly = true;
            this.registersTextBox.Size = new System.Drawing.Size(335, 223);
            this.registersTextBox.TabIndex = 2;
            this.registersTextBox.Text = "";
            // 
            // runButton
            // 
            this.runButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runButton.Location = new System.Drawing.Point(565, 376);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(87, 27);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "RUN";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(813, 376);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(87, 27);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "CLEAR";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // runStepButton
            // 
            this.runStepButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runStepButton.Location = new System.Drawing.Point(659, 376);
            this.runStepButton.Name = "runStepButton";
            this.runStepButton.Size = new System.Drawing.Size(87, 27);
            this.runStepButton.TabIndex = 5;
            this.runStepButton.Text = "RUN STEP";
            this.runStepButton.UseVisualStyleBackColor = true;
            this.runStepButton.Click += new System.EventHandler(this.runStepButton_Click);
            // 
            // stepBox
            // 
            this.stepBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepBox.Location = new System.Drawing.Point(659, 346);
            this.stepBox.Name = "stepBox";
            this.stepBox.Size = new System.Drawing.Size(87, 25);
            this.stepBox.TabIndex = 6;
            this.stepBox.Text = "1";
            this.stepBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepBox.TextChanged += new System.EventHandler(this.stepBox_TextChanged);
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepLabel.Location = new System.Drawing.Point(656, 325);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(72, 18);
            this.stepLabel.TabIndex = 7;
            this.stepLabel.Text = "At step:";
            // 
            // legendLabel
            // 
            this.legendLabel.AutoSize = true;
            this.legendLabel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendLabel.Location = new System.Drawing.Point(562, 9);
            this.legendLabel.Name = "legendLabel";
            this.legendLabel.Size = new System.Drawing.Size(312, 126);
            this.legendLabel.TabIndex = 8;
            this.legendLabel.Text = resources.GetString("legendLabel.Text");
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(565, 138);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "SAVE";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(565, 167);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 10;
            this.loadButton.Text = "LOAD";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // clearCodeButton
            // 
            this.clearCodeButton.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearCodeButton.Location = new System.Drawing.Point(565, 196);
            this.clearCodeButton.Name = "clearCodeButton";
            this.clearCodeButton.Size = new System.Drawing.Size(75, 23);
            this.clearCodeButton.TabIndex = 11;
            this.clearCodeButton.Text = "CLS";
            this.clearCodeButton.UseVisualStyleBackColor = true;
            this.clearCodeButton.Click += new System.EventHandler(this.clearCodeButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 647);
            this.Controls.Add(this.clearCodeButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.legendLabel);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.stepBox);
            this.Controls.Add(this.runStepButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.registersTextBox);
            this.Controls.Add(this.LineNumberTextBox);
            this.Controls.Add(this.codeTextBox);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CPU";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox codeTextBox;
        private System.Windows.Forms.RichTextBox LineNumberTextBox;
        private System.Windows.Forms.RichTextBox registersTextBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button runStepButton;
        private System.Windows.Forms.TextBox stepBox;
        private System.Windows.Forms.Label stepLabel;
        private System.Windows.Forms.Label legendLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button clearCodeButton;
    }
}

