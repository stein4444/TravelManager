using Esri.ArcGISRuntime;
using System;
using System.Windows;
using TravelManager.Presentation.DependencyInjection;

namespace TravelManager.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                IocKernel.Initialize(new IocConfiguration());
                ArcGISRuntimeEnvironment.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ArcGIS Runtime initialization failed.");

                // Exit application
                this.Shutdown();
            }

        }
    }
}
