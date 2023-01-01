using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Basics.Factories
{
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<T> _Factory;

        public AbstractFactory(Func<T> _Factory)
        {
            this._Factory = _Factory;
        }

        public T Create()
        {
            return _Factory();
        }
    }
}
