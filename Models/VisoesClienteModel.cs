namespace TChart.Models;

public class VisoesClienteModel 
{ 
    public int IdCliente { get; set; } 
    public string NomeRazaoSocial { get; set; } 
    public string ApelidoNomeFantasia { get; set; } 
    public DateTime DtClienteDesde { get; set; } 
    public string CategoriaFinan { get; set; } 
    public string TpClassificacaoFinan { get; set; } 
    public string DsClassificacaoFinan { get; set; } 
    public decimal VlLimite { get; set; } 
    public string NomeResponsavel { get; set;}
}