using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Dark.Net;
using Stfu.Linq;

namespace WpfCalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        DarkNet.Instance.SetWindowThemeWpf(this, Theme.Dark);
    }

    private void AppendOperator(object sender, RoutedEventArgs e) {
        Button btn = (Button)sender;
        string op = (string)btn.Content;
        
        if (TxtDisplay.Text == "" && op != "-") {
            return;
        }
        
        TxtDisplay.Text = Regex.Replace(TxtDisplay.Text, @"[-+\/*]+$", "") + op;
    }

    private void AppendDigit(object sender, RoutedEventArgs e) {
        Button btn = (Button)sender;
        string dg = (string)btn.Content;
        
        if (TxtDisplay.Text == "0") {
            TxtDisplay.Text = dg;
            return;
        }
        
        TxtDisplay.Text += dg;
    }

    private void BtnDot_Click(object sender, RoutedEventArgs e) {
        if (Regex.IsMatch(TxtDisplay.Text, @"\.$")) return;
        if (TxtDisplay.Text == "" || Regex.IsMatch(TxtDisplay.Text, @"[-+\/*]+$")) {
            TxtDisplay.Text += "0.";
            return;
        }
        TxtDisplay.Text += ".";
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e) {
        TxtDisplay.Text = "";
    }

    private void BtnSubmit_Click(object sender, RoutedEventArgs e) {
        if (!Regex.IsMatch(TxtDisplay.Text, @"^[\d-].*[\d]$") || Regex.IsMatch(TxtDisplay.Text, @"÷0$")) { 
            TxtDisplay.Text = "NaN";
            return;
        }

        // More readable Regex.Replace(Math.Round(decimal.Parse(new DataTable().Compute(TxtDisplay.Text, null).ToString()!), 2).ToString().Replace(",", "."), @"\.0{0,2}$", "")
             string output = TxtDisplay.Text; // Get expression
                    output = output.Replace(@"÷", "/").Replace(@"×", "*");
                    output = new DataTable().Compute(output, null).ToString()!; // Compute expression and convert to String
         double tempOutput = double.Parse(output);
                tempOutput = Math.Round(tempOutput, 2);
                    output = tempOutput.ToString().Replace(",", ".");
        
        TxtDisplay.Text = output;
    }

    private void BtnBackspace_Click(object sender, RoutedEventArgs e) {
        if (TxtDisplay.Text.Length == 0) return;
        TxtDisplay.Text = TxtDisplay.Text.Remove(TxtDisplay.Text.Length - 1, 1);
    }

    private void BtnFUN_Click(object sender, RoutedEventArgs e) {
        InfoWindow infoWindow = new InfoWindow();
        infoWindow.ShowDialog();
    }
}