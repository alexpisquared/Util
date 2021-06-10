using System;
using System.Windows;
using System.Windows.Input;

namespace DotNetVersionChecker_WpfApp4._0
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      MouseLeftButtonDown += (s, e) => DragMove(); KeyDown += (s, e) => { switch (e.Key) { case Key.Escape: Close(); App.Current.Shutdown(); break; } }; //tu: ultimate one-liner ;)
    }

    void onLoaded(object s, RoutedEventArgs e) { try { tb1.Text = xArcGageWpfApp.GetDotNetVersion.Get1(); } catch (Exception ex) { tb1.Text = ex.ToString(); } }
  }
}
