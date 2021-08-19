using Modalmais.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Services.Response
{
    public interface IClienteServiceRequest
    {
        Task AdicionarCliente(Cliente clienteAdicionar);
    }
}
