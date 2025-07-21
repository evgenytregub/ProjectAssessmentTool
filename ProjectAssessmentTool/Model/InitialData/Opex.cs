using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model
{
    public partial class Opex : ObservableObject
    {
        [ObservableProperty]
        private string materialName = string.Empty;

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
                OnPropertyChanged(nameof(OpexSum));
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
                OnPropertyChanged(nameof(OpexSum));
            }
        }

        public decimal OpexSum
        {
            get
            {
                try
                {
                    return checked(ValueA * ValueB);
                }
                catch (OverflowException)
                {
                    SystemMessages.SystemMessages.ValueOutOfRange("0", decimal.MaxValue.ToString());
                    return decimal.MaxValue;
                }
            }
        }
    }
}
