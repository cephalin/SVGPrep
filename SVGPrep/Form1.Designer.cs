namespace SVGPrep
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
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.ButtonSaveAs = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ALinksGrid = new System.Windows.Forms.DataGridView();
            this.ButtonDeleteTitles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRef = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InATag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ALinksGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonOpen.Location = new System.Drawing.Point(1, 1);
            this.ButtonOpen.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(103, 28);
            this.ButtonOpen.TabIndex = 1;
            this.ButtonOpen.Text = "Open SVG File";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // ButtonSaveAs
            // 
            this.ButtonSaveAs.Enabled = false;
            this.ButtonSaveAs.Location = new System.Drawing.Point(109, 1);
            this.ButtonSaveAs.Name = "ButtonSaveAs";
            this.ButtonSaveAs.Size = new System.Drawing.Size(90, 28);
            this.ButtonSaveAs.TabIndex = 2;
            this.ButtonSaveAs.Text = "Save As";
            this.ButtonSaveAs.UseVisualStyleBackColor = true;
            this.ButtonSaveAs.Click += new System.EventHandler(this.ButtonSaveAs_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Enabled = false;
            this.ButtonSave.Location = new System.Drawing.Point(205, 1);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(79, 27);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ALinksGrid
            // 
            this.ALinksGrid.AllowUserToAddRows = false;
            this.ALinksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ALinksGrid.ColumnHeadersVisible = false;
            this.ALinksGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.HRef,
            this.Title,
            this.Desc,
            this.TabIndex,
            this.InATag,
            this.Delete});
            this.ALinksGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ALinksGrid.Location = new System.Drawing.Point(0, 35);
            this.ALinksGrid.Name = "ALinksGrid";
            this.ALinksGrid.RowHeadersVisible = false;
            this.ALinksGrid.Size = new System.Drawing.Size(866, 348);
            this.ALinksGrid.TabIndex = 4;
            // 
            // ButtonDeleteTitles
            // 
            this.ButtonDeleteTitles.Location = new System.Drawing.Point(740, 1);
            this.ButtonDeleteTitles.Name = "ButtonDeleteTitles";
            this.ButtonDeleteTitles.Size = new System.Drawing.Size(114, 28);
            this.ButtonDeleteTitles.TabIndex = 5;
            this.ButtonDeleteTitles.Text = "Delete all other titles";
            this.ButtonDeleteTitles.UseVisualStyleBackColor = true;
            this.ButtonDeleteTitles.Click += new System.EventHandler(this.ButtonDeleteTitles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(457, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Every SVG file is automatically processed, such as adding cursor, target, and tab" +
    "index attributes";
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 5;
            // 
            // HRef
            // 
            this.HRef.HeaderText = "HRef";
            this.HRef.Name = "HRef";
            this.HRef.ReadOnly = true;
            this.HRef.Width = 200;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.Width = 200;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.Width = 200;
            // 
            // TabIndex
            // 
            this.TabIndex.HeaderText = "TabIndex";
            this.TabIndex.Name = "TabIndex";
            this.TabIndex.Width = 77;
            // 
            // InATag
            // 
            this.InATag.HeaderText = "Nested?";
            this.InATag.Name = "InATag";
            this.InATag.ReadOnly = true;
            this.InATag.Width = 53;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete?";
            this.Delete.Name = "Delete";
            this.Delete.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 383);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonDeleteTitles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ALinksGrid);
            this.Controls.Add(this.ButtonSaveAs);
            this.Controls.Add(this.ButtonOpen);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "SVG Prep Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ALinksGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonOpen;
        private System.Windows.Forms.Button ButtonSaveAs;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.DataGridView ALinksGrid;
        private System.Windows.Forms.Button ButtonDeleteTitles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewLinkColumn HRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TabIndex;
        private System.Windows.Forms.DataGridViewCheckBoxColumn InATag;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Delete;
    }
}

