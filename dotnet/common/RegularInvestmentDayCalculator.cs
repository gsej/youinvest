namespace common;

public static class RegularInvestmentDayCalculator
{

    public static bool IsRegularInvestmentDay(string dateAsString)
    {

        var year = int.Parse(dateAsString.Substring(0, 4));
        var month = int.Parse(dateAsString.Substring(5, 2));
        var day = int.Parse(dateAsString.Substring(8, 2));

        var date = new DateTime(year, month, day);
        
        if (day == 10 && (
                date.DayOfWeek == DayOfWeek.Monday ||
                date.DayOfWeek == DayOfWeek.Tuesday ||
                date.DayOfWeek == DayOfWeek.Wednesday ||
                date.DayOfWeek == DayOfWeek.Thursday ||
                date.DayOfWeek == DayOfWeek.Friday))
        {
            return true;
        }

        if (  (day == 11 || day == 12) && date.DayOfWeek == DayOfWeek.Monday)
        {
            return true;
        }

        return false;
        
        //return true;
    }
}