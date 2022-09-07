using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace RedTechChallenge.Models
{
    public class OrderRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets all orders from database
        /// </summary>
        /// <returns>List of OrderDtos</returns>
        public async Task<List<OrderDto>> getOrders()
        {
            return _mapper.Map<List<Order>, List<OrderDto>>(await _context.Orders.ToListAsync());
        }
        /// <summary>
        /// Gets and order by a specific id
        /// </summary>
        /// <param name="id">uuid of order</param>
        /// <returns>OrderDto if it exists in the database. Null otherwise</returns>
        public async Task<OrderDto> getOrderById(Guid id)
        {
            var temp = await _context.Orders.FindAsync(id);
            return _mapper.Map<Order,OrderDto>(temp);
        }
        //todo: Add endpoint
        /// <summary>
        /// Adds new internal object Order into the database
        /// </summary>
        /// <param name="order">OrderDto to be added to database</param>
        /// <returns>OrderDto that was added</returns>
        public async Task<OrderDto> addOrder(OrderDto order)
        {
            //map orderDto object to internal order object
            var intOrder = _mapper.Map<OrderDto, Order>(order);
            var temp await _context.Orders.AddAsync(intOrder);
            await _context.SaveChangesAsync();
            return order;
        }
        //todo: delete endpoint
        /// <summary>
        /// Deletes order with supplied id
        /// </summary>
        /// <param name="ids">id of order to be deleted</param>
        /// <returns>Boolean denoting if removal was successful. All or none since it is a list</returns>
        public async Task<Boolean> deleteOrder(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return false;
                }
                _context.Orders.Remove(order);
            }
            await _context.SaveChangesAsync();
            return true;

        }

        //todo: edit endpoint
        /// <summary>
        /// Edit an existing order
        /// </summary>
        /// <param name="order">OrderDto to be edited</param>
        /// <returns>Boolean whether editing was successful</returns>
        public async Task<Boolean> editOrder(OrderDto order)
        {
            var intOrder = _mapper.Map<OrderDto, Order>(order);
            if (order == null)
            {
                return false;
            }
            var temp = await _context.Orders.FindAsync(intOrder.Id);
            if (temp == null)
            {
                return false;
            }
            temp = intOrder;
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// returns all orders of supplied type
        /// </summary>
        /// <param name="type">Type to search on</param>
        /// <returns>List of OrderDto that match supplied type. Returns null if none found.</returns>
        public async Task<List<OrderDto>> getOrdersByType(string type)
        {
            var objList = await _context.Orders.Where(o => o.Type == type).ToListAsync();
            return _mapper.Map<List<Order> ,List<OrderDto>>(objList);
        }

    }
}
