namespace TChart.Models;

public class ContratoModel 
{ 
    public int IdContrato { get; set; }
    public string Cliente { get; set; }
    public DateTime DtContrato { get; set; }
    public decimal ValorEmprestimo { get; set; }
    public decimal JurosOriginal { get; set; }
    public decimal JurosAtual { get; set; }
    public int DiaVencimento { get; set; }
    public string TipoOperacao { get; set; }
    public string StatusContrato { get; set; }
    public decimal Capital { get; set; }
    public string Responsavel { get; set; }
}