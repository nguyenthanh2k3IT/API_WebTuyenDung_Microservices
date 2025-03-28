﻿namespace Blog.API.Data.Seeding;

public interface IDataInitializer
{
    Task SeedAsync();
    Task InitStatus();
    Task InitTagName();
    Task InitCategory();
    Task InitPost();
}
