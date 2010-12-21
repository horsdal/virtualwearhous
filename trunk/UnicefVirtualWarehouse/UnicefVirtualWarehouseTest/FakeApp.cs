using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnicefVirtualWarehouse;

namespace UnicefVirtualWarehouseTest
{
    public class FakeApp : MvcApplication
    {
        public FakeApp()
        {
            this.Init();
            this.StartDatabaseContext();
        }

        public void BeginTest()
        {
            Application_BeginRequest(this, new EventArgs());
        }

        public void EndTest()
        {
            Application_EndRequest(this, new EventArgs());
        }
    }
}
