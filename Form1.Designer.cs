
namespace BDO_Info_Scraper
{
    partial class Form1
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
            this.scrapeButton = new System.Windows.Forms.Button();
            this.viewFileButton = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.failLogView = new System.Windows.Forms.Button();
            this.viewSuccess = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.batchWaitCheck = new System.Windows.Forms.CheckBox();
            this.fetchItem = new System.Windows.Forms.Button();
            this.idBox = new System.Windows.Forms.TextBox();
            this.cleanButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.recScrapeButton = new System.Windows.Forms.Button();
            this.mRecScrapeButton = new System.Windows.Forms.Button();
            this.dRecScrapeButton = new System.Windows.Forms.Button();
            this.recIdText = new System.Windows.Forms.TextBox();
            this.mRecText = new System.Windows.Forms.TextBox();
            this.dRecBox = new System.Windows.Forms.TextBox();
            this.recipeFetchButton = new System.Windows.Forms.Button();
            this.mRecipeFetchButton = new System.Windows.Forms.Button();
            this.designFetchButton = new System.Windows.Forms.Button();
            this.recipeLabel = new System.Windows.Forms.LinkLabel();
            this.mRecLabel = new System.Windows.Forms.LinkLabel();
            this.dRecLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // scrapeButton
            // 
            this.scrapeButton.Location = new System.Drawing.Point(222, 141);
            this.scrapeButton.Name = "scrapeButton";
            this.scrapeButton.Size = new System.Drawing.Size(75, 23);
            this.scrapeButton.TabIndex = 0;
            this.scrapeButton.Text = "Scrape";
            this.scrapeButton.UseVisualStyleBackColor = true;
            this.scrapeButton.Click += new System.EventHandler(this.scrapeButton_Click);
            // 
            // viewFileButton
            // 
            this.viewFileButton.Location = new System.Drawing.Point(12, 78);
            this.viewFileButton.Name = "viewFileButton";
            this.viewFileButton.Size = new System.Drawing.Size(111, 23);
            this.viewFileButton.TabIndex = 1;
            this.viewFileButton.Text = "View Scrape";
            this.viewFileButton.UseVisualStyleBackColor = true;
            this.viewFileButton.Click += new System.EventHandler(this.viewFileButton_Click_1);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(12, 196);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(192, 23);
            this.Close.TabIndex = 2;
            this.Close.Text = "Stop and Exit";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // failLogView
            // 
            this.failLogView.Location = new System.Drawing.Point(12, 107);
            this.failLogView.Name = "failLogView";
            this.failLogView.Size = new System.Drawing.Size(111, 23);
            this.failLogView.TabIndex = 3;
            this.failLogView.Text = "Fail Log";
            this.failLogView.UseVisualStyleBackColor = true;
            // 
            // viewSuccess
            // 
            this.viewSuccess.Location = new System.Drawing.Point(12, 136);
            this.viewSuccess.Name = "viewSuccess";
            this.viewSuccess.Size = new System.Drawing.Size(111, 23);
            this.viewSuccess.TabIndex = 4;
            this.viewSuccess.Text = "Success Log";
            this.viewSuccess.UseVisualStyleBackColor = true;
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(12, 12);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 60);
            this.pauseButton.TabIndex = 5;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click_1);
            // 
            // batchWaitCheck
            // 
            this.batchWaitCheck.AutoSize = true;
            this.batchWaitCheck.Location = new System.Drawing.Point(12, 173);
            this.batchWaitCheck.Name = "batchWaitCheck";
            this.batchWaitCheck.Size = new System.Drawing.Size(117, 17);
            this.batchWaitCheck.TabIndex = 6;
            this.batchWaitCheck.Text = "Manual Batch Start";
            this.batchWaitCheck.UseVisualStyleBackColor = true;
            this.batchWaitCheck.CheckedChanged += new System.EventHandler(this.batchWaitCheck_CheckedChanged);
            // 
            // fetchItem
            // 
            this.fetchItem.Location = new System.Drawing.Point(222, 196);
            this.fetchItem.Name = "fetchItem";
            this.fetchItem.Size = new System.Drawing.Size(75, 23);
            this.fetchItem.TabIndex = 7;
            this.fetchItem.Text = "Fetch Item";
            this.fetchItem.UseVisualStyleBackColor = true;
            this.fetchItem.Click += new System.EventHandler(this.fetchItem_Click_1);
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(222, 170);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(75, 20);
            this.idBox.TabIndex = 8;
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(93, 12);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(74, 60);
            this.cleanButton.TabIndex = 9;
            this.cleanButton.Text = "Clean Database";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(235, 125);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(48, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "bdolytics";
            // 
            // recScrapeButton
            // 
            this.recScrapeButton.Location = new System.Drawing.Point(303, 141);
            this.recScrapeButton.Name = "recScrapeButton";
            this.recScrapeButton.Size = new System.Drawing.Size(75, 23);
            this.recScrapeButton.TabIndex = 11;
            this.recScrapeButton.Text = "Scrape";
            this.recScrapeButton.UseVisualStyleBackColor = true;
            this.recScrapeButton.Click += new System.EventHandler(this.recScrapeButton_Click);
            // 
            // mRecScrapeButton
            // 
            this.mRecScrapeButton.Location = new System.Drawing.Point(384, 141);
            this.mRecScrapeButton.Name = "mRecScrapeButton";
            this.mRecScrapeButton.Size = new System.Drawing.Size(75, 23);
            this.mRecScrapeButton.TabIndex = 12;
            this.mRecScrapeButton.Text = "Scrape";
            this.mRecScrapeButton.UseVisualStyleBackColor = true;
            // 
            // dRecScrapeButton
            // 
            this.dRecScrapeButton.Location = new System.Drawing.Point(465, 141);
            this.dRecScrapeButton.Name = "dRecScrapeButton";
            this.dRecScrapeButton.Size = new System.Drawing.Size(75, 23);
            this.dRecScrapeButton.TabIndex = 13;
            this.dRecScrapeButton.Text = "Scrape";
            this.dRecScrapeButton.UseVisualStyleBackColor = true;
            // 
            // recIdText
            // 
            this.recIdText.Location = new System.Drawing.Point(303, 170);
            this.recIdText.Name = "recIdText";
            this.recIdText.Size = new System.Drawing.Size(75, 20);
            this.recIdText.TabIndex = 14;
            // 
            // mRecText
            // 
            this.mRecText.Location = new System.Drawing.Point(384, 170);
            this.mRecText.Name = "mRecText";
            this.mRecText.Size = new System.Drawing.Size(75, 20);
            this.mRecText.TabIndex = 15;
            // 
            // dRecBox
            // 
            this.dRecBox.Location = new System.Drawing.Point(465, 170);
            this.dRecBox.Name = "dRecBox";
            this.dRecBox.Size = new System.Drawing.Size(75, 20);
            this.dRecBox.TabIndex = 16;
            // 
            // recipeFetchButton
            // 
            this.recipeFetchButton.Location = new System.Drawing.Point(303, 196);
            this.recipeFetchButton.Name = "recipeFetchButton";
            this.recipeFetchButton.Size = new System.Drawing.Size(75, 23);
            this.recipeFetchButton.TabIndex = 17;
            this.recipeFetchButton.Text = "Fetch Item";
            this.recipeFetchButton.UseVisualStyleBackColor = true;
            this.recipeFetchButton.Click += new System.EventHandler(this.recipeFetchButton_Click);
            // 
            // mRecipeFetchButton
            // 
            this.mRecipeFetchButton.Location = new System.Drawing.Point(384, 196);
            this.mRecipeFetchButton.Name = "mRecipeFetchButton";
            this.mRecipeFetchButton.Size = new System.Drawing.Size(75, 23);
            this.mRecipeFetchButton.TabIndex = 18;
            this.mRecipeFetchButton.Text = "Fetch Item";
            this.mRecipeFetchButton.UseVisualStyleBackColor = true;
            // 
            // designFetchButton
            // 
            this.designFetchButton.Location = new System.Drawing.Point(467, 196);
            this.designFetchButton.Name = "designFetchButton";
            this.designFetchButton.Size = new System.Drawing.Size(75, 23);
            this.designFetchButton.TabIndex = 19;
            this.designFetchButton.Text = "Fetch Item";
            this.designFetchButton.UseVisualStyleBackColor = true;
            // 
            // recipeLabel
            // 
            this.recipeLabel.AutoSize = true;
            this.recipeLabel.Location = new System.Drawing.Point(316, 125);
            this.recipeLabel.Name = "recipeLabel";
            this.recipeLabel.Size = new System.Drawing.Size(41, 13);
            this.recipeLabel.TabIndex = 20;
            this.recipeLabel.TabStop = true;
            this.recipeLabel.Text = "Recipe";
            // 
            // mRecLabel
            // 
            this.mRecLabel.AutoSize = true;
            this.mRecLabel.Location = new System.Drawing.Point(398, 125);
            this.mRecLabel.Name = "mRecLabel";
            this.mRecLabel.Size = new System.Drawing.Size(49, 13);
            this.mRecLabel.TabIndex = 21;
            this.mRecLabel.TabStop = true;
            this.mRecLabel.Text = "mRecipe";
            // 
            // dRecLabel
            // 
            this.dRecLabel.AutoSize = true;
            this.dRecLabel.Location = new System.Drawing.Point(465, 125);
            this.dRecLabel.Name = "dRecLabel";
            this.dRecLabel.Size = new System.Drawing.Size(77, 13);
            this.dRecLabel.TabIndex = 22;
            this.dRecLabel.TabStop = true;
            this.dRecLabel.Text = "Design Recipe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 230);
            this.Controls.Add(this.dRecLabel);
            this.Controls.Add(this.mRecLabel);
            this.Controls.Add(this.recipeLabel);
            this.Controls.Add(this.designFetchButton);
            this.Controls.Add(this.mRecipeFetchButton);
            this.Controls.Add(this.recipeFetchButton);
            this.Controls.Add(this.dRecBox);
            this.Controls.Add(this.mRecText);
            this.Controls.Add(this.recIdText);
            this.Controls.Add(this.dRecScrapeButton);
            this.Controls.Add(this.mRecScrapeButton);
            this.Controls.Add(this.recScrapeButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.fetchItem);
            this.Controls.Add(this.batchWaitCheck);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.viewSuccess);
            this.Controls.Add(this.failLogView);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.viewFileButton);
            this.Controls.Add(this.scrapeButton);
            this.Name = "Form1";
            this.Text = "BDOS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scrapeButton;
        private System.Windows.Forms.Button viewFileButton;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button failLogView;
        private System.Windows.Forms.Button viewSuccess;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.CheckBox batchWaitCheck;
        private System.Windows.Forms.Button fetchItem;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button recScrapeButton;
        private System.Windows.Forms.Button mRecScrapeButton;
        private System.Windows.Forms.Button dRecScrapeButton;
        private System.Windows.Forms.TextBox recIdText;
        private System.Windows.Forms.TextBox mRecText;
        private System.Windows.Forms.TextBox dRecBox;
        private System.Windows.Forms.Button recipeFetchButton;
        private System.Windows.Forms.Button mRecipeFetchButton;
        private System.Windows.Forms.Button designFetchButton;
        private System.Windows.Forms.LinkLabel recipeLabel;
        private System.Windows.Forms.LinkLabel mRecLabel;
        private System.Windows.Forms.LinkLabel dRecLabel;
    }
}

