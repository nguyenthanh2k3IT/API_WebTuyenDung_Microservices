﻿
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Core.Paging;

public class PaginatedList<T>
{
	public IReadOnlyCollection<T> Items { get; }
	public int PageIndex { get; }
	public int PageSize { get; }
	public int TotalPages { get; }
	public int TotalRecords { get; }

	public PaginatedList(IReadOnlyCollection<T> items, int count, int pageIndex, int pageSize)
	{
		PageIndex = pageIndex;
		PageSize = pageSize;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		TotalRecords = count;
		Items = items;
	}

	public bool HasPreviousPage => PageIndex > 1;
	public bool HasNextPage => PageIndex < TotalPages;

	public static PaginatedList<T> Empty(int pageIndex)
	{
		return new PaginatedList<T>(Array.Empty<T>(), 0, pageIndex, 1);
	}

	public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
	{
		var count = await source.CountAsync();
		var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

		return new PaginatedList<T>(items, count, pageNumber, pageSize);
	}

	public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
	{
		var count = source.Count();
		var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

		return new PaginatedList<T>(items, count, pageNumber, pageSize);
	}
}
