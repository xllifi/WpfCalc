using System.Windows;
using Dark.Net;

namespace WpfCalc;

public partial class InfoWindow : Window {
    public InfoWindow() {
        InitializeComponent();
        DarkNet.Instance.SetWindowThemeWpf(this, Theme.Dark);
    }
}