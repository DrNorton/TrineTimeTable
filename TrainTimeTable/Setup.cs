using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using TrainTimeTable.Shared;

namespace TrainTimeTable
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new MainSetup();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }

    public class DebugTrace : IMvxTrace
    {
        public void Trace(MvxTraceLevel level, string tag, Func<string> message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message());
        }

        public void Trace(MvxTraceLevel level, string tag, string message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message);
        }

        public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
        {
            try
            {
                Debug.WriteLine(string.Format(tag + ":" + level + ":" + message, args));
            }
            catch (FormatException)
            {
                Trace(MvxTraceLevel.Error, tag, "Exception during trace of {0} {1} {2}", level, message);
            }
        }
    }
}
