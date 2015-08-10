﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Views;
using TrainTimeTable.Shared.ViewModels;
using TrainTimeTable.Shared.ViewModels.Base;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrainTimeTable.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellView : MvxWindowsPage
    {
     
        public ShellView(Frame frame)
        {
            this.InitializeComponent();
            this.SplitView.Content = frame;
            this.Loaded += ShellView_Loaded;
        }

        private void ShellView_Loaded(object sender, RoutedEventArgs e)
        {
          //this.SplitView.Pane.Visibility=Visibility.Collapsed;
            
        }

     

        private void HamburgerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.SplitView.IsPaneOpen)
            {
                
                this.SplitView.IsPaneOpen = true;
               
            }
            else
            {
                this.SplitView.IsPaneOpen = false;
                
            }
        }

       

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Mvx.Resolve<ShellViewModel>();
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            var frame = (this.SplitView.Content as Frame);
            var currentViewModel=((frame.Content as MvxWindowsPage).DataContext as LoadingScreen);
            currentViewModel.Close();
        }
    }
}
