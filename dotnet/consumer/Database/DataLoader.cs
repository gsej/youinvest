using consumer.Entities;

namespace consumer.Database;

public class DataLoader
{
    private readonly ConsumerDbContext _consumerDbContext;

    public DataLoader(ConsumerDbContext consumerDbContext)
    {
        _consumerDbContext = consumerDbContext;
    }

    public async Task LoadStocks()
    {
        var stocks = new List<Stock>
        {
            new()
            {
                Symbol = "AZN.L",
                Description = "AstraZeneca PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "BP.L",
                Description = "BP PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "BL.L",
                Description = "British Land Co PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "BVIC.L",
                Description = "Britvic PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "CPI.L",
                Description = "CAPITA PLC ORD GBP0.02066666(NP-24/05/",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "CPI.L",
                Description = "Capita PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "CML.L",
                Description = "CML Microsystems PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "XWLD.L",
                Description = "DB X-TRACKERS MSCI WLD TRN IDX UCITS ETF1",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "DLG.L",
                Description = "DIRECT LINE INS GR ORD GBP0.10",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "DLG.L",
                Description = "Direct Line Insurance Group PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "ECOR.L",
                Description = "Ecora Resources PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                // enterprise inns
                Symbol = "EIG.L",
                Description = "EI GROUP PLC ORD GBP0.025",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "SONG.L",
                Description = "Hipgnosis Songs Ord",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "HSV.L",
                Description = "HOMESERVE ORD GBP0.025",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "IBST.L",
                Description = "Ibstock PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "IDS.L",
                Description = "International Distributions Services PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "SLXX.L",
                Description = "iShares Core £ Corp Bond ETF GBP Dist",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "ISF.L",
                Description = "iShares Core FTSE 100 ETF GBP Dist",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "IGLT.L",
                Description = "iShares Core MSCI World ETF USD Acc GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "IGLT.L",
                Description = "iShares Core UK Gilts ETF GBP Dist",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "MIDD.L",
                Description = "iShares FTSE 250 ETF GBP Dist",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "IUKP.L",
                Description = "iShares UK Property ETF GBP Dist",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "GILS.L",
                Description = "Lyxor Core UK Govt Bd (DR) ETF D GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "MCFUF.L", // ???'
                Description = "MICRO FOCUS INT. PROVISIONAL CAP RET SHS",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "MCFUF.L", // ???'
                Description = "MICRO FOCUS INTL ORD GBP0.10",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "MRW.L",
                Description = "MORRISON(W)SUPRMKT ORD GBP0.10",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "NEG.L",
                Description = "National Express Group PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "NG.L",
                Description = "NATIONAL GRID ORD GBP0.113953",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "NG.L",
                Description = "National Grid PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "ROYMF",
                Description = "Royal Mail PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "SMT.L",
                Description = "Scottish Mortgage Ord",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "TSCO.L",
                Description = "TESCO ORD GBP0.05",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "TET.L",
                Description = "Treatt PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "VECP.L",
                Description = "Vanguard EUR Corp Bd UCITS ETF GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VMID.L",
                Description = "Vanguard FTSE 250 UCITS ETF",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VWRP.L",
                Description = "Vanguard FTSE All-World ETF USD Acc GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VWRL.L",
                Description = "Vanguard FTSE All-World UCITS ETF GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VWRD.L",
                Description = "Vanguard FTSE All-World UCITS ETF",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VAPX.L",
                Description = "Vanguard FTSE Dev AsiaPac exJpn ETF $Dis GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VEVE.L",
                Description = "Vanguard FTSE Dev World ETF $Dis GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "VFEM.L",
                Description = "Vanguard FTSE Emerg Markets ETF $Dis GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = " VJPN.L",
                Description = "Vanguard FTSE Japan ETF $Dis GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = " VUCP.L",
                Description = "Vanguard USD Corp Bd UCITS ETF GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "JDW.L",
                Description = "Wetherspoon (J D) PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "WTB.L",
                Description = "Whitbread PLC",
                SubjectToStampDuty = true
            },
            new()
            {
                Symbol = "SGBX.L",
                Description = "WisdomTree Physical Swiss Gold ETC GBP",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "XUKX.L",
                Description = "Xtrackers FTSE 100 Income ETF 1D",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "XASX.L",
                Description = "Xtrackers MSCI UK ESG ETF 1D £",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "XMWD.L",
                Description = "Xtrackers MSCI World Swap ETF 1C USD",
                SubjectToStampDuty = false
            },
            new()
            {
                Symbol = "XMWD.L",
                Description = "XTRACKERS MSCI WORLD SWAP UCITS ETF 1",
                SubjectToStampDuty = false
            },
        };
        
        _consumerDbContext.Stocks.AddRange(stocks);

        await _consumerDbContext.SaveChangesAsync();
    }
}