using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public class NavigationContent : INavigationContent
    {
        private List<object>? _addedParameters;

        public Type ClassType { get; }
        public IList<object>? Parameters { get; private set; }
        public IList<INavigationContent>? NotInstancedParameters { get; }

        public NavigationContent(Type classType, IList<object>? parameters = null, IList<INavigationContent>? notInstancedParameters = null)
        {
            ClassType = classType;
            Parameters = parameters;
            NotInstancedParameters = notInstancedParameters;
        }

        public object Instance()
        {
            if (NotInstancedParameters != null && Parameters != null && _addedParameters != null)
            {
                List<object> instancedParameters = new();
                foreach (var item in NotInstancedParameters)
                    instancedParameters.Add(item.Instance());
                object[] array = Parameters.Concat(instancedParameters).Concat(_addedParameters).ToArray() ?? throw new ApplicationException(nameof(array));
                _addedParameters = null;
                return Activator.CreateInstance(ClassType, array) ?? throw new ApplicationException(ClassType.ToString());
            }
            else if (NotInstancedParameters != null && Parameters != null)
            {
                List<object> instancedParameters = new();
                foreach (var item in NotInstancedParameters)
                    instancedParameters.Add(item.Instance());
                object[] array = Parameters.Concat(instancedParameters).ToArray() ?? throw new ApplicationException(nameof(array));
                return Activator.CreateInstance(ClassType, array) ?? throw new ApplicationException(ClassType.ToString());
            }
            else if (NotInstancedParameters != null && _addedParameters != null)
            {
                List<object> instancedParameters = new();
                foreach (var item in NotInstancedParameters)
                    instancedParameters.Add(item.Instance());
                object[] array = instancedParameters.Concat(_addedParameters).ToArray() ?? throw new ApplicationException(nameof(array));
                _addedParameters = null;
                return Activator.CreateInstance(ClassType, array) ?? throw new ApplicationException(ClassType.ToString());
            }
            else if (Parameters != null && _addedParameters != null)
            {
                object[] array = Parameters.Concat(_addedParameters).ToArray() ?? throw new ApplicationException(nameof(array));
                _addedParameters = null;
                return Activator.CreateInstance(ClassType, array) ?? throw new ApplicationException(ClassType.ToString());
            }
            else if (NotInstancedParameters != null)
            {
                List<object> instancedParameters = new();
                foreach (var item in NotInstancedParameters)
                    instancedParameters.Add(item.Instance());
                return Activator.CreateInstance(ClassType, instancedParameters.ToArray()) ?? throw new ApplicationException(ClassType.ToString());
            }
            else if (_addedParameters != null)
            {
                var instance = Activator.CreateInstance(ClassType, _addedParameters.ToArray()) ?? throw new ApplicationException(ClassType.ToString());
                _addedParameters = null;
                return instance;
            }
            else if (Parameters != null)
                return Activator.CreateInstance(ClassType, Parameters.ToArray()) ?? throw new ApplicationException(ClassType.ToString());

            return Activator.CreateInstance(ClassType) ?? throw new ApplicationException(ClassType.ToString());
        }

        public void AddParameter(object parameter)
        {
            if (_addedParameters == null) _addedParameters = new List<object>();
            if(!_addedParameters.Contains(parameter))
                _addedParameters.Add(parameter);
        }
    }
}
