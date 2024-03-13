using UnitOfWork.Model;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var shopDbContext = new ShopDbContext();

            shopDbContext.Database.EnsureDeleted();
            shopDbContext.Database.EnsureCreated();

            using var unitOfWork = new UnitOfWork.UnitOfWork(shopDbContext);

            FillDatabase(unitOfWork);

            var mostFrequentlyPurchasedProducts = unitOfWork.GetRepository<IOrderItemRepository>().GetMostFrequentlyPurchasedProducts();

            Console.WriteLine("Самые часто покупаемые товары:");
            foreach (var product in mostFrequentlyPurchasedProducts)
            {
                Console.WriteLine(product.Name);
            }
            Console.WriteLine();

            var customersMaxSpentMoney = unitOfWork.GetRepository<IOrderRepository>().GetClientsMaxSpentMoney();

            Console.WriteLine("Сколько каждый клиент потратил денег за все время:");
            foreach (var customerMoneyPair in customersMaxSpentMoney)
            {
                var customer = customerMoneyPair.Key;
                var money = customerMoneyPair.Value;

                Console.WriteLine($"Клиент {customer.FirstName} {customer.MiddleName} {customer.LastName} потратил: {money}");
            }
            Console.WriteLine();

            var categoriesProductsCount = unitOfWork.GetRepository<ICategoryRepository>().GetCategoriesProductsCount();

            Console.WriteLine("Сколько товаров каждой категории купили:");
            foreach (var categoryNameProductCountPair in categoriesProductsCount)
            {
                var categoryName = categoryNameProductCountPair.Key;
                var productsCount = categoryNameProductCountPair.Value;

                Console.WriteLine($"Категория - {categoryName}, количество товара - {productsCount}");
            }

            Console.Read();
        }

        public static void FillDatabase(UnitOfWork.UnitOfWork unitOfWork)
        {
            var categoriesRepository = unitOfWork.GetRepository<ICategoryRepository>();
            var productsRepository = unitOfWork.GetRepository<IProductRepository>();
            var customersRepository = unitOfWork.GetRepository<ICustomerRepository>();
            var ordersRepository = unitOfWork.GetRepository<IOrderRepository>();
            var orderItemsRepository = unitOfWork.GetRepository<IOrderItemRepository>();

            try
            {
                unitOfWork.BeginTransaction();

                var toyCar = new Product
                {
                    Name = "Toy car",
                    Price = 100
                };

                var doll = new Product
                {
                    Name = "Doll",
                    Price = 300
                };

                var toyotaCar = new Product
                {
                    Name = "Toyota car",
                    Price = 1_000_000
                };

                var mercedesCar = new Product
                {
                    Name = "Mercedes car",
                    Price = 3_000_000
                };

                var potato = new Product
                {
                    Name = "Potato",
                    Price = 90
                };

                var onion = new Product
                {
                    Name = "Onion",
                    Price = 45
                };

                var toyCategory = new Category
                {
                    Name = "Toy"
                };

                var plasticProductCategory = new Category
                {
                    Name = "Plastic product"
                };

                var carCategory = new Category
                {
                    Name = "Car"
                };

                var foodCategory = new Category
                {
                    Name = "Food"
                };

                var vegetableCategory = new Category
                {
                    Name = "Vegetable"
                };

                categoriesRepository.Create(new List<Category> { plasticProductCategory, toyCategory, carCategory, foodCategory, vegetableCategory });
                productsRepository.Create(new List<Product> { toyCar, doll, toyotaCar, mercedesCar, onion, potato });

                toyCar.Categories.Add(toyCategory);
                toyCar.Categories.Add(plasticProductCategory);

                doll.Categories.Add(toyCategory);
                doll.Categories.Add(plasticProductCategory);

                toyotaCar.Categories.Add(carCategory);

                mercedesCar.Categories.Add(carCategory);

                onion.Categories.Add(foodCategory);
                onion.Categories.Add(vegetableCategory);

                potato.Categories.Add(foodCategory);
                potato.Categories.Add(vegetableCategory);

                var customer1 = new Customer
                {
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    LastName = "Иванов",
                    PhoneNumber = "777777",
                    Email = "ivanov@mail.ru"
                };

                var customer2 = new Customer
                {
                    FirstName = "Петр",
                    MiddleName = "Петрович",
                    LastName = "Петров",
                    PhoneNumber = "888888",
                    Email = "petrov@mail.ru"
                };

                var customer3 = new Customer
                {
                    FirstName = "Сидр",
                    MiddleName = "Сидорович",
                    LastName = "Сидоров",
                    PhoneNumber = "999999",
                    Email = "sidorov@mail.ru"
                };

                var customer4 = new Customer
                {
                    FirstName = "Артем",
                    MiddleName = "Артемович",
                    LastName = "Артемов",
                    PhoneNumber = "333333",
                    Email = "artemov@mail.ru"
                };


                customersRepository.Create(new List<Customer> { customer1, customer2, customer3, customer4 });

                var order1 = new Order
                {
                    Customer = customer1,
                    OrderDate = new DateTime(2015, 10, 10, 1, 1, 12)
                };

                var order2 = new Order
                {
                    Customer = customer2,
                    OrderDate = new DateTime(2017, 12, 11, 13, 12, 20)
                };

                var order3 = new Order
                {
                    Customer = customer3,
                    OrderDate = new DateTime(2021, 3, 11, 11, 25, 17)
                };

                var order4 = new Order
                {
                    Customer = customer4,
                    OrderDate = new DateTime(2010, 4, 15, 17, 15, 10)
                };

                var order5 = new Order
                {
                    Customer = customer1,
                    OrderDate = new DateTime(2012, 2, 9, 12, 12, 15)
                };

                ordersRepository.Create(new List<Order> { order1, order2, order3, order4, order5 });

                var orderItem11 = new OrderItem
                {
                    Product = toyCar,
                    Order = order1,
                    Count = 3
                };

                var orderItem12 = new OrderItem
                {
                    Product = toyotaCar,
                    Order = order1,
                    Count = 2
                };

                var orderItem21 = new OrderItem
                {
                    Product = doll,
                    Order = order2,
                    Count = 5
                };

                var orderItem22 = new OrderItem
                {
                    Product = mercedesCar,
                    Order = order2,
                    Count = 1
                };

                var orderItem31 = new OrderItem
                {
                    Product = onion,
                    Order = order3,
                    Count = 10
                };

                var orderItem32 = new OrderItem
                {
                    Product = potato,
                    Order = order3,
                    Count = 10
                };

                var orderItem4 = new OrderItem
                {
                    Product = mercedesCar,
                    Order = order4,
                    Count = 5
                };

                var orderItem5 = new OrderItem
                {
                    Product = toyotaCar,
                    Order = order5,
                    Count = 10
                };

                orderItemsRepository.Create(new List<OrderItem> { orderItem11, orderItem12, orderItem21,
                    orderItem22, orderItem31, orderItem32, orderItem4, orderItem5});

                order1.OrderItems.Add(orderItem11);
                order1.OrderItems.Add(orderItem12);

                order2.OrderItems.Add(orderItem21);
                order2.OrderItems.Add(orderItem22);

                order3.OrderItems.Add(orderItem31);
                order3.OrderItems.Add(orderItem32);

                order4.OrderItems.Add(orderItem4);

                order5.OrderItems.Add(orderItem5);

                unitOfWork.CommitTransaction();

                unitOfWork.Save();
            }
            catch (Exception exception)
            {
                unitOfWork.RollbackTransaction();
            }
        }
    }
}