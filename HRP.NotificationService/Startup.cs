using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.NotificationService
{
    public class Startup
    {
        public static void Start()
        {
            NotificationService s = new NotificationService();
        }

        public Startup()
        {
            NotificationService s = new NotificationService();
        }
    }
}
