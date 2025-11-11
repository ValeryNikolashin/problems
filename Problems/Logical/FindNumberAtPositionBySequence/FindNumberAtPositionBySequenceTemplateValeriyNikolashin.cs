using System.Collections.Immutable;
using System.Diagnostics;
using Xunit;

namespace Problems.Logical.FindNumberAtPositionBySequence;

public class FindNumberAtPositionBySequenceTemplateValeriyNikolashin
{
    public int FindNumber(ImmutableArray<int> digits, int position)
    {
        if (digits.Length is < 1 or > 99)
            throw new ArgumentException("Digits count must be between 1 and 99.", nameof(digits));
        if (digits.Length < position)
            throw new ArgumentException("Position is more than the length of the digits.", nameof(position));

        const int maxDigit = 9;

        if (digits.Length < maxDigit)
            return digits[^position];

        var lastNumber = digits.Last();
        var lastNumberTens = lastNumber / 10;
        var lastNumberUnits = lastNumber % 10;

        var currentPosition = 0;

        var minDigit = lastNumberTens > lastNumberUnits
            ? lastNumberTens
            : lastNumberTens + 1;

        for (var i = maxDigit; i >= minDigit; i--)
        {
            currentPosition++;

            Console.Write(i);

            if (currentPosition == position)
                return i;
        }

        for (var tens = lastNumberTens; tens > 0; tens--)
        {
            var maxUnit = tens == lastNumberTens
                ? lastNumberUnits
                : maxDigit;

            for (var units = maxUnit; units >= 0; units--)
            {
                if (tens == units)
                {
                    currentPosition++;

                    Console.Write(units);

                    if (currentPosition == position)
                        return units;
                }

                currentPosition++;

                Console.Write(tens * 10 + units);

                if (currentPosition == position)
                    return tens * 10 + units;
            }
        }

        throw new UnreachableException();
    }

    [Theory]
    [InlineData(99, 15, 87)]
    [InlineData(99, 28, 75)]
    [InlineData(99, 99, 10)]
    [InlineData(89, 17, 76)]
    [InlineData(79, 35, 50)]
    [InlineData(10, 10, 10)]
    [InlineData(9, 5, 5)]
    [InlineData(5, 2, 4)]
    [InlineData(1, 1, 1)]
    public void Test(int max, int position, int expected)
    {
        var sequence = Enumerable.Range(1, max).ToImmutableArray();

        Assert.Equal(expected, FindNumber(sequence, position));
    }
}