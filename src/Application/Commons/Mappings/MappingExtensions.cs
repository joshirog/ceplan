using Application.Commons.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Mappings;

public static class MappingExtensions
{
    public static Task<Paginated<TDestination>> PaginatedAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        => Paginated<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListsAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
        => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
}