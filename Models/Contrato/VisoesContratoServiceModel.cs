using Microsoft.EntityFrameworkCore;
namespace TChart.Models;
public class VisoesContratoServiceModel
{
    private readonly ApplicationDbContext _context;

    public VisoesContratoServiceModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContratoModel>> GetContratosAsync(DateTime? startDate, DateTime? endDate)
    {
        var defaultStartDate = DateTime.Now.AddDays(-365*5);
        var defaultEndDate = DateTime.Now;

        // Preenchendo as datas apenas se forem nulas 
        startDate = startDate ?? defaultStartDate; 
        endDate = endDate ?? defaultEndDate;

        var query = @"
            SELECT 
            NR_CONTRATO AS IdContrato
            , PESSOA_CLI.DS_NOME_RAZAO_SOCIAL AS Cliente            
            ,DT_CONTRATO AS DtContrato
            ,VL_EMPRESTIMO_CONTRATO AS ValorEmprestimo
            ,PC_JUROS_ORIG_CONTRATO AS JurosOriginal
            ,PC_JUROS_ATUAL_CONTRATO AS JurosAtual
            ,NR_DIA_VENCIMENTO AS  DiaVencimento
            ,DS_TIPO_OPERACAO AS TipoOperacao
            ,DS_STATUS_CONTRATO AS StatusContrato
            ,VL_CAPITAL_CONTRATO AS Capital
            ,PESSOA_USU.DS_NOME_RAZAO_SOCIAL AS Responsavel
            FROM [BancoC_DB].[dbo].[PAD_CONTRATO] AS CONTRATO
            JOIN [BancoC_DB].[dbo].[PAD_CLIENTE] AS CLIENTE ON CONTRATO.ID_CLIENTE = CLIENTE.ID_CLIENTE
            JOIN [BancoC_DB].[dbo].[ACE_USUARIO] AS USUARIO ON CONTRATO.ID_USUARIO_RESPONSAVEL = USUARIO.ID_USUARIO
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA_FISICA] AS PF_USU ON USUARIO.ID_PESSOA_FISICA = PF_USU.ID_PESSOA_FISICA
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA] AS PESSOA_USU ON PF_USU.ID_PESSOA = PESSOA_USU.ID_PESSOA
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA] AS PESSOA_CLI ON CLIENTE.ID_PESSOA = PESSOA_CLI.ID_PESSOA
            JOIN [BancoC_DB].[dbo].[PAD_STATUS_CONTRATO] C_STATUS ON CONTRATO.ID_STATUS = C_STATUS.ID_STATUS_CONTRATO
            JOIN [BancoC_DB].[dbo].[PAD_TIPO_OPERACAO] TP_OPER ON CONTRATO.ID_TIPO_OPERACAO = TP_OPER.ID_TIPO_OPERACAO
            WHERE CONTRATO.ID_STATUS IN (1,2,4)
            AND DT_CONTRATO BETWEEN {0} AND {1}";

        return await _context.Set<ContratoModel>()
            .FromSqlRaw(query, startDate, endDate)
            .ToListAsync();
    }

    public async Task<List<ContratoPeriodoModel>> GetContratoPeriodoCountsAsync(DateTime? startDate, DateTime? endDate)
    {
        var defaultStartDate = DateTime.Now.AddDays(-365*5);
        var defaultEndDate = DateTime.Now;

        // Preenchendo as datas apenas se forem nulas 
        startDate = startDate ?? defaultStartDate; 
        endDate = endDate ?? defaultEndDate;

        var query = @"
            SELECT
            FORMAT(DT_CONTRATO, 'MMM/yyyy', 'pt-BR') AS PERIODO, COUNT(*) QUANTIDADE
            FROM [BancoC_DB].[dbo].[PAD_CONTRATO] AS CONTRATO
            JOIN [BancoC_DB].[dbo].[PAD_STATUS_CONTRATO] C_STATUS ON CONTRATO.ID_STATUS = C_STATUS.ID_STATUS_CONTRATO
            WHERE CONTRATO.ID_STATUS IN (1,2,4)
            AND DT_CONTRATO BETWEEN {0} AND {1}
            GROUP BY FORMAT(DT_CONTRATO, 'MMM/yyyy', 'pt-BR'), YEAR(DT_CONTRATO),MONTH(DT_CONTRATO)
            ORDER BY YEAR(DT_CONTRATO),MONTH(DT_CONTRATO)";

        return await _context.Set<ContratoPeriodoModel>()
            .FromSqlRaw(query, startDate, endDate)
            .ToListAsync();
    }


    public async Task<List<CategoriaContratoModel>> GetCategoriaContratoCountsAsync(DateTime? startDate, DateTime? endDate)
    {
        var defaultStartDate = DateTime.Now.AddDays(-365*5);
        var defaultEndDate = DateTime.Now;

        // Preenchendo as datas apenas se forem nulas 
        startDate = startDate ?? defaultStartDate; 
        endDate = endDate ?? defaultEndDate;

        var query = @"
            SELECT 
            DS_TIPO_OPERACAO AS CATEGORIA, COUNT(*) QUANTIDADE
            FROM [BancoC_DB].[dbo].[PAD_CONTRATO] AS CONTRATO
            JOIN [BancoC_DB].[dbo].[PAD_STATUS_CONTRATO] C_STATUS ON CONTRATO.ID_STATUS = C_STATUS.ID_STATUS_CONTRATO
            JOIN [BancoC_DB].[dbo].[PAD_TIPO_OPERACAO] TP_OPER ON CONTRATO.ID_TIPO_OPERACAO = TP_OPER.ID_TIPO_OPERACAO
            WHERE CONTRATO.ID_STATUS IN (1,2,4)
            AND DT_CONTRATO BETWEEN {0} AND {1}
            GROUP BY DS_TIPO_OPERACAO";

        return await _context.Set<CategoriaContratoModel>()
            .FromSqlRaw(query, startDate, endDate)
            .ToListAsync();
    }

    public async Task<List<ResponsavelContratoModel>> GetResponsaveContratoCountsAsync(DateTime? startDate, DateTime? endDate)
    {
        var defaultStartDate = DateTime.Now.AddDays(-365*5);
        var defaultEndDate = DateTime.Now;

        // Preenchendo as datas apenas se forem nulas 
        startDate = startDate ?? defaultStartDate; 
        endDate = endDate ?? defaultEndDate;

        var query = @"
            SELECT
            PESSOA_USU.DS_NOME_RAZAO_SOCIAL AS RESPONSAVEL
            , COUNT(*) AS QUANTIDADE
            FROM [BancoC_DB].[dbo].[PAD_CONTRATO] AS CONTRATO
            JOIN [BancoC_DB].[dbo].[ACE_USUARIO] AS USUARIO ON CONTRATO.ID_USUARIO_RESPONSAVEL = USUARIO.ID_USUARIO
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA_FISICA] AS PF_USU ON USUARIO.ID_PESSOA_FISICA = PF_USU.ID_PESSOA_FISICA
            JOIN [BancoC_DB].[dbo].[PAD_PESSOA] AS PESSOA_USU ON PF_USU.ID_PESSOA = PESSOA_USU.ID_PESSOA
            JOIN [BancoC_DB].[dbo].[PAD_STATUS_CONTRATO] C_STATUS ON CONTRATO.ID_STATUS = C_STATUS.ID_STATUS_CONTRATO
            WHERE CONTRATO.ID_STATUS IN (1,2,4)
            AND DT_CONTRATO BETWEEN {0} AND {1}
            GROUP BY PESSOA_USU.DS_NOME_RAZAO_SOCIAL";

        return await _context.Set<ResponsavelContratoModel>()
            .FromSqlRaw(query, startDate, endDate)
            .ToListAsync();
    }
}

