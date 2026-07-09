namespace ModBusSensorReader.UI;
using ModbusSensorReader.Library.Models;
using ModbusSensorReader.Library.Services;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Diagnostics.Metrics;
using ModbusSensorReader.UI;

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
        btnAddSensor.Click += btnAddSensor_Click;

    }

    // Form yüklendiğinde combobox ve textbox değerlerini ayarlama kısmı.
    private void Form1_Load(object sender, EventArgs e)
    {
        InitializeSensorParameterGrid();
        btnStop.Enabled = false;

        // form yüklendiğinde comboboxların dropdownlist olarak ayarlanması sağlanır. Bu sayede ekran büyütüldüğünde mavi seçili alanlar gelmez.
        cmbConnectionType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPortName.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbParity.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFuncCode.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDataType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDataBits.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbWriteDataType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbSensorList.DropDownStyle = ComboBoxStyle.DropDownList;
        //

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


        // Baud Rate ve Data Bits seçeneklerini combobox'a ekleme
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
        LoadDefaultSensorProfiles();

        cmbFuncCode_SelectedIndexChanged(null, EventArgs.Empty);

        dgvSensorParameters.DataError += dgvSensorParameters_DataError;

        BeginInvoke(new Action(ClearUiSelection));

    }

    // DataGridView üzerinde veri hatası oluştuğunda exception fırlatılmasını engeller ve kullanıcıya hata mesajı göstermez.
    private void dgvSensorParameters_DataError(object? sender, DataGridViewDataErrorEventArgs e)
    {
        e.ThrowException = false;
    }

    // Sayfa ilk yüklendiğinde default değer olarak MAWS sensörünün bilgileri yazdırılır.
    private void LoadDefaultSensorProfiles()
    {
        if (_sensorProfiles.Any(p => p.SensorName == "MAWS"))
            return;

        SensorProfile mawsProfile = new SensorProfile
        {
            SensorName = "MAWS",
            SlaveId = 1,
            Parameters = new List<SensorParameter>
            {
                new SensorParameter { ParameterName = "Sıcaklık", Unit = "°C", Coefficient = 0},
                new SensorParameter { ParameterName = "Basınç", Unit = "hPa", Coefficient = 0},
                new SensorParameter { ParameterName = "Rüzgar Hızı", Unit =  "m/s", Coefficient = 0 },
                new SensorParameter { ParameterName = "Rüzgar Yönü", Unit = "°", Coefficient = 0 }
            }

        };

        _sensorProfiles.Add(mawsProfile);
        RefreshSensorList();
        cmbSensorList.SelectedItem = mawsProfile.SensorName;
    }

    // Sensör ekleme butonuna tıklandığında yeni sensör profili oluşturmak için SensorProfileForm formunu açar ve kullanıcıdan sensördeki parametre bilgilerini alır.
    private void btnAddSensor_Click(object? sender, EventArgs e)
    {
        using SensorProfileForm form = new SensorProfileForm();

        if (form.ShowDialog() != DialogResult.OK)
            return;

        if (form.CreatedProfile == null)
            return;

        bool exists = _sensorProfiles.Any(p =>
            p.SensorName.Equals(form.CreatedProfile.SensorName, StringComparison.OrdinalIgnoreCase));

        if (exists)
        {
            MessageBox.Show("Bu isimde bir sensör zaten var.");
            return;
        }

        _sensorProfiles.Add(form.CreatedProfile);

        RefreshSensorList();

        cmbSensorList.SelectedItem = form.CreatedProfile.SensorName;

    }


    // Sensör listesi combobox'ını günceller ve kullanıcıya mevcut sensör profillerini gösterir.
    private void RefreshSensorList()
    {
        cmbSensorList.Items.Clear();
        foreach (var profile in _sensorProfiles)
        {
            cmbSensorList.Items.Add(profile.SensorName);
        }
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
            this.ActiveControl = btnConnected;
        }
    }

    // Form yeniden boyutlandırıldığında DataGridView üzerindeki seçimi temizler ve odaklanmayı "Bağlan" butonuna verir.
    private void Form1_Resize(object sender, EventArgs e)
    {
        BeginInvoke(new Action(ClearUiSelection));
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

    // Sürekli okuma başladığında diğer alanların pasifleşmesi
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
        btnSaveProfile.Enabled = enabled;
    }
    // Sürekli okuma için timer tick eventi. Bu event her tick olduğunda ReadModbus fonksiyonunu çağırır ve okuma işlemini gerçekleştirir.
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

    // Fonksiyon koduna göre alanları günceller. Read, write fonksiyonları arasındaki ayrımı yapar. 
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

    // "Write" butonuna tıklandığında Modbus yazma işlemini başlatır.
    private void btnWrite_Click(object sender, EventArgs e)
    {
        WriteModbus();

    }

    // Sensör kaydetme butonuna tıklandığında kullanıcı tarafından girilen sensör profilini kaydeder veya günceller.
    private void btnSaveProfile_Click(object sender, EventArgs e)
    {
        if (selectedProfile == null)
        {
            MessageBox.Show("Önce bir sensör seçmelisiniz.");
            return;
        }

        selectedProfile.SensorName = txtSensorName.Text.Trim();

        if (byte.TryParse(txtSensorSlave.Text, out byte slaveId))
            selectedProfile.SlaveId = slaveId;

        SaveGridValuesToSelectedProfile();

        RefreshSensorList();
        cmbSensorList.SelectedItem = selectedProfile.SensorName;

        Log("Sensör profili güncellendi: " + selectedProfile.SensorName);
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
            Log("Önce sensör profili seçilmelidir.");
            return;
        }

        SaveGridValuesToSelectedProfile();

        List<string> valueTexts = new List<string>();

        if (selectedProfile.Parameters == null || selectedProfile.Parameters.Count == 0)
        {
            Log("Seçili sensöre ait parametre bulunamadı.");
            return;
        }

        // Her bir parametre için Modbus üzerinden register değerini okur, katsayıyı uygular ve UI üzerinde gösterir.

        foreach (var parameter in selectedProfile.Parameters)
        {
            ushort rawValue = ReadSingleRegister(parameter.RegisterAddress);

            double calculatedValue = rawValue * parameter.Coefficient;

            parameter.RawValue = rawValue.ToString();
            parameter.CalculatedValue = calculatedValue.ToString("0.##") + " " + parameter.Unit;

            valueTexts.Add($"{parameter.ParameterName}: {parameter.CalculatedValue}");
        }

        LoadSelectedSensorToGrid();

        lblActiveSensor.Text = "Aktif Sensör: " + selectedProfile.SensorName;
        lblSensorLastRead.Text = "Son Okuma: " + DateTime.Now.ToString("HH:mm:ss");

        Log($"{selectedProfile.SensorName} okundu → " + string.Join(", ", valueTexts));
    }

    // DataGridView üzerindeki değerleri seçili sensör profilinin parametrelerine kaydeder.
    private void SaveGridValuesToSelectedProfile()
    {
        if (selectedProfile == null)
            return;

        foreach (DataGridViewRow row in dgvSensorParameters.Rows)
        {
            if (row.IsNewRow)
                continue;

            string parameterName = row.Cells["ParameterName"].Value?.ToString() ?? "";

            SensorParameter? parameter = selectedProfile.Parameters
                .FirstOrDefault(p => p.ParameterName == parameterName);

            if (parameter == null)
                continue;

            int.TryParse(
                row.Cells["RegisterAddress"].Value?.ToString(),
                out int registerAddress
            );

            TryParseCoefficient(
                row.Cells["Coefficient"].Value?.ToString() ?? "1",
                out double coefficient
            );

            parameter.RegisterAddress = registerAddress;
            parameter.Coefficient = coefficient;
            parameter.Unit = row.Cells["Unit"].Value?.ToString() ?? "";
        }
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

        LoadSelectedSensorToGrid();

        Log("Aktif sensör seçildi: " + selectedProfile.SensorName);
    }

    //DataGrid yapısı ile sensör parametrelerini ekleme, silme ve düzenleme işlemlerini sağlar.
    private void InitializeSensorParameterGrid()
    {
        dgvSensorParameters.Columns.Clear();

        dgvSensorParameters.Columns.Add("ParameterName", "Parametre");
        dgvSensorParameters.Columns.Add("RegisterAddress", "Register");
        dgvSensorParameters.Columns.Add("Coefficient", "Katsayı");
        DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
        unitColumn.Name = "Unit";
        unitColumn.HeaderText = "Birim";
        unitColumn.Items.AddRange("","°C", "hPa", "%", "m/s", "°", "mm", "V", "A", "Pa", "rpm");
        dgvSensorParameters.Columns.Add(unitColumn);


        dgvSensorParameters.Columns.Add("RawValue", "Ham Değer");
        dgvSensorParameters.Columns.Add("CalculatedValue", "Sonuç");

        dgvSensorParameters.Columns["ParameterName"].ReadOnly = true;
        dgvSensorParameters.Columns["RawValue"].ReadOnly = true;
        dgvSensorParameters.Columns["CalculatedValue"].ReadOnly = true;
    }

    // Sensör listesinden sensörü seçince parametreleri doldur
    private void LoadSelectedSensorToGrid()
    {
        dgvSensorParameters.Rows.Clear();

        if (selectedProfile == null)
            return;


        foreach (var parameter in selectedProfile.Parameters)
        {
            dgvSensorParameters.Rows.Add(
                parameter.ParameterName,
                parameter.RegisterAddress,
                parameter.Coefficient,
                parameter.Unit,
                parameter.RawValue,
                parameter.CalculatedValue
            );
        }

        lblActiveSensor.Text = "Aktif Sensör: " + selectedProfile.SensorName;
        txtSensorSlave.Text = selectedProfile.SlaveId.ToString();
        txtSensorName.Text = selectedProfile.SensorName;

        dgvSensorParameters.ClearSelection();
        dgvSensorParameters.CurrentCell = null;
    }

    // DataGridView üzerindeki seçimi temizler ve odaklanmayı "Bağlan" butonuna verir.
    private void ClearUiSelection()
    {
        dgvSensorParameters.ClearSelection();
        dgvSensorParameters.CurrentCell = null;

        btnConnected.Focus();
    }
}