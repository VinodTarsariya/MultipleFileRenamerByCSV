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

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        string oldFilePath1 = parts[0];
                        string oldFilePath2 = parts[1];
                        string newFileName = parts[2];
                        string prefix = prefixTextBox.Text; // Get prefix from TextBox
                        string suggestedFileName = $"{prefix}_{newFileName}"; // Add prefix here
                        string suggestedFilePath1 = Path.Combine(Path.GetDirectoryName(oldFilePath1), suggestedFileName);
                        string suggestedFilePath2 = Path.Combine(Path.GetDirectoryName(oldFilePath2), suggestedFileName);

                        table.Rows.Add(oldFilePath1, oldFilePath2, newFileName, suggestedFilePath1, suggestedFilePath2);
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
                    string suggestedFileName = $"{prefix}_{newFileName}";
                    string suggestedFilePath1 = Path.Combine(Path.GetDirectoryName(row["OldFilePath1"].ToString()), suggestedFileName);
                    string suggestedFilePath2 = Path.Combine(Path.GetDirectoryName(row["OldFilePath2"].ToString()), suggestedFileName);
                    row["SuggestedFilePath1"] = suggestedFilePath1;
                    row["SuggestedFilePath2"] = suggestedFilePath2;
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
                    // Create new files with suggested file names
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
