using database.Entities;
using database.ValueTypes;

namespace loader;

public static class StaticData
{
    public static IList<Account> Accounts()
    {
        var accounts = new List<Account>
        {
            new(accountCode: "GSEJ-SIPP"),
            new(accountCode: "GSEJ-ISA"),
            new(accountCode: "SHEJ-SIPP"),
            new(accountCode: "SHEJ-ISA"),
            new(accountCode: "AMEJ-SIPP"),
            new(accountCode: "TKEJ-SIPP"),
            new(accountCode: "GOLD"),
            new(accountCode: "GSEJ-ISA-VIRGIN"),
        };
        return accounts;
    }
    public static IList<Stock> Stocks()
    {
        var stocks = new List<Stock>
        {
            new(stockSymbol: "AZN.L", description: "AstraZeneca PLC", stockType: StockTypes.Share),
            new(stockSymbol: "GOLD", description: "Gold", stockType: StockTypes.Commodity),
            new(stockSymbol: "BP.L", description: "BP PLC", stockType: StockTypes.Share),
            new(stockSymbol: "BLND.L", description: "British Land Co PLC", StockTypes.Share),
            new(stockSymbol: "BVIC.L", description: "Britvic PLC", stockType: StockTypes.Share),
            new(stockSymbol: "CPI.L", description: "Capita PLC", stockType: StockTypes.Share, aliases: new List<StockAlias>
            {
                new(description: "CAPITA PLC ORD GBP0.02066666(NP-24/05/")
            }),
            new(stockSymbol: "CML.L", description: "CML Microsystems PLC", stockType: StockTypes.Share),
            new(stockSymbol: "XWND.L", description: "DB X-TRACKERS MSCI WLD TRN IDX UCITS ETF1",
                stockType: StockTypes.Etf,
                notes: "aka db x-trackers MSCI World Information Technology TRN Index UCITS ETF, Acquired by DBK.GE"),
            new(stockSymbol: "DLG.L", description: "Direct Line Insurance Group PLC", stockType: StockTypes.Share, aliases: new List<StockAlias>
            {
                new(description: "DIRECT LINE INS GR ORD GBP0.10")
            }),
            new(stockSymbol: "ECOR.L", description: "Ecora Resources PLC", stockType: StockTypes.Share),
            new(stockSymbol: "EIG.L", description: "EI GROUP PLC ORD GBP0.025", stockType: StockTypes.Share,
                notes: "formerly Enterprise Inns"),
            new(stockSymbol: "SONG.L", description: "Hipgnosis Songs Ord", stockType: StockTypes.Share),
            new(stockSymbol: "T26A", description: "HM TREASURY GILT 0.375% (22/10/26)", stockType: StockTypes.Gilt),
            new(stockSymbol: "HSV.L", description: "HOMESERVE ORD GBP0.025", stockType: StockTypes.Share),
            new(stockSymbol: "IBST.L", description: "Ibstock PLC", stockType: StockTypes.Share),
            new(stockSymbol: "IDS.L", description: "International Distributions Services PLC",
                stockType: StockTypes.Share),
            new(stockSymbol: "SLXX.L", description: "iShares Core £ Corp Bond ETF GBP Dist", stockType: StockTypes.Etf),
            new(stockSymbol: "ISF.L", description: "iShares Core FTSE 100 ETF GBP Dist", stockType: StockTypes.Etf),
            new(stockSymbol:"SWDA.L", description: "iShares Core MSCI World ETF USD Acc GBP", stockType: StockTypes.Etf)
            {
                Isin = "IE00B4L5Y983",
            },
            new(stockSymbol: "IGLT.L", description: "iShares Core UK Gilts ETF GBP Dist", stockType: StockTypes.Etf),
            new(stockSymbol: "MIDD.L", description: "iShares FTSE 250 ETF GBP Dist", stockType: StockTypes.Etf),
            new(stockSymbol: "IUKP.L", description: "iShares UK Property ETF GBP Dist", stockType: StockTypes.Etf),
            new(stockSymbol: "GILS.L", description: "Lyxor Core UK Govt Bd (DR) ETF D GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "MICROFOCUS", // not the correct ticker
                description: "MICRO FOCUS INTL ORD GBP0.10", stockType: StockTypes.Share, aliases: new List<StockAlias>
                {
                    new(description: "MICRO FOCUS INT. PROVISIONAL CAP RET SHS")
                }),
            new(stockSymbol: "MRW.L", description: "MORRISON(W)SUPRMKT ORD GBP0.10", stockType: StockTypes.Share),
            new(stockSymbol: "MCG.L", description: "Mobico Group PLC",
                stockType: StockTypes.Share, notes: "Formerly National Express", aliases: new List<StockAlias>
                {
                    new(description: "National Express Group PLC")
                }, alternativeSymbols: new List<AlternativeSymbol>
                {
                    new(alternative: "NEX.L")
                }),
            new(stockSymbol: "NG.L", description: "National Grid PLC", stockType: StockTypes.Share, aliases: new List<StockAlias>
            {
                new(description: "NATIONAL GRID ORD GBP0.113953")
            }),
            new(stockSymbol: "RMG.L", description: "Royal Mail PLC", stockType: StockTypes.Share),
            new(stockSymbol: "SMT.L", description: "Scottish Mortgage Ord", stockType: StockTypes.Share),
            new(stockSymbol: "TSCO.L", description: "TESCO ORD GBP0.05", stockType: StockTypes.Share),
            new(stockSymbol: "TET.L", description: "Treatt PLC", stockType: StockTypes.Share),
            new(stockSymbol: "T25", description: "UK(GOVT OF) 2% SNR 07/09/2025 GBP1000", stockType: StockTypes.Gilt),
            new(stockSymbol: "VECP.L", description: "Vanguard EUR Corp Bd UCITS ETF GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "VMID.L", description: "Vanguard FTSE 250 UCITS ETF", stockType: StockTypes.Etf),
            new(stockSymbol: "VWRP.L", description: "Vanguard FTSE All-World ETF USD Acc GBP", stockType: StockTypes.Etf, aliases: new List<StockAlias>
            {
                new(description: "Vanguard FTSE All-World UCITS ETF" // is this correct?
                )
            }),
            new(stockSymbol: "VWRL.L", description: "Vanguard FTSE All-World UCITS ETF GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "VAPX.L", description: "Vanguard FTSE Dev AsiaPac exJpn ETF $Dis GBP",
                stockType: StockTypes.Etf),
            new(stockSymbol: "VEVE.L", description: "Vanguard FTSE Dev World ETF $Dis GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "VFEM.L", description: "Vanguard FTSE Emerg Markets ETF $Dis GBP",
                stockType: StockTypes.Etf),
            new(stockSymbol: "VJPN.L", description: "Vanguard FTSE Japan ETF $Dis GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "VUCP.L", description: "Vanguard USD Corp Bd UCITS ETF GBP", stockType: StockTypes.Etf),
            new(stockSymbol: "JDW.L", description: "Wetherspoon (J D) PLC", stockType: StockTypes.Share),
            new(stockSymbol: "WTB.L", description: "Whitbread PLC", stockType: StockTypes.Share),
            new(stockSymbol: "SGBX.L", description: "WisdomTree Physical Swiss Gold ETC GBP",
                stockType: StockTypes.Etf),
            new(stockSymbol: "XUKX.L", description: "Xtrackers FTSE 100 Income ETF 1D", stockType: StockTypes.Etf),
            new(stockSymbol: "XASX.L", description: "Xtrackers MSCI UK ESG ETF 1D £", stockType: StockTypes.Etf),
            new(stockSymbol: "XMWD.L", description: "Xtrackers MSCI World Swap ETF 1C USD", stockType: StockTypes.Etf, aliases: new List<StockAlias>
            {
                new(description: "XTRACKERS MSCI WORLD SWAP UCITS ETF 1" // is this the same thing??
                )
            })
        };

        return stocks;

    }
}