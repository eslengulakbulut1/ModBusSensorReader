namespace ModBusSensorReader.UI;
using ModbusSensorReader.Library.Models;
using ModbusSensorReader.Library.Services;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Diagnostics.Metrics;
using ModbusSensorReader.UI;
using System.Drawing.Text;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using SkiaSharp;
using LiveChartsCore.Defaults;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView.SKCharts;

public partial class Form1 : Form
{
    // Servisler
    private readonly ModbusReaderServices _modbusReaderService = new();
    private readonly ExcelExportServices _excelExportService = new();

    // Zamanlayıcılars
    private readonly System.Windows.Forms.Timer _readTimer = new System.Windows.Forms.Timer();
    private readonly System.Windows.Forms.Timer _writeTimer = new System.Windows.Forms.Timer();

    private readonly ContextMenuStrip _chartContextMenu =
    new ContextMenuStrip();

    // Grafiği kaydetme menü öğesi. Kullanıcı grafiği kaydetmek istediğinde bu menü öğesine tıklar.
    private readonly ToolStripMenuItem _saveChartMenuItem =
        new ToolStripMenuItem("Grafiği Kaydet");

    private string _logFilePath = "";

    // Loglama sırasında UI güncellemelerini engellemek için kullanılan flag. Bu sayede log eklenirken UI'nin güncellenmesi engellenir ve performans artar.
    private bool _updatingLogControls = false;

    // SensorProfile listesi, kullanıcı tarafından tanımlanan sensör profillerini saklar.
    private readonly List<SensorProfile> _sensorProfiles = new List<SensorProfile>();
    private SensorProfile? selectedProfile;

    // Grafik değişimleri
    private CartesianChart sensorChart = new CartesianChart();
    private readonly Dictionary<string, LineSeries<DateTimePoint>> chartSeries =
    new Dictionary<string, LineSeries<DateTimePoint>>();
    private const int MaxChartPoints = 50;

    // Excel'e aktarmak için kullanılan liste. Her okuma sonrası bu listeye veri eklenir ve kullanıcı istediğinde Excel'e aktarılır.
    private readonly List<ExcelRecord> _excelRecords = new List<ExcelRecord>();

    private bool _isConnected = false;

    private string _logFolderPath = "";

    // form1 class'ı.
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
        InitializeSensorChart();
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


        LoadDefaultSensorProfiles();
        RefreshSensorList();

        if (_sensorProfiles.Count > 0)
        {
            //selectedProfile = _sensorProfiles[0];
            cmbSensorList.SelectedIndex = 0;

            //LoadSelectedSensorToGrid();
            // CreateChartSeries();
        }

        cmbFuncCode_SelectedIndexChanged(null, EventArgs.Empty);

        Log("Form yüklendi.");

        dgvSensorParameters.DataError += dgvSensorParameters_DataError;

        BeginInvoke(new Action(ClearUiSelection));

    }

    // Grafik alanını başlatır ve gerekli ayarları yapar. X ve Y eksenlerini, tooltip ve legend ayarlarını içerir.
    private void InitializeSensorChart()
    {
        sensorChart.Dock = DockStyle.Fill;

        sensorChart.BackColor = Color.White;

        sensorChart.LegendPosition = LegendPosition.Top;
        sensorChart.LegendTextSize = 12;

        sensorChart.TooltipPosition = TooltipPosition.Top;
        sensorChart.TooltipFindingStrategy =
            LiveChartsCore.Measure.TooltipFindingStrategy.CompareAllTakeClosest;

        sensorChart.ZoomMode = ZoomAndPanMode.X;
        sensorChart.DrawMarginFrame = new DrawMarginFrame
        {
            Fill = null,
            Stroke = new SolidColorPaint(
                new SKColor(225, 225, 225),
                1)
        };

        sensorChart.XAxes = new[]
        {
            new DateTimeAxis(
                TimeSpan.FromSeconds(1),
                date => date.ToString("HH:mm:ss"))
            {
                Name = "Okuma Zamanı",

                TextSize = 11,

                LabelsPaint = new SolidColorPaint(
                    new SKColor(90, 90, 90)),

                NamePaint = new SolidColorPaint(
                    new SKColor(55, 55, 55)),

                SeparatorsPaint = new SolidColorPaint(
                    new SKColor(230, 230, 230),
                    1),

                TicksPaint = null,
                SubticksPaint = null,

                MinStep = TimeSpan.FromSeconds(1).Ticks
            }
        };

        sensorChart.YAxes = new[]
        {
            new Axis
            {
                Name = "Değer",

                TextSize = 11,

                LabelsPaint = new SolidColorPaint(
                    new SKColor(90, 90, 90)),

                NamePaint = new SolidColorPaint(
                    new SKColor(55, 55, 55)),

                SeparatorsPaint = new SolidColorPaint(
                    new SKColor(230, 230, 230),
                    1),

                TicksPaint = null,
                SubticksPaint = null
            }
        };

        grpGraph.Controls.Clear();
        grpGraph.Controls.Add(sensorChart);

        InitializeChartContextMenu();
    }

    // Log durumunu güncelleyen ortak metot
    private void UpdateLogControls()
    {
        _updatingLogControls = true;

        try
        {
            bool hasLogFile =
                !string.IsNullOrWhiteSpace(_logFilePath);

            chcLogToFile.Checked = hasLogFile;

            if (hasLogFile)
            {
                lblFilePath.Text =
                    "Log dosyası: " + _logFilePath;

                lblFilePath.AutoEllipsis = true;

                btnSelectLogFolder.Text =
                    "Log Dosyasını Değiştir";
            }
            else
            {
                lblFilePath.Text =
                    "Log dosyası: -----";

                btnSelectLogFolder.Text =
                    "Log Dosyası Seç";
            }
        }
        finally
        {
            _updatingLogControls = false;
        }
    }

    // Grafik menüsünü hazırlayan metot
    private void InitializeChartContextMenu()
    {
        _chartContextMenu.Items.Clear();

        _saveChartMenuItem.Text = "Grafiği Kaydet";
        _saveChartMenuItem.Click -= SaveChartMenuItem_Click;
        _saveChartMenuItem.Click += SaveChartMenuItem_Click;

        _chartContextMenu.Items.Add(_saveChartMenuItem);

        sensorChart.ContextMenuStrip = _chartContextMenu;
    }


    // Grafiği kaydetme menü öğesine tıklandığında SaveCurrentChartImage fonksiyonunu çağırır.
    private void SaveChartMenuItem_Click(object? sender, EventArgs e)
    {
        SaveCurrentChartImage();
    }

    // Grafiği kaydetme işlemini gerçekleştiren metot. Kullanıcıya dosya kaydetme dialog'u açar ve grafiği PNG formatında kaydeder.
    private void SaveCurrentChartImage()
    {
        if (sensorChart.Series == null ||
            !sensorChart.Series.Any())
        {
            MessageBox.Show(
                "Kaydedilecek grafik verisi bulunamadı.",
                "Grafik Kaydet",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        using SaveFileDialog dialog = new SaveFileDialog();

        dialog.Title = "Grafik görüntüsünü kaydet";
        dialog.Filter = "PNG Görüntüsü (*.png)|*.png";
        dialog.DefaultExt = "png";
        dialog.AddExtension = true;

        string sensorName =
            selectedProfile?.SensorName ?? "Sensor";

        string safeSensorName =
            MakeSafeFileName(sensorName);

        dialog.FileName =
            $"{safeSensorName}_Grafik_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            int chartWidth = Math.Max(sensorChart.Width, 900);
            int chartHeight = Math.Max(sensorChart.Height, 600);

            SKCartesianChart chartImage =
                new SKCartesianChart(sensorChart)
                {
                    Width = chartWidth,
                    Height = chartHeight
                };

            chartImage.SaveImage(dialog.FileName);

            MessageBox.Show(
                "Grafik başarıyla kaydedildi.",
                "Grafik Kaydet",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            Log("Grafik görüntüsü kaydedildi: " + dialog.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Grafik kaydedilemedi:\n" + ex.Message,
                "Grafik Kaydetme Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Log("Grafik kaydetme hatası: " + ex.Message);
        }
    }


    // Dosya adında geçersiz karakterleri alt çizgi ile değiştirir. Bu sayede dosya adı geçerli hale gelir.
    private string MakeSafeFileName(string fileName)
    {
        foreach (char invalidCharacter in
                 Path.GetInvalidFileNameChars())
        {
            fileName =
                fileName.Replace(
                    invalidCharacter,
                    '_');
        }

        return fileName;
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
            new SensorParameter { ParameterName = "Sıcaklık", RegisterAddress = 0, Coefficient = 1, Unit = "°C" },
            new SensorParameter { ParameterName = "Basınç", RegisterAddress = 0, Coefficient = 1, Unit = "hPa" },
            new SensorParameter { ParameterName = "Rüzgar Hızı", RegisterAddress = 0, Coefficient = 1, Unit = "m/s" },
            new SensorParameter { ParameterName = "Rüzgar Yönü", RegisterAddress = 0, Coefficient = 1, Unit = "°" }
        }
        };

        _sensorProfiles.Add(mawsProfile);
    }


    // Grafik serisinin oluşturulması ve her parametre için ayrı bir çizgi serisi eklenmesi. Renkler döngüsel olarak atanır.
    private void CreateChartSeries()
    {
        chartSeries.Clear();

        if (selectedProfile?.Parameters == null ||
            selectedProfile.Parameters.Count == 0)
        {
            sensorChart.Series = Array.Empty<ISeries>();
            return;
        }

        List<ISeries> seriesList = new List<ISeries>();

        SKColor[] seriesColors =
        {
        new SKColor(33, 150, 243),  // Mavi
        new SKColor(244, 67, 54),   // Kırmızı
        new SKColor(139, 195, 74),  // Yeşil
        new SKColor(255, 152, 0),   // Turuncu
        new SKColor(156, 39, 176),  // Mor
        new SKColor(0, 188, 212)    // Turkuaz
    };

        int colorIndex = 0;

        // Seçili sensör profilindeki her parametre için
        // ayrı bir çizgi serisi oluşturulur.
        foreach (SensorParameter parameter in selectedProfile.Parameters)
        {
            if (string.IsNullOrWhiteSpace(parameter.ParameterName))
                continue;

            SKColor color =
                seriesColors[colorIndex % seriesColors.Length];

            // Her parametrenin grafik değerlerini ayrı koleksiyonda tutar.
            ObservableCollection<DateTimePoint> parameterValues =
                new ObservableCollection<DateTimePoint>();

            LineSeries<DateTimePoint> lineSeries =
                new LineSeries<DateTimePoint>
                {
                    Name =
                        $"{parameter.ParameterName} ({parameter.Unit})",

                    Values = parameterValues,

                    // Çizgi görünümü
                    Stroke = new SolidColorPaint(color)
                    {
                        StrokeThickness = 1.5f
                    },

                    // Çizginin altındaki dolguyu kaldırır.
                    Fill = null,

                    // Grafik noktalarının boyutu
                    GeometrySize = 4,

                    GeometryFill =
                        new SolidColorPaint(color),

                    GeometryStroke =
                        new SolidColorPaint(color)
                        {
                            StrokeThickness = 1
                        },

                    // Çizgilerin düz olmasını sağlar.
                    LineSmoothness = 0,

                    // Tooltip içinde okuma zamanını gösterir.
                    XToolTipLabelFormatter = point =>
                    {
                        DateTime? readTime =
                            point.Model?.DateTime;

                        return readTime.HasValue
                            ? $"Zaman: {readTime.Value:HH:mm:ss}"
                            : "Zaman: -";
                    },

                    // Tooltip içinde anlık, minimum,
                    // maksimum ve ortalama değerleri gösterir.
                    YToolTipLabelFormatter = point =>
                    {
                        List<double> values = parameterValues
                            .Where(item => item.Value.HasValue)
                            .Select(item => item.Value!.Value)
                            .ToList();

                        double currentValue =
                            point.Model?.Value ?? 0;

                        if (values.Count == 0)
                        {
                            return
                                $"{parameter.ParameterName}\n" +
                                $"Anlık: {currentValue:0.##} " +
                                $"{parameter.Unit}";
                        }

                        double minimumValue = values.Min();
                        double maximumValue = values.Max();
                        double averageValue = values.Average();

                        return
                            $"{parameter.ParameterName}\n" +
                            $"Anlık: {currentValue:0.##} " +
                            $"{parameter.Unit}\n" +
                            $"Minimum: {minimumValue:0.##} " +
                            $"{parameter.Unit}\n" +
                            $"Maksimum: {maximumValue:0.##} " +
                            $"{parameter.Unit}\n" +
                            $"Ortalama: {averageValue:0.##} " +
                            $"{parameter.Unit}";
                    }
                };

            chartSeries[parameter.ParameterName] = lineSeries;
            seriesList.Add(lineSeries);

            colorIndex++;
        }

        sensorChart.Series = seriesList;
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

        selectedProfile = form.CreatedProfile;

        int newIndex = _sensorProfiles.IndexOf(selectedProfile);

        cmbSensorList.SelectedIndexChanged -= cmbSensorList_SelectedIndexChanged;
        cmbSensorList.SelectedIndex = newIndex;
        cmbSensorList.SelectedIndexChanged += cmbSensorList_SelectedIndexChanged;

        LoadSelectedSensorToGrid();
        CreateChartSeries();

        Log("Yeni sensör eklendi: " + selectedProfile.SensorName);
    }



    // Sensör listesi combobox'ını günceller ve kullanıcıya mevcut sensör profillerini gösterir.
    private void RefreshSensorList()
    {
        cmbSensorList.SelectedIndexChanged -= cmbSensorList_SelectedIndexChanged;

        cmbSensorList.Items.Clear();

        foreach (var profile in _sensorProfiles)
        {
            cmbSensorList.Items.Add(profile.SensorName);
        }

        cmbSensorList.SelectedIndexChanged += cmbSensorList_SelectedIndexChanged;
    }


    // Form gösterildiğinde log dosyasına kayıt yapılacaksa kullanıcıya log klasörü seçmesi için uyarı verir.
    private void Form1_Shown(object sender, EventArgs e)
    {
        UpdateLogControls();

        if (chcLogToFile.Checked &&
            string.IsNullOrWhiteSpace(_logFilePath))
        {
            MessageBox.Show(
                "Logları dosyaya kaydetmek için lütfen bir log dosyası seçin.",
                "Log Dosyası Seçimi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            bool selected = SelectLogFolder();

            if (!selected)
            {
                _logFilePath = "";
                UpdateLogControls();
            }
        }

        ActiveControl = btnConnected;
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
        cmbSensorList.Enabled = enabled;
        groupBox5.Enabled = enabled;
        dgvSensorParameters.Enabled = enabled;
        cmbConnectionType.Enabled = enabled;
        cmbPortName.Enabled = enabled;
        txtIpAddress.Enabled = enabled;
        txtTCPPort.Enabled = enabled;
        cmbParity.Enabled = enabled;
        cmbStopBits.Enabled = enabled;
        cmbBaudRate.Enabled = enabled;
        cmbDataBits.Enabled = enabled;

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
        string logMessage =
            $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] {message}";

        txtLog.AppendText(
            logMessage + Environment.NewLine);

        if (!chcLogToFile.Checked)
            return;

        if (string.IsNullOrWhiteSpace(_logFilePath))
        {
            _logFilePath = "";
            UpdateLogControls();

            txtLog.AppendText(
                $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] " +
                "Log dosyası seçilmedi. Dosyaya kayıt kapatıldı." +
                Environment.NewLine);

            return;
        }

        try
        {
            File.AppendAllText(
                _logFilePath,
                logMessage + Environment.NewLine);
        }
        catch (Exception ex)
        {
            txtLog.AppendText(
                $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] " +
                $"Log dosyasına yazma hatası: {ex.Message}" +
                Environment.NewLine);

            _logFilePath = "";
            UpdateLogControls();
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

            if (selectedProfile != null)
            {
                ReadSensorProfileValues();
            }

            ModbusReadRequest request = BuildRequest();
            ModbusReadResult result = _modbusReaderService.Read(request);
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
        DateTime readingTime = DateTime.Now;
        if (selectedProfile == null)
        {
            Log("Önce sensör profili seçilmelidir.");
            return;
        }

        SaveGridValuesToSelectedProfile();

        if (selectedProfile.Parameters == null || selectedProfile.Parameters.Count == 0)
        {
            Log("Seçili sensöre ait parametre bulunamadı.");
            return;
        }

        List<string> rawTexts = new List<string>();
        List<string> valueTexts = new List<string>();

        foreach (var parameter in selectedProfile.Parameters)
        {
            ushort rawValue = ReadSingleRegister(parameter.RegisterAddress);
            double calculatedValue = rawValue * parameter.Coefficient;

            parameter.RawValue = rawValue.ToString();
            parameter.CalculatedValue = calculatedValue.ToString("0.##") + " " + parameter.Unit;

            rawTexts.Add($"{parameter.ParameterName}={rawValue}");
            valueTexts.Add($"{parameter.ParameterName}: {parameter.CalculatedValue}");

            AddValueToChart(parameter.ParameterName, calculatedValue);

            // Excel kayıtlarını tutan listeye yeni bir kayıt ekler.
            _excelRecords.Add(new ExcelRecord
            {
                ReadingTime = readingTime,
                SensorName = selectedProfile.SensorName,
                SlaveId = selectedProfile.SlaveId,
                ParameterName = parameter.ParameterName,
                RegisterAddress = parameter.RegisterAddress,
                RawValue = rawValue,
                CalculatedValue = calculatedValue,
                Unit = parameter.Unit
            });
        }

        // Log($"Excel kayıt listesine {selectedProfile.Parameters.Count} kayıt eklendi. Toplam kayıt: {_excelRecords.Count}");

        LoadSelectedSensorToGrid();

        lblActiveSensor.Text = "Aktif Sensör: " + selectedProfile.SensorName;
        lblSensorLastRead.Text = "Son Okuma: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

        lblRawRegister.Text =
            "Raw Register:" + Environment.NewLine +
            string.Join(Environment.NewLine, rawTexts);

        lblSensorValue.Text =
            "Sensör Değeri:" + Environment.NewLine +
            string.Join(Environment.NewLine, valueTexts);

        Log($"Okuma Başarılı | {selectedProfile.SensorName} sensörü okundu => {string.Join(", ", valueTexts)}");
    }


    // Grafik üzerinde ilgili parametreye ait değeri ekler ve maksimum nokta sayısını aşarsa eski değerleri siler. Ayrıca X eksenini günceller.
    private void AddValueToChart(string parameterName, double value)
    {
        if (!chartSeries.TryGetValue(
                parameterName,
                out LineSeries<DateTimePoint>? series))
        {
            return;
        }

        if (series.Values is not
            ObservableCollection<DateTimePoint> values)
        {
            values = new ObservableCollection<DateTimePoint>();
            series.Values = values;
        }

        DateTime readTime = DateTime.Now;

        values.Add(
            new DateTimePoint(
                readTime,
                value));
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


    // Kullanıcıya log dosyası seçmesi için SaveFileDialog açar ve seçilen dosya yolunu _logFilePath değişkenine atar. Eğer kullanıcı iptal ederse false döner.
    private bool SelectLogFolder()
    {
        using SaveFileDialog dialog = new SaveFileDialog();

        dialog.Title = "Log dosyasını seçin";
        dialog.Filter = "Text Dosyası (*.txt)|*.txt";
        dialog.DefaultExt = "txt";
        dialog.AddExtension = true;

        // Daha önce dosya seçildiyse mevcut dosya adını göster.
        if (!string.IsNullOrWhiteSpace(_logFilePath))
        {
            dialog.InitialDirectory =
                Path.GetDirectoryName(_logFilePath);

            dialog.FileName =
                Path.GetFileName(_logFilePath);
        }
        else
        {
            dialog.FileName =
                $"ModbusLog_{DateTime.Now:yyyyMMdd}.txt";
        }

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return false;
        }

        _logFilePath = dialog.FileName;

        UpdateLogControls();

        Log("Log dosyası seçildi: " + _logFilePath);

        return true;
    }


    // Dosya yolunu seçme butonuna bastığında kullanıcıya klasör seçme dialogu açar ve seçilen klasörü log dosyası için kullanır.
    private void btnSelectLogFolder_Click(object sender, EventArgs e)
    {
        string previousPath = _logFilePath;

        bool selected = SelectLogFolder();

        if (!selected)
        {
            // Kullanıcı iptal ettiyse önceki seçim korunur.
            _logFilePath = previousPath;
            UpdateLogControls();
        }
    }


    // Log dosyasına kayıt yapılıp yapılmayacağını belirleyen checkbox değiştiğinde çalışır.
    // Eğer checkbox işaretlenmişse ve log klasörü seçilmemişse kullanıcıya klasör seçme dialogu açılır.
    // Checkbox işaretlenmemişse log klasörü temizlenir ve UI üzerinde gösterilir.
    private void chcLogToFile_CheckedChanged(object sender, EventArgs e)
    {
        if (_updatingLogControls)
            return;

        // Checkbox işaretlendiyse
        if (chcLogToFile.Checked)
        {
            // Zaten bir dosya seçilmişse tekrar pencere açma.
            if (!string.IsNullOrWhiteSpace(_logFilePath))
            {
                UpdateLogControls();
                return;
            }

            bool selected = SelectLogFolder();

            // Kullanıcı dosya seçmeden pencereyi kapattıysa
            // checkbox tekrar kapatılır.
            if (!selected)
            {
                _logFilePath = "";
                UpdateLogControls();
            }

            return;
        }

        // Checkbox kullanıcı tarafından kapatıldıysa
        // dosyaya yazma devre dışı bırakılır.
        _logFilePath = "";

        UpdateLogControls();

        Log("Dosyaya log kaydı kapatıldı.");
    }


    // Sensör listesini combobox'dan seçtiğinde ilgili sensör profilini yükler ve UI üzerinde gösterir.
    private void cmbSensorList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbSensorList.SelectedIndex < 0)
            return;

        selectedProfile = _sensorProfiles[cmbSensorList.SelectedIndex];

        LoadSelectedSensorToGrid();
        CreateChartSeries();

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
        unitColumn.Items.AddRange("", "°C", "hPa", "%", "m/s", "°", "mm", "V", "A", "Pa", "rpm");
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
        {
            Log("selectedProfile null geldi.");
            return;
        }


        txtSensorSlave.Text = selectedProfile.SlaveId.ToString();
        txtSensorName.Text = selectedProfile.SensorName;

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

        dgvSensorParameters.ClearSelection();
        dgvSensorParameters.CurrentCell = null;

        // CreateChartSeries();
    }


    // DataGridView üzerindeki seçimi temizler ve odaklanmayı "Bağlan" butonuna verir.
    private void ClearUiSelection()
    {
        dgvSensorParameters.ClearSelection();
        dgvSensorParameters.CurrentCell = null;

        btnConnected.Focus();
    }

    // Excel'e aktarma butonunun çalışması
    private void btnExportExcel_Click(object sender, EventArgs e)
    {
        if(_excelRecords.Count == 0)
        {
            MessageBox.Show(
                "Excel'e aktarılacak veri bulunamadı.");
            return;
        }

        using SaveFileDialog dialog = new SaveFileDialog();

        dialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
        dialog.FileName = $"SensorData_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

        if(dialog.ShowDialog() != DialogResult.OK)
            return;
        _excelExportService.Export(_excelRecords, dialog.FileName);

        MessageBox.Show("Excel dosyası başarıyla oluşturuldu");
    }
}