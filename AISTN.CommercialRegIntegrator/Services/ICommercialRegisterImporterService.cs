using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.CommercialRegIntegrator.Services
{
    public interface ICommercialRegisterImporterService
    {
        Task<bool> ImportDeedAsync(string XMLFilePath);

        Task<bool> ConnectEntitiesAsync();
    }
}
