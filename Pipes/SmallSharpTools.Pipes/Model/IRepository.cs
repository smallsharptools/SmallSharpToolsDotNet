using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallSharpTools.Pipes.Model
{

    public interface IRepository<T>
    {
    
        void Save(T instance);

        void SaveAll(IEnumerable<T> collection);

    }

}
