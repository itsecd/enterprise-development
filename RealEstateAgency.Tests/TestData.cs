using RealEstateAgency.Domain;

public static class TestData
{
    public static List<Client> GetTestClients()
    {
        return new List<Client>
        {
            new Client(
                clientId: 1,
                firstAndLastName: "Chukarev Michail",
                pasport: "1234 567890",
                numberPhone: "+7 123 456 7890",
                address: "123 Lenin St, Samara",
                email: "misha@gmail.com"
            )
            {
                Orders = new List<Order>
                {
                    new Order(
                        id: 1,
                        time: DateTime.Now.AddMonths(-2),
                        type: Order.PurchaseOrSale.Purchase,
                        price: 450000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Residential,
                            "45 Kuybysheva St, Samara",
                            85.5,
                            3
                        )
                    ),
                    new Order(
                        id: 2,
                        time: DateTime.Now.AddMonths(-1),
                        type: Order.PurchaseOrSale.Sale,
                        price: 1200000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Uninhabitable,
                            "12 Sovetskaya St, Samara",
                            200.0,
                            5
                        )
                    )
                }
            },

            new Client(
                clientId: 2,
                firstAndLastName: "Stepanov Dima",
                pasport: "4321 987654",
                numberPhone: "+7 987 654 3210",
                address: "34 Pushkina St, Samara",
                email: "dima@gmail.com"
            )
            {
                Orders = new List<Order>
                {
                    new Order(
                        id: 3,
                        time: DateTime.Now.AddMonths(-4),
                        type: Order.PurchaseOrSale.Sale,
                        price: 950000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Residential,
                            "99 Gagarina Ave, Samara",
                            110.0,
                            4
                        )
                    )
                }
            },

            new Client(
                clientId: 3,
                firstAndLastName: "Ivan Ivanov",
                pasport: "3456 789012",
                numberPhone: "+7 654 321 0987",
                address: "5 Molodogvardeyskaya St, Samara",
                email: "ivanov@example.com"
            )
            {
                Orders = new List<Order>
                {
                    new Order(
                        id: 4,
                        time: DateTime.Now.AddMonths(-3),
                        type: Order.PurchaseOrSale.Purchase,
                        price: 500000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Residential,
                            "67 Sovetskaya St, Samara",
                            90.0,
                            3
                        )
                    )
                }
            },

            new Client(
                clientId: 4,
                firstAndLastName: "Petr Petrov",
                pasport: "6789 012345",
                numberPhone: "+7 321 654 7890",
                address: "89 Komsomolskaya St, Samara",
                email: "petrov@example.com"
            )
            {
                Orders = new List<Order>
                {
                    new Order(
                        id: 5,
                        time: DateTime.Now.AddMonths(-5),
                        type: Order.PurchaseOrSale.Sale,
                        price: 700000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Residential,
                            "22 Aerodromnaya St, Samara",
                            120.0,
                            4
                        )
                    ),
                    new Order(
                        id: 6,
                        time: DateTime.Now.AddMonths(-2),
                        type: Order.PurchaseOrSale.Purchase,
                        price: 850000,
                        item: new RealEstate(
                            RealEstate.PropertyType.Residential,
                            "13 Krasnoarmeyskaya St, Samara",
                            100.0,
                            3
                        )
                    )
                }
            }
        };
    }
}
