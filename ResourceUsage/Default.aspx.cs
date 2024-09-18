using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace ResourceUsage
{
    public partial class Default : System.Web.UI.Page
    {
        protected static PerformanceCounter CPUCounter;
        protected static PerformanceCounter availableMemoryCounter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //PrintPerformanceCounterParameters();

                CPUCounter = new PerformanceCounter();
                CPUCounter.CategoryName = "Processor";
                CPUCounter.CounterName = "% Processor Time";
                CPUCounter.InstanceName = "_Total";

                availableMemoryCounter = new PerformanceCounter();
                availableMemoryCounter.CategoryName = "Memory";
                availableMemoryCounter.CounterName = "Available KBytes";
            }
        }

        protected void OnTick(object sender, EventArgs e)
        {
            float cpuUsage = GetCpuUsage();
            float availableMemory = GetAvailableMemory()/1024;
            float totalMemory = GetTotalMemory()/1024/1024;

            // Check the if condition for value greater than 90.
            lblCPU.Text = cpuUsage.ToString("N2") + " %";
            lblRamUsage.Text = ((totalMemory-availableMemory) / totalMemory * 100).ToString("N2") + " %";
            lblRam.Text = availableMemory.ToString("N0") + " MB / " + totalMemory.ToString("N0") + " MB";
        }

        public float GetCpuUsage()
        {
            return CPUCounter.NextValue();
        }

        public float GetAvailableMemory()
        {
            return availableMemoryCounter.NextValue();
        }

        public float GetTotalMemory()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }

        private static void PrintPerformanceCounterParameters()
        {
            var sb = new StringBuilder();
            PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();

            var desiredCategories = new HashSet<string> { "Process", "Memory" };

            foreach (var category in categories)
            {
                sb.AppendLine("Category: " + category.CategoryName);
                if (desiredCategories.Contains(category.CategoryName))
                {
                    PerformanceCounter[] counters;
                    try
                    {
                        counters = category.GetCounters("devenv");
                    }
                    catch (Exception)
                    {
                        counters = category.GetCounters();
                    }

                    foreach (var counter in counters)
                    {
                        sb.AppendLine(counter.CounterName + ": " + counter.CounterHelp);
                    }
                }
            }
            File.WriteAllText(@"C:\performanceCounters.txt", sb.ToString());
        }
    }
}