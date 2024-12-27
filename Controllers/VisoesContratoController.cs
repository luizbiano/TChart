using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TChart.Models;

namespace TChart.Controllers;

public class VisoesContratoController : Controller
    {
        private readonly VisoesContratoServiceModel _contratoServiceModel;

        public VisoesContratoController(VisoesContratoServiceModel contratoServiceModel)
        {
            _contratoServiceModel = contratoServiceModel;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var viewModel = new VisoesContratoViewModel 
            { 
                Contratos = await _contratoServiceModel.GetContratosAsync(startDate, endDate),
                ContratosPeriodo = await _contratoServiceModel.GetContratoPeriodoCountsAsync(startDate, endDate),
                CategoriaContrato = await _contratoServiceModel.GetCategoriaContratoCountsAsync(startDate, endDate),
                ResponsavelContrato = await _contratoServiceModel.GetResponsaveContratoCountsAsync(startDate, endDate)
            };
            return View(viewModel);
        }     
    }
