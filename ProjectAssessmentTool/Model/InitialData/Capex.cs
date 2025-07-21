using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model
{
    public partial class Capex : ObservableObject
    {
        [ObservableProperty]
        private string unitName = string.Empty;

        private decimal valueA;
        public decimal ValueA
        {
            get => valueA;
            set
            {
                if (value < 0 || value > 100_000_000m)
                {
                    SystemMessages.SystemMessages.ValueOutOfRange("0", "100 000 000");
                }

                SetProperty(ref valueA, value);
                OnPropertyChanged(nameof(CapexSum));
            }
        }
        public decimal CapexSum => ValueA;
    }
}
