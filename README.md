# Modbus Sensor Reader

## Proje Amacı

Modbus Sensor Reader, Modbus RTU ve Modbus TCP üzerinden haberleşen farklı sensörlerden veri okumak ve yönetmek amacıyla geliştirilmiş masaüstü uygulamasıdır.

Uygulamanın temel amacı, farklı üreticilere ait sensörlerin sabit bir yapıya bağlı kalmadan dinamik olarak tanımlanabilmesini sağlamaktır. Her sensör kendi parametrelerini, register adreslerini, katsayılarını ve birimlerini kullanıcı tarafından oluşturulabilen sensör profilleri ile yönetebilir.

Bu sayede uygulama yalnızca belirli bir sensöre değil, Modbus protokolünü kullanan farklı sensör tiplerine uyarlanabilir hale gelmiştir.

---

# Temel Özellikler

- Modbus RTU desteği
- Modbus TCP desteği
- Tek seferlik veri okuma
- Sürekli veri okuma
- Modbus register yazma işlemleri
- Birden fazla register'a yazabilme
- Farklı veri tiplerini destekleme
  - UInt16
  - Int16
  - UInt32
  - Int32
  - Float
  - Double

---

# Sensör Profil Sistemi

Uygulamanın en önemli özelliği dinamik sensör profil yapısıdır.

Her sensör için;

- Sensör Adı
- Slave ID
- Parametre Listesi

tanımlanabilir.

Örneğin;

MAWS

- Sıcaklık
- Basınç
- Nem
- Rüzgar Hızı
- Rüzgar Yönü

veya

BOSCH

- Sıcaklık
- Gaz Yoğunluğu

şeklinde tamamen farklı parametreler oluşturulabilir.

Her parametre için;

- Register Adresi
- Katsayı
- Birim

bilgileri kullanıcı tarafından belirlenebilir.

---

# Veri Okuma

Seçilen sensör profiline göre;

- Register okunur
- Ham değer alınır
- Katsayı uygulanır
- Sonuç hesaplanır

ve uygulama içerisinde görüntülenir.

Örnek;

| Parametre | Register | Ham Değer | Katsayı | Sonuç |
|-----------|----------|-----------|----------|--------|
| Sıcaklık | 0 | 253 | 0.1 | 25.3 °C |
| Basınç | 1 | 1013 | 1 | 1013 hPa |

---

# Desteklenen Modbus Fonksiyonları

Okuma

- FC01 - Read Coil Status
- FC02 - Read Input Status
- FC03 - Read Holding Register
- FC04 - Read Input Register

Yazma

- FC05 - Write Single Coil
- FC06 - Write Single Register
- FC15 - Write Multiple Coils
- FC16 - Write Multiple Registers

---

# Kullanım Akışı

1. Bağlantı tipi seçilir (RTU / TCP)
2. Bağlantı bilgileri girilir.
3. Sensör profili seçilir.
4. Register ve katsayı bilgileri tanımlanır.
5. Veri okunur.
6. Ham ve hesaplanan değerler görüntülenir.

---

# Kullanılan Teknolojiler

- C#
- .NET Windows Forms
- NModbus
- SerialPort
- Modbus RTU
- Modbus TCP

---

# Gelecek Geliştirmeler

- Sensör profillerini JSON dosyasında saklama
- Sensör profili düzenleme
- Sensör silme
- Parametre bazlı veri tipi seçimi
- Alarm limitleri
- Grafiksel veri gösterimi
- CSV / Excel veri aktarımı
- Gerçek zamanlı trend ekranı

---

# Hedef

Bu proje, farklı marka ve model Modbus sensörlerini tek bir uygulama üzerinden yönetebilen, genişletilebilir ve kullanıcı tarafından özelleştirilebilir bir sensör okuma platformu oluşturmayı amaçlamaktadır.

---

# Bu proje staj sürecinde geliştirilmektedir.
