using System.Collections;
using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Infrastructure.Tests.SqlDataManagerTests;

public class SqlDataManagerTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _validCombinations = new()
    {
        new object[] { SqlServerJobFrequencyTypes.Once, SqlServerJobSubDayFrequencyTypes.SpecifiedTime },
        new object[] { SqlServerJobFrequencyTypes.Daily, SqlServerJobSubDayFrequencyTypes.SpecifiedTime },
        new object[] { SqlServerJobFrequencyTypes.Weekly, SqlServerJobSubDayFrequencyTypes.SpecifiedTime },
        new object[] { SqlServerJobFrequencyTypes.Monthly, SqlServerJobSubDayFrequencyTypes.SpecifiedTime }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        var sqlFrequencyTypes = Enum.GetValues(typeof(SqlServerJobFrequencyTypes));
        var sqlSubDayFrequencyTypes = Enum.GetValues(typeof(SqlServerJobSubDayFrequencyTypes));

        var result = new List<object[]>();
        foreach (var sqlFrequencyType in sqlFrequencyTypes)
        {
            foreach (var sqlSubDayFrequencyType in sqlSubDayFrequencyTypes)
            {
                var isValid = _validCombinations.Any(item => item.SequenceEqual(new[] { sqlFrequencyType, sqlSubDayFrequencyType }));
                result.Add(new [] { sqlFrequencyType, sqlSubDayFrequencyType, isValid });
            }
        }

        return result.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    { return GetEnumerator(); }
}