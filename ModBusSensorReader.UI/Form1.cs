namespace ModBusSensorReader.UI;
using ModbusSensorReader.Library.Models;
using ModbusSensorReader.Library.Services;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Diagnostics.Metrics;

public partial class Form1 : Form
{
    private readonly ModbusReaderServices _modbusReaderService = new ModbusReaderServices();

    private readonly System.Windows.Forms.Timer _readTimer = new System.Windows.Forms.Timer();
    private readonly System.Windows.Forms.Timer _writeTimer = new System.Windows.Forms.Timer();

    // SensorProfile listesi, kullanıcı tarafından tanımlanan sensör profillerini saklar.
    private readonly List<SensorProfile> _sensorProfiles = new List<SensorProfile>();
    private SensorProfile? selectedProfile;

    private bool _isConnected = false;

    private string _logFolderPath = "";
    public Form1()
    {
        InitializeComponent();
        this.Shown += Form1_Shown;

        cmbFuncCode.SelectedIndexChanged += cmbFuncCode_SelectedIndexChanged;

        btnConnected.Click += btnConnected_Click;
        btnDisconnected.Click += btnDisconnected_Click;

        btnRead.Click += btnRead_Click;
        btnWrite.Click += btnWrite_Click;
        btnStartRead.Click += btnStartRead_Click;
        btnStop.Click += btnStop_Click;

    }



    // Form yüklendiğinde combobox ve textbox değerlerini ayarlama kısmı.
    private void Form1_Load(object sender, EventArgs e)
    {
        btnStop.Enabled = false;

        cmbConnectionType.Items.AddRange(new string[] { "RTU", "TCP" });
        cmbConnectionType.SelectedItem = "RTU";

        // Port isimlerini combobox'a ekleme
        cmbPortName.Items.AddRange(SerialPort.GetPortNames());
        if (cmbPortName.Items.Count > 0)
            cmbPortName.SelectedIndex = 0;

        // Parity ve StopBits enum değerlerini combobox'a ekleme
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

    // Form gösterildiğinde log dosyasına kayıt yapılacaksa kullanıcıya log klasörü seçmesi için uyarı verir.
    private void Form1_Shown(object sender, EventArgs e)
    {
        if (chcLogToFile.Checked)
        {
            MessageBox.Show(
                "Logları dosyaya kaydetmek için lütfen log klasörü seçin.",
                "Log Klasörü Seçimi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            SelectLogFolder();
        }
    }

    // fonksiyonların bulunduğu checkbox seçildiğinde ilgili alanları etkinleştirir veya devre dışı bırakır.
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

        UpdateWriteFieldsByFunctionCode();
    }

    // Sürelli okuma başladığında diğer alanların pasifleşmesi
    private void SetModbusSettingsEnabled(bool enabled)
    {
        // Bağlantı ayarları ve okuma/yazma alanlarını etkinleştir veya devre dışı bırak
        txtSlaveId.Enabled = enabled;
        txtStartAddress.Enabled = enabled;
        cmbFuncCode.Enabled = enabled;
        txtRegisterCount.Enabled = enabled;
        cmbDataType.Enabled = enabled;
        txtSamplingTime.Enabled = enabled;
        txtWriteAddress.Enabled = enabled;
        txtWriteValue.Enabled = enabled;
        cmbWriteDataType.Enabled = enabled;

        // Sensör alanlarını da devre dışı bırak
        txtSensorName.Enabled = enabled;
        txtSensorSlave.Enabled = enabled;
        txtTemperatureRegister.Enabled = enabled;
        txtTemperatureCoefficient.Enabled = enabled;
        txtPressureRegister.Enabled = enabled;
        txtPressureCoefficient.Enabled = enabled;
        txtHumidityRegister.Enabled = enabled;
        txtHumidityCoefficient.Enabled = enabled;
        btnSaveProfile.Enabled = enabled;
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
    // Loglama fonksiyonu, txtLog TextBox'ına mesajları ekler ve zaman damgası ekler.
    private void Log(string message)
    {
        string logMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";

        txtLog.AppendText(logMessage + Environment.NewLine);

        if (chcLogToFile.Checked)
        {
            if (string.IsNullOrWhiteSpace(_logFolderPath))
            {
                chcLogToFile.Checked = false;
                Log("Log klasörü seçilmedi. Dosyaya kayıt kapatıldı.");
                return;
            }

            Directory.CreateDirectory(_logFolderPath);

            string filePath = Path.Combine(
                _logFolderPath,
                $"ModbusLog_{DateTime.Now:yyyyMMdd}.txt"
            );

            File.AppendAllText(filePath, logMessage + Environment.NewLine);
        }
    }

    // "Read" butonuna tıklandığında okuma işlemini başlatır.
    private void btnRead_Click(object sender, EventArgs e)
    {
        ReadModbus();
    }

    // "Sürekli Okuma Başlat" butonuna tıklandığında timer'ı başlatır.
    private void btnStartRead_Click(object sender, EventArgs e)
    {
        if (!_isConnected)
        {
            Log("Önce bağlantı kurmalısınız.");
            return;
        }

        // Sampling time kullanıcı girdili
        if (!int.TryParse(txtSamplingTime.Text, out int second) || second <= 0)
        {
            Log("Okuma aralığı geçerli bir saniye değeri olmalı.");
            return;
        }

        btnRead.Enabled = false;
        btnStartRead.Enabled = false;
        btnStop.Enabled = true;

        SetModbusSettingsEnabled(false);

        _readTimer.Interval = second * 1000;
        _readTimer.Start();

        Log($"Sürekli okuma başlatıldı. Aralık: {second} saniye.");
    }

    // "Okuma Durdur" butonuna tıklandığında timer'ı durdurur.
    private void btnStop_Click(object sender, EventArgs e)
    {
        _readTimer.Stop();

        btnRead.Enabled = true;
        btnStartRead.Enabled = true;
        btnStop.Enabled = false;

        SetModbusSettingsEnabled(true);
        UpdateWriteFieldsByFunctionCode();

        Log("Sürekli okuma durduruldu.");
    }

    // Fonksiyon koduna göre 
    private void UpdateWriteFieldsByFunctionCode()
    {
        int functionCode = GetSelectedFunctionCode();

        bool isWriteFunction =
            functionCode == 5 ||
            functionCode == 6 ||
            functionCode == 15 ||
            functionCode == 16;

        txtWriteAddress.Enabled = isWriteFunction;
        txtWriteValue.Enabled = isWriteFunction;
        cmbWriteDataType.Enabled = isWriteFunction;
        btnWrite.Enabled = isWriteFunction;

        btnRead.Enabled = !isWriteFunction;
    }

    // Modbus okuma işlemini gerçekleştirir ve sonuçları UI üzerinde gösterir.
    private void ReadModbus()
    {
        if (!_isConnected)
        {
            Log("Önce bağlantı kurmalısınız.");
            return;
        }
        try
        {
            ModbusReadRequest request = BuildRequest();
            ModbusReadResult result = _modbusReaderService.Read(request);

            lblRawRegister.Text = "Raw Register : " + result.RawText;
            lblSensorValue.Text = "Sensör Değeri : " + result.ParsedValue;
            lblLastRead.Text = "Son Okuma : " + result.ReadTime.ToString("dd.MM.yyyy HH:mm:ss");

            Log("Okuma başarılı. Değer: " + result.ParsedValue);

            // Sensör profili varsa sıcaklık / basınç / nem değerlerini de oku
            if (selectedProfile != null)
            {
                ReadSensorProfileValues();
            }

        }
        catch (Exception ex)
        {
            Log("Okuma Hatası: " + ex.Message);
        }

    }

    // ModbusReadRequest nesnesini UI'dan alınan değerlerle oluşturur.
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

    // Read ve write fonksiyonlarının seçilmesini sağlar.
    private int GetSelectedFunctionCode()
    {
        if (string.IsNullOrWhiteSpace(cmbFuncCode.Text))
            return 0;

        string codeText = cmbFuncCode.Text.Split('-')[0].Trim();

        if (int.TryParse(codeText, out int functionCode))
            return functionCode;

        return 0;
    }

    // "Bağlan" butonuna tıklandığında bağlantı ayarlarını hazırlar ve UI üzerinde durumu günceller.
    private void btnConnected_Click(object sender, EventArgs e)
    {
        try
        {
            ModbusReadRequest request = BuildRequest();
            _isConnected = true;
            Log("Bağlantı ayarları hazırlandı");
            Log($"Tip: {request.ConnectionType}, Slave ID: {request.SlaveId}");

            btnConnected.Enabled = false;
            btnDisconnected.Enabled = true;
        }
        catch (Exception ex)
        {
            Log("Bağlantı hatası: " + ex.Message);
        }
    }

    // "Bağlantıyı Kes" butonuna tıklandığında bağlantıyı keser ve UI üzerinde durumu günceller.
    private void btnDisconnected_Click(object sender, EventArgs e)
    {
        _readTimer.Stop();
        _isConnected = false;

        btnConnected.Enabled = true;
        btnDisconnected.Enabled = false;

        Log("Bağlantı kesildi.");
    }

    // "Write" butonuna tıklandığında Modbus yazma işlemini gerçekleştirir. Eğer bağlantı kurulmamışsa önce bağlantı kurulur.
    private void WriteModbus()
    {
        if (!_isConnected)
        {
            Log("Önce bağlantı kurmalısınız.");
            return;
        }
        try
        {
            ModbusReadRequest request = BuildRequest();
            _modbusReaderService.Write(request);

            Log($"Yazma işlemi başarılı. Address: {request.WriteAddress}, Value: {request.WriteValue}");

        }
        catch (Exception ex)
        {
            Log("Yazma Hatası: " + ex.Message);
        }

    }

    private void btnWrite_Click(object sender, EventArgs e)
    {
        WriteModbus();

    }

    // Sensör kaydetme butonuna tıklandığında kullanıcı tarafından girilen sensör profilini kaydeder veya günceller.
    private void btnSaveProfile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSensorName.Text))
        {
            Log("Sensör adı boş bırakılamaz.");
            return;
        }

        if (!byte.TryParse(txtSensorSlave.Text, out byte slaveId))
        {
            Log("Slave ID geçersiz.");
            return;
        }

        if (!int.TryParse(txtTemperatureRegister.Text, out int temperatureRegister))
        {
            Log("Sıcaklık register değeri geçersiz.");
            return;
        }

        if (!TryParseCoefficient(txtTemperatureCoefficient.Text, out double temperatureCoefficient))
        {
            Log("Sıcaklık katsayı değeri geçersiz.");
            return;
        }

        if (!int.TryParse(txtPressureRegister.Text, out int pressureRegister))
        {
            Log("Basınç register değeri geçersiz.");
            return;
        }

        if (!TryParseCoefficient(txtPressureCoefficient.Text, out double pressureCoefficient))
        {
            Log("Basınç katsayı değeri geçersiz.");
            return;
        }

        if (!int.TryParse(txtHumidityRegister.Text, out int humidityRegister))
        {
            Log("Nem register değeri geçersiz.");
            return;
        }

        if (!TryParseCoefficient(txtHumidityCoefficient.Text, out double humidityCoefficient))
        {
            Log("Nem katsayı değeri geçersiz.");
            return;
        }

        var newProfile = new SensorProfile
        {
            SensorName = txtSensorName.Text.Trim(),
            SlaveId = slaveId,

            TemperatureRegister = temperatureRegister,
            TemperatureCoefficient = temperatureCoefficient,

            PressureRegister = pressureRegister,
            PressureCoefficient = pressureCoefficient,

            HumidityRegister = humidityRegister,
            HumidityCoefficient = humidityCoefficient
        };

        var existingProfile = _sensorProfiles
            .FirstOrDefault(x => x.SensorName == newProfile.SensorName);

        if (existingProfile != null)
        {
            existingProfile.SlaveId = newProfile.SlaveId;
            existingProfile.TemperatureRegister = newProfile.TemperatureRegister;
            existingProfile.TemperatureCoefficient = newProfile.TemperatureCoefficient;
            existingProfile.PressureRegister = newProfile.PressureRegister;
            existingProfile.PressureCoefficient = newProfile.PressureCoefficient;
            existingProfile.HumidityRegister = newProfile.HumidityRegister;
            existingProfile.HumidityCoefficient = newProfile.HumidityCoefficient;

            selectedProfile = existingProfile;

            Log("Sensör profili güncellendi: " + selectedProfile.SensorName);
        }
        else
        {
            _sensorProfiles.Add(newProfile);
            cmbSensorList.Items.Add(newProfile.SensorName);

            selectedProfile = newProfile;

            Log("Sensör profili kaydedildi: " + selectedProfile.SensorName);
        }

        cmbSensorList.SelectedItem = selectedProfile.SensorName;
    }

    // Kullanıcı tarafından girilen katsayı değerlerini double tipine dönüştürür. Virgül veya nokta ile ayrılmış sayıları destekler.
    private bool TryParseCoefficient(string text, out double value)
    {
        return double.TryParse(
            text.Replace(",", "."),
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out value);
    }

    // Sensör profilini okur ve register değerlerini Modbus üzerinden alır, ardından katsayıları uygular ve UI üzerinde gösterir.
    private void ReadSensorProfileValues()
    {
        if (selectedProfile == null)
        {
            Log("Önce sensör profili kaydedilmelidir.");
            return;
        }

        ushort tempRaw = ReadSingleRegister(selectedProfile.TemperatureRegister);
        ushort pressureRaw = ReadSingleRegister(selectedProfile.PressureRegister);
        ushort humidityRaw = ReadSingleRegister(selectedProfile.HumidityRegister);

        double temperature = tempRaw * selectedProfile.TemperatureCoefficient;
        double pressure = pressureRaw * selectedProfile.PressureCoefficient;
        double humidity = humidityRaw * selectedProfile.HumidityCoefficient;

        lblRawRegister.Text =
            $"Raw Register: T={tempRaw}, P={pressureRaw}, H={humidityRaw}";

        lblActiveSensor.Text = "Aktif Sensör: " + selectedProfile.SensorName;
        lblTemperature.Text = $"Sıcaklık: {temperature} °C";
        lblPressure.Text = $"Basınç: {pressure} bar";
        lblHumidity.Text = $"Nem: {humidity} %";
        lblSensorLastRead.Text = "Son Okuma: " + DateTime.Now.ToString("HH:mm:ss");

        Log($"{selectedProfile.SensorName} okundu → Sıcaklık: {temperature} °C, Basınç: {pressure} bar, Nem: {humidity} %");
    }

    // Modbus üzerinden tek bir register okur ve ushort tipinde döner.
    private ushort ReadSingleRegister(int registerAddress)
    {
        var request = new ModbusReadRequest
        {
            ConnectionType = cmbConnectionType.Text,
            PortName = cmbPortName.Text,
            IpAddress = txtIpAddress.Text,
            TcpPort = int.Parse(txtTCPPort.Text),

            BaudRate = int.Parse(cmbBaudRate.Text),
            DataBits = int.Parse(cmbDataBits.Text),
            Parity = cmbParity.Text,
            StopBits = cmbStopBits.Text,

            SlaveId = selectedProfile.SlaveId,
            FunctionCode = 3,
            StartAddress = (ushort)registerAddress,
            RegisterCount = 1,
            DataType = "UInt16"
        };

        ModbusReadResult result = _modbusReaderService.Read(request);

        return result.Registers[0];
    }

    // Dosya yolu seç
    private void SelectLogFolder()
    {
        using FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = "Log dosyalarının kaydedileceği klasörü seçin";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _logFolderPath = dialog.SelectedPath;
            lblFilePath.Text = "Dosya yolu: " + _logFolderPath;
            Log("Log klasörü seçildi: " + _logFolderPath);
        }
        else
        {
            chcLogToFile.Checked = false;
            _logFolderPath = "";

            lblFilePath.Text = "Dosya yolu: -------";

            Log("Log klasörü seçilmedi. Dosyaya kayıt kapatıldı.");
        }
    }

    // Dosya yolunu seçme butonuna bastığında kullanıcıya klasör seçme dialogu açar ve seçilen klasörü log dosyası için kullanır.
    private void btnSelectLogFolder_Click(object sender, EventArgs e)
    {
        SelectLogFolder();
    }

    // Log dosyasına kayıt yapılıp yapılmayacağını belirleyen checkbox değiştiğinde çalışır.
    // Eğer checkbox işaretlenmişse ve log klasörü seçilmemişse kullanıcıya klasör seçme dialogu açılır.
    // Checkbox işaretlenmemişse log klasörü temizlenir ve UI üzerinde gösterilir.
    private void chcLogToFile_CheckedChanged(object sender, EventArgs e)
    {
        if (chcLogToFile.Checked && string.IsNullOrWhiteSpace(_logFolderPath))
        {
            SelectLogFolder();
        }

        if (!chcLogToFile.Checked)
        {
            _logFolderPath = "";
            lblFilePath.Text = "Dosya yolu: -----";
        }

    }

    // Sensör listesini combobox'dan seçtiğinde ilgili sensör profilini yükler ve UI üzerinde gösterir.
    private void cmbSensorList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbSensorList.SelectedItem == null)
            return;

        selectedProfile = _sensorProfiles
            .FirstOrDefault(p => p.SensorName == cmbSensorList.SelectedItem.ToString());

        if (selectedProfile == null)
        {
            Log("Seçilen sensör listede bulunamadı: " + cmbSensorList.Text);
            return;
        }

        txtSensorName.Text = selectedProfile.SensorName;
        txtSensorSlave.Text = selectedProfile.SlaveId.ToString();

        txtTemperatureRegister.Text = selectedProfile.TemperatureRegister.ToString();
        txtTemperatureCoefficient.Text = selectedProfile.TemperatureCoefficient.ToString();

        txtPressureRegister.Text = selectedProfile.PressureRegister.ToString();
        txtPressureCoefficient.Text = selectedProfile.PressureCoefficient.ToString();

        txtHumidityRegister.Text = selectedProfile.HumidityRegister.ToString();
        txtHumidityCoefficient.Text = selectedProfile.HumidityCoefficient.ToString();

        Log("Aktif sensör seçildi: " + selectedProfile.SensorName);
    }
}