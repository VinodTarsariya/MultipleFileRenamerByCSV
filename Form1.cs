using System.Data;
using System.Windows.Forms;

namespace MultipleFileRenamerByCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Read and display CSV data
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    DataTable table = new DataTable();
                    table.Columns.Add("OldFilePath1");
                    table.Columns.Add("OldFilePath2");
                    table.Columns.Add("NewFileName");
                    table.Columns.Add("SuggestedFileName1");
                    table.Columns.Add("SuggestedFileName2");

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        string oldFilePath1 = parts[0];
                        string oldFilePath2 = parts[1];
                        string newFileName = parts[2];
                        string prefix = prefixTextBox.Text; // Get prefix from TextBox

                        // Calculate suggested file names for first and second columns
                        string suggestedFileName1 = $"{prefix}_{newFileName}{Path.GetExtension(oldFilePath1)}";
                        string suggestedFileName2 = $"{prefix}_{newFileName}{Path.GetExtension(oldFilePath2)}";

                        table.Rows.Add(oldFilePath1, oldFilePath2, newFileName, suggestedFileName1, suggestedFileName2);
                    }

                    dataGridView.DataSource = table;
                }
            }
        }

        private void suggestButton_Click(object sender, EventArgs e)
        {
            string prefix = prefixTextBox.Text;
            DataTable table = (DataTable)dataGridView.DataSource;
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string newFileName = row["NewFileName"].ToString();

                    // Update suggested file names in the respective columns
                    row["SuggestedFileName1"] = $"{prefix}_{newFileName}{Path.GetExtension(row["OldFilePath1"].ToString())}";
                    row["SuggestedFileName2"] = $"{prefix}_{newFileName}{Path.GetExtension(row["OldFilePath2"].ToString())}";
                }
            }
        }

        private void renameFilesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                string oldFilePath1 = row.Cells["OldFilePath1"].Value.ToString();
                string oldFilePath2 = row.Cells["OldFilePath2"].Value.ToString();
                string suggestedFileName1 = row.Cells["SuggestedFileName1"].Value.ToString();
                string suggestedFileName2 = row.Cells["SuggestedFileName2"].Value.ToString();

                try
                {
                    // Create new files with suggested file names
                    string suggestedFilePath1 = Path.Combine(Path.GetDirectoryName(oldFilePath1), suggestedFileName1);
                    string suggestedFilePath2 = Path.Combine(Path.GetDirectoryName(oldFilePath2), suggestedFileName2);
                    File.Copy(oldFilePath1, suggestedFilePath1);
                    File.Copy(oldFilePath2, suggestedFilePath2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating new files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("File creation completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
