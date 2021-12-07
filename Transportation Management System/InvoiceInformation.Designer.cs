
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
            this.totalAmount = new System.Windows.Forms.Label();
            this.origin = new System.Windows.Forms.Label();
            this.destination = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalDistance = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // order
            // 
            this.order.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order.Location = new System.Drawing.Point(12, 84);
            this.order.Name = "order";
            this.order.Size = new System.Drawing.Size(251, 58);
            this.order.TabIndex = 0;
            // 
            // clientName
            // 
            this.clientName.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientName.Location = new System.Drawing.Point(12, 131);
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(251, 58);
            this.clientName.TabIndex = 1;
            // 
            // totalAmount
            // 
            this.totalAmount.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmount.Location = new System.Drawing.Point(12, 341);
            this.totalAmount.Name = "totalAmount";
            this.totalAmount.Size = new System.Drawing.Size(251, 58);
            this.totalAmount.TabIndex = 3;
            // 
            // origin
            // 
            this.origin.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origin.Location = new System.Drawing.Point(12, 178);
            this.origin.Name = "origin";
            this.origin.Size = new System.Drawing.Size(251, 58);
            this.origin.TabIndex = 4;
            // 
            // destination
            // 
            this.destination.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destination.Location = new System.Drawing.Point(12, 225);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(251, 58);
            this.destination.TabIndex = 5;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(375, 443);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(103, 41);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Omnicorp Shipping Handling and Transportation";
            // 
            // totalDistance
            // 
            this.totalDistance.Font = new System.Drawing.Font("Britannic Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalDistance.Location = new System.Drawing.Point(12, 283);
            this.totalDistance.Name = "totalDistance";
            this.totalDistance.Size = new System.Drawing.Size(251, 58);
            this.totalDistance.TabIndex = 8;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(484, 443);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(103, 41);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InvoiceInformation
            // 
            this.ClientSize = new System.Drawing.Size(597, 495);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.totalDistance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.destination);
            this.Controls.Add(this.origin);
            this.Controls.Add(this.totalAmount);
            this.Controls.Add(this.clientName);
            this.Controls.Add(this.order);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "InvoiceInformation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label order;
        private System.Windows.Forms.Label clientName;
        private System.Windows.Forms.Label totalAmount;
        private System.Windows.Forms.Label origin;
        private System.Windows.Forms.Label destination;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalDistance;
        private System.Windows.Forms.Button CloseButton;
    }
}
