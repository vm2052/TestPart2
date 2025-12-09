using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPart2.Models;

namespace TestPart2.Services
{
    public interface IEnvironmentVariableService
    {
        List<string> GetVariableNames();
        ObservableCollection<EnvironmentVariable> LoadEnvironmentVariables();
        void SaveEnvironmentVariable(string name, string value);

    }
}
