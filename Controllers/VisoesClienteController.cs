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
            var clientes = await _clienteServiceModel.GetClientesAsync(startDate, endDate);
            return View(clientes);
        }
    }
}
