using CoreStore.Application.CustomerModule.Dto;
using CoreStore.Application.CustomerModule.Interface;
using CoreStore.Domain.StoreContext.Entities;
using CoreStore.Infra.Data.Common;
using CoreStore.Infra.Data.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace CoreStore.Application.CustomerModule.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseConnection _database;

        public CustomerRepository(DatabaseConnection database)
        {
            _database = database;
        }
        public bool ExistDocument(string cpf)
        {
            var cmd = new CommandDefinition("SELECT COUNT(1) FROM dbo.CUSTOMER WHERE Cpf = @cpf", new { Cpf = cpf });
            return _database.GetConnection().ExecuteScalar<int>(cmd) > 0;
        }

        public bool ExistEmail(string email)
        {
            var cmd = new CommandDefinition("SELECT COUNT(1) FROM dbo.CUSTOMER WHERE Email = @email", new { Email = email });
            return _database.GetConnection().ExecuteScalar<int>(cmd) > 0;
        }

        public async Task Save(Customer customer)
        {
            string sql = @"INSERT INTO dbo.CUSTOMER (Cpf, FirstName, LastName, Email, Phone) 
                            Values (@Cpf, @FirstName, @LastName, @Email, @Phone);";

            _ =
                await _database.GetConnection().ExecuteAsync(sql,
                new
                {
                    customer.Cpf,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Phone
                });
        }

        public async Task<PagedView<CustomerDto>> GetPagedCustomers
            (string name, string cpf, string email, int page, int pageSize)
        {
            int totalItems = 0;
            PagedView<CustomerDto> result = new PagedView<CustomerDto>();

            var offset = QueryCommon.OffSet(page, pageSize);

            string sql = @"SELECT  FirstName + ' ' + LastName As  Name,
                                   Email,
                                   Phone
                            FROM dbo.CUSTOMER
                            WHERE (Cpf = @Cpf OR ISNULL(@Cpf,'') = '') 
                            AND
                            (Email = @Email OR ISNULL(@Email,'') = '') 
                            AND
                            ((FirstName like @FirstName OR ISNULL(@FirstName,'') = '') 
                            OR
                            (LastName like @LastName OR ISNULL(@LastName,'') = ''))
                            ";

            string sqlPaged = QueryCommon.PagedQuery(orderBy: "Name", sql, offset, pageSize);
            string sqlTotalCount = QueryCommon.TotalItemsQuery(sql);

            var customers = await _database.GetConnection()
                            .QueryAsync<CustomerDto>(sqlPaged,
                            new
                            {
                                Cpf = cpf ?? string.Empty,
                                Email = email ?? string.Empty,
                                FirstName = $"%{name}%" ?? $"%{string.Empty}%",
                                LastName = $"%{name}%" ?? $"%{string.Empty}%"
                            });

            totalItems = await _database.GetConnection()
                            .ExecuteScalarAsync<int>(sqlTotalCount,
                            new
                            {
                                Cpf = cpf ?? string.Empty,
                                Email = email ?? string.Empty,
                                FirstName = name ?? string.Empty,
                                LastName = name ?? string.Empty
                            });

            result.Data = customers.ToList();
            result.Page = page;
            result.PageSize = pageSize;
            result.TotalPages = totalItems / pageSize + (totalItems % pageSize == 0 ? 0 : 1);
            result.TotalItems = totalItems;

            return result;
        }
    }
}
