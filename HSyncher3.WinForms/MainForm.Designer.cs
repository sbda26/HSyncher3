
namespace HSyncher3.WinForms
{
    partial class MainForm
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
            this.lblSourceDirectoryRootHeader = new System.Windows.Forms.Label();
            this.lblDestinationDirectoryRootHeader = new System.Windows.Forms.Label();
            this.lblSourceDirectoryRoot = new System.Windows.Forms.Label();
            this.lblDestinationDirectoryRoot = new System.Windows.Forms.Label();
            this.btnSourceDirectory = new System.Windows.Forms.Button();
            this.btnDestinationDirectory = new System.Windows.Forms.Button();
            this.chkDeleteDestinationFilesNotInSource = new System.Windows.Forms.CheckBox();
            this.chkBreakOnError = new System.Windows.Forms.CheckBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.pbCopy = new System.Windows.Forms.ProgressBar();
            this.chkRunAsynchronously = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSourceDirectoryRootHeader
            // 
            this.lblSourceDirectoryRootHeader.AutoSize = true;
            this.lblSourceDirectoryRootHeader.Location = new System.Drawing.Point(9, 12);
            this.lblSourceDirectoryRootHeader.Name = "lblSourceDirectoryRootHeader";
            this.lblSourceDirectoryRootHeader.Size = new System.Drawing.Size(125, 15);
            this.lblSourceDirectoryRootHeader.TabIndex = 0;
            this.lblSourceDirectoryRootHeader.Text = "Source Directory Root:";
            // 
            // lblDestinationDirectoryRootHeader
            // 
            this.lblDestinationDirectoryRootHeader.AutoSize = true;
            this.lblDestinationDirectoryRootHeader.Location = new System.Drawing.Point(9, 41);
            this.lblDestinationDirectoryRootHeader.Name = "lblDestinationDirectoryRootHeader";
            this.lblDestinationDirectoryRootHeader.Size = new System.Drawing.Size(149, 15);
            this.lblDestinationDirectoryRootHeader.TabIndex = 1;
            this.lblDestinationDirectoryRootHeader.Text = "Destination Directory Root:";
            // 
            // lblSourceDirectoryRoot
            // 
            this.lblSourceDirectoryRoot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSourceDirectoryRoot.Location = new System.Drawing.Point(167, 11);
            this.lblSourceDirectoryRoot.Name = "lblSourceDirectoryRoot";
            this.lblSourceDirectoryRoot.Size = new System.Drawing.Size(447, 23);
            this.lblSourceDirectoryRoot.TabIndex = 2;
            this.lblSourceDirectoryRoot.Text = "--";
            this.lblSourceDirectoryRoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDestinationDirectoryRoot
            // 
            this.lblDestinationDirectoryRoot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDestinationDirectoryRoot.Location = new System.Drawing.Point(167, 41);
            this.lblDestinationDirectoryRoot.Name = "lblDestinationDirectoryRoot";
            this.lblDestinationDirectoryRoot.Size = new System.Drawing.Size(447, 23);
            this.lblDestinationDirectoryRoot.TabIndex = 3;
            this.lblDestinationDirectoryRoot.Text = "--";
            this.lblDestinationDirectoryRoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSourceDirectory
            // 
            this.btnSourceDirectory.Location = new System.Drawing.Point(620, 12);
            this.btnSourceDirectory.Name = "btnSourceDirectory";
            this.btnSourceDirectory.Size = new System.Drawing.Size(36, 23);
            this.btnSourceDirectory.TabIndex = 4;
            this.btnSourceDirectory.Text = "...";
            this.btnSourceDirectory.UseVisualStyleBackColor = true;
            this.btnSourceDirectory.Click += new System.EventHandler(this.btnSourceDirectory_Click);
            // 
            // btnDestinationDirectory
            // 
            this.btnDestinationDirectory.Location = new System.Drawing.Point(620, 41);
            this.btnDestinationDirectory.Name = "btnDestinationDirectory";
            this.btnDestinationDirectory.Size = new System.Drawing.Size(36, 23);
            this.btnDestinationDirectory.TabIndex = 5;
            this.btnDestinationDirectory.Text = "...";
            this.btnDestinationDirectory.UseVisualStyleBackColor = true;
            this.btnDestinationDirectory.Click += new System.EventHandler(this.btnDestinationDirectory_Click);
            // 
            // chkDeleteDestinationFilesNotInSource
            // 
            this.chkDeleteDestinationFilesNotInSource.AutoSize = true;
            this.chkDeleteDestinationFilesNotInSource.Location = new System.Drawing.Point(9, 77);
            this.chkDeleteDestinationFilesNotInSource.Name = "chkDeleteDestinationFilesNotInSource";
            this.chkDeleteDestinationFilesNotInSource.Size = new System.Drawing.Size(385, 19);
            this.chkDeleteDestinationFilesNotInSource.TabIndex = 6;
            this.chkDeleteDestinationFilesNotInSource.Text = "Delete destination files that are not in source (unless thay are >= 4G)";
            this.chkDeleteDestinationFilesNotInSource.UseVisualStyleBackColor = true;
            // 
            // chkBreakOnError
            // 
            this.chkBreakOnError.AutoSize = true;
            this.chkBreakOnError.Location = new System.Drawing.Point(9, 102);
            this.chkBreakOnError.Name = "chkBreakOnError";
            this.chkBreakOnError.Size = new System.Drawing.Size(100, 19);
            this.chkBreakOnError.TabIndex = 7;
            this.chkBreakOnError.Text = "Break on error";
            this.chkBreakOnError.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGo.Location = new System.Drawing.Point(419, 73);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(237, 73);
            this.btnGo.TabIndex = 9;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // pbCopy
            // 
            this.pbCopy.Location = new System.Drawing.Point(9, 166);
            this.pbCopy.Name = "pbCopy";
            this.pbCopy.Size = new System.Drawing.Size(647, 55);
            this.pbCopy.TabIndex = 10;
            // 
            // chkRunAsynchronously
            // 
            this.chkRunAsynchronously.AutoSize = true;
            this.chkRunAsynchronously.Enabled = false;
            this.chkRunAsynchronously.Location = new System.Drawing.Point(9, 127);
            this.chkRunAsynchronously.Name = "chkRunAsynchronously";
            this.chkRunAsynchronously.Size = new System.Drawing.Size(133, 19);
            this.chkRunAsynchronously.TabIndex = 8;
            this.chkRunAsynchronously.Text = "Run asynchronously";
            this.chkRunAsynchronously.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 224);
            this.Controls.Add(this.pbCopy);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.chkRunAsynchronously);
            this.Controls.Add(this.chkBreakOnError);
            this.Controls.Add(this.chkDeleteDestinationFilesNotInSource);
            this.Controls.Add(this.btnDestinationDirectory);
            this.Controls.Add(this.btnSourceDirectory);
            this.Controls.Add(this.lblDestinationDirectoryRoot);
            this.Controls.Add(this.lblSourceDirectoryRoot);
            this.Controls.Add(this.lblDestinationDirectoryRootHeader);
            this.Controls.Add(this.lblSourceDirectoryRootHeader);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HSyncher3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSourceDirectoryRootHeader;
        private System.Windows.Forms.Label lblDestinationDirectoryRootHeader;
        private System.Windows.Forms.Label lblSourceDirectoryRoot;
        private System.Windows.Forms.Label lblDestinationDirectoryRoot;
        private System.Windows.Forms.Button btnSourceDirectory;
        private System.Windows.Forms.Button btnDestinationDirectory;
        private System.Windows.Forms.CheckBox chkDeleteDestinationFilesNotInSource;
        private System.Windows.Forms.CheckBox chkBreakOnError;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ProgressBar pbCopy;
        private System.Windows.Forms.CheckBox chkRunAsynchronously;
    }
}

