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
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tlpModBus = new TableLayoutPanel();
        txtSamplingTime = new TextBox();
        label16 = new Label();
        btnWrite = new Button();
        btnStop = new Button();
        label8 = new Label();
        txtSlaveId = new TextBox();
        cmbWriteDataType = new ComboBox();
        btnRead = new Button();
        label9 = new Label();
        label15 = new Label();
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
        groupBox3 = new GroupBox();
        lblLastReadTime = new Label();
        lblParsedValue = new Label();
        lblRawRegisters = new Label();
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
        groupBox4 = new GroupBox();
        tlpLog = new TableLayoutPanel();
        chcLogToFile = new CheckBox();
        txtLog = new TextBox();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tlpModBus.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox1.SuspendLayout();
        tlpConnection.SuspendLayout();
        groupBox4.SuspendLayout();
        tlpLog.SuspendLayout();
        SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 4;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26F));
        tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
        tableLayoutPanel1.Controls.Add(groupBox3, 2, 0);
        tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox4, 3, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1343, 637);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(tlpModBus);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Location = new Point(379, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(370, 631);
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
        tlpModBus.Controls.Add(cmbWriteDataType, 1, 8);
        tlpModBus.Controls.Add(btnRead, 0, 10);
        tlpModBus.Controls.Add(label9, 0, 1);
        tlpModBus.Controls.Add(label15, 0, 8);
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
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tlpModBus.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        tlpModBus.Size = new Size(364, 605);
        tlpModBus.TabIndex = 0;
        // 
        // txtSamplingTime
        // 
        txtSamplingTime.Dock = DockStyle.Fill;
        txtSamplingTime.Location = new Point(161, 123);
        txtSamplingTime.Name = "txtSamplingTime";
        txtSamplingTime.Size = new Size(200, 27);
        txtSamplingTime.TabIndex = 37;
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Dock = DockStyle.Fill;
        label16.Location = new Point(3, 120);
        label16.Name = "label16";
        label16.Size = new Size(152, 40);
        label16.TabIndex = 36;
        label16.Text = "Sampling Time";
        // 
        // btnWrite
        // 
        btnWrite.Dock = DockStyle.Fill;
        btnWrite.Location = new Point(161, 508);
        btnWrite.Name = "btnWrite";
        btnWrite.Size = new Size(200, 44);
        btnWrite.TabIndex = 35;
        btnWrite.Text = "Yaz";
        btnWrite.UseVisualStyleBackColor = true;
        // 
        // btnStop
        // 
        btnStop.Dock = DockStyle.Fill;
        btnStop.Location = new Point(161, 558);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(200, 44);
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
        label8.Size = new Size(152, 40);
        label8.TabIndex = 9;
        label8.Text = "Slave ID";
        // 
        // txtSlaveId
        // 
        txtSlaveId.Dock = DockStyle.Fill;
        txtSlaveId.Location = new Point(161, 3);
        txtSlaveId.Name = "txtSlaveId";
        txtSlaveId.Size = new Size(200, 27);
        txtSlaveId.TabIndex = 10;
        // 
        // cmbWriteDataType
        // 
        cmbWriteDataType.Dock = DockStyle.Fill;
        cmbWriteDataType.FormattingEnabled = true;
        cmbWriteDataType.Location = new Point(161, 323);
        cmbWriteDataType.Name = "cmbWriteDataType";
        cmbWriteDataType.Size = new Size(200, 28);
        cmbWriteDataType.TabIndex = 34;
        // 
        // btnRead
        // 
        btnRead.Dock = DockStyle.Fill;
        btnRead.Location = new Point(3, 508);
        btnRead.Name = "btnRead";
        btnRead.Size = new Size(152, 44);
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
        label9.Size = new Size(152, 40);
        label9.TabIndex = 19;
        label9.Text = "Function Code";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Dock = DockStyle.Fill;
        label15.Location = new Point(3, 320);
        label15.Name = "label15";
        label15.Size = new Size(152, 40);
        label15.TabIndex = 33;
        label15.Text = "W-Data Type";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Dock = DockStyle.Fill;
        label10.Location = new Point(3, 80);
        label10.Name = "label10";
        label10.Size = new Size(152, 40);
        label10.TabIndex = 21;
        label10.Text = "Register Count";
        // 
        // txtWriteValue
        // 
        txtWriteValue.Dock = DockStyle.Fill;
        txtWriteValue.Location = new Point(161, 283);
        txtWriteValue.Name = "txtWriteValue";
        txtWriteValue.Size = new Size(200, 27);
        txtWriteValue.TabIndex = 32;
        // 
        // cmbFuncCode
        // 
        cmbFuncCode.Dock = DockStyle.Fill;
        cmbFuncCode.FormattingEnabled = true;
        cmbFuncCode.Location = new Point(161, 43);
        cmbFuncCode.Name = "cmbFuncCode";
        cmbFuncCode.Size = new Size(200, 28);
        cmbFuncCode.TabIndex = 20;
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Dock = DockStyle.Fill;
        label14.Location = new Point(3, 280);
        label14.Name = "label14";
        label14.Size = new Size(152, 40);
        label14.TabIndex = 31;
        label14.Text = "W-Value";
        // 
        // txtRegisterCount
        // 
        txtRegisterCount.Dock = DockStyle.Fill;
        txtRegisterCount.Location = new Point(161, 83);
        txtRegisterCount.Name = "txtRegisterCount";
        txtRegisterCount.Size = new Size(200, 27);
        txtRegisterCount.TabIndex = 22;
        // 
        // txtWriteAddress
        // 
        txtWriteAddress.Dock = DockStyle.Fill;
        txtWriteAddress.Location = new Point(161, 243);
        txtWriteAddress.Name = "txtWriteAddress";
        txtWriteAddress.Size = new Size(200, 27);
        txtWriteAddress.TabIndex = 30;
        // 
        // txtStartAddress
        // 
        txtStartAddress.Dock = DockStyle.Fill;
        txtStartAddress.Location = new Point(161, 163);
        txtStartAddress.Name = "txtStartAddress";
        txtStartAddress.Size = new Size(200, 27);
        txtStartAddress.TabIndex = 24;
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Dock = DockStyle.Fill;
        label13.Location = new Point(3, 240);
        label13.Name = "label13";
        label13.Size = new Size(152, 40);
        label13.TabIndex = 29;
        label13.Text = "W-Address";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Dock = DockStyle.Fill;
        label12.Location = new Point(3, 200);
        label12.Name = "label12";
        label12.Size = new Size(152, 40);
        label12.TabIndex = 25;
        label12.Text = "R-Data Type";
        // 
        // cmbDataType
        // 
        cmbDataType.Dock = DockStyle.Fill;
        cmbDataType.FormattingEnabled = true;
        cmbDataType.Location = new Point(161, 203);
        cmbDataType.Name = "cmbDataType";
        cmbDataType.Size = new Size(200, 28);
        cmbDataType.TabIndex = 26;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Dock = DockStyle.Fill;
        label11.Location = new Point(3, 160);
        label11.Name = "label11";
        label11.Size = new Size(152, 40);
        label11.TabIndex = 23;
        label11.Text = "R-Start Address";
        // 
        // btnStartRead
        // 
        btnStartRead.Dock = DockStyle.Fill;
        btnStartRead.Location = new Point(3, 558);
        btnStartRead.Name = "btnStartRead";
        btnStartRead.Size = new Size(152, 44);
        btnStartRead.TabIndex = 28;
        btnStartRead.Text = "Sürekli Okuma Başlat";
        btnStartRead.UseVisualStyleBackColor = true;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(lblLastReadTime);
        groupBox3.Controls.Add(lblParsedValue);
        groupBox3.Controls.Add(lblRawRegisters);
        groupBox3.Dock = DockStyle.Fill;
        groupBox3.Location = new Point(755, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(235, 631);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "Okunan Değer";
        // 
        // lblLastReadTime
        // 
        lblLastReadTime.AutoSize = true;
        lblLastReadTime.Location = new Point(0, 146);
        lblLastReadTime.Name = "lblLastReadTime";
        lblLastReadTime.Size = new Size(106, 20);
        lblLastReadTime.TabIndex = 2;
        lblLastReadTime.Text = "Son Okuma:  - ";
        // 
        // lblParsedValue
        // 
        lblParsedValue.AutoSize = true;
        lblParsedValue.Location = new Point(0, 103);
        lblParsedValue.Name = "lblParsedValue";
        lblParsedValue.Size = new Size(123, 20);
        lblParsedValue.TabIndex = 1;
        lblParsedValue.Text = "Sensör Değeri:  - ";
        // 
        // lblRawRegisters
        // 
        lblRawRegisters.AutoSize = true;
        lblRawRegisters.Location = new Point(0, 63);
        lblRawRegisters.Name = "lblRawRegisters";
        lblRawRegisters.Size = new Size(116, 20);
        lblRawRegisters.TabIndex = 0;
        lblRawRegisters.Text = "Raw Register:  - ";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(tlpConnection);
        groupBox1.Dock = DockStyle.Fill;
        groupBox1.Location = new Point(3, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(370, 631);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "Bağlantı Ayarları";
        // 
        // tlpConnection
        // 
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
        tlpConnection.Size = new Size(364, 605);
        tlpConnection.TabIndex = 0;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = DockStyle.Fill;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(152, 40);
        label1.TabIndex = 21;
        label1.Text = "Bağlantı Tipi";
        // 
        // cmbConnectionType
        // 
        cmbConnectionType.Dock = DockStyle.Fill;
        cmbConnectionType.FormattingEnabled = true;
        cmbConnectionType.Location = new Point(161, 3);
        cmbConnectionType.Name = "cmbConnectionType";
        cmbConnectionType.Size = new Size(200, 28);
        cmbConnectionType.TabIndex = 22;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Dock = DockStyle.Fill;
        label4.Location = new Point(3, 40);
        label4.Name = "label4";
        label4.Size = new Size(152, 40);
        label4.TabIndex = 26;
        label4.Text = "COM Port";
        // 
        // cmbPortName
        // 
        cmbPortName.Dock = DockStyle.Fill;
        cmbPortName.FormattingEnabled = true;
        cmbPortName.Location = new Point(161, 43);
        cmbPortName.Name = "cmbPortName";
        cmbPortName.Size = new Size(200, 28);
        cmbPortName.TabIndex = 27;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Dock = DockStyle.Fill;
        label3.Location = new Point(3, 80);
        label3.Name = "label3";
        label3.Size = new Size(152, 40);
        label3.TabIndex = 25;
        label3.Text = "IP Adresi";
        // 
        // txtIpAddress
        // 
        txtIpAddress.Dock = DockStyle.Fill;
        txtIpAddress.Location = new Point(161, 83);
        txtIpAddress.Name = "txtIpAddress";
        txtIpAddress.Size = new Size(200, 27);
        txtIpAddress.TabIndex = 28;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Dock = DockStyle.Fill;
        label7.Location = new Point(3, 120);
        label7.Name = "label7";
        label7.Size = new Size(152, 40);
        label7.TabIndex = 35;
        label7.Text = "TCP Port";
        // 
        // txtTCPPort
        // 
        txtTCPPort.Dock = DockStyle.Fill;
        txtTCPPort.Location = new Point(161, 123);
        txtTCPPort.Name = "txtTCPPort";
        txtTCPPort.Size = new Size(200, 27);
        txtTCPPort.TabIndex = 36;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Location = new Point(3, 160);
        label2.Name = "label2";
        label2.Size = new Size(152, 40);
        label2.TabIndex = 23;
        label2.Text = "BaudRate";
        // 
        // cmbBaudRate
        // 
        cmbBaudRate.Dock = DockStyle.Fill;
        cmbBaudRate.FormattingEnabled = true;
        cmbBaudRate.Location = new Point(161, 163);
        cmbBaudRate.Name = "cmbBaudRate";
        cmbBaudRate.Size = new Size(200, 28);
        cmbBaudRate.TabIndex = 37;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Dock = DockStyle.Fill;
        label5.Location = new Point(3, 200);
        label5.Name = "label5";
        label5.Size = new Size(152, 40);
        label5.TabIndex = 29;
        label5.Text = "DataBits";
        // 
        // cmbDataBits
        // 
        cmbDataBits.Dock = DockStyle.Fill;
        cmbDataBits.FormattingEnabled = true;
        cmbDataBits.Location = new Point(161, 203);
        cmbDataBits.Name = "cmbDataBits";
        cmbDataBits.Size = new Size(200, 28);
        cmbDataBits.TabIndex = 38;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Dock = DockStyle.Fill;
        label6.Location = new Point(3, 240);
        label6.Name = "label6";
        label6.Size = new Size(152, 40);
        label6.TabIndex = 30;
        label6.Text = "Parity";
        // 
        // cmbParity
        // 
        cmbParity.Dock = DockStyle.Fill;
        cmbParity.FormattingEnabled = true;
        cmbParity.Location = new Point(161, 243);
        cmbParity.Name = "cmbParity";
        cmbParity.Size = new Size(200, 28);
        cmbParity.TabIndex = 24;
        // 
        // StopBits
        // 
        StopBits.AutoSize = true;
        StopBits.Dock = DockStyle.Fill;
        StopBits.Location = new Point(3, 280);
        StopBits.Name = "StopBits";
        StopBits.Size = new Size(152, 40);
        StopBits.TabIndex = 32;
        StopBits.Text = "Stop Bits";
        // 
        // cmbStopBits
        // 
        cmbStopBits.Dock = DockStyle.Fill;
        cmbStopBits.FormattingEnabled = true;
        cmbStopBits.Location = new Point(161, 283);
        cmbStopBits.Name = "cmbStopBits";
        cmbStopBits.Size = new Size(200, 28);
        cmbStopBits.TabIndex = 31;
        // 
        // btnConnected
        // 
        btnConnected.Dock = DockStyle.Fill;
        btnConnected.Location = new Point(6, 561);
        btnConnected.Margin = new Padding(6);
        btnConnected.Name = "btnConnected";
        btnConnected.Size = new Size(146, 38);
        btnConnected.TabIndex = 38;
        btnConnected.Text = "Bağlan";
        btnConnected.UseVisualStyleBackColor = true;
        // 
        // btnDisconnected
        // 
        btnDisconnected.Dock = DockStyle.Fill;
        btnDisconnected.Location = new Point(164, 561);
        btnDisconnected.Margin = new Padding(6);
        btnDisconnected.Name = "btnDisconnected";
        btnDisconnected.Size = new Size(194, 38);
        btnDisconnected.TabIndex = 39;
        btnDisconnected.Text = "Bağlantıyı Kes";
        btnDisconnected.UseVisualStyleBackColor = true;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(tlpLog);
        groupBox4.Dock = DockStyle.Fill;
        groupBox4.Location = new Point(996, 3);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(344, 631);
        groupBox4.TabIndex = 5;
        groupBox4.TabStop = false;
        groupBox4.Text = "Log Kayıtları";
        // 
        // tlpLog
        // 
        tlpLog.ColumnCount = 1;
        tlpLog.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        tlpLog.Controls.Add(chcLogToFile, 0, 1);
        tlpLog.Controls.Add(txtLog, 0, 0);
        tlpLog.Dock = DockStyle.Fill;
        tlpLog.Location = new Point(3, 23);
        tlpLog.Name = "tlpLog";
        tlpLog.RowCount = 2;
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpLog.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        tlpLog.Size = new Size(338, 605);
        tlpLog.TabIndex = 0;
        // 
        // chcLogToFile
        // 
        chcLogToFile.AutoSize = true;
        chcLogToFile.Checked = true;
        chcLogToFile.CheckState = CheckState.Checked;
        chcLogToFile.Dock = DockStyle.Fill;
        chcLogToFile.Location = new Point(3, 573);
        chcLogToFile.Name = "chcLogToFile";
        chcLogToFile.Size = new Size(332, 29);
        chcLogToFile.TabIndex = 1;
        chcLogToFile.Text = "Dosyaya Kaydet";
        chcLogToFile.UseVisualStyleBackColor = true;
        // 
        // txtLog
        // 
        txtLog.Dock = DockStyle.Fill;
        txtLog.Location = new Point(3, 3);
        txtLog.Multiline = true;
        txtLog.Name = "txtLog";
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = ScrollBars.Vertical;
        txtLog.Size = new Size(332, 564);
        txtLog.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1343, 637);
        Controls.Add(tableLayoutPanel1);
        Name = "Form1";
        Text = "Modbus Sensor Reader";
        Load += Form1_Load;
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tlpModBus.ResumeLayout(false);
        tlpModBus.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        groupBox1.ResumeLayout(false);
        tlpConnection.ResumeLayout(false);
        tlpConnection.PerformLayout();
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
    private GroupBox groupBox3;
    private Label lblLastReadTime;
    private Label lblParsedValue;
    private Label lblRawRegisters;
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
}
