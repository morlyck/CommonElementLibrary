using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonElement
{
    public interface IUpstairEnvironment
    {
        bool MultiBand { get; }
        IChainEnvironmentDataHolder GetDataHolder(string typeName);
        IChainEnvironmentDataHolder TryGetDataHolder(string typeName);
        void MultiBandDataHolderAll_Break(string typeName, Func<IChainEnvironmentDataHolder, bool> func);

    }
}
