﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Security;
using UnicefVirtualWarehouse;

namespace UnicefVirtualWarehouseTest
{
    public class FakeApp : MvcApplication
    {
        public FakeApp()
        {
            this.Init();
            this.StartDatabaseContext();

            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\projects\virtualwearhouse\UnicefVirtualWarehouse\UnicefVirtualWarehouse\App_Data");
        }

        public void RenewUnicefContext()
        {
            currentContext.Dispose();
            currentContext = new UnicefContext();
        }

        public void BeginTest()
        {
            Application_BeginRequest(this, new EventArgs());
        }

        public void EndTest()
        {
            Application_EndRequest(this, new EventArgs());
        }

        public void Application_Error(object sender, EventArgs e)
        {
            base.Application_Error(sender, e);
        }
    }
}
