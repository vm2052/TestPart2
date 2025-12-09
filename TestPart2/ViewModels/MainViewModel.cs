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
        
        private ObservableCollection<EnvironmentVariable> _environmentVariables;
        public ObservableCollection<EnvironmentVariable> EnvironmentVariables
        {
            get
            {
                return _environmentVariables;
            }
            set
            {
                _environmentVariables = value;
                OnPropertyChanged(nameof(EnvironmentVariables));
            }
        }


        private readonly IEnvironmentVariableService _environmentService;
        private readonly ILogger _logger;
        public MainViewModel(IEnvironmentVariableService environmentService, ILogger logger)
        {
            _environmentService = environmentService;
            EnvironmentVariables = _environmentService.LoadEnvironmentVariables();
        }
        //сохраняем переменную, вызов из code-behind mainwindow
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
