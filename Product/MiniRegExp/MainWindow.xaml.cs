using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RegExpService;

namespace MiniRegExp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Service service = new();
        private readonly bool initialized = false;

        public MainWindow()
        {
            InitializeComponent();
            service.Load(Service.C_DefaultName);
            RefreshRegexComponents();
            initialized = true;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            service.Name = Service.C_DefaultName;
            service.Save();
            Close();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string warningText = string.Empty;
            if (string.IsNullOrEmpty(RegexTextBox.Text))
                warningText += "The regular expression is not set!\r\n";
            if (string.IsNullOrEmpty(InputTextBox.Text))
                warningText += "The text is not set!\r\n";
            if (ComboBoxRegexType.Text == RegExpType.Replace.ToString() && string.IsNullOrEmpty(ReplacementTextBox.Text))
                warningText += "The pattern is not set!\r\n";
            if (warningText != string.Empty)
            {
                MessageBox.Show(warningText, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SetRegexParamsAndRun();
            ButtonResultInfo.Visibility = Visibility.Visible;
            ButtonStart.Visibility = Visibility.Hidden;
        }

        private void SetRegexParamsAndRun()
        {
            service.Multiline = (bool)CheckBoxMultiLine.IsChecked;
            service.IgnoreCase = (bool)CheckBoxIgnoreCase.IsChecked;
            service.Singleline = (bool)CheckBoxSingleLine.IsChecked;
            service.Pattern = RegexTextBox.Text;
            service.Input = InputTextBox.Text;
            service.Replacement = ReplacementTextBox.Text;
            service.RegExpType = (RegExpType)Enum.Parse(typeof(RegExpType), ((ComboBoxItem)ComboBoxRegexType.SelectedItem).Content.ToString());
            ResultTextBox.Text = service.GetResultText();
        }

        private void ChangedRegexData()
        {
            string regExpType = ((ComboBoxItem)ComboBoxRegexType.SelectedItem).Content.ToString();
            if (ResultTextBox.Text != string.Empty && RegexTextBox.Text == service.Pattern && InputTextBox.Text == service.Input && regExpType == service.RegExpType.ToString() 
                && CheckBoxMultiLine.IsChecked == service.Multiline && CheckBoxIgnoreCase.IsChecked == service.IgnoreCase && CheckBoxSingleLine.IsChecked == service.Singleline &&
                ((regExpType == RegExpType.Match.ToString()) || (regExpType == RegExpType.Replace.ToString() && service.Replacement == ReplacementTextBox.Text)) )
            {
                ButtonResultInfo.Visibility = Visibility.Visible;
                ButtonStart.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonResultInfo.Visibility = Visibility.Hidden;
                ButtonStart.Visibility = Visibility.Visible;
            }
        }

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void RefreshRegexComponents()
        {
            RegexTextBox.Text = service.Pattern;
            InputTextBox.Text = service.Input;
            CheckBoxMultiLine.IsChecked = service.Multiline;
            CheckBoxIgnoreCase.IsChecked = service.IgnoreCase;
            CheckBoxSingleLine.IsChecked = service.Singleline;
        }

        private void ComboBoxRegexType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!initialized)
                return;
            ChangedRegexData();
            if (((ComboBoxItem)ComboBoxRegexType.SelectedItem).Content.ToString() == RegExpType.Match.ToString())
            {
                ReplacementLabel.Visibility = Visibility.Collapsed;
                ReplacementTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                ReplacementLabel.Visibility = Visibility.Visible;
                ReplacementTextBox.Visibility = Visibility.Visible;
            }
        }

        private void CheckBoxMultiLine_Checked(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void CheckBoxMultiLine_Unloaded(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void CheckBoxIgnoreCase_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void CheckBoxIgnoreCase_Checked(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void CheckBoxSingleLine_Checked(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void CheckBoxSingleLine_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangedRegexData();
        }

        private void ButtonResultInfo_Click(object sender, RoutedEventArgs e)
        {
            ActionControls(Visibility.Hidden);
            InfoPanel infoPanel = new(service.GetMatchInformations());
            infoPanel.ShowDialog();
            ActionControls(Visibility.Visible);
        }

        private void ActionControls(Visibility pHidden)
        {
            ButtonStart.Visibility = pHidden;
            ButtonExit.Visibility = pHidden;
            //ButtonLoad.Visibility = pHidden;
            //ButtonSave.Visibility = pHidden;
            ButtonResultInfo.Visibility = pHidden;
        }

        private void RegexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangedRegexData();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangedRegexData();
        }

        private void ReplacementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangedRegexData();
        }
    }
}
