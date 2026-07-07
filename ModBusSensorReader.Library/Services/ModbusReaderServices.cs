using ModbusSensorReader.Library.Models;
using NModbus;
using NModbus.Serial;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSensorReader.Library.Services
{
    public class ModbusReaderServices
    {
        // TCP veya RTU bağlantı tipine göre modbus okuma işlemi yapar
        public ModbusReadResult Read(ModbusReadRequest request)
        {
            if (request.ConnectionType == "TCP")
            {
                return ReadTcp(request);
            }
            return ReadRtu(request);
        }

        // TCP veya RTU bağlantı tipine göre modbus yazma işlemi yapar
        public void Write(ModbusReadRequest request)
        {
            switch (request.ConnectionType)
            {
                case "TCP":
                    WriteTcp(request);
                    break;

                case "RTU":
                    WriteRtu(request);
                    break;

                default:
                    throw new Exception("Geçersiz bağlantı tipi.");
            }
        }

        // RTU modbus yazma işlemi yapar
        private void WriteRtu(ModbusReadRequest request)
        {
            using var serialPort = new SerialPort(request.PortName)
            {
                BaudRate = request.BaudRate,
                DataBits = request.DataBits,
                Parity = Enum.Parse<Parity>(request.Parity),
                StopBits = Enum.Parse<StopBits>(request.StopBits),
                ReadTimeout = 3000,
                WriteTimeout = 3000
            };

            serialPort.Open();

            var factory = new ModbusFactory();
            var master = factory.CreateRtuMaster(serialPort);

            WriteValue(master, request);
        }

        // Yazma işlemi için TCP bağlantısı kurar ve belirtilen IP ve port üzerinden register yazma işlemini yapar.
        private void WriteTcp(ModbusReadRequest request)
        {
            using var tcpClient = new TcpClient();
            tcpClient.Connect(request.IpAddress, request.TcpPort);

            var factory = new ModbusFactory();
            var master = factory.CreateMaster(tcpClient);

            WriteValue(master, request);
        }

        // Modbus master üzerinden istekten gelen function code'a göre register yazma işlemini yapar
        private void WriteValue(IModbusMaster master, ModbusReadRequest request)
        {
            switch (request.FunctionCode)
            {
                case 5:
                    master.WriteSingleCoil(
                        request.SlaveId,
                        request.WriteAddress,
                        ParseBool(request.WriteValue));
                    break;

                case 6:
                    master.WriteSingleRegister(
                        request.SlaveId,
                        request.WriteAddress,
                        ushort.Parse(request.WriteValue));
                    break;

                case 15:
                    bool[] coils = request.WriteValue
                        .Split(',')
                        .Select(x => x.Trim() == "1" || x.Trim().Equals("true", StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    master.WriteMultipleCoils(request.SlaveId, request.WriteAddress, coils);
                    break;

                case 16:
                    ushort[] addresses = request.WriteAddressText
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => ushort.Parse(x.Trim()))
                        .ToArray();

                    ushort[] values = request.WriteValue
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => ushort.Parse(x.Trim()))
                        .ToArray();

                    if (addresses.Length == 1)
                    {
                        master.WriteMultipleRegisters(
                            request.SlaveId,
                            addresses[0],
                            values);
                    }
                    else
                    {
                        if (addresses.Length != values.Length)
                            throw new Exception("Adres sayısı ile değer sayısı aynı olmalı.");

                        for (int i = 0; i < addresses.Length; i++)
                        {
                            master.WriteSingleRegister(
                                request.SlaveId,
                                addresses[i],
                                values[i]);
                        }
                    }

                    break;
                default:
                    throw new NotSupportedException("...");
            }
        }


        // Yazma işlemi için gelen değerleri bool tipine dönüştürür
        private bool ParseBool(string value)
        {
            return value == "1" ||
                   value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                   value.Equals("on", StringComparison.OrdinalIgnoreCase);
        }

        // istekten gelen parametrelere göre RTU modbus okuma işlemi yapar
        private ModbusReadResult ReadRtu(ModbusReadRequest request)
        {
            using var serialPort = new SerialPort(request.PortName)
            {
                BaudRate = request.BaudRate,
                DataBits = request.DataBits,
                Parity = Enum.Parse<Parity>(request.Parity),
                StopBits = Enum.Parse<StopBits>(request.StopBits),
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            serialPort.Open();

            var factory = new ModbusFactory();
            var master = factory.CreateRtuMaster(serialPort);

            ushort[] registers = ReadRegisters(master, request);
            return BuildResult(registers, request);
        }

        // Modbus TCP bağlantısı kurarak belirtilen IP ve port üzerinden register okuma işlemini yapar.
        private ModbusReadResult ReadTcp(ModbusReadRequest request)
        {
            using var tcpClient = new TcpClient();
            tcpClient.Connect(request.IpAddress, request.TcpPort);

            var factory = new ModbusFactory();
            var master = factory.CreateMaster(tcpClient);

            ushort[] registers = ReadRegisters(master, request);

            return BuildResult(registers, request);
        }

        // Master üzerinden istekten gelen function code'a göre register okuma işlemini yapar
        private ushort[] ReadRegisters(IModbusMaster master, ModbusReadRequest request)
        {
            return request.FunctionCode switch
            {
                1 => master.ReadCoils(
                        request.SlaveId,
                        request.StartAddress,
                        request.RegisterCount)
                    .Select(x => (ushort)(x ? 1 : 0))
                    .ToArray(),

                2 => master.ReadInputs(
                        request.SlaveId,
                        request.StartAddress,
                        request.RegisterCount)
                    .Select(x => (ushort)(x ? 1 : 0))
                    .ToArray(),

                3 => master.ReadHoldingRegisters(
                        request.SlaveId,
                        request.StartAddress,
                        request.RegisterCount),

                4 => master.ReadInputRegisters(
                        request.SlaveId,
                        request.StartAddress,
                        request.RegisterCount),

                _ => throw new NotSupportedException("..")
            };
        }

        // ModbusReadResult nesnesini oluşturur ve register değerlerini, ham metni, parse edilmiş değeri ve okuma zamanını içerir.
        private ModbusReadResult BuildResult(ushort[] registers, ModbusReadRequest request)
        {
            return new ModbusReadResult
            {
                Registers = registers,
                RawText = string.Join(", ", registers),
                ParsedValue = ConvertValue(registers, request.DataType),
                ReadTime = DateTime.Now

            };
           
        }

        // Register değerlerini istenilen veri tipine göre dönüştürür ve string olarak döner
        private string ConvertValue(ushort[] registers, string dataType)
        {
            if (registers == null || registers.Length == 0)
                return "-";

            return dataType switch
            {
                "UInt16" => registers[0].ToString(),

                "Int16" => unchecked((short)registers[0]).ToString(),

                "Int32" when registers.Length >= 2 =>
                    ((registers[0] << 16) | registers[1]).ToString(),

                "UInt32" when registers.Length >= 2 =>
                    (((uint)registers[0] << 16) | registers[1]).ToString(),

                _ => registers[0].ToString()
            };
        }

    }
}


        

