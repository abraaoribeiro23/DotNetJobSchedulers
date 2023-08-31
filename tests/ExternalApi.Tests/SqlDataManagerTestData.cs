using System.Collections;
using Infrastructure.Dkron.Common.Enums.Legacy;

namespace Infrastructure.Dkron.Tests;

public class SqlDataManagerTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var sqlFrequencyTypes = Enum.GetValues(typeof(SqlServerJobFrequencyTypes));
        var sqlSubDayFrequencyTypes = Enum.GetValues(typeof(SqlServerJobSubDayFrequencyTypes));

        var result = new List<object[]>();
        foreach (var sqlFrequencyType in sqlFrequencyTypes)
        {
            foreach (var sqlSubDayFrequencyType in sqlSubDayFrequencyTypes)
            {
                result.Add(new[] { sqlFrequencyType, sqlSubDayFrequencyType });
            }
        }

        return result.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    { return GetEnumerator(); }
}