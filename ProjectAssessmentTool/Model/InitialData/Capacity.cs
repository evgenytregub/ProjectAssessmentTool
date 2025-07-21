using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ProjectAssessmentTool.Model
{
    public partial class Capacity : ObservableObject
    {
        [ObservableProperty]
        private string productName = string.Empty;

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
                OnPropertyChanged(nameof(Sum));
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
                OnPropertyChanged(nameof(Sum));
            }
        }
        public decimal Sum
        {
            get
            {
                try
                {
                    return checked(ValueA * ValueB);
                }
                catch (OverflowException)
                {
                    return decimal.MaxValue;
                }
            }
        }
    }
}
