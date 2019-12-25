using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace DataManager
{
    class Program:Application
    {
        [STAThread()]
        static void Main()
        {
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            if (lf.ifom)
            {
                //MainWindow window = new MainWindow();
                Program app = new Program();
                //app.MainWindow = new LoadWindow();
                //app.MainWindow.ShowDialog();
                app.MainWindow = new MainWindow();
                RuntimeManager.Bind(ProductCode.EngineOrDesktop);
                app.MainWindow.ShowDialog();

            }
        }
    }
}
