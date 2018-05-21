using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.ViewModels
{
    public class UpdateLaneStatusViewModel
    {
        public string laneName { get; set; }

        public string changelaneName { get; set; }

        public int currentLaneStatus { get; set; }

        public int changeLaneStatus { get; set; }

        public int queuelistID { get; set; }

        public QueueListViewModel queuelist { get; set; }
    }
}
