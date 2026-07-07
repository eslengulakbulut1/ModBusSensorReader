# Modbus Sensor Reader

## Proje Hakkında

Modbus Sensor Reader, Modbus RTU ve Modbus TCP haberleşme protokollerini kullanarak sensör veya PLC cihazlarından veri okumak ve Modbus slave cihazlarına veri yazmak amacıyla geliştirilmiş bir Windows Forms uygulamasıdır.

Uygulama NModbus kütüphanesini kullanarak farklı Modbus Function Code'larını desteklemektedir. Kullanıcı arayüzü üzerinden bağlantı parametreleri, okuma ve yazma ayarları dinamik olarak yapılandırılabilir.

---

# Özellikler

## Bağlantı

- Modbus RTU desteği
- Modbus TCP desteği
- COM Port seçimi
- BaudRate seçimi
- DataBits seçimi
- Parity seçimi
- StopBits seçimi
- Slave ID seçimi

---

## Desteklenen Function Code'lar

### Okuma

- 01 - Read Coil Status
- 02 - Read Input Status
- 03 - Read Holding Registers
- 04 - Read Input Registers

### Yazma

- 05 - Write Single Coil
- 06 - Write Single Register
- 15 - Write Multiple Coils
- 16 - Write Multiple Registers

---

## Desteklenen Veri Tipleri

### Okuma

- UInt16
- Int16
- UInt32
- Int32

### Yazma

- Bool
- UInt16
- Int16
- UInt32
- Int32
- Float
- Double

---

# Kullanıcı Arayüzü

Uygulama dört ana bölümden oluşmaktadır.

## 1. Bağlantı Ayarları

Bu bölümde;

- Bağlantı tipi
- COM Port
- TCP/IP bilgileri
- BaudRate
- DataBits
- Parity
- StopBits

ayarları yapılmaktadır.

---

## 2. ModBus İşlem Ayarları

Bu bölümde;

- Slave ID
- Function Code
- Register Count
- Sampling Time
- Read Start Address
- Read Data Type
- Write Address
- Write Value
- Write Data Type

alanları bulunmaktadır.

Seçilen Function Code'a göre ilgili okuma veya yazma alanları otomatik olarak aktif/pasif hale gelmektedir.

---

## 3. Okunan Değer

Okuma sonucunda;

- Ham Register Verisi
- Parse Edilmiş Sensör Verisi
- Son Okuma Zamanı

gösterilmektedir.

---

## 4. Log Kayıtları

Gerçekleşen tüm işlemler;

- Bağlantı
- Okuma
- Yazma
- Hatalar

anlık olarak görüntülenmektedir.

İstenirse "Dosyaya Kaydet" seçeneği ile loglar otomatik olarak txt dosyasına kaydedilmektedir.

---

# Sürekli Okuma

Sampling Time alanına girilen saniye değerine göre uygulama belirlenen aralıklarla otomatik olarak Modbus okuma işlemini gerçekleştirmektedir.

Örnek:

Sampling Time = 2

→ Her 2 saniyede bir okuma yapılır.

---

# Multiple Register Yazma

16 - Write Multiple Registers fonksiyonu geliştirilmiştir.

Tek bir istekte;

Adresler

0,1,2,3

Değerler

100,200,300,400

şeklinde girilerek birden fazla register aynı anda yazılabilmektedir.

---

# Veri Dönüştürme

Okunan register verileri seçilen veri tipine göre dönüştürülmektedir.

Örneğin;

- UInt16
- Int16
- UInt32
- Int32

tipleri desteklenmektedir.

Yazma işlemlerinde ise;

- UInt16
- Int16
- UInt32
- Int32
- Float
- Double

veri tipleri kullanılabilmektedir.

---

# Log Sistemi

Uygulama içerisinde gerçekleşen tüm işlemler zaman damgası ile kayıt altına alınmaktadır.

Örnek;

```
[10:45:15] Bağlantı ayarları hazırlandı.
[10:45:17] Okuma başarılı.
[10:45:20] Yazma işlemi başarılı.
```

Loglar isteğe bağlı olarak;

```
Desktop
 └── staj
      └── Log
            └── ModbusLog_YYYYMMDD.txt
```

konumuna otomatik kaydedilmektedir.

---

# Responsive Arayüz

Arayüz yeniden düzenlenmiştir.

Yapılan geliştirmeler;

- TableLayoutPanel kullanımı
- Responsive pencere yapısı
- Dock = Fill düzeni
- Dinamik GroupBox yerleşimi
- Butonların responsive hizalanması
- Log alanının otomatik genişlemesi

---

# Kullanılan Teknolojiler

- C#
- .NET Windows Forms
- NModbus
- SerialPort
- TCP/IP
- Modbus RTU
- Modbus TCP

---

# Proje Yapısı

```
ModBusSensorReader

│
├── UI
│     Form1
│
├── Library
│     Services
│         ModbusReaderServices.cs
│
│     Models
│         ModbusReadRequest.cs
│         ModbusReadResult.cs
│
└── README.md
```

---

# Geliştirme Sürecinde Yapılan Başlıca Çalışmalar

- Modbus RTU haberleşmesi oluşturuldu.
- Modbus TCP haberleşmesi oluşturuldu.
- Function Code yönetimi geliştirildi.
- Sürekli okuma (Timer) sistemi geliştirildi.
- Log sistemi oluşturuldu.
- Txt dosyasına log kaydetme özelliği eklendi.
- Responsive kullanıcı arayüzü geliştirildi.
- Multiple Register Write desteği eklendi.
- Birden fazla register adresine aynı anda veri yazma desteği geliştirildi.
- Veri tipi dönüşümleri geliştirildi.
- Bağlantı yönetimi iyileştirildi.
- Hata yönetimi ve kullanıcı bilgilendirme sistemi geliştirildi.

---

# Gelecek Geliştirmeler

- Float ve Double veri tiplerinin tüm Function Code'larda tam desteklenmesi
- Word Swap / Byte Swap seçenekleri
- Modbus Mapping ekranı
- CSV/Excel veri aktarımı
- Grafiksel veri izleme
- PLC Tag yönetimi
- Konfigürasyon dosyası desteği
- Tema (Dark / Light Mode)
- Çoklu Slave desteği
- Bağlantı profili kaydetme
- Alarm ve uyarı sistemi

---

# Lisans

Bu proje staj süreci kapsamında geliştirilmiştir.
