namespace ModBusSensorReader.UI;
using ModbusSensorReader.Library.Models;
using ModbusSensorReader.Library.Services;
using System.IO.Ports;
using System.IO;
public partial class Form1 : Form
{
    private readonly ModbusReaderServices _modbusReaderService = new ModbusReaderServices();
    private readonly System.Windows.Forms.Timer _readTimer = new System.Windows.Forms.Timer();
    private readonly System.Windows.Forms.Timer _writeTimer = new System.Windows.Forms.Timer();
    private bool _isConnected = false;

    // Log kayýtlarýný "Desktop/staj/Log" klasörüne kaydeder ve dosya adýný tarih formatýnda oluþturur.
    private readonly string _logFilePath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
    "staj",
    "Log",
    $"ModbusLog_{DateTime.Now:yyyyMMdd}.txt"
    );
    public Form1()
    {
        InitializeComponent();

        cmbFuncCode.SelectedIndexChanged += cmbFuncCode_SelectedIndexChanged;

        btnConnected.Click += btnConnected_Click;
        btnDisconnected.Click += btnDisconnected_Click;

        btnRead.Click += btnRead_Click;
        btnWrite.Click += btnWrite_Click;
        btnStartRead.Click += btnStartRead_Click;
        btnStop.Click += btnStop_Click;
    }

    

    // Form yüklendiðinde combobox ve textbox deðerlerini ayarlama kýsmý.
    private void Form1_Load(object sender, EventArgs e)
    {
        cmbConnectionType.Items.AddRange(new string[] { "RTU", "TCP" });
        cmbConnectionType.SelectedItem = "RTU";

        // Port isimlerini combobox'a ekleme
        cmbPortName.Items.AddRange(SerialPort.GetPortNames());
        if (cmbPortName.Items.Count > 0)
            cmbPortName.SelectedIndex = 0;

        // Parity ve StopBits enum deðerlerini combobox'a ekleme
        cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
        cmbParity.SelectedItem = "None";

        cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
        cmbStopBits.SelectedItem = "One";

        chcLogToFile.Checked = true;

        // Function Code ve Data Type seçeneklerini combobox'a ekleme
        cmbFuncCode.Items.AddRange(new object[]
        {
            "01 - Read Coil Status",
            "02 - Read Input Status",
            "03 - Read Holding Register",
            "04 - Read Input Register",
            "05 - Write Single Coil",
            "06 - Write Single Register",
            "15 - Write Multiple Coils",
            "16 - Write Multiple Registers"
        });
        cmbFuncCode.SelectedIndex = 0;

        // Data Type seçeneklerini combobox'a ekleme
        cmbDataType.Items.AddRange(new object[]
        {
            "UInt16",
            "Int16",
            "UInt32",
            "Int32",
            "Float",
            "Double"
        });
        cmbDataType.SelectedIndex = 0;

        cmbBaudRate.Items.AddRange(new object[]
        {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"
        });
        cmbBaudRate.SelectedItem = "9600";

        cmbDataBits.Items.AddRange(new object[]
        {
            "5",
            "6",
            "7",
            "8"
        });
        cmbDataBits.SelectedItem = "8";



        txtIpAddress.Text = "127.0.0.1";
        txtTCPPort.Text = "502";
        txtSlaveId.Text = "1";
        txtStartAddress.Text = "0";
        txtRegisterCount.Text = "1";
        txtSamplingTime.Text = "2";
        _readTimer.Interval = 1000;
        _readTimer.Tick += ReadTimer_Tick;

        cmbWriteDataType.Items.AddRange(new object[]
        {
            "Bool",
            "UInt16",
            "Int16",
            "UInt32",
            "Int32",
            "Float",
            "Double"
        });
        cmbWriteDataType.SelectedItem = "UInt16";

        txtWriteAddress.Text = "0";
        txtWriteValue.Text = "0";

        Log("Form yüklendi.");
        cmbFuncCode_SelectedIndexChanged(null, EventArgs.Empty);

    }
    private void cmbFuncCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        int code = GetSelectedFunctionCode();

        bool isRead = code == 1 || code == 2 || code == 3 || code == 4;
        bool isWrite = code == 5 || code == 6 || code == 15 || code == 16;

        txtStartAddress.Enabled = isRead;
        txtRegisterCount.Enabled = isRead;
        cmbDataType.Enabled = isRead;
        btnRead.Enabled = isRead;
        btnStartRead.Enabled = isRead;
        btnStop.Enabled = isRead;

        txtWriteAddress.Enabled = isWrite;
        txtWriteValue.Enabled = isWrite;
        cmbWriteDataType.Enabled = isWrite;
        btnWrite.Enabled = isWrite;
    }
    // Sürekli okuma için timer tick eventi
    private void ReadTimer_Tick(object? sender, EventArgs e)
    {
        _readTimer.Stop();

        try
        {
            ReadModbus();
        }
        finally
        {
            _readTimer.Start();
        }
    }
    // Loglama fonksiyonu, txtLog TextBox'ýna mesajlarý ekler ve zaman damgasý ekler.
    private void Log(string message)
    {
        string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";

        txtLog.AppendText(logLine + Environment.NewLine);

        if (chcLogToFile.Checked)
        {
            try
            {
                string logDirectory = Path.GetDirectoryName(_logFilePath);

                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                File.AppendAllText(_logFilePath, logLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Log dosyasýna yazýlamadý: {ex.Message}{Environment.NewLine}");
            }
        }
    }

    // "Read" butonuna týklandýðýnda okuma iþlemini baþlatýr.
    private void btnRead_Click(object sender, EventArgs e)
    {
        ReadModbus();
    }

    // "Sürekli Okuma Baþlat" butonuna týklandýðýnda timer'ý baþlatýr.
    private void btnStartRead_Click(object sender, EventArgs e)
    {
        if (!_isConnected)
        {
            Log("Önce baðlantý kurmalýsýnýz.");
            return;
        }

        // Sampling time kullanýcý girdili
        if (!int.TryParse(txtSamplingTime.Text, out int second) || second <= 0)
        {
            Log("Okuma aralýðý geçerli bir saniye deðeri olmalý.");
            return;
        }

        _readTimer.Interval = second * 1000;
        _readTimer.Start();

        Log($"Sürekli okuma baþlatýldý. Aralýk: {second} saniye.");
    }

    // "Okuma Durdur" butonuna týklandýðýnda timer'ý durdurur.
    private void btnStop_Click(object sender, EventArgs e)
    {
        _readTimer.Stop();
        Log("Sürekli okuma durduruldu.");
    }

    // Modbus okuma iþlemini gerçekleþtirir ve sonuçlarý UI üzerinde gösterir.
    private void ReadModbus()
    {
        if (!_isConnected)
        {
            Log("Önce baðlantý kurmalýsýnýz.");
            return;
        }
        try
        {
            ModbusReadRequest request = BuildRequest();
            ModbusReadResult result = _modbusReaderService.Read(request);

            lblRawRegisters.Text = "Raw Register : " + result.RawText;
            lblParsedValue.Text = "Sensör Deðeri : " + result.ParsedValue;
            lblLastReadTime.Text = "Son Okuma : " + result.ReadTime.ToString("dd.MM.yyyy HH:mm:ss");

            Log("Okuma baþarýlý. Deðer: " + result.ParsedValue);
        }
        catch (Exception ex)
        {
            Log("Okuma Hatasý: " + ex.Message);
        }

    }

    // ModbusReadRequest nesnesini UI'dan alýnan deðerlerle oluþturur.
    private ModbusReadRequest BuildRequest()
    {
        return new ModbusReadRequest
        {
            ConnectionType = cmbConnectionType.Text,

            PortName = cmbPortName.Text,
            BaudRate = int.Parse(cmbBaudRate.Text),
            DataBits = int.Parse(cmbDataBits.Text),
            Parity = cmbParity.Text,
            StopBits = cmbStopBits.Text,

            IpAddress = txtIpAddress.Text,
            TcpPort = int.Parse(txtTCPPort.Text),

            SlaveId = byte.Parse(txtSlaveId.Text),
            FunctionCode = GetSelectedFunctionCode(),
            StartAddress = ushort.Parse(txtStartAddress.Text),
            RegisterCount = ushort.Parse(txtRegisterCount.Text),
            DataType = cmbDataType.Text,

            WriteAddressText = txtWriteAddress.Text,
            WriteAddress = ushort.Parse(txtWriteAddress.Text.Split(',')[0].Trim()),
            WriteValue = txtWriteValue.Text,
            WriteDataType = cmbWriteDataType.Text
        };
    }

    private int GetSelectedFunctionCode()
    {
        if (string.IsNullOrWhiteSpace(cmbFuncCode.Text))
            return 0;

        return int.Parse(cmbFuncCode.Text.Substring(0, 2));
    }
    // "Baðlan" butonuna týklandýðýnda baðlantý ayarlarýný hazýrlar ve UI üzerinde durumu günceller.
    private void btnConnected_Click(object sender, EventArgs e)
    {
        try
        {
            ModbusReadRequest request = BuildRequest();
            _isConnected = true;
            Log("Baðlantý ayarlarý hazýrlandý");
            Log($"Tip: {request.ConnectionType}, Slave ID: {request.SlaveId}");

            btnConnected.Enabled = false;
            btnDisconnected.Enabled = true;
        }
        catch (Exception ex)
        {
            Log("Baðlantý hatasý: " + ex.Message);
        }
    }

    // "Baðlantýyý Kes" butonuna týklandýðýnda baðlantýyý keser ve UI üzerinde durumu günceller.
    private void btnDisconnected_Click(object sender, EventArgs e)
    {
        _readTimer.Stop();
        _isConnected = false;

        btnConnected.Enabled = true;
        btnDisconnected.Enabled = false;

        Log("Baðlantý kesildi.");
    }

    private void WriteModbus()
    {
        if (!_isConnected)
        {
            Log("Önce baðlantý kurmalýsýnýz.");
            return;
        }
        try
        {
            ModbusReadRequest request = BuildRequest();
            _modbusReaderService.Write(request);

            Log($"Yazma iþlemi baþarýlý. Address: {request.WriteAddress}, Value: {request.WriteValue}");
        
        }
        catch (Exception ex)
        {
            Log("Yazma Hatasý: " + ex.Message);
        }

    }

    private void btnWrite_Click(object sender, EventArgs e)
    {
        WriteModbus();

    }
}   