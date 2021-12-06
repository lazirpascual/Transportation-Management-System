
namespace Transportation_Management_System
{
    partial class InvoiceInformation
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
            this.order = new System.Windows.Forms.Label();
            this.clientName = new System.Windows.Forms.Label();
            this.totalDistance = new System.Windows.Forms.Label();
            this.totalAmount = new System.Windows.Forms.Label();
            this.origin = new System.Windows.Forms.Label();
            this.destination = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // order
            // 
            this.order.Location = new System.Drawing.Point(12, 23);
            this.order.Name = "order";
            this.order.Size = new System.Drawing.Size(251, 58);
            this.order.TabIndex = 0;
            // 
            // clientName
            // 
            this.clientName.Location = new System.Drawing.Point(12, 99);
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(251, 58);
            this.clientName.TabIndex = 1;
            // 
            // totalDistance
            // 
            this.totalDistance.Location = new System.Drawing.Point(12, 157);
            this.totalDistance.Name = "totalDistance";
            this.totalDistance.Size = new System.Drawing.Size(251, 58);
            this.totalDistance.TabIndex = 2;
            // 
            // totalAmount
            // 
            this.totalAmount.Location = new System.Drawing.Point(12, 246);
            this.totalAmount.Name = "totalAmount";
            this.totalAmount.Size = new System.Drawing.Size(251, 58);
            this.totalAmount.TabIndex = 3;
            // 
            // origin
            // 
            this.origin.Location = new System.Drawing.Point(12, 292);
            this.origin.Name = "origin";
            this.origin.Size = new System.Drawing.Size(251, 58);
            this.origin.TabIndex = 4;
            // 
            // destination
            // 
            this.destination.Location = new System.Drawing.Point(12, 383);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(251, 58);
            this.destination.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(682, 418);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // InvoiceInformation
            // 
            this.ClientSize = new System.Drawing.Size(922, 496);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.destination);
            this.Controls.Add(this.origin);
            this.Controls.Add(this.totalAmount);
            this.Controls.Add(this.totalDistance);
            this.Controls.Add(this.clientName);
            this.Controls.Add(this.order);
            this.Name = "InvoiceInformation";
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Label order;
        private System.Windows.Forms.Label clientName;
        private System.Windows.Forms.Label totalDistance;
        private System.Windows.Forms.Label totalAmount;
        private System.Windows.Forms.Label origin;
        private System.Windows.Forms.Label destination;
        private System.Windows.Forms.Button saveButton;
    }
}
