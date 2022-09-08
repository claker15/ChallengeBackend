using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace RedTechChallenge.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly OrderRepository _orderRepository;

        public OrdersController( ApplicationDbContext context, OrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Endpoint to get all orders from database
        /// </summary>
        /// <returns>List of all orders</returns>
        [HttpGet(Name = "GetOrders")]
        public async Task<IActionResult> get()
        {
            var orders = await _orderRepository.getOrders();

            return Ok(orders);
        }

        /// <summary>
        /// Endpoint to get an order with a certain id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Response with OrderDto with id if it exists. No content response otherwise.</returns>
        [HttpGet("{id}",Name = "GetOrder")]
        public async Task<IActionResult> getOrderById(Guid id)
        {
            var temp = await _orderRepository.getOrderById(id);
            if (temp == null)
            {
                return NoContent();
            }
            else
            {
                return new OkObjectResult(temp);
            }
        }

        /// <summary>
        /// Endpoint to add new order to database
        /// </summary>
        /// <param name="order">OrderDto to add to database</param>
        /// <returns>Response with boolean on adding status. NoContent if orderDto supplied is invalid</returns>
        [HttpPost(Name = "PostOrder")]
        public async Task<IActionResult> addOrder(OrderDto order)
        {
            //validate parameters
            var temp = await _orderRepository.addOrder(order);
            if (order == null)
            {
                return NoContent();
            }

            return new OkObjectResult(temp);
        }

        /// <summary>
        /// Endpoint to edit order if it exists in the database
        /// </summary>
        /// <param name="order">OrderDto of object with changes</param>
        /// <returns>Ok response if succesesful. NoContent if unsuccessful.</returns>
        [HttpPut(Name = "EditOrder")]
        public async Task<IActionResult> editOrder(OrderDto order)
        {
            var temp = await _orderRepository.editOrder(order);
            if (temp)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Endpoint to delete order if it exists in database.
        /// </summary>
        /// <param name="ids">id of order to delete</param>
        /// <returns>Ok response if successful. No Content resonse if unsuccessful</returns>
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> deleteOrder(List<Guid> ids)
        {
            var temp = await _orderRepository.deleteOrder(ids);
            if (temp)
            {
                return Ok();
            }
            else
            { 
                return NoContent();
            }   
        }

        /// <summary>
        /// Endpoint to get orders of a certain type
        /// </summary>
        /// <param name="type">value to match on order.type</param>
        /// <returns>List of orderdtos that match supplied type. Can be null.</returns>
        [HttpGet]
        [Route("ByType")]
        public async Task<IActionResult> getOrdersByType(string type)
        {
            var list = await _orderRepository.getOrdersByType(type);
            return Ok(list);
        }
    }
}