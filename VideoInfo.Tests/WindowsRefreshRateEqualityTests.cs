using DisplayMagicianShared.Windows;
using Xunit;

namespace VideoInfo.Tests;

public class WindowsRefreshRateEqualityTests
{
    [Theory]
    [InlineData(120u, 2u, 60u, 1u)]
    [InlineData(120000u, 2002u, 60000u, 1001u)]
    [InlineData(0u, 2u, 0u, 1u)]
    [InlineData(uint.MaxValue, uint.MaxValue, 1u, 1u)]
    [InlineData(0u, 0u, 0u, 0u)]
    [InlineData(2u, 0u, 2u, 0u)]
    public void Equivalent_rates_have_equal_values_and_hashes_without_changing_stored_fractions(
        uint savedNumerator, uint savedDenominator, uint currentNumerator, uint currentDenominator)
    {
        DISPLAYCONFIG_RATIONAL saved = new DISPLAYCONFIG_RATIONAL
        {
            Numerator = savedNumerator,
            Denominator = savedDenominator
        };
        DISPLAYCONFIG_RATIONAL current = new DISPLAYCONFIG_RATIONAL
        {
            Numerator = currentNumerator,
            Denominator = currentDenominator
        };

        Assert.Equal(saved, current);
        Assert.True(saved == current);
        Assert.False(saved != current);
        Assert.Equal(saved.GetHashCode(), current.GetHashCode());
        Assert.Equal(savedNumerator, saved.Numerator);
        Assert.Equal(savedDenominator, saved.Denominator);
        Assert.Equal(currentNumerator, current.Numerator);
        Assert.Equal(currentDenominator, current.Denominator);
    }

    [Theory]
    [InlineData(60000u, 1001u, 60u, 1u)]
    [InlineData(60u, 1u, 120u, 1u)]
    [InlineData(uint.MaxValue, 1u, uint.MaxValue - 1u, 1u)]
    [InlineData(uint.MaxValue, uint.MaxValue - 1u, uint.MaxValue - 1u, uint.MaxValue - 2u)]
    [InlineData(0u, 0u, 0u, 1u)]
    [InlineData(1u, 0u, 2u, 0u)]
    [InlineData(2u, 0u, 2u, 1u)]
    public void Different_rates_and_zero_denominator_values_remain_unequal(
        uint savedNumerator, uint savedDenominator, uint currentNumerator, uint currentDenominator)
    {
        DISPLAYCONFIG_RATIONAL saved = new DISPLAYCONFIG_RATIONAL
        {
            Numerator = savedNumerator,
            Denominator = savedDenominator
        };
        DISPLAYCONFIG_RATIONAL current = new DISPLAYCONFIG_RATIONAL
        {
            Numerator = currentNumerator,
            Denominator = currentDenominator
        };

        Assert.NotEqual(saved, current);
        Assert.True(saved != current);
        Assert.False(saved == current);
    }

    [Fact]
    public void Equivalent_refresh_rates_do_not_change_display_path_equality()
    {
        DISPLAYCONFIG_PATH_INFO saved = new DISPLAYCONFIG_PATH_INFO();
        saved.TargetInfo.RefreshRate = new DISPLAYCONFIG_RATIONAL { Numerator = 120, Denominator = 2 };
        DISPLAYCONFIG_PATH_INFO current = saved;
        current.TargetInfo.RefreshRate = new DISPLAYCONFIG_RATIONAL { Numerator = 60, Denominator = 1 };

        Assert.Equal(saved, current);
    }
}
