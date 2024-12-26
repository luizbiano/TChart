using Microsoft.EntityFrameworkCore;
namespace TChart.Models;
public class VisoesClienteServiceModel
{
    private readonly ApplicationDbContext _context;

    public VisoesClienteServiceModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VisoesClienteModel>> GetClientesAsync(DateTime? startDate, DateTime? endDate)
    {
        var defaultStartDate = DateTime.Now.AddDays(-365*5);
        var defaultEndDate = DateTime.Now;

        // Preenchendo as datas apenas se forem nulas 
        startDate = startDate ?? defaultStartDate; 
        endDate = endDate ?? defaultEndDate;

        var query = @"
            SELECT TOP (1000)
                CLIENTE.ID_CLIENTE AS IdCliente,
                PESSOA.DS_NOME_RAZAO_SOCIAL AS NomeRazaoSocial,
                PESSOA.DS_APELIDO_NOME_FANTASIA AS ApelidoNomeFantasia,
                CLIENTE.DT_CLIENTE_DESDE AS DtClienteDesde,
                CATEGORIA.DS_CATEGORIA_FINAN AS CategoriaFinan,
                CLASSIFICACAO.TP_CLASSIFICACAO_FINAN AS TpClassificacaoFinan,
                CLASSIFICACAO.DS_CLASSIFICACAO_FINAN AS DsClassificacaoFinan,
                LIMITE.VL_LIMITE AS VlLimite,
                PESSOA_USU.DS_NOME_RAZAO_SOCIAL AS NomeResponsavel
            FROM [BancoC_DB].[dbo].[PAD_CLIENTE] AS CLIENTE
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA] AS PESSOA ON CLIENTE.ID_PESSOA = PESSOA.ID_PESSOA
            JOIN [BancoC_DB].[dbo].[ACE_USUARIO] AS USUARIO ON CLIENTE.ID_USUARIO_RESPONSAVEL = USUARIO.ID_USUARIO
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA_FISICA] AS PF_USU ON USUARIO.ID_PESSOA_FISICA = PF_USU.ID_PESSOA_FISICA
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA] AS PESSOA_USU ON PF_USU.ID_PESSOA = PESSOA_USU.ID_PESSOA
            JOIN [BancoC_DB].[dbo].[PAD_CLIENTE_LIMITE] AS LIMITE ON CLIENTE.ID_CLIENTE = LIMITE.ID_CLIENTE
            JOIN [BancoC_DB].[dbo].[PAD_CATEGORIA_FINAN] AS CATEGORIA ON LIMITE.ID_CATEGORIA = CATEGORIA.ID_CATEGORIA_FINAN
            JOIN [BancoC_DB].[dbo].[PAD_CLASSIFICACAO_FINAN] AS CLASSIFICACAO ON LIMITE.ID_CLASSIFICACAO = CLASSIFICACAO.ID_CLASSIFICACAO_FINAN
            WHERE CLIENTE.DT_CLIENTE_DESDE BETWEEN {0} AND {1}";

        return await _context.Set<VisoesClienteModel>()
            .FromSqlRaw(query, startDate, endDate)
            .ToListAsync();
    }
}

