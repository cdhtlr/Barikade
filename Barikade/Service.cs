using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Management;
using System.Threading.Tasks;

namespace Barikade
{
    /// <summary>
    /// The main service class.
    /// </summary>
    internal partial class Service : ServiceBase
    {
        private ManagementEventWatcher processWatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        internal Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start function to set up the event watcher to monitor process creation.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");
            processWatcher = new ManagementEventWatcher(query);
            processWatcher.EventArrived += ProcessWatcher_EventArrived;
            processWatcher.Start();
        }

        /// <summary>
        /// Stop function to stop and clean up the event watcher.
        /// </summary>
        protected override void OnStop()
        {
            processWatcher.EventArrived -= ProcessWatcher_EventArrived;
            processWatcher.Stop();
            processWatcher.Dispose();
        }

        /// <summary>
        /// Processes watcher function.
        /// </summary>
        private void ProcessWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            try
            {
                var targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                var processPath = targetInstance["ExecutablePath"].ToString();

                if (!ProcessPath.IsAllowedPath(processPath))
                {
                    using (Process process = Process.GetProcessById(Convert.ToInt32(targetInstance["ProcessId"])))
                    {
                        Parallel.Invoke(
                            () => NetworkConnection.Drop(processPath),
                            () => process.Kill(),
                            () => FileOperation.Delete(processPath)
                        );
                    }
                }
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
