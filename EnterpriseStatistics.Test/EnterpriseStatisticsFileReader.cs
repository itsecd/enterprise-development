using EnterpriseStatistics.Domain;

namespace EnterpriseStatistics.Test;

internal class EnterpriseStatisticsFileReader(string fileName)
{
    public List<Enterprise> ReadEnterprise()
    {
        var enterprises = new List<Enterprise>();

        var streamReader = new StreamReader(fileName);
        while (!streamReader.EndOfStream)
        {
            var enterprisesLine = streamReader.ReadLine();
            if (enterprisesLine == null || !enterprisesLine.Contains('"')) continue;

        }
        return enterprises;
    }
}
