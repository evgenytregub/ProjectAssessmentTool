using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectAssessmentTool.Enums;
using ProjectAssessmentTool.Model;
using ProjectAssessmentTool.Model.Assessment.Interfaces;
using ProjectAssessmentTool.Model.Assessment.Services;
using ProjectAssessmentTool.Model.Projects;
using ProjectAssessmentTool.Model.Projects.DataModel;
using ProjectAssessmentTool.Model.Projects.Services;
using ProjectAssessmentTool.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectAssessmentTool.ViewModel
{
    public partial class AssessmentViewModel : ObservableObject
    {
        public ObservableCollection<Capacity> Pairs { get; } = new();
        public ObservableCollection<Opex> OpexPairs { get; } = new();
        public ObservableCollection<Capex> CapexPairs { get; } = new();
        public ObservableCollection<CashFlow> CashFlowPairs { get; } = new();
        public ICommand SaveProjectCommand { get; }

        [ObservableProperty]
        private decimal plantСapacity = 100;

        [ObservableProperty]
        private decimal opexStaff;
        partial void OnOpexStaffChanged(decimal oldValue, decimal newValue)
        {
            OnPropertyChanged(nameof(OpexTotalSum));
        }

        private decimal _npvDiscountRate = 0.1m;
        public decimal NPVDiscountRate
        {
            get => _npvDiscountRate;
            set
            {
                if (SetProperty(ref _npvDiscountRate, value))
                {
                    TriggerRecalculation(TriggerAssessment.NPV);
                }
            }
        }

        private decimal _investment;
        public decimal Investment
        {
            get => _investment;
            set
            {
                if (SetProperty(ref _investment, value))
                {
                    TriggerRecalculation(TriggerAssessment.NPV);
                    TriggerRecalculation(TriggerAssessment.PaybackPeriod);
                    TriggerRecalculation(TriggerAssessment.PI);
                }
            }
        }

        private decimal _npvResult;
        public decimal NPVResult
        {
            get => _npvResult;
            set => SetProperty(ref _npvResult, value);
        }

        private decimal _irrResult;
        public decimal IRRResult
        {
            get => _irrResult;
            set => SetProperty(ref _irrResult, value);
        }

        private decimal _annualCashFlow;
        public decimal AnnualCashFlow
        {
            get => _annualCashFlow;
            set
            {
                if (SetProperty(ref _annualCashFlow, value))
                {
                    TriggerRecalculation(TriggerAssessment.PaybackPeriod);
                }
            }
        }

        private decimal _piResult;
        public decimal PIResult
        {
            get => _piResult;
            set => SetProperty(ref _piResult, value);
        }

        private decimal _paybackPeriodResult;
        public decimal PaybackPeriodResult
        {
            get => _paybackPeriodResult;
            set => SetProperty(ref _paybackPeriodResult, value);
        }

        private decimal _servicelife;
        public decimal Servicelife
        {
            get => _servicelife;
            set
            {
                if (SetProperty(ref _servicelife, value))
                {
                    TriggerRecalculation(TriggerAssessment.ROI);
                }
            }
        }

        private decimal _roiResult;
        public decimal ROIResult
        {
            get => _roiResult;
            set => SetProperty(ref _roiResult, value);
        }

        public string AssessmentProjectName { get; }

        [ObservableProperty]
        private string updateDate = string.Empty;

        public AssessmentViewModel(string projectName)
        {
            InitializeCollection(Pairs, 10, i => new Capacity { ProductName = $"Product {i + 1}" });
            InitializeCollection(OpexPairs, 5, i => new Opex { MaterialName = $"Material {i + 1}" });
            InitializeCollection(CapexPairs, 5, i => new Capex { UnitName = $"Unit {i + 1}" });
            InitializeCollection(CashFlowPairs, 10, i => new CashFlow { CashFlowYear = $"{i + 1} year" });
            AssessmentProjectName = projectName;

            SaveProjectCommand = new RelayCommand(SaveProject);

            LoadData();

            CloseAction = () => { };
        }

        private void InitializeCollection<T>(ObservableCollection<T> collection, int count, Func<int, T> factory)
            where T : INotifyPropertyChanged
        {
            for (int i = 0; i < count; i++)
            {
                var item = factory(i);
                item.PropertyChanged += Item_PropertyChanged;
                collection.Add(item);
            }
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (sender)
            {
                case Capacity when e.PropertyName == nameof(Capacity.Sum):
                    OnPropertyChanged(nameof(TotalSum));
                    break;
                case Opex when e.PropertyName == nameof(Opex.OpexSum):
                    OnPropertyChanged(nameof(OpexTotalSum));
                    break;
                case Capex when e.PropertyName == nameof(Capex.CapexSum):
                    OnPropertyChanged(nameof(CapexTotalSum));
                    break;
                case CashFlow when e.PropertyName == nameof(CashFlow.CashFlowSum):
                    OnPropertyChanged(nameof(CashFlowTotalSum));
                    OnPropertyChanged(nameof(CashFlowExpensesSum));
                    OnPropertyChanged(nameof(CashFlowIncomeSum));
                    break;
            }

            if (sender is CashFlow && e.PropertyName == nameof(CashFlow.CashFlowSum))
            {
                TriggerRecalculation(TriggerAssessment.NPV);
                TriggerRecalculation(TriggerAssessment.IRR);
                TriggerRecalculation(TriggerAssessment.PI);
                TriggerRecalculation(TriggerAssessment.ROI);
            }
        }

        public decimal TotalSum => Pairs.Sum(p => p.Sum);
        public decimal OpexTotalSum => OpexPairs.Sum(p => p.OpexSum) + OpexStaff;
        public decimal CapexTotalSum => CapexPairs.Sum(p => p.CapexSum);
        public decimal CashFlowTotalSum => CashFlowPairs.Sum(p => p.CashFlowSum);
        public decimal CashFlowExpensesSum => CashFlowPairs.Sum(p => p.ExpensesSum);
        public decimal CashFlowIncomeSum => CashFlowPairs.Sum(p => p.IncomeSum);

        private List<decimal> GetCashFlows() => CashFlowPairs.Select(cf => cf.CashFlowSum).ToList();
        public void TriggerRecalculation(TriggerAssessment type)
        {
            switch (type)
            {
                case TriggerAssessment.NPV:
                    NPVResult = new NPV().calculation(GetCashFlows(), NPVDiscountRate, Investment);
                    break;
                case TriggerAssessment.IRR:
                    IRRResult = new IRR().calculation(GetCashFlows());
                    break;
                case TriggerAssessment.PaybackPeriod:
                    PaybackPeriodResult = new PaybackPeriod().calculation(CapexTotalSum, AnnualCashFlow);
                    break;
                case TriggerAssessment.PI:
                    PIResult = new PI().calculation(GetCashFlows(), CapexTotalSum);
                    break;
                case TriggerAssessment.ROI:
                    ROIResult = new ROI().calculation(CashFlowIncomeSum, CapexTotalSum);
                    break;
            }
        }

        private List<TOut> BuildProjectData<TIn, TOut>(IEnumerable<TIn> source, Func<TIn, TOut> selector)
        {
            return source.Select(selector).ToList();
        }

        private void SaveProject()
        {
            var projectData = new ProjectModel
            {
                Name = AssessmentProjectName,
                PlantСapacity = PlantСapacity,
                UpdateDate = DateTime.Now,
                // Get Plant capacity data
                PlantCapacityData = BuildProjectData(Pairs, p => new ProjectDataModel
                {
                    LineName = p.ProductName,
                    ValueA = p.ValueA,
                    ValueB = p.ValueB,
                    Sum = p.Sum
                }),
                PlantCapacityTotal = TotalSum,
                // Get OPRX data
                OpexData = BuildProjectData(OpexPairs, p => new ProjectDataModel
                {
                    LineName = p.MaterialName,
                    ValueA = p.ValueA,
                    ValueB = p.ValueB,
                    Sum = p.OpexSum
                }),
                OpexStaff = OpexStaff,
                OpexTotalSum = OpexTotalSum,
                // Get CAPEX data
                CapexData = BuildProjectData(CapexPairs, p => new ProjectDataModel
                {
                    LineName = p.UnitName,
                    ValueA = p.ValueA
                }),
                CapexTotalSum = CapexTotalSum,
                // Get Cash Flow data
                CashFlowData = BuildProjectData(CashFlowPairs, p => new ProjectDataModel
                {
                    LineName = p.CashFlowYear,
                    ValueA = p.ValueA,
                    ValueB = p.ValueB,
                    Sum = p.CashFlowSum
                }),
            };

            var saveStatus = new SaveProjectData();
            saveStatus.Save(projectData);

            var newWindow = new MainWindow();
            newWindow.Show();
            CloseAction?.Invoke();

        }

        public Action CloseAction { get; set; }

        private void LoadData()
        {
            var projectData = new ProjectData();
            List<ProjectModel> data = projectData.GetProjectData(AssessmentProjectName);
            var project = data.FirstOrDefault();

            if (project != null)
            {
                PlantСapacity = project.PlantСapacity;

                if (project.PlantCapacityData != null && project.PlantCapacityData.Count > 0)
                {
                    for (int i = 0; i < project.PlantCapacityData.Count && i < Pairs.Count; i++)
                    {
                        Pairs[i].ProductName = project.PlantCapacityData[i].LineName;
                        Pairs[i].ValueA = project.PlantCapacityData[i].ValueA;
                        Pairs[i].ValueB = project.PlantCapacityData[i].ValueB ?? 0;
                    }
                }

                if (project.OpexData != null && project.OpexData.Count > 0)
                {
                    for (int i = 0; i < project.OpexData.Count && i < OpexPairs.Count; i++)
                    {
                        OpexPairs[i].MaterialName = project.OpexData[i].LineName;
                        OpexPairs[i].ValueA = project.OpexData[i].ValueA;
                        OpexPairs[i].ValueB = project.OpexData[i].ValueB ?? 0;
                    }
                }

                OpexStaff = project.OpexStaff;

                if (project.CapexData != null && project.CapexData.Count > 0)
                {
                    for (int i = 0; i < project.CapexData.Count && i < CapexPairs.Count; i++)
                    {
                        CapexPairs[i].UnitName = project.CapexData[i].LineName;
                        CapexPairs[i].ValueA = project.CapexData[i].ValueA;
                    }
                }

                if (project.CashFlowData != null && project.CashFlowData.Count > 0)
                {
                    for (int i = 0; i < project.CashFlowData.Count && i < CashFlowPairs.Count; i++)
                    {
                        CashFlowPairs[i].CashFlowYear = project.CashFlowData[i].LineName;
                        CashFlowPairs[i].ValueA = project.CashFlowData[i].ValueA;
                        CashFlowPairs[i].ValueB = project.CashFlowData[i].ValueB ?? 0;
                    }
                }
            }
        }
    }
}
