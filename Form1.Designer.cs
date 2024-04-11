namespace MultipleFileRenamerByCSV
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selectFileButton = new Button();
            dataGridView = new DataGridView();
            renameFilesButton = new Button();
            prefixTextBox = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // selectFileButton
            // 
            selectFileButton.Location = new Point(17, 18);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(75, 23);
            selectFileButton.TabIndex = 0;
            selectFileButton.Text = "Select File";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += selectFileButton_Click;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Bottom;
            dataGridView.Location = new Point(0, 61);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(800, 389);
            dataGridView.TabIndex = 1;
            // 
            // renameFilesButton
            // 
            renameFilesButton.Location = new Point(361, 18);
            renameFilesButton.Name = "renameFilesButton";
            renameFilesButton.Size = new Size(113, 23);
            renameFilesButton.TabIndex = 2;
            renameFilesButton.Text = "Rename File";
            renameFilesButton.UseVisualStyleBackColor = true;
            // 
            // prefixTextBox
            // 
            prefixTextBox.Location = new Point(204, 18);
            prefixTextBox.Name = "prefixTextBox";
            prefixTextBox.Size = new Size(134, 23);
            prefixTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(113, 22);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 4;
            label1.Text = "New file prefix: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(prefixTextBox);
            Controls.Add(renameFilesButton);
            Controls.Add(dataGridView);
            Controls.Add(selectFileButton);
            Name = "Form1";
            Text = "Shri Hari | Multiple File Rename using CSV file";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button selectFileButton;
        private DataGridView dataGridView;
        private Button renameFilesButton;
        private TextBox prefixTextBox;
        private Label label1;
    }
}
