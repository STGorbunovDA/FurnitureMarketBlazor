namespace FurnitureMarketBlazor.Client.Services.AddressService
{
    public interface IAddressServiceClient
    {
        Task<Address> GetAddress();
        Task<Address> AddOrUpdateAddress(Address address);
    }
}
