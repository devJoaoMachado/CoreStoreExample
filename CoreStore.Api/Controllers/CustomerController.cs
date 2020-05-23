using CoreStore.Application.CustomerModule.Command;
using CoreStore.Application.CustomerModule.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreStore.Api.Controllers
{
    [Route("api/v1/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            if (command == null) return BadRequest();

            var result = await _mediator.Send(command);

            return new OkObjectResult(result);
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomersQuery customersQuery)
        {
            if (customersQuery == null) return BadRequest();

            var result = await _mediator.Send(customersQuery);

            if (customersQuery.Invalid)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }
    }
}
