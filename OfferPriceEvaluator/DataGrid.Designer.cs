namespace OfferPriceEvaluator
{
    partial class DataGrid
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
            this.grdOffers = new System.Windows.Forms.DataGridView();
            this.btnDownload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdOffers)).BeginInit();
            this.SuspendLayout();
            // 
            // grdOffers
            // 
            this.grdOffers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOffers.Location = new System.Drawing.Point(12, 12);
            this.grdOffers.Name = "grdOffers";
            this.grdOffers.Size = new System.Drawing.Size(1245, 570);
            this.grdOffers.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(536, 588);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(202, 32);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "TakeOffers";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // DataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 632);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.grdOffers);
            this.Name = "DataGrid";
            this.Text = "DataGrid";
            ((System.ComponentModel.ISupportInitialize)(this.grdOffers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdOffers;
        private System.Windows.Forms.Button btnDownload;
    }
}