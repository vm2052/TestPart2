using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TestPart2.Models;
using TestPart2.Services;

namespace TestPart2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        private EnvironmentVariable? _selectedVariable;
        private string _statusMessage = "Готово";

        private ObservableCollection<EnvironmentVariable> _environmentVariables;
        public ObservableCollection<EnvironmentVariable> EnvironmentVariables {
          get { return _environmentVariables; }
            set { _environmentVariables = value; }
           }

        public EnvironmentVariable? SelectedVariable
        {
            get => _selectedVariable;
            set
            {
                if (_selectedVariable != value)
                {
                    _selectedVariable = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsVariableSelected));
                }
            }
        }

        public bool IsVariableSelected => SelectedVariable != null;

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand RefreshListCommand { get; }
        private readonly IEnvironmentVariableService _environmentService;
        private readonly ILogger _logger;
        public MainViewModel(IEnvironmentVariableService environmentService, ILogger logger)
        {
            _environmentService = environmentService;
            EnvironmentVariables = _environmentService.LoadEnvironmentVariables();
        }

        public void SaveVariable(EnvironmentVariable variable)
        {
            if (variable == null) return;

            _environmentService.SaveEnvironmentVariable(variable.Name, variable.Value);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
