using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using BDO_Info_Scraper;

namespace BDO_Info_Scraper
{
    public partial class Form1 : Form
    {
        private bool isPaused = false;  // Flag to track if scraping is paused

        private string dataDirectory; // Assuming the data directory is a subfolder named "ScrapedData" in the application directory

        public Form1()
        {
            InitializeComponent();
            InitializeDataDirectory();
            failLogView.Click += failLogView_Click;
            viewSuccess.Click += viewSuccess_Click;
            recScrapeButton.Click += recScrapeButton_Click;  // Ensure this is set up

            ScrapeMaster.ConfirmProceed = this.ConfirmContinueAsync;  // Ensure this delegate is assigned
            this.scrapeButton.Click += new EventHandler(scrapeButton_Click);
            cleanButton.Click += cleanButton_Click;  // Connect the click event



        }

        private void InitializeDataDirectory()
        {
            // Get the full path to the data directory
            dataDirectory = Path.Combine(Application.StartupPath, "ScrapedData");
            // Ensure the directory exists
            Directory.CreateDirectory(dataDirectory);
        }



        private void viewFileButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(dataDirectory))
                {
                    Process.Start("explorer.exe", dataDirectory);
                }
                else
                {
                    MessageBox.Show("The data directory does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to open the directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewSuccess_Click(object sender, EventArgs e)
        {
            try
            {
                string logFilePath = Path.Combine(Application.StartupPath, "successlog.txt");

                if (File.Exists(logFilePath))
                {
                    Process.Start("explorer.exe", logFilePath);
                }
                else
                {
                    MessageBox.Show("The success log file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void failLogView_Click(object sender, EventArgs e)
        {
            try
            {
                string logFilePath = Path.Combine(Application.StartupPath, "failedfetch.txt");

                if (File.Exists(logFilePath))
                {
                    Process.Start("explorer.exe", logFilePath);
                }
                else
                {
                    MessageBox.Show("The failure log file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to open the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void fetchItem_Click_1(object sender, EventArgs e)
        {
            // Parse the ID entered in idBox and validate it
            if (int.TryParse(idBox.Text, out int itemId))
            {
                if (itemId >= 1 && itemId <= 820909)  // Assuming the valid range is from 1 to 820909
                {
                    try
                    {
                        JObject result = await ScrapeMaster.FetchItemData(itemId);
                        if (result != null)
                        {
                            MessageBox.Show("Item fetched successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Optionally display the fetched data or process further
                        }
                        else
                        {
                            MessageBox.Show("Failed to fetch the item. See log for details.", "Fetch Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Item ID is out of the valid range. Please enter a number between 1 and 820909.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric item ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void scrapeButton_Click(object sender, EventArgs e)
        {
            scrapeButton.Enabled = false; // Disable the button to prevent multiple concurrent scraping sessions
            isPaused = false;
            await ScrapeMaster.ScrapeData();
            MessageBox.Show("Scraping completed!");
            scrapeButton.Enabled = true;  // Re-enable the button once scraping is done


        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void batchWaitCheck_CheckedChanged(object sender, EventArgs e)
        {

        }


        private async Task<bool> ConfirmContinueAsync()
        {
            if (InvokeRequired)  // Ensure we are on the UI thread
            {
                return (bool)Invoke(new Func<Task<bool>>(ConfirmContinueAsync));
            }
            else
            {
                if (batchWaitCheck.Checked)
                {
                    var result = MessageBox.Show("Do you want to continue to the next batch?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return result == DialogResult.Yes;
                }
                return true;  // If the checkbox is not checked, continue automatically
            }
        }

        private void pauseButton_Click_1(object sender, EventArgs e)
        {
            if (isPaused)
            {
                // The scraping is currently paused, ask the user if they want to resume
                var result = MessageBox.Show("The scraping is paused. Do you want to resume?", "Resume Scrape?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    isPaused = false;  // Update flag
                    ScrapeMaster.ResumeScraping();  // Resume scraping
                    pauseButton.Text = "Pause";  // Update button text to reflect current action
                }
            }
            else
            {
                // Pause the scraping operation
                var result = MessageBox.Show("Are you sure you want to pause scraping?", "Pause Scrape?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    isPaused = true;
                    ScrapeMaster.PauseScraping();  // Pause scraping
                    pauseButton.Text = "Resume";  // Update button text to reflect current action
                }
            }
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            CleanSmallFiles();
        }

        private void LogDeletion(string filePath)
        {
            string logMessage = $"Deleted file: {filePath}\n";
            string logFilePath = Path.Combine(Application.StartupPath, "deletionLog.txt");  // Log file path

            File.AppendAllText(logFilePath, logMessage);  // Append deletion log to the file
        }

        private void CleanSmallFiles()
        {
            string directoryPath = Path.Combine(Application.StartupPath, "ScrapedData");
            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
                FileInfo[] files = dirInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    // Check if the file size is 1KB or less (1024 bytes)
                    if (file.Length <= 1024)
                    {
                        try
                        {
                            string filePath = file.FullName;
                            file.Delete();  // Delete the file
                            LogDeletion(filePath);  // Log the deletion
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to delete {file.Name}: {ex.Message}", "Error Deleting File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                MessageBox.Show("Cleanup completed successfully.", "Cleanup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ScrapedData directory does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void recScrapeButton_Click(object sender, EventArgs e)
        {
            recScrapeButton.Enabled = false; // Disable the button while scraping is in progress
            try
            {
                var scraper = new RecipeScraper();
                await scraper.RunScraping();
                MessageBox.Show("Scraping of recipes completed!", "Scraping Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                recScrapeButton.Enabled = true; // Re-enable the button once scraping is done or if an error occurs
            }
        }


        private async void recipeFetchButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(recIdText.Text, out int recipeId))
            {
                recipeFetchButton.Enabled = false; // Disable the button while fetching is in progress
                try
                {
                    var scraper = new RecipeScraper();
                    var result = await scraper.FetchRecipeData(recipeId);
                    if (result != null)
                    {
                        MessageBox.Show("Recipe fetched successfully and saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No valid data found for the provided ID.", "Fetch Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    recipeFetchButton.Enabled = true; // Re-enable the button once fetching is done
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric recipe ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
