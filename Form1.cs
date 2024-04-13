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
                    table.Columns.Add("SuggestedFilePath1");
                    table.Columns.Add("SuggestedFilePath2");

                    string prefix1 = prefixTextBox1.Text.Trim(); // Get prefix for first column
                    string prefix2 = prefixTextBox2.Text.Trim(); // Get prefix for second column

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        string oldFilePath1 = parts[0];
                        string oldFilePath2 = parts[1];
                        string newFileName = parts[2];

                        // Construct suggested file paths using respective prefixes
                        string suggestedFileName1 = ConstructSuggestedFileName(prefix1, newFileName, oldFilePath1);
                        string suggestedFileName2 = ConstructSuggestedFileName(prefix2, newFileName, oldFilePath2);

                        string suggestedFilePath1 = Path.Combine(Path.GetDirectoryName(oldFilePath1), suggestedFileName1);
                        string suggestedFilePath2 = Path.Combine(Path.GetDirectoryName(oldFilePath2), suggestedFileName2);

                        table.Rows.Add(oldFilePath1, oldFilePath2, newFileName, suggestedFilePath1, suggestedFilePath2);
                    }

                    dataGridView.DataSource = table;
                }
            }
        }

        private string ConstructSuggestedFileName(string prefix, string newFileName, string oldFilePath)
        {
            if (!string.IsNullOrEmpty(prefix))
            {
                return $"{prefix}_{newFileName}{Path.GetExtension(oldFilePath)}";
            }
            else
            {
                return $"{newFileName}{Path.GetExtension(oldFilePath)}";
            }
        }

        private void suggestButton_Click(object sender, EventArgs e)
        {
            string prefix1 = prefixTextBox1.Text.Trim(); // Get prefix for first column
            string prefix2 = prefixTextBox2.Text.Trim(); // Get prefix for second column
            DataTable table = (DataTable)dataGridView.DataSource;

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string newFileName = row["NewFileName"].ToString();

                    // Update suggested file paths in the respective columns
                    row["SuggestedFilePath1"] = ConstructSuggestedFileName(prefix1, newFileName, row["OldFilePath1"].ToString());
                    row["SuggestedFilePath2"] = ConstructSuggestedFileName(prefix2, newFileName, row["OldFilePath2"].ToString());
                }
            }
        }

        private void renameFilesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                string oldFilePath1 = row.Cells["OldFilePath1"].Value.ToString();
                string oldFilePath2 = row.Cells["OldFilePath2"].Value.ToString();
                string suggestedFilePath1 = row.Cells["SuggestedFilePath1"].Value.ToString();
                string suggestedFilePath2 = row.Cells["SuggestedFilePath2"].Value.ToString();

                try
                {
                    // Rename the files instead of copying them
                    File.Move(oldFilePath1, suggestedFilePath1);
                    File.Move(oldFilePath2, suggestedFilePath2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error renaming files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("File renaming completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
