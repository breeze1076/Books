using Books.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Infrastructure.Services
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            return services;
        }
    }
}
