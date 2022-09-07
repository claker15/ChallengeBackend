namespace RedTechChallenge.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            AllowNullCollections = true;
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
