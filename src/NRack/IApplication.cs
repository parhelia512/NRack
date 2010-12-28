using System.Collections.Generic;

namespace NRack
{
    public interface IApplication
    {
        dynamic[] Call(IDictionary<string, object> environment);
    }
}