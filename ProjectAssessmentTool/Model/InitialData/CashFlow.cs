using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model
{
    public partial class CashFlow : ObservableObject
    {
        [ObservableProperty]
        private string cashFlowYear = string.Empty;

        private decimal valueA;
        public decimal ValueA
        {
            get => valueA;
            set
            {
                if (value < 0 || value > 10_000_000m)
                {
                    SystemMessages.SystemMessages.ValueOutOfRange("0", "10 000 000");
                }

                SetProperty(ref valueA, value);
                OnPropertyChanged(nameof(CashFlowSum));
            }
        }

        private decimal valueB;
        public decimal ValueB
        {
            get => valueB;
            set
            {
                if (value < 0 || value > 10_000_000m)
                {
                    SystemMessages.SystemMessages.ValueOutOfRange("0", "10 000 000");
                }

                SetProperty(ref valueB, value);
                OnPropertyChanged(nameof(CashFlowSum));
            }
        }


        public decimal CashFlowSum => ValueB - ValueA;
        public decimal ExpensesSum => ValueA;
        public decimal IncomeSum => ValueB;

    }
}
