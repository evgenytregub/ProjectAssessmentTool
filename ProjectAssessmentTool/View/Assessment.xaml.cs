using ProjectAssessmentTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectAssessmentTool.View
{
    /// <summary>
    /// Interaction logic for Assessment.xaml
    /// </summary>
    public partial class Assessment : Window
    {
        private bool isProcessing = false;
        public Assessment(string projectName)
        {
            InitializeComponent();

            var viewModel = new AssessmentViewModel(projectName);
            viewModel.CloseAction = new Action(() => this.Close());
            this.DataContext = viewModel;



        }

        private void OnlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string input = e.Text;

                if (char.IsDigit(input, 0))
                {
                    e.Handled = false;
                    return;
                }

                if ((input == "." || input == ",") &&
                    !textBox.Text.Contains(".") &&
                    !textBox.Text.Contains(","))
                {
                    e.Handled = false;
                    return;
                }
            }

            e.Handled = true;
        }

        private void TextBox_Separator(object sender, TextChangedEventArgs e)
        {
            if (isProcessing) return;
            isProcessing = true;

            var tb = sender as TextBox;
            if (tb == null) return;

            // Заменим запятую на точку для парсинга
            string raw = tb.Text.Replace(" ", "").Replace(",", ".");

            if (decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal number))
            {
                tb.TextChanged -= TextBox_Separator;

                // Формат: пробел для тысяч, 2 знака после запятой
                tb.Text = number.ToString("#,0.##", CultureInfo.InvariantCulture).Replace(",", " ").Replace(".", ",");

                tb.CaretIndex = tb.Text.Length;
                tb.TextChanged += TextBox_Separator;
            }

            isProcessing = false;
        }
    }
}
