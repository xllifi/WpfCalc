using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using Dark.Net;

namespace WpfCalc;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        DarkNet.Instance.SetWindowThemeWpf(this, Theme.Dark);
    }

    private void AppendOperator(string op) {
        if (TxtDisplay.Text == "" && op != "-") {
            return;
        }
        
        TxtDisplay.Text = Regex.Replace(TxtDisplay.Text, @"[-+\/*]+$", "") + op;
    }

    private void AppendDigit(string dg) {
        if (TxtDisplay.Text == "0") {
            TxtDisplay.Text = dg;
            return;
        }
        
        TxtDisplay.Text += dg;
    }

    private void Btn9_Click(object sender, RoutedEventArgs e) {
        AppendDigit("9");
    }

    private void Btn8_Click(object sender, RoutedEventArgs e) {
        AppendDigit("8");
    }

    private void Btn7_Click(object sender, RoutedEventArgs e) {
        AppendDigit("7");
    }

    private void Btn6_Click(object sender, RoutedEventArgs e) {
        AppendDigit("6");
    }

    private void Btn5_Click(object sender, RoutedEventArgs e) {
        AppendDigit("5");
    }

    private void Btn4_Click(object sender, RoutedEventArgs e) {
        AppendDigit("4");
    }

    private void Btn3_Click(object sender, RoutedEventArgs e) {
        AppendDigit("3");
    }

    private void Btn2_Click(object sender, RoutedEventArgs e) {
        AppendDigit("2");
    }

    private void Btn1_Click(object sender, RoutedEventArgs e) {
        AppendDigit("1");
    }

    private void Btn0_Click(object sender, RoutedEventArgs e) {
        AppendDigit("0");
    }

    private void BtnDot_Click(object sender, RoutedEventArgs e) {
        if (TxtDisplay.Text == "" || Regex.IsMatch(TxtDisplay.Text, @"[-+\/*]+$")) {
            TxtDisplay.Text += "0.";
            return;
        }
        TxtDisplay.Text += ".";
    }

    private void BtnClear_Click(object sender, RoutedEventArgs e) {
        TxtDisplay.Text = "";
    }

    private void BtnDivide_Click(object sender, RoutedEventArgs e) {
        AppendOperator(@"÷");
    }

    private void BtnMultiply_Click(object sender, RoutedEventArgs e) {
        AppendOperator(@"×");
    }

    private void BtnMinus_Click(object sender, RoutedEventArgs e) {
        AppendOperator("-");
    }

    private void BtnPlus_Click(object sender, RoutedEventArgs e) {
        AppendOperator("+");
    }

    private void BtnSubmit_Click(object sender, RoutedEventArgs e) {
        if (!Regex.IsMatch(TxtDisplay.Text, "^[\\d-].*[\\d]$")) { return; }

        // More readable Regex.Replace(Math.Round(decimal.Parse(new DataTable().Compute(TxtDisplay.Text, null).ToString()!), 2).ToString().Replace(",", "."), @"\.0{0,2}$", "")
             string output = TxtDisplay.Text; // Get expression
                    output = output.Replace(@"÷", "/").Replace(@"×", "*");
                    output = new DataTable().Compute(output, null).ToString()!; // Compute expression and convert to String
        decimal tempOutput = decimal.Parse(output);
                tempOutput = Math.Round(tempOutput, 2);
                    output = Regex.Replace(tempOutput.ToString(), @"0+$", "");
                    output = output.Replace(",", ".");
        
        TxtDisplay.Text = output;
    }

    private void BtnBackspace_Click(object sender, RoutedEventArgs e) {
        TxtDisplay.Text = TxtDisplay.Text.Remove(TxtDisplay.Text.Length - 1, 1);
    }

    private void BtnFUN_Click(object sender, RoutedEventArgs e) {
        InfoWindow infoWindow = new InfoWindow();
        infoWindow.ShowDialog();
    }
}