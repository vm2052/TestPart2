using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPart2.Models;

namespace TestPart2.Services
{
    
    public class EnvironmentVariableService :IEnvironmentVariableService
    {
        private readonly IConfiguration _configuration;
        private readonly List<string> _variableNames;
        private readonly ILogger _logger;
        public EnvironmentVariableService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _variableNames = _configuration.GetSection("EnvironmentVariables").Get<List<string>>() ?? new List<string>();
        }
        //грузим переменные из файла 
        public ObservableCollection<EnvironmentVariable> LoadEnvironmentVariables()
        {
            var variables = new ObservableCollection<EnvironmentVariable>();

            foreach (var variableName in _variableNames)
            {
                var value = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);

                variables.Add(new EnvironmentVariable
                {
                    Name = variableName,
                    Value = value 
                });
                _logger.Information($"Переменная {variableName} = {value}");
            }
            _logger.Information("Переменные среды загружены");
            return variables;
        }
        //сохраняем переменную
        public void SaveEnvironmentVariable(string name, string value)
        {
            try
            {
                Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.User);
                _logger.Information($"Переменная {name} сохранена. Значение: {value}");
            }
            catch (Exception ex)
            {
                var message = $"Ошибка сохранения переменной '{name}': {ex.Message}";
                _logger.Error(message);
                throw new InvalidOperationException(message, ex);
            }
        }
        public List<string> GetVariableNames() => _variableNames;
    }
}
