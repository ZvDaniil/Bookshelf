﻿using AutoMapper;
using Bookshelf.Application.Common.Mappings;
using Bookshelf.Application.Reviews.Commands.PublishReview;

namespace Bookshelf.Api.Models.Review;

public class PublishReviewDto : IMapWith<PublishReviewCommand>
{
    public Guid ReviewId { get; set; }

    public void Mapping(Profile profile) =>
        profile.CreateMap<PublishReviewDto, PublishReviewCommand>()
            .ForMember(command => command.Id, opt => opt.MapFrom(dto => dto.ReviewId));
}