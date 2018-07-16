using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.DeviceService
{
    public class DeviceConfig
    {
        public static DeviceService _deviceService;

        public static void Register()
        {
            // Add device service
            _deviceService = new DeviceService();
            System.Threading.Thread th = new System.Threading.Thread(_deviceService.DeviceServiceThread);
            th.Start();
        }
    }
}
