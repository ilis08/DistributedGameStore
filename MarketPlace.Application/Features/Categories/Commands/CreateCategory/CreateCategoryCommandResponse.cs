﻿using MarketPlace.Application.Responses;

namespace MarketPlace.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandResponse : BaseResponse
{
    public CreateCategoryCommandResponse() : base()
    {

    }

    public CreateCategoryDto Category { get; set; } = default!;
}
