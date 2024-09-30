using EnterpriseStatistics.Domain;
using System.Globalization;


namespace EnterpriseStatistics.Tests;

public class EnterpriseStatisticsFileReader(string fileName)
{
    private StreamReader _streamReader = new StreamReader(fileName);

    /// <summary>
    /// Чтение данных из csv-файла с последующей записью в объекты классов
    /// </summary>
    public List<Supply> ReadSupply()
    {
        var supplies = new List<Supply>();
        var enterprises = new Dictionary<ulong, Enterprise>();
        var suppliers = new Dictionary<int, Supplier>();

        
        while (!_streamReader.EndOfStream)
        {
            var suppliesLine = _streamReader.ReadLine();
            if (suppliesLine == null || !suppliesLine.Contains('"')) continue;
            var tokens = suppliesLine.Split(',');

                
            ulong enterpriseKey = ulong.Parse(tokens[0]);
            if (!enterprises.TryGetValue(enterpriseKey, out var enterprise))
            {
                enterprise = new Enterprise
                {
                    MainStateRegistrationNumber = enterpriseKey, 
                    Name = tokens[1],
                    Address = tokens[2],
                    Phone = tokens[3],
                    EmployeeCount = int.Parse(tokens[4]),
                    TotalArea = int.Parse(tokens[5]),
                    IndustryType = (IndustryTypes)Enum.Parse(typeof(IndustryTypes), tokens[6]),
                    OwnershipForm = (OwnershipForms)Enum.Parse(typeof(OwnershipForms), tokens[7])
                };
                enterprises[enterpriseKey] = enterprise; 
            }

            var supplierKey = int.Parse(tokens[8]);
            if (!suppliers.TryGetValue(supplierKey, out var supplier))
            {
                supplier = new Supplier
                {
                    Id = supplierKey,
                    FullName = tokens[9],
                    Address = tokens[10],
                    Phone = tokens[11],
                };
                suppliers[supplierKey] = supplier;
            }

            var supply = new Supply
            {
                Id = int.Parse(tokens[12]),
                Supplier = supplier,
                Enterprise = enterprise,
                Quanity = int.Parse(tokens[13]),
                Date = DateTime.ParseExact(tokens[14], "yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            supplies.Add(supply);
            
        }
        _streamReader.Dispose();
        return supplies;
    }

}
