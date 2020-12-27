using AAV.WPF.AltBpr;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AudioFrequencyExplorer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Loaded += async (s, e) => 
      {
        await ChimerAlt.PlayFreqList();
      };
    }

    async void on0(object s, RoutedEventArgs e) => await ChimerAlt.PlayFreqList();
    async void on1(object s, RoutedEventArgs e) => await ChimerAlt.FreqWalkUp();
    async void on2(object s, RoutedEventArgs e) => await ChimerAlt.FreqWalkDn();
    async void on3(object s, RoutedEventArgs e) => await ChimerAlt.FreqRunUpDn();
    async void on4(object s, RoutedEventArgs e) => await ChimerAlt.WakeAudio();
    async void on5(object s, RoutedEventArgs e) => await ChimerAlt.NoteWalk();

    void onFreq(object s, RoutedPropertyChangedEventArgs<double> e)
    {
      tbxFreq.Text = $" {freq(e.NewValue):N0} Hz ";
      tbxWLen.Text = $" {wavelength(e.NewValue).ToString().Substring(0, 4)} m ";
    }
    void onDurn(object s, RoutedPropertyChangedEventArgs<double> e) => tbxDurn.Text = $" {e.NewValue:N0} ms ";
    async void onPlay(object s, RoutedEventArgs e) => await play();

    void onClose(object sender, RoutedEventArgs e) => Close();
    void chkIsAuto_Checked(object s, RoutedEventArgs e) { }

    static double freq(double e) => Math.Pow(2, e);
    static double wavelength(double e) => 344.007 / Math.Pow(2, e);
    async Task play()
    {
      try
      {
        do
        {
          await ChimerAlt.BeepFD((int)freq(slrFreq.Value), slrDurn.Value * .001); //todo: .ConfigureAwait(false);
          //if (chkIsKern.IsChecked == true)          {          }          else          {          }
        } while (chkIsAuto.IsChecked == true);
      }
      catch (Exception ex) { tbxEx.Text = ex.Message; }
    }
  }
}
