using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModbusSensorReader.Library.Models;

namespace ModbusSensorReader.UI
{
    public partial class SensorProfileForm : Form
    {
        public SensorProfile? CreatedProfile { get; private set; }
        public SensorProfileForm()
        {
            InitializeComponent();
            txtSlaveId.Text = "1";

            // Form'un boyut özellikleri ayarlandı ve kilitlendi.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            this.ClientSize = new Size(516, 375);
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

        }

        // Ekle butonuna basıldığında parametreyi listeye ekle
        private void btnAddParam_Click(object sender, EventArgs e)
        {
            string parameterName = txtParameterName.Text.Trim();
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                MessageBox.Show("Parametre adı boş olamaz.");
                return;
            }
            if (lstParameters.Items.Contains(parameterName))
            {
                MessageBox.Show("Bu parametre zaten listede mevcut.");
                return;
            }
            lstParameters.Items.Add(parameterName);
            txtParameterName.Clear();
            txtParameterName.Focus();
        }

        // Sil butonuna basıldığında seçili parametreyi listeden kaldır
        private void btnRemoveParam_Click(object sender, EventArgs e)
        {
            if (lstParameters.SelectedItem == null)
            {
                MessageBox.Show("Silmek için bir parametre seçmelisiniz.");
                return;
            }

            lstParameters.Items.Remove(lstParameters.SelectedItem);
        }

        // Kaydet butonuna basıldığında formu doğrula ve yeni sensör profilini oluştur
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSensorName.Text))
            {
                MessageBox.Show("Sensör adı boş bırakılamaz.");
                return;
            }

            if (!byte.TryParse(txtSlaveId.Text, out byte slaveId))
            {
                MessageBox.Show("Slave ID geçerli bir sayı olmalıdır.");
                return;
            }

            if (lstParameters.Items.Count == 0)
            {
                MessageBox.Show("En az bir parametre eklemelisiniz.");
                return;
            }

            CreatedProfile = new SensorProfile
            {
                SensorName = txtSensorName.Text.Trim(),
                SlaveId = slaveId
            };

            foreach (var item in lstParameters.Items)
            {
                CreatedProfile.Parameters.Add(new SensorParameter
                {
                    ParameterName = item.ToString() ?? "",
                    RegisterAddress = 0,
                    Coefficient = 1,
                    Unit = GetDefaultUnitByParameterName(item.ToString() ?? ""),
                });
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        // "Ana Ekrana Dön " butonu. Buna basıldığında kaydetmez. Ana sayfaya döner.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Parametre adı grildiğinde varsayılan birim atamak için kullanılan yardımcı metot.
        private string GetDefaultUnitByParameterName(string parameterName)
        {
            string name = parameterName.ToLower();

            if (name.Contains("sıcak") || name.Contains("sicak"))
                return "°C";

            if (name.Contains("basınç") || name.Contains("basinc"))
                return "hPa";

            if (name.Contains("nem"))
                return "%";

            if (name.Contains("rüzgar hızı") || name.Contains("ruzgar hizi"))
                return "m/s";

            if (name.Contains("rüzgar yön") || name.Contains("ruzgar yon"))
                return "°";

            return "";
        }
    }
}
