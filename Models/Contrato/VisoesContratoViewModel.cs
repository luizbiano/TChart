namespace TChart.Models 
{ 
    public class VisoesContratoViewModel 
    { 
        public List<ContratoModel> Contratos { get; set; }
        public List<ContratoPeriodoModel> ContratosPeriodo { get; set; }
        public List<CategoriaContratoModel> CategoriaContrato { get; set; }
        public List<ResponsavelContratoModel> ResponsavelContrato { get; set; }
    } 
}