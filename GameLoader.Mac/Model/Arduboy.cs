using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using MonoMac.IOKit;

namespace GameLoader.Mac.Model
{
    public class Arduboy
    {
        public bool TryGetBootloader(out USBCommunicationDevice bootloader)
		{
			bootloader = default(USBCommunicationDevice);
   
            var arduboy = default(USBCommunicationDevice);

            if (TryGetArduboy(out arduboy))
            {
                SendResetSignal(arduboy);

                var success = false;
                var attempt = 0;

                while (attempt++ < 100 && !success)
                {
                    success = TryGetArduboyBootloader(out bootloader);
                    Thread.Sleep(100);
                }

                return success;
            }
			
            return false;			
		}

        private static void SendResetSignal(USBCommunicationDevice arduboy)
        {
            try
            {
                using (var port = new SerialPort(arduboy.ComName, 1200))
                {
                    port.DtrEnable = true;
                    port.Open();
                    Thread.Sleep(500);
                    port.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to send reset signal: {ex.Message}");
            }
        }

        private static bool TryGetArduboy(out USBCommunicationDevice device) => TryGetDevice(9025, 32822, out device);
		private static bool TryGetArduboyBootloader(out USBCommunicationDevice device) => TryGetDevice(9025, 54, out device);

		private static bool TryGetDevice(int vendorId, int productId, out USBCommunicationDevice device)
		{
			device = SerialPortIOKit.GetUSBCommunicationDevices().FirstOrDefault(d => d.VendorID == vendorId && d.ProductID == productId);
			return device.VendorID == vendorId && device.ProductID == productId;
		}
    }
}
