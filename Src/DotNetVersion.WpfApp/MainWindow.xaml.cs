using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotNetVersionChecker_WpfApp4._0
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    void Window_Loaded(object sender, RoutedEventArgs e)
    {

      try
      {

        tb1.Text = xArcGageWpfApp.GetDotNetVersion.Get();

      }
      catch (Exception ex)
      {
        tb1.Text = ex.ToString();
      }
    }
  }
}
