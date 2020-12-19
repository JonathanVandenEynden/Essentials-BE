using P3Backend.Model.Users;

namespace P3Backend.Model.RepoInterfaces {
    public interface IDeviceTokensRepository {
        DeviceTokens Get();
        void SaveChanges();
        void Update(DeviceTokens dt);
    }
}