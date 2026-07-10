namespace ModBusSensorReader.UI;

partial class Form1
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox1 = new GroupBox();
        tlpConnection = new TableLayoutPanel();
        label1 = new Label();
        cmbConnectionType = new ComboBox();
        label4 = new Label();
        cmbPortName = new ComboBox();
        label3 = new Label();
        txtIpAddress = new TextBox();
        label7 = new Label();
        txtTCPPort = new TextBox();
        label2 = new Label();
        cmbBaudRate = new ComboBox();
        label5 = new Label();
        cmbDataBits = new ComboBox();
        label6 = new Label();
        cmbParity = new ComboBox();
        StopBits = new Label();
        cmbStopBits = new ComboBox();
        btnConnected = new Button();
        btnDisconnected = new Button();
        groupBox2 = new GroupBox();
        tlpModBus = new TableLayoutPanel();
        txtSamplingTime = new TextBox();
        label16 = new Label();
        btnWrite = new Button();
        btnStop = new Button();
        label8 = new Label();
        txtSlaveId = new TextBox();
        btnRead = new Button();
        label9 = new Label();
        label10 = new Label();
        txtWriteValue = new TextBox();
        cmbFuncCode = new ComboBox();
        label14 = new Label();
        txtRegisterCount = new TextBox();
        txtWriteAddress = new TextBox();
        txtStartAddress = new TextBox();
        label13 = new Label();
        label12 = new Label();
        cmbDataType = new ComboBox();
        label11 = new Label();
        btnStartRead = new Button();
        cmbWriteDataType = new ComboBox();
        label15 = new Label();
        groupBox5 = new GroupBox();
        tableLayoutPanel2 = new TableLayoutPanel();
        label17 = new Label();
        txtSensorName = new TextBox();
        label18 = new Label();
        txtSensorSlave = new TextBox();
        lblParameterTitle = new Label();
        dgvSensorParameters = new DataGridView();
        buttonPanel = new TableLayoutPanel();
        btnAddSensor = new Button();
        btnSaveProfile = new Button();
        grpGraph = new GroupBox();
        grpSensorValues = new GroupBox();
        cmbSensorList = new ComboBox();
        lblSensorValue = new Label();
        lblSensorLastRead = new Label();
        lblActiveSensor = new Label();
        lblRawRegister = new Label();
        groupBox4 = new GroupBox();
        tlpLog = new TableLayoutPanel();
        btnExportExcel = new Button();
        txtLog = new TextBox();
        chcLogToFile = new CheckBox();
        btnSelectLogFolder = new Button();
        lblFilePath = new Label();
        tableLayoutPanel1.SuspendLayout();
        groupBox1.SuspendLayout();
        tlpConnection.SuspendLayout();
        groupBox2.SuspendLayout();
        tlpModBus.SuspendLayout();
        groupBox5.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSensorParameters).BeginInit();
        buttonPanel.SuspendLayout();
        grpSensorValues.SuspendLayout();
        groupBox4.SuspendLayout();
        tlpLog.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 4;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.4707813F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.587F));
        tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
        tableLayoutPanel1.Controls.Add(grpGraph, 2, 0);
        tableLayoutPanel1.Controls.Add(grpSensorValues, 2, 1);
        tableLayoutPanel1.Controls.Add(groupBox4, 3, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 54.9120979F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.0879021F));
        tableLayoutPanel1.Size = new Size(1523, 967);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(tlpConnection);
        groupBox1.Dock = DockStyle.Fill;
        groupBox1.Location = new Point(3, 3);
        groupBox1.Name = "groupBox1";
        tableLayoutPanel1.SetRowSpan(groupBox1, 2);
        groupBox1.Size = new Size(420, 961);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "Bağlantı Ayarları";
        // 
        // tlpConnection
        // 
        tlpConnection.BackgroundImage = ModbusSensorReader.UI.Properties.Resources.yiva;
        tlpConnection.BackgroundImageLayout = ImageLayout.Center;
        tlpConnection.ColumnCount = 2;
        tlpConnection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpConnection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        tlpConnection.Controls.Add(label1, 0, 0);
        tlpConnection.Controls.Add(cmbConnectionType, 1, 0);
        tlpConnection.Controls.Add(label4, 0, 1);
        tlpConnection.Controls.Add(cmbPortName, 1, 1);
        tlpConnection.Controls.Add(label3, 0, 2);
        tlpConnection.Controls.Add(txtIpAddress, 1, 2);
        tlpConnection.Controls.Add(label7, 0, 3);
        tlpConnection.Controls.Add(txtTCPPort, 1, 3);
        tlpConnection.Controls.Add(label2, 0, 4);
        tlpConnection.Controls.Add(cmbBaudRate, 1, 4);
        tlpConnection.Controls.Add(label5, 0, 5);
        tlpConnection.Controls.Add(cmbDataBits, 1, 5);
        tlpConnection.Controls.Add(label6, 0, 6);
        tlpConnection.Controls.Add(cmbParity, 1, 6);
        tlpConnection.Controls.Add(StopBits, 0, 7);
        tlpConnection.Controls.Add(cmbStopBits, 1, 7);
        tlpConnection.Controls.Add(btnConnected, 0, 9);
        tlpConnection.Controls.Add(btnDisconnected, 1, 9);
        tlpConnection.Dock = DockStyle.Fill;
        tlpConnection.Location = new Point(3, 23);
        tlpConnection.Name = "tlpConnection";
        tlpConnection.RowCount = 10;
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tlpConnection.Size = new Size(414, 935);
        tlpConnection.TabIndex = 0;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = DockStyle.Fill;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(174, 40);
        label1.TabIndex = 21;
        label1.Text = "Bağlantı Tipi";
        // 
        // cmbConnectionType
        // 
        cmbConnectionType.Dock = DockStyle.Fill;
        cmbConnectionType.FormattingEnabled = true;
        cmbConnectionType.Location = new Point(183, 3);
        cmbConnectionType.Name = "cmbConnectionType";
        cmbConnectionType.Size = new Size(228, 28);
        cmbConnectionType.TabIndex = 22;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Dock = DockStyle.Fill;
        label4.Location = new Point(3, 40);
        label4.Name = "label4";
        label4.Size = new Size(174, 40);
        label4.TabIndex = 26;
        label4.Text = "COM Port";
        // 
        // cmbPortName
        // 
        cmbPortName.Dock = DockStyle.Fill;
        cmbPortName.FormattingEnabled = true;
        cmbPortName.Location = new Point(183, 43);
        cmbPortName.Name = "cmbPortName";
        cmbPortName.Size = new Size(228, 28);
        cmbPortName.TabIndex = 27;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(3, 80);
        label3.Name = "label3";
        label3.Size = new Size(174, 40);
        label3.TabIndex = 25;
        label3.Text = "IP Adresi";
        // 
        // txtIpAddress
        // 
        txtIpAddress.Dock = DockStyle.Fill;
        txtIpAddress.Location = new Point(183, 83);
        txtIpAddress.Name = "txtIpAddress";
        txtIpAddress.Size = new Size(228, 27);
        txtIpAddress.TabIndex = 28;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Dock = DockStyle.Fill;
        label7.Location = new Point(3, 120);
        label7.Name = "label7";
        label7.Size = new Size(174, 40);
        label7.TabIndex = 35;
        label7.Text = "TCP Port";
        // 
        // txtTCPPort
        // 
        txtTCPPort.Dock = DockStyle.Fill;
        txtTCPPort.Location = new Point(183, 123);
        txtTCPPort.Name = "txtTCPPort";
        txtTCPPort.Size = new Size(228, 27);
        txtTCPPort.TabIndex = 36;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(3, 160);
        label2.Name = "label2";
        label2.Size = new Size(174, 40);
        label2.TabIndex = 23;
        label2.Text = "BaudRate";
        // 
        // cmbBaudRate
        // 
        cmbBaudRate.Dock = DockStyle.Fill;
        cmbBaudRate.FormattingEnabled = true;
        cmbBaudRate.Location = new Point(183, 163);
        cmbBaudRate.Name = "cmbBaudRate";
        cmbBaudRate.Size = new Size(228, 28);
        cmbBaudRate.TabIndex = 37;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Dock = DockStyle.Fill;
        label5.Location = new Point(3, 200);
        label5.Name = "label5";
        label5.Size = new Size(174, 40);
        label5.TabIndex = 29;
        label5.Text = "DataBits";
        // 
        // cmbDataBits
        // 
        cmbDataBits.Dock = DockStyle.Fill;
        cmbDataBits.FormattingEnabled = true;
        cmbDataBits.Location = new Point(183, 203);
        cmbDataBits.Name = "cmbDataBits";
        cmbDataBits.Size = new Size(228, 28);
        cmbDataBits.TabIndex = 38;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Dock = DockStyle.Fill;
        label6.Location = new Point(3, 240);
        label6.Name = "label6";
        label6.Size = new Size(174, 40);
        label6.TabIndex = 30;
        label6.Text = "Parity";
        // 
        // cmbParity
        // 
        cmbParity.Dock = DockStyle.Fill;
        cmbParity.FormattingEnabled = true;
        cmbParity.Location = new Point(183, 243);
        cmbParity.Name = "cmbParity";
        cmbParity.Size = new Size(228, 28);
        cmbParity.TabIndex = 24;
        // 
        // StopBits
        // 
        StopBits.AutoSize = true;
        StopBits.Dock = DockStyle.Fill;
        StopBits.Location = new Point(3, 280);
        StopBits.Name = "StopBits";
        StopBits.Size = new Size(174, 40);
        StopBits.TabIndex = 32;
        StopBits.Text = "Stop Bits";
        // 
        // cmbStopBits
        // 
        cmbStopBits.Dock = DockStyle.Fill;
        cmbStopBits.FormattingEnabled = true;
        cmbStopBits.Location = new Point(183, 283);
        cmbStopBits.Name = "cmbStopBits";
        cmbStopBits.Size = new Size(228, 28);
        cmbStopBits.TabIndex = 31;
        // 
        // btnConnected
        // 
        btnConnected.Dock = DockStyle.Fill;
        btnConnected.Location = new Point(6, 891);
        btnConnected.Margin = new Padding(6);
        btnConnected.Name = "btnConnected";
        btnConnected.Size = new Size(168, 38);
        btnConnected.TabIndex = 38;
        btnConnected.Text = "Bağlan";
        btnConnected.UseVisualStyleBackColor = true;
        // 
        // btnDisconnected
        // 
        btnDisconnected.Dock = DockStyle.Fill;
        btnDisconnected.Location = new Point(186, 891);
        btnDisconnected.Margin = new Padding(6);
        btnDisconnected.Name = "btnDisconnected";
        btnDisconnected.Size = new Size(222, 38);
        btnDisconnected.TabIndex = 39;
        btnDisconnected.Text = "Bağlantıyı Kes";
        btnDisconnected.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(tlpModBus);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Location = new Point(429, 3);
        groupBox2.Name = "groupBox2";
        tableLayoutPanel1.SetRowSpan(groupBox2, 2);
        groupBox2.Size = new Size(420, 961);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "ModBus İşlem Ayarları";
        // 
        // tlpModBus
        // 
        tlpModBus.ColumnCount = 2;
        tlpModBus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpModBus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        tlpModBus.Controls.Add(txtSamplingTime, 1, 3);
        tlpModBus.Controls.Add(label16, 0, 3);
        tlpModBus.Controls.Add(btnWrite, 1, 10);
        tlpModBus.Controls.Add(btnStop, 1, 11);
        tlpModBus.Controls.Add(label8, 0, 0);
        tlpModBus.Controls.Add(txtSlaveId, 1, 0);
        tlpModBus.Controls.Add(btnRead, 0, 10);
        tlpModBus.Controls.Add(label9, 0, 1);
        tlpModBus.Controls.Add(label10, 0, 2);
        tlpModBus.Controls.Add(txtWriteValue, 1, 7);
        tlpModBus.Controls.Add(cmbFuncCode, 1, 1);
        tlpModBus.Controls.Add(label14, 0, 7);
        tlpModBus.Controls.Add(txtRegisterCount, 1, 2);
        tlpModBus.Controls.Add(txtWriteAddress, 1, 6);
        tlpModBus.Controls.Add(txtStartAddress, 1, 4);
        tlpModBus.Controls.Add(label13, 0, 6);
        tlpModBus.Controls.Add(label12, 0, 5);
        tlpModBus.Controls.Add(cmbDataType, 1, 5);
        tlpModBus.Controls.Add(label11, 0, 4);
        tlpModBus.Controls.Add(btnStartRead, 0, 11);
        tlpModBus.Controls.Add(cmbWriteDataType, 1, 8);
        tlpModBus.Controls.Add(label15, 0, 8);
        tlpModBus.Controls.Add(groupBox5, 0, 9);
        tlpModBus.Dock = DockStyle.Fill;
        tlpModBus.Location = new Point(3, 23);
        tlpModBus.Name = "tlpModBus";
        tlpModBus.RowCount = 12;
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlpModBus.Size = new Size(414, 935);
        tlpModBus.TabIndex = 0;
        // 
        // txtSamplingTime
        // 
        txtSamplingTime.Dock = DockStyle.Fill;
        txtSamplingTime.Location = new Point(183, 123);
        txtSamplingTime.Name = "txtSamplingTime";
        txtSamplingTime.Size = new Size(228, 27);
        txtSamplingTime.TabIndex = 37;
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Dock = DockStyle.Fill;
        label16.Location = new Point(3, 120);
        label16.Name = "label16";
        label16.Size = new Size(174, 40);
        label16.TabIndex = 36;
        label16.Text = "Sampling Time";
        // 
        // btnWrite
        // 
        btnWrite.Dock = DockStyle.Fill;
        btnWrite.Location = new Point(183, 851);
        btnWrite.Name = "btnWrite";
        btnWrite.Size = new Size(228, 37);
        btnWrite.TabIndex = 35;
        btnWrite.Text = "Yaz";
        btnWrite.UseVisualStyleBackColor = true;
        // 
        // btnStop
        // 
        btnStop.Dock = DockStyle.Fill;
        btnStop.Location = new Point(183, 894);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(228, 38);
        btnStop.TabIndex = 27;
        btnStop.Text = "Okumayı Durdur";
        btnStop.UseVisualStyleBackColor = true;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Dock = DockStyle.Fill;
        label8.Location = new Point(3, 0);
        label8.Name = "label8";
        label8.Size = new Size(174, 40);
        label8.TabIndex = 9;
        label8.Text = "Slave ID";
        // 
        // txtSlaveId
        // 
        txtSlaveId.Dock = DockStyle.Fill;
        txtSlaveId.Location = new Point(183, 3);
        txtSlaveId.Name = "txtSlaveId";
        txtSlaveId.Size = new Size(228, 27);
        txtSlaveId.TabIndex = 10;
        // 
        // btnRead
        // 
        btnRead.Dock = DockStyle.Fill;
        btnRead.Location = new Point(3, 851);
        btnRead.Name = "btnRead";
        btnRead.Size = new Size(174, 37);
        btnRead.TabIndex = 19;
        btnRead.Text = "Oku";
        btnRead.UseVisualStyleBackColor = true;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Dock = DockStyle.Fill;
        label9.Location = new Point(3, 40);
        label9.Name = "label9";
        label9.Size = new Size(174, 40);
        label9.TabIndex = 19;
        label9.Text = "Function Code";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Dock = DockStyle.Fill;
        label10.Location = new Point(3, 80);
        label10.Name = "label10";
        label10.Size = new Size(174, 40);
        label10.TabIndex = 21;
        label10.Text = "Register Count";
        // 
        // txtWriteValue
        // 
        txtWriteValue.Dock = DockStyle.Fill;
        txtWriteValue.Location = new Point(183, 283);
        txtWriteValue.Name = "txtWriteValue";
        txtWriteValue.Size = new Size(228, 27);
        txtWriteValue.TabIndex = 32;
        // 
        // cmbFuncCode
        // 
        cmbFuncCode.Dock = DockStyle.Fill;
        cmbFuncCode.FormattingEnabled = true;
        cmbFuncCode.Location = new Point(183, 43);
        cmbFuncCode.Name = "cmbFuncCode";
        cmbFuncCode.Size = new Size(228, 28);
        cmbFuncCode.TabIndex = 20;
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Dock = DockStyle.Fill;
        label14.Location = new Point(3, 280);
        label14.Name = "label14";
        label14.Size = new Size(174, 40);
        label14.TabIndex = 31;
        label14.Text = "W-Value";
        // 
        // txtRegisterCount
        // 
        txtRegisterCount.Dock = DockStyle.Fill;
        txtRegisterCount.Location = new Point(183, 83);
        txtRegisterCount.Name = "txtRegisterCount";
        txtRegisterCount.Size = new Size(228, 27);
        txtRegisterCount.TabIndex = 22;
        // 
        // txtWriteAddress
        // 
        txtWriteAddress.Dock = DockStyle.Fill;
        txtWriteAddress.Location = new Point(183, 243);
        txtWriteAddress.Name = "txtWriteAddress";
        txtWriteAddress.Size = new Size(228, 27);
        txtWriteAddress.TabIndex = 30;
        // 
        // txtStartAddress
        // 
        txtStartAddress.Dock = DockStyle.Fill;
        txtStartAddress.Location = new Point(183, 163);
        txtStartAddress.Name = "txtStartAddress";
        txtStartAddress.Size = new Size(228, 27);
        txtStartAddress.TabIndex = 24;
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Dock = DockStyle.Fill;
        label13.Location = new Point(3, 240);
        label13.Name = "label13";
        label13.Size = new Size(174, 40);
        label13.TabIndex = 29;
        label13.Text = "W-Address";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Dock = DockStyle.Fill;
        label12.Location = new Point(3, 200);
        label12.Name = "label12";
        label12.Size = new Size(174, 40);
        label12.TabIndex = 25;
        label12.Text = "R-Data Type";
        // 
        // cmbDataType
        // 
        cmbDataType.Dock = DockStyle.Fill;
        cmbDataType.FormattingEnabled = true;
        cmbDataType.Location = new Point(183, 203);
        cmbDataType.Name = "cmbDataType";
        cmbDataType.Size = new Size(228, 28);
        cmbDataType.TabIndex = 26;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Dock = DockStyle.Fill;
        label11.Location = new Point(3, 160);
        label11.Name = "label11";
        label11.Size = new Size(174, 40);
        label11.TabIndex = 23;
        label11.Text = "R-Start Address";
        // 
        // btnStartRead
        // 
        btnStartRead.Dock = DockStyle.Fill;
        btnStartRead.Location = new Point(3, 894);
        btnStartRead.Name = "btnStartRead";
        btnStartRead.Size = new Size(174, 38);
        btnStartRead.TabIndex = 28;
        btnStartRead.Text = "Sürekli Okuma Başlat";
        btnStartRead.UseVisualStyleBackColor = true;
        // 
        // cmbWriteDataType
        // 
        cmbWriteDataType.Dock = DockStyle.Fill;
        cmbWriteDataType.FormattingEnabled = true;
        cmbWriteDataType.Location = new Point(183, 323);
        cmbWriteDataType.Name = "cmbWriteDataType";
        cmbWriteDataType.Size = new Size(228, 28);
        cmbWriteDataType.TabIndex = 34;
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Dock = DockStyle.Fill;
        label15.Location = new Point(3, 320);
        label15.Name = "label15";
        label15.Size = new Size(174, 40);
        label15.TabIndex = 33;
        label15.Text = "W-Data Type";
        // 
        // groupBox5
        // 
        tlpModBus.SetColumnSpan(groupBox5, 2);
        groupBox5.Controls.Add(tableLayoutPanel2);
        groupBox5.Dock = DockStyle.Fill;
        groupBox5.Location = new Point(3, 363);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(408, 482);
        groupBox5.TabIndex = 38;
        groupBox5.TabStop = false;
        groupBox5.Text = "Sensör Profili";
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        tableLayoutPanel2.Controls.Add(label17, 0, 0);
        tableLayoutPanel2.Controls.Add(txtSensorName, 1, 0);
        tableLayoutPanel2.Controls.Add(label18, 0, 1);
        tableLayoutPanel2.Controls.Add(txtSensorSlave, 1, 1);
        tableLayoutPanel2.Controls.Add(lblParameterTitle, 0, 2);
        tableLayoutPanel2.Controls.Add(dgvSensorParameters, 0, 3);
        tableLayoutPanel2.Controls.Add(buttonPanel, 0, 4);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(3, 23);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 5;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        tableLayoutPanel2.Size = new Size(402, 456);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.Dock = DockStyle.Fill;
        label17.Location = new Point(3, 0);
        label17.Name = "label17";
        label17.Size = new Size(134, 30);
        label17.TabIndex = 0;
        label17.Text = "Sensör Adı";
        // 
        // txtSensorName
        // 
        txtSensorName.Dock = DockStyle.Fill;
        txtSensorName.Location = new Point(143, 3);
        txtSensorName.Name = "txtSensorName";
        txtSensorName.Size = new Size(256, 27);
        txtSensorName.TabIndex = 8;
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.Dock = DockStyle.Fill;
        label18.Location = new Point(3, 30);
        label18.Name = "label18";
        label18.Size = new Size(134, 30);
        label18.TabIndex = 1;
        label18.Text = "Slave ID";
        // 
        // txtSensorSlave
        // 
        txtSensorSlave.Dock = DockStyle.Fill;
        txtSensorSlave.Location = new Point(143, 33);
        txtSensorSlave.Name = "txtSensorSlave";
        txtSensorSlave.Size = new Size(256, 27);
        txtSensorSlave.TabIndex = 9;
        // 
        // lblParameterTitle
        // 
        lblParameterTitle.AutoSize = true;
        tableLayoutPanel2.SetColumnSpan(lblParameterTitle, 2);
        lblParameterTitle.Dock = DockStyle.Fill;
        lblParameterTitle.Location = new Point(3, 60);
        lblParameterTitle.Name = "lblParameterTitle";
        lblParameterTitle.Size = new Size(396, 25);
        lblParameterTitle.TabIndex = 10;
        lblParameterTitle.Text = "Parametre Ayarları";
        // 
        // dgvSensorParameters
        // 
        dgvSensorParameters.AllowUserToAddRows = false;
        dgvSensorParameters.AllowUserToDeleteRows = false;
        dgvSensorParameters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSensorParameters.BackgroundColor = SystemColors.Window;
        dgvSensorParameters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        tableLayoutPanel2.SetColumnSpan(dgvSensorParameters, 2);
        dgvSensorParameters.Dock = DockStyle.Fill;
        dgvSensorParameters.Location = new Point(3, 88);
        dgvSensorParameters.Name = "dgvSensorParameters";
        dgvSensorParameters.RowHeadersVisible = false;
        dgvSensorParameters.RowHeadersWidth = 51;
        dgvSensorParameters.ScrollBars = ScrollBars.Vertical;
        dgvSensorParameters.Size = new Size(396, 327);
        dgvSensorParameters.TabIndex = 11;
        // 
        // buttonPanel
        // 
        buttonPanel.ColumnCount = 2;
        tableLayoutPanel2.SetColumnSpan(buttonPanel, 2);
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        buttonPanel.Controls.Add(btnAddSensor, 0, 0);
        buttonPanel.Controls.Add(btnSaveProfile, 1, 0);
        buttonPanel.Dock = DockStyle.Fill;
        buttonPanel.Location = new Point(3, 421);
        buttonPanel.Name = "buttonPanel";
        buttonPanel.RowCount = 1;
        buttonPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        buttonPanel.Size = new Size(396, 32);
        buttonPanel.TabIndex = 12;
        // 
        // btnAddSensor
        // 
        btnAddSensor.Dock = DockStyle.Fill;
        btnAddSensor.Location = new Point(3, 3);
        btnAddSensor.Name = "btnAddSensor";
        btnAddSensor.Size = new Size(192, 26);
        btnAddSensor.TabIndex = 17;
        btnAddSensor.Text = "Yeni Sensör";
        btnAddSensor.UseVisualStyleBackColor = true;
        // 
        // btnSaveProfile
        // 
        btnSaveProfile.Dock = DockStyle.Fill;
        btnSaveProfile.Location = new Point(201, 3);
        btnSaveProfile.Name = "btnSaveProfile";
        btnSaveProfile.Size = new Size(192, 26);
        btnSaveProfile.TabIndex = 16;
        btnSaveProfile.Text = "Sensörü Kaydet";
        btnSaveProfile.UseVisualStyleBackColor = true;
        btnSaveProfile.Click += btnSaveProfile_Click;
        // 
        // grpGraph
        // 
        tableLayoutPanel1.SetColumnSpan(grpGraph, 2);
        grpGraph.Dock = DockStyle.Fill;
        grpGraph.Location = new Point(855, 3);
        grpGraph.Name = "grpGraph";
        grpGraph.Size = new Size(665, 525);
        grpGraph.TabIndex = 4;
        grpGraph.TabStop = false;
        grpGraph.Text = "Parametre Grafiği";
        // 
        // grpSensorValues
        // 
        grpSensorValues.Controls.Add(cmbSensorList);
        grpSensorValues.Controls.Add(lblSensorValue);
        grpSensorValues.Controls.Add(lblSensorLastRead);
        grpSensorValues.Controls.Add(lblActiveSensor);
        grpSensorValues.Controls.Add(lblRawRegister);
        grpSensorValues.Dock = DockStyle.Fill;
        grpSensorValues.Location = new Point(855, 534);
        grpSensorValues.Name = "grpSensorValues";
        grpSensorValues.Size = new Size(320, 430);
        grpSensorValues.TabIndex = 4;
        grpSensorValues.TabStop = false;
        grpSensorValues.Text = "Sensör Değerleri";
        // 
        // cmbSensorList
        // 
        cmbSensorList.Dock = DockStyle.Top;
        cmbSensorList.FormattingEnabled = true;
        cmbSensorList.Location = new Point(3, 23);
        cmbSensorList.Name = "cmbSensorList";
        cmbSensorList.Size = new Size(314, 28);
        cmbSensorList.TabIndex = 5;
        cmbSensorList.Text = "Sensör Seç";
        // 
        // lblSensorValue
        // 
        lblSensorValue.AutoSize = true;
        lblSensorValue.Location = new Point(6, 259);
        lblSensorValue.Name = "lblSensorValue";
        lblSensorValue.Size = new Size(123, 20);
        lblSensorValue.TabIndex = 1;
        lblSensorValue.Text = "Sensör Değeri:  - ";
        // 
        // lblSensorLastRead
        // 
        lblSensorLastRead.AutoSize = true;
        lblSensorLastRead.Location = new Point(8, 102);
        lblSensorLastRead.Name = "lblSensorLastRead";
        lblSensorLastRead.Size = new Size(102, 20);
        lblSensorLastRead.TabIndex = 4;
        lblSensorLastRead.Text = "Son Okuma: - ";
        // 
        // lblActiveSensor
        // 
        lblActiveSensor.AutoSize = true;
        lblActiveSensor.Location = new Point(8, 73);
        lblActiveSensor.Name = "lblActiveSensor";
        lblActiveSensor.Size = new Size(105, 20);
        lblActiveSensor.TabIndex = 0;
        lblActiveSensor.Text = "Aktif Sensör: - ";
        // 
        // lblRawRegister
        // 
        lblRawRegister.AutoSize = true;
        lblRawRegister.Location = new Point(8, 131);
        lblRawRegister.Name = "lblRawRegister";
        lblRawRegister.Size = new Size(116, 20);
        lblRawRegister.TabIndex = 0;
        lblRawRegister.Text = "Raw Register:  - ";
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(tlpLog);
        groupBox4.Dock = DockStyle.Fill;
        groupBox4.Location = new Point(1181, 534);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(339, 430);
        groupBox4.TabIndex = 5;
        groupBox4.TabStop = false;
        groupBox4.Text = "Log Kayıtları";
        // 
        // tlpLog
        // 
        tlpLog.ColumnCount = 1;
        tlpLog.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpLog.Controls.Add(btnExportExcel, 0, 4);
        tlpLog.Controls.Add(txtLog, 0, 0);
        tlpLog.Controls.Add(chcLogToFile, 0, 1);
        tlpLog.Controls.Add(btnSelectLogFolder, 0, 3);
        tlpLog.Controls.Add(lblFilePath, 0, 2);
        tlpLog.Dock = DockStyle.Fill;
        tlpLog.Location = new Point(3, 23);
        tlpLog.Name = "tlpLog";
        tlpLog.RowCount = 5;
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tlpLog.Size = new Size(333, 404);
        tlpLog.TabIndex = 0;
        // 
        // btnExportExcel
        // 
        btnExportExcel.Dock = DockStyle.Fill;
        btnExportExcel.Location = new Point(3, 367);
        btnExportExcel.Name = "btnExportExcel";
        btnExportExcel.Size = new Size(327, 34);
        btnExportExcel.TabIndex = 6;
        btnExportExcel.Text = "Verileri Excel'e Aktar";
        btnExportExcel.UseVisualStyleBackColor = true;
        btnExportExcel.Click += btnExportExcel_Click;
        // 
        // txtLog
        // 
        txtLog.Dock = DockStyle.Fill;
        txtLog.Location = new Point(3, 3);
        txtLog.Multiline = true;
        txtLog.Name = "txtLog";
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.Size = new Size(327, 238);
        txtLog.TabIndex = 0;
        // 
        // chcLogToFile
        // 
        chcLogToFile.AutoSize = true;
        chcLogToFile.Checked = true;
        chcLogToFile.CheckState = CheckState.Checked;
        chcLogToFile.Location = new Point(3, 247);
        chcLogToFile.Name = "chcLogToFile";
        chcLogToFile.Size = new Size(137, 24);
        chcLogToFile.TabIndex = 1;
        chcLogToFile.Text = "Dosyaya Kaydet";
        chcLogToFile.UseVisualStyleBackColor = true;
        chcLogToFile.CheckedChanged += chcLogToFile_CheckedChanged;
        // 
        // btnSelectLogFolder
        // 
        btnSelectLogFolder.Dock = DockStyle.Fill;
        btnSelectLogFolder.Location = new Point(3, 319);
        btnSelectLogFolder.Name = "btnSelectLogFolder";
        btnSelectLogFolder.Size = new Size(327, 42);
        btnSelectLogFolder.TabIndex = 2;
        btnSelectLogFolder.Text = "Log Klasörü Seç";
        btnSelectLogFolder.UseVisualStyleBackColor = true;
        btnSelectLogFolder.Click += btnSelectLogFolder_Click;
        // 
        // lblFilePath
        // 
        lblFilePath.AutoEllipsis = true;
        lblFilePath.AutoSize = true;
        lblFilePath.Dock = DockStyle.Fill;
        lblFilePath.Location = new Point(3, 276);
        lblFilePath.Name = "lblFilePath";
        lblFilePath.Size = new Size(327, 40);
        lblFilePath.TabIndex = 6;
        lblFilePath.Text = "Log Dosya Yolu: - ";
        lblFilePath.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1523, 967);
        Controls.Add(tableLayoutPanel1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "Form1";
        Text = "Modbus Sensor Reader";
        Load += Form1_Load;
        tableLayoutPanel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        tlpConnection.ResumeLayout(false);
        tlpConnection.PerformLayout();
        groupBox2.ResumeLayout(false);
        tlpModBus.ResumeLayout(false);
        tlpModBus.PerformLayout();
        groupBox5.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSensorParameters).EndInit();
        buttonPanel.ResumeLayout(false);
        grpSensorValues.ResumeLayout(false);
        grpSensorValues.PerformLayout();
        groupBox4.ResumeLayout(false);
        tlpLog.ResumeLayout(false);
        tlpLog.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
    private GroupBox groupBox2;
    private TextBox txtSamplingTime;
    private Label label16;
    private Button btnWrite;
    private ComboBox cmbWriteDataType;
    private Label label15;
    private TextBox txtRegisterCount;
    private TextBox txtWriteValue;
    private Label label14;
    private Label label10;
    private TextBox txtWriteAddress;
    private Label label13;
    private Button btnStartRead;
    private Button btnStop;
    private Button btnRead;
    private ComboBox cmbDataType;
    private Label label12;
    private TextBox txtStartAddress;
    private Label label11;
    private ComboBox cmbFuncCode;
    private Label label9;
    private TextBox txtSlaveId;
    private Label label8;
    private GroupBox groupBox1;
    private GroupBox groupBox4;
    private TextBox txtLog;
    private TableLayoutPanel tlpConnection;
    private ComboBox cmbDataBits;
    private ComboBox cmbBaudRate;
    private TextBox txtTCPPort;
    private Label label7;
    private Label StopBits;
    private ComboBox cmbStopBits;
    private Label label6;
    private Label label5;
    private TextBox txtIpAddress;
    private ComboBox cmbPortName;
    private Label label4;
    private Label label3;
    private ComboBox cmbParity;
    private Label label2;
    private ComboBox cmbConnectionType;
    private Label label1;
    private Button btnConnected;
    private Button btnDisconnected;
    private TableLayoutPanel tlpModBus;
    private CheckBox chcLogToFile;
    private TableLayoutPanel tlpLog;
    private GroupBox groupBox5;
    private TableLayoutPanel tableLayoutPanel2;
    private Label label17;
    private Label label18;
    private TextBox txtSensorName;
    private TextBox txtSensorSlave;
    private Button btnSaveProfile;
    private Button btnSelectLogFolder;
    private Label lblFilePath;
    private Button btnAddSensor;
    private Label lblParameterTitle;
    private TableLayoutPanel buttonPanel;
    private DataGridView dgvSensorParameters;
    private GroupBox grpSensorValues;
    private ComboBox cmbSensorList;
    private Label lblSensorValue;
    private Label lblSensorLastRead;
    private Label lblActiveSensor;
    private Label lblRawRegister;
    private GroupBox grpGraph;
    private Button btnExportExcel;
}
