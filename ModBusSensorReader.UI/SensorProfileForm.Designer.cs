namespace ModbusSensorReader.UI
{
    partial class SensorProfileForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtSensorName = new TextBox();
            txtSlaveId = new TextBox();
            txtParameterName = new TextBox();
            btnAddParam = new Button();
            lstParameters = new ListBox();
            btnRemoveParam = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 31);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 0;
            label1.Text = "Sensör Adı: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 75);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 1;
            label2.Text = "Slave ID: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 122);
            label3.Name = "label3";
            label3.Size = new Size(110, 20);
            label3.TabIndex = 2;
            label3.Text = "Parametre Adı: ";
            // 
            // txtSensorName
            // 
            txtSensorName.Location = new Point(167, 28);
            txtSensorName.Name = "txtSensorName";
            txtSensorName.Size = new Size(125, 27);
            txtSensorName.TabIndex = 3;
            // 
            // txtSlaveId
            // 
            txtSlaveId.Location = new Point(167, 75);
            txtSlaveId.Name = "txtSlaveId";
            txtSlaveId.Size = new Size(125, 27);
            txtSlaveId.TabIndex = 4;
            // 
            // txtParameterName
            // 
            txtParameterName.Location = new Point(167, 119);
            txtParameterName.Name = "txtParameterName";
            txtParameterName.Size = new Size(125, 27);
            txtParameterName.TabIndex = 5;
            // 
            // btnAddParam
            // 
            btnAddParam.Location = new Point(315, 118);
            btnAddParam.Name = "btnAddParam";
            btnAddParam.Size = new Size(124, 29);
            btnAddParam.TabIndex = 6;
            btnAddParam.Text = "Parametre Ekle";
            btnAddParam.UseVisualStyleBackColor = true;
            btnAddParam.Click += btnAddParam_Click;
            // 
            // lstParameters
            // 
            lstParameters.FormattingEnabled = true;
            lstParameters.Location = new Point(44, 186);
            lstParameters.Name = "lstParameters";
            lstParameters.Size = new Size(248, 104);
            lstParameters.TabIndex = 7;
            // 
            // btnRemoveParam
            // 
            btnRemoveParam.Location = new Point(315, 186);
            btnRemoveParam.Name = "btnRemoveParam";
            btnRemoveParam.Size = new Size(124, 29);
            btnRemoveParam.TabIndex = 8;
            btnRemoveParam.Text = "Parametre Sil";
            btnRemoveParam.UseVisualStyleBackColor = true;
            btnRemoveParam.Click += btnRemoveParam_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(315, 221);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(124, 29);
            btnSave.TabIndex = 9;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(315, 256);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(124, 29);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Ana Ekrana Dön";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SensorProfileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 328);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnRemoveParam);
            Controls.Add(lstParameters);
            Controls.Add(btnAddParam);
            Controls.Add(txtParameterName);
            Controls.Add(txtSlaveId);
            Controls.Add(txtSensorName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SensorProfileForm";
            Text = "Sensör Ekle";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtSensorName;
        private TextBox txtSlaveId;
        private TextBox txtParameterName;
        private Button btnAddParam;
        private ListBox lstParameters;
        private Button btnRemoveParam;
        private Button btnSave;
        private Button btnCancel;
    }
}