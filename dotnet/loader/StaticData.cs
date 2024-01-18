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
            new Stock.StockBuilder("AZN.L", "AstraZeneca PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("GOLD", "Gold", StockTypes.Commodity).Build(),
            new Stock.StockBuilder("BP.L", "BP PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("BLND.L", "British Land Co PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("BVIC.L", "Britvic PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("CPI.L", "Capita PLC", StockTypes.Share)
                .WithAliases(new List<StockAlias> { new(description: "CAPITA PLC ORD GBP0.02066666(NP-24/05/") })
                .Build(),
            new Stock.StockBuilder("CML.L", "CML Microsystems PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("XWND.L", "DB X-TRACKERS MSCI WLD TRN IDX UCITS ETF1", StockTypes.Etf)
                .WithNotes("aka db x-trackers MSCI World Information Technology TRN Index UCITS ETF, Acquired by DBK.GE")
                .Build(),
            new Stock.StockBuilder("DLG.L", "Direct Line Insurance Group PLC", StockTypes.Share)
                .WithAliases(aliases: new List<StockAlias> { new(description: "DIRECT LINE INS GR ORD GBP0.10") })
                .WithDefaultCurrency("GBp")
                .Build(),
            new Stock.StockBuilder("ECOR.L", "Ecora Resources PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("EIG.L", "EI GROUP PLC ORD GBP0.025", StockTypes.Share)
                .WithNotes("formerly Enterprise Inns")
                .Build(),
            new Stock.StockBuilder("SONG.L", "Hipgnosis Songs Ord", StockTypes.Share).Build(),
            new Stock.StockBuilder("T26A", "HM TREASURY GILT 0.375% (22/10/26)", StockTypes.Gilt).Build(),
            new Stock.StockBuilder("HSV.L", "HOMESERVE ORD GBP0.025", StockTypes.Share).Build(),
            new Stock.StockBuilder("IBST.L", "Ibstock PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("IDS.L", "International Distributions Services PLC", StockTypes.Share)
                .WithDefaultCurrency("GBp")
                .Build(),
            new Stock.StockBuilder("SLXX.L", "iShares Core £ Corp Bond ETF GBP Dist", StockTypes.Etf).Build(),
            new Stock.StockBuilder("ISF.L", "iShares Core FTSE 100 ETF GBP Dist", StockTypes.Etf).Build(),
            new Stock.StockBuilder("SWDA.L", "iShares Core MSCI World ETF USD Acc GBP", StockTypes.Etf)
                .WithIsin("IE00B4L5Y983")
                .Build(),
            new Stock.StockBuilder("IGLT.L", "iShares Core UK Gilts ETF GBP Dist", StockTypes.Etf).Build(),
            new Stock.StockBuilder("MIDD.L", "iShares FTSE 250 ETF GBP Dist", StockTypes.Etf).Build(),
            new Stock.StockBuilder("IUKP.L", "iShares UK Property ETF GBP Dist", StockTypes.Etf)
                .WithDefaultCurrency("GBp")
                .Build(),
            new Stock.StockBuilder("GILS.L", "Lyxor Core UK Govt Bd (DR) ETF D GBP", StockTypes.Etf)
                .WithDefaultCurrency("GBp")
                .Build(),
            new Stock.StockBuilder("MICROFOCUS", "MICRO FOCUS INTL ORD GBP0.10", StockTypes.Share)
                .WithAliases(new List<StockAlias> { new(description: "MICRO FOCUS INT. PROVISIONAL CAP RET SHS") })
                .WithNotes("Not the right ticker")
                .Build(),
            new Stock.StockBuilder("MRW.L", "MORRISON(W)SUPRMKT ORD GBP0.10", StockTypes.Share)
                .WithAliases(new List<StockAlias> { new(description: "MORRISON (W) SUPERMARKETS PLC ORD GBP0.10") })
                .Build(),
            new Stock.StockBuilder("MCG.L", "MOBICO GROUP PLC ORD GBP0.10", StockTypes.Share)
                .WithAliases(new List<StockAlias> { new(description: "MOBICO GROUP PLC ORD GBP0.10") })
                .WithAlternativeSymbols(new List<AlternativeSymbol> { new(alternative: "NEX.L") })
                .WithNotes("Formerly National Express")
                .Build(),
            new Stock.StockBuilder("NG.L", "NATIONAL GRID ORD GBP0.113953", StockTypes.Share)
                .WithAliases(new List<StockAlias> { new(description: "NATIONAL GRID ORD GBP0.113953") })
                .Build(),
            new Stock.StockBuilder("RMG.L", "ROYAL MAIL PLC ORD GBP0.01", StockTypes.Share).Build(),
            new Stock.StockBuilder("SMT.L", "SCOTTISH MORTGAGE ORD GBP0.05", StockTypes.Share).Build(),
            new Stock.StockBuilder("TSCO.L", "TESCO ORD GBP0.05", StockTypes.Share).Build(),
            new Stock.StockBuilder("TET.L", "TREATT PLC ORD GBP0.10", StockTypes.Share).Build(),
            new Stock.StockBuilder("T25", "UK(GOVT OF) 2% SNR 07/09/2025 GBP1000", StockTypes.Gilt).Build(),
            new Stock.StockBuilder("VECP.L", "VANGUARD EUR CORP BD UCITS ETF GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VMID.L", "VANGUARD FTSE 250 UCITS ETF", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VWRP.L", "VANGUARD FTSE ALL-WORLD ETF USD ACC GBP", StockTypes.Etf)
                .WithAliases(
                    new List<StockAlias>
                    {
                        new(description: "VANGUARD FTSE ALL-WORLD UCITS ETF" // is this correct?
                        )
                    })
                .Build(),
            new Stock.StockBuilder("VWRL.L", "Vanguard FTSE All-World UCITS ETF GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VAPX.L", "Vanguard FTSE Dev AsiaPac exJpn ETF $Dis GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VEVE.L", "Vanguard FTSE Dev World ETF $Dis GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VFEM.L", "Vanguard FTSE Emerg Markets ETF $Dis GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VJPN.L", "Vanguard FTSE Japan ETF $Dis GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("VUCP.L", "Vanguard USD Corp Bd UCITS ETF GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("JDW.L", "Wetherspoon (J D) PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("WTB.L", "Whitbread PLC", StockTypes.Share).Build(),
            new Stock.StockBuilder("SGBX.L", "WisdomTree Physical Swiss Gold ETC GBP", StockTypes.Etf).Build(),
            new Stock.StockBuilder("XUKX.L", "Xtrackers FTSE 100 Income ETF 1D", StockTypes.Etf).Build(),
            new Stock.StockBuilder("XASX.L", "Xtrackers MSCI UK ESG ETF 1D £", StockTypes.Etf).Build(),
            new Stock.StockBuilder("XMWD.L", "Xtrackers MSCI World Swap ETF 1C USD", StockTypes.Etf)
                .WithAliases(
                    new List<StockAlias>
                    {
                        new(description: "XTRACKERS MSCI WORLD SWAP UCITS ETF 1" // is this the same thing??
                        )
                    })
                .Build(),
        };

        return stocks;
    }
}
