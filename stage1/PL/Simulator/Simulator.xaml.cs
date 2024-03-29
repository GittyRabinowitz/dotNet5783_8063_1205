﻿using Simulator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BlApi.IBl bl;
        string nextStatus;
        string previousStatus;
        BackgroundWorker worker;
        //====== disable the option of closing the window =======
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //=====================================================
        private Stopwatch stopWatch;
        private bool isTimerRun;
        //BackgroundWorker worker;
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;
        Tuple<BO.Order, int, string, string> dcT;
        Details details;

        public SimulatorWindow(BlApi.IBl Bl)
        {
            InitializeComponent();
            this.bl = Bl;
            Loaded += ToolWindow_Loaded;
            TimerStart();
        }
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        void ProgressBarStart(int sec)
        {
            if (ProgressBar != null)
            {
                SBar.Items.Remove(ProgressBar);
            }
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 30;
            duration = new Duration(TimeSpan.FromSeconds(sec * 2));
            doubleanimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            SBar.Items.Add(ProgressBar);
        }
        void TimerStart()
        {
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += TimerDoWork;
            worker.ProgressChanged += TimerProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }

        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.ProgressChange += changeOrder;
            Simulator.Simulator.StopSimulator += Stop;
            Simulator.Simulator.run();
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SimulatorTXTB.Text = DateTime.Now.ToString();
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.DoStop();

            this.Close();

        }

        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is Details))
                return;

            this.details = e as Details;
            this.previousStatus = (details.order.ShipDate == DateTime.MinValue) ? BO.eOrderStatus.ordered.ToString() : BO.eOrderStatus.shipped.ToString();
            this.nextStatus = (details.order.ShipDate == DateTime.MinValue) ? BO.eOrderStatus.shipped.ToString() : BO.eOrderStatus.delivered.ToString();
            dcT = new Tuple<BO.Order, int, string, string>(details.order, details.seconds / 1000, previousStatus, nextStatus);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                DataContext = dcT;
                ProgressBarStart(details.seconds / 1000);
            }
        }
        public void Stop(object sender, EventArgs e)
        {
            Simulator.Simulator.ProgressChange -= changeOrder;
            Simulator.Simulator.StopSimulator -= Stop;
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }

            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(Stop, sender, e);
            }
            else
            {
                MessageBox.Show("complete updating");
                this.Close();
            }
        }
    }
}