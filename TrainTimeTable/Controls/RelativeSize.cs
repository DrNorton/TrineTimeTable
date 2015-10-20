using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace TrainTimeTable.Controls
{
    public class RelativeSize : DependencyObject
    {
        private static List<FrameworkElement> elements = new List<FrameworkElement>();

        private static FrameworkElement Container = null;
        private static bool containerready = false;

        public static void SetContainer(UIElement element, FrameworkElement value)
        {
            element.SetValue(ContainerProperty, value);
        }
        public static FrameworkElement GetContainer(UIElement element)
        {
            return (FrameworkElement)element.GetValue(ContainerProperty);
        }
        public static readonly DependencyProperty ContainerProperty =
            DependencyProperty.RegisterAttached("Container", typeof(FrameworkElement), typeof(RelativeSize), new PropertyMetadata(null, ContainerChanged));

        private static void ContainerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Container = (e.NewValue as FrameworkElement);
            Container.SizeChanged += (sc, ec) =>
            {

                foreach (var element in elements)
                {
                    var rWidth = element.GetValue(RelativeSize.WidthProperty);

                    if (rWidth != null)
                    {
                        element.Width = (double)rWidth * Container.ActualWidth;
                    }
                }
            };
            containerready = true;
        }

        public static void SetWidth(UIElement element, double value)
        {
            element.SetValue(WidthProperty, value);
        }
        public static double GetWidth(UIElement element)
        {
            return (double)element.GetValue(WidthProperty);
        }
        public static readonly DependencyProperty WidthProperty =
          DependencyProperty.RegisterAttached("Width", typeof(double), typeof(RelativeSize), new PropertyMetadata(0.0, WidthChanged));

        private static async void WidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            while (!containerready)
                await Task.Delay(60);

           var fe = d as FrameworkElement;
            if (fe != null)
            {
                if (!elements.Contains(fe))
                    elements.Add(fe);
               
                    fe.Width = (double)e.NewValue * Container.ActualWidth;
                
         
            }
        }
    }
}
