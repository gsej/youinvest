using common;
using FluentAssertions;

namespace unit_tests;

public class RegularInvestmentDayCalculatorTests
{
    [Theory]
    [InlineData("2023-05-10", true, "is a weekday")]
    [InlineData("2023-06-12", true, "is a monday, so the 10th was on the Saturday")]
    [InlineData("2023-09-11", true, "is a monday, so the 10th was on the Sunday")]
    [InlineData("2023-09-15", false, "is not 10th, 11th, 12th")]
    [InlineData("2023-09-10", false, "is 10th, but not a weekday")]
    public void IsRegularInvestmentDay_ReturnsCorrectResult(string date, bool expectedResult, string because)
    {
        var isRegularInvestmentDay = RegularInvestmentDayCalculator.IsRegularInvestmentDay(date);

        isRegularInvestmentDay.Should().Be(expectedResult, because);

    }
}