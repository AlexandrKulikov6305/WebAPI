using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SoundCloudLite.DAL
{
    public class SoundCloudLiteContextFactory : IDbContextFactory<SoundCloudLiteContext>
    {
        public SoundCloudLiteContext Create()
        {
            return new SoundCloudLiteContext();
        }
    }
}