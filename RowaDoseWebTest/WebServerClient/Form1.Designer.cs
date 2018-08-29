namespace WebServerClient
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.show_confirmation_label = new System.Windows.Forms.Label();
            this.setting_save_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.port_textbox = new System.Windows.Forms.TextBox();
            this.ip_textbox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.add_canister_button = new System.Windows.Forms.Button();
            this.add_order_button = new System.Windows.Forms.Button();
            this.add_manufacturer_button = new System.Windows.Forms.Button();
            this.add_medicine_button = new System.Windows.Forms.Button();
            this.get_all_canister_button = new System.Windows.Forms.Button();
            this.get_all_orders_button = new System.Windows.Forms.Button();
            this.get_all_manufacturer_button = new System.Windows.Forms.Button();
            this.get_all_medicine_button = new System.Windows.Forms.Button();
            this.json_textbox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.show_confirmation_label);
            this.groupBox1.Controls.Add(this.setting_save_button);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.port_textbox);
            this.groupBox1.Controls.Add(this.ip_textbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Web service configuration";
            // 
            // show_confirmation_label
            // 
            this.show_confirmation_label.AutoSize = true;
            this.show_confirmation_label.ForeColor = System.Drawing.Color.DarkGreen;
            this.show_confirmation_label.Location = new System.Drawing.Point(487, 35);
            this.show_confirmation_label.Name = "show_confirmation_label";
            this.show_confirmation_label.Size = new System.Drawing.Size(72, 13);
            this.show_confirmation_label.TabIndex = 5;
            this.show_confirmation_label.Text = "Setting saved";
            this.show_confirmation_label.Visible = false;
            // 
            // setting_save_button
            // 
            this.setting_save_button.Location = new System.Drawing.Point(376, 28);
            this.setting_save_button.Name = "setting_save_button";
            this.setting_save_button.Size = new System.Drawing.Size(75, 21);
            this.setting_save_button.TabIndex = 4;
            this.setting_save_button.Text = "Save";
            this.setting_save_button.UseVisualStyleBackColor = true;
            this.setting_save_button.Click += new System.EventHandler(this.setting_save_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // port_textbox
            // 
            this.port_textbox.Location = new System.Drawing.Point(245, 28);
            this.port_textbox.Name = "port_textbox";
            this.port_textbox.Size = new System.Drawing.Size(100, 20);
            this.port_textbox.TabIndex = 1;
            this.port_textbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.port_textbox_KeyUp);
            // 
            // ip_textbox
            // 
            this.ip_textbox.Location = new System.Drawing.Point(39, 29);
            this.ip_textbox.Name = "ip_textbox";
            this.ip_textbox.Size = new System.Drawing.Size(158, 20);
            this.ip_textbox.TabIndex = 0;
            this.ip_textbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ip_textbox_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.add_canister_button);
            this.groupBox2.Controls.Add(this.add_order_button);
            this.groupBox2.Controls.Add(this.add_manufacturer_button);
            this.groupBox2.Controls.Add(this.add_medicine_button);
            this.groupBox2.Controls.Add(this.get_all_canister_button);
            this.groupBox2.Controls.Add(this.get_all_orders_button);
            this.groupBox2.Controls.Add(this.get_all_manufacturer_button);
            this.groupBox2.Controls.Add(this.get_all_medicine_button);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(652, 222);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Web Calls";
            // 
            // add_canister_button
            // 
            this.add_canister_button.Location = new System.Drawing.Point(333, 184);
            this.add_canister_button.Name = "add_canister_button";
            this.add_canister_button.Size = new System.Drawing.Size(191, 23);
            this.add_canister_button.TabIndex = 8;
            this.add_canister_button.Text = "Add Canister";
            this.add_canister_button.UseVisualStyleBackColor = true;
            this.add_canister_button.Click += new System.EventHandler(this.add_canister_button_Click);
            // 
            // add_order_button
            // 
            this.add_order_button.Location = new System.Drawing.Point(333, 136);
            this.add_order_button.Name = "add_order_button";
            this.add_order_button.Size = new System.Drawing.Size(191, 23);
            this.add_order_button.TabIndex = 7;
            this.add_order_button.Text = "Add Order";
            this.add_order_button.UseVisualStyleBackColor = true;
            this.add_order_button.Click += new System.EventHandler(this.add_order_button_Click);
            // 
            // add_manufacturer_button
            // 
            this.add_manufacturer_button.Location = new System.Drawing.Point(333, 92);
            this.add_manufacturer_button.Name = "add_manufacturer_button";
            this.add_manufacturer_button.Size = new System.Drawing.Size(191, 23);
            this.add_manufacturer_button.TabIndex = 6;
            this.add_manufacturer_button.Text = "Add Manufacturer";
            this.add_manufacturer_button.UseVisualStyleBackColor = true;
            this.add_manufacturer_button.Click += new System.EventHandler(this.add_manufacturer_button_Click);
            // 
            // add_medicine_button
            // 
            this.add_medicine_button.Location = new System.Drawing.Point(333, 44);
            this.add_medicine_button.Name = "add_medicine_button";
            this.add_medicine_button.Size = new System.Drawing.Size(191, 23);
            this.add_medicine_button.TabIndex = 5;
            this.add_medicine_button.Text = "Add Medicine";
            this.add_medicine_button.UseVisualStyleBackColor = true;
            this.add_medicine_button.Click += new System.EventHandler(this.add_medicine_button_Click);
            // 
            // get_all_canister_button
            // 
            this.get_all_canister_button.Location = new System.Drawing.Point(18, 184);
            this.get_all_canister_button.Name = "get_all_canister_button";
            this.get_all_canister_button.Size = new System.Drawing.Size(178, 23);
            this.get_all_canister_button.TabIndex = 3;
            this.get_all_canister_button.Text = "Get all Canister";
            this.get_all_canister_button.UseVisualStyleBackColor = true;
            this.get_all_canister_button.Click += new System.EventHandler(this.get_all_canister_button_Click);
            // 
            // get_all_orders_button
            // 
            this.get_all_orders_button.Location = new System.Drawing.Point(18, 136);
            this.get_all_orders_button.Name = "get_all_orders_button";
            this.get_all_orders_button.Size = new System.Drawing.Size(178, 23);
            this.get_all_orders_button.TabIndex = 2;
            this.get_all_orders_button.Text = "Get all Orders";
            this.get_all_orders_button.UseVisualStyleBackColor = true;
            this.get_all_orders_button.Click += new System.EventHandler(this.get_all_orders_button_Click);
            // 
            // get_all_manufacturer_button
            // 
            this.get_all_manufacturer_button.Location = new System.Drawing.Point(18, 92);
            this.get_all_manufacturer_button.Name = "get_all_manufacturer_button";
            this.get_all_manufacturer_button.Size = new System.Drawing.Size(178, 23);
            this.get_all_manufacturer_button.TabIndex = 1;
            this.get_all_manufacturer_button.Text = "Get Manufacturers";
            this.get_all_manufacturer_button.UseVisualStyleBackColor = true;
            this.get_all_manufacturer_button.Click += new System.EventHandler(this.get_all_manufacturer_button_Click);
            // 
            // get_all_medicine_button
            // 
            this.get_all_medicine_button.Location = new System.Drawing.Point(18, 44);
            this.get_all_medicine_button.Name = "get_all_medicine_button";
            this.get_all_medicine_button.Size = new System.Drawing.Size(178, 23);
            this.get_all_medicine_button.TabIndex = 0;
            this.get_all_medicine_button.Text = "Get all Medicine";
            this.get_all_medicine_button.UseVisualStyleBackColor = true;
            this.get_all_medicine_button.Click += new System.EventHandler(this.get_all_medicine_button_Click);
            // 
            // json_textbox
            // 
            this.json_textbox.Location = new System.Drawing.Point(13, 341);
            this.json_textbox.Multiline = true;
            this.json_textbox.Name = "json_textbox";
            this.json_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.json_textbox.Size = new System.Drawing.Size(652, 397);
            this.json_textbox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 748);
            this.Controls.Add(this.json_textbox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Web server test client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button setting_save_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox port_textbox;
        private System.Windows.Forms.TextBox ip_textbox;
        private System.Windows.Forms.Label show_confirmation_label;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox json_textbox;
        private System.Windows.Forms.Button add_canister_button;
        private System.Windows.Forms.Button add_order_button;
        private System.Windows.Forms.Button add_manufacturer_button;
        private System.Windows.Forms.Button add_medicine_button;
        private System.Windows.Forms.Button get_all_canister_button;
        private System.Windows.Forms.Button get_all_orders_button;
        private System.Windows.Forms.Button get_all_manufacturer_button;
        private System.Windows.Forms.Button get_all_medicine_button;
    }
}

