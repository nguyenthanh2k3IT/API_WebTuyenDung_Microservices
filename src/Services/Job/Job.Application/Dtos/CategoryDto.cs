﻿namespace Job.Application.Dtos;

public class CategoryDto : BaseDto<Guid>
{
    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
