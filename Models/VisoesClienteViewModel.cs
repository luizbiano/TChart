namespace TChart.Models 
{ 
    public class VisoesClienteViewModel 
    { 
        public List<ClienteModel> Clientes { get; set; } 
        public List<CategoriaClienteModel> CategoriaCliente { get; set; } 
        public List<ClientePeriodoModel> ClientesPeriodo { get; set; }
        public  List<ResponsavelClienteModel> ResponsavelCliente { get; set; }
    } 
}