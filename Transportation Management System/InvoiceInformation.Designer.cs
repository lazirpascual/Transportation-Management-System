
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
            this.order.BackColor = System.Drawing.Color.Transparent;
            this.order.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order.ForeColor = System.Drawing.Color.DarkBlue;
            this.order.Location = new System.Drawing.Point(88, 74);
            this.order.Name = "order";
            this.order.Size = new System.Drawing.Size(366, 58);
            this.order.TabIndex = 0;
            // 
            // clientName
            // 
            this.clientName.BackColor = System.Drawing.Color.Transparent;
            this.clientName.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientName.ForeColor = System.Drawing.Color.DarkBlue;
            this.clientName.Location = new System.Drawing.Point(88, 132);
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(366, 58);
            this.clientName.TabIndex = 1;
            // 
            // totalAmount
            // 
            this.totalAmount.BackColor = System.Drawing.Color.Transparent;
            this.totalAmount.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmount.ForeColor = System.Drawing.Color.DarkBlue;
            this.totalAmount.Location = new System.Drawing.Point(88, 287);
            this.totalAmount.Name = "totalAmount";
            this.totalAmount.Size = new System.Drawing.Size(366, 44);
            this.totalAmount.TabIndex = 3;
            // 
            // origin
            // 
            this.origin.BackColor = System.Drawing.Color.Transparent;
            this.origin.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origin.ForeColor = System.Drawing.Color.DarkBlue;
            this.origin.Location = new System.Drawing.Point(88, 179);
            this.origin.Name = "origin";
            this.origin.Size = new System.Drawing.Size(366, 58);
            this.origin.TabIndex = 4;
            // 
            // destination
            // 
            this.destination.BackColor = System.Drawing.Color.Transparent;
            this.destination.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destination.ForeColor = System.Drawing.Color.DarkBlue;
            this.destination.Location = new System.Drawing.Point(88, 226);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(366, 58);
            this.destination.TabIndex = 5;
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveButton.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.DarkBlue;
            this.SaveButton.Location = new System.Drawing.Point(332, 390);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(103, 41);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Elephant", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(532, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "Omnicorp Shipping Handling and Transportation";
            // 
            // totalDistance
            // 
            this.totalDistance.BackColor = System.Drawing.Color.Transparent;
            this.totalDistance.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalDistance.ForeColor = System.Drawing.Color.DarkBlue;
            this.totalDistance.Location = new System.Drawing.Point(88, 331);
            this.totalDistance.Name = "totalDistance";
            this.totalDistance.Size = new System.Drawing.Size(366, 46);
            this.totalDistance.TabIndex = 8;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseButton.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.DarkBlue;
            this.CloseButton.Location = new System.Drawing.Point(441, 390);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(103, 41);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InvoiceInformation
            // 
            this.ClientSize = new System.Drawing.Size(553, 441);
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
