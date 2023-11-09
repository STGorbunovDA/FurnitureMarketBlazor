namespace FurnitureMarketBlazor.Server.Services.AddressService
{
    public interface IAddressServiceServer
    {
        Task<ServiceResponse<Address>> GetAddress();
        Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address);
    }
}
