namespace Omission.WindowsDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblOptions = new System.Windows.Forms.Label();
            this.lbxOptions = new System.Windows.Forms.ListBox();
            this.btnThrow = new System.Windows.Forms.Button();
            this.lbxLogEntries = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.Location = new System.Drawing.Point(12, 36);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(59, 13);
            this.lblOptions.TabIndex = 0;
            this.lblOptions.Text = "Exceptions";
            // 
            // lbxOptions
            // 
            this.lbxOptions.FormattingEnabled = true;
            this.lbxOptions.Location = new System.Drawing.Point(12, 63);
            this.lbxOptions.Name = "lbxOptions";
            this.lbxOptions.Size = new System.Drawing.Size(375, 95);
            this.lbxOptions.TabIndex = 1;
            // 
            // btnThrow
            // 
            this.btnThrow.Location = new System.Drawing.Point(397, 63);
            this.btnThrow.Name = "btnThrow";
            this.btnThrow.Size = new System.Drawing.Size(75, 95);
            this.btnThrow.TabIndex = 2;
            this.btnThrow.Text = "&Throw";
            this.btnThrow.UseVisualStyleBackColor = true;
            this.btnThrow.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbxLogEntries
            // 
            this.lbxLogEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLogEntries.FormattingEnabled = true;
            this.lbxLogEntries.Location = new System.Drawing.Point(12, 193);
            this.lbxLogEntries.Name = "lbxLogEntries";
            this.lbxLogEntries.Size = new System.Drawing.Size(632, 212);
            this.lbxLogEntries.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Demo Logger";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 417);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxLogEntries);
            this.Controls.Add(this.btnThrow);
            this.Controls.Add(this.lbxOptions);
            this.Controls.Add(this.lblOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.ListBox lbxOptions;
        private System.Windows.Forms.Button btnThrow;
        private System.Windows.Forms.ListBox lbxLogEntries;
        private System.Windows.Forms.Label label1;
    }
}

