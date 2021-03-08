using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat
{
    public partial class Chat : ServiceBase
    {
        public Chat()
        {
            InitializeComponent();
        }

        static public void escribeEvento(string mensaje)
        {
            string nombre = "Service";
            string logDestino = "Application";
            if (!EventLog.SourceExists(nombre))
            {
                EventLog.CreateEventSource(nombre, logDestino);
            }
            EventLog.WriteEntry(nombre, mensaje);
        }
        protected override void OnStart(string[] args)
        {
            escribeEvento("Ejecutando OnStart");
            TheProgram tp = new TheProgram();
            Thread th = new Thread(tp.init);
            th.IsBackground = true;
            th.Start();
        }
        private int tiempo = 0;
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            escribeEvento(string.Format("Servicio en ejecución durante {0} segundos",

            tiempo));

            tiempo += 10;
        }
        protected override void OnStop()
        {
            escribeEvento("Deteniendo servicio");
            tiempo = 0;
        }

        protected override void OnPause()
        {
            escribeEvento("Servicio en Pausa");
        }
        protected override void OnContinue()
        {
            escribeEvento("Continuando servicio");
        }
    }
}
