using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TChart.Models;

namespace TChart.Controllers
{
    public class VisoesClienteController : Controller
    {
        private readonly VisoesClienteServiceModel _clienteServiceModel;

        public VisoesClienteController(VisoesClienteServiceModel clienteServiceModel)
        {
            _clienteServiceModel = clienteServiceModel;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var viewModel = new VisoesClienteViewModel 
            { 
                Clientes = await _clienteServiceModel.GetClientesAsync(startDate, endDate), 
                CategoriaCliente = await _clienteServiceModel.GetCategoriaFinanCountsAsync(startDate, endDate),
                ClientesPeriodo = await _clienteServiceModel.GetClientePeriodoCountsAsync(startDate, endDate),
                ResponsavelCliente = await _clienteServiceModel.GetResponsaveClienteCountsAsync(startDate, endDate)
            };
            
            //var clientes = await _clienteServiceModel.GetClientesAsync(startDate, endDate);
            //return View(clientes);
            return View(viewModel);
        }     
    }
}
