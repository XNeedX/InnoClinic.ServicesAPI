using MediatR;
using Services.Application.Abstractions;
using Services.Application.DTOs;
using Services.Application.Queries;
using Services.Application.Results;
using Services.Domain.Models;

namespace Services.Application.Handlers;

public class ViewServiceByCategoryHandler : IRequestHandler<ViewServiceByCategoryQuery, Result<ViewCategoryDataDTO>>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ISpecializationRepository _specializationRepository;

    public ViewServiceByCategoryHandler(IServiceRepository serviceRepository, ISpecializationRepository specializationRepository)
    {
        _serviceRepository = serviceRepository;
        _specializationRepository = specializationRepository;
    }

    public async Task<Result<ViewCategoryDataDTO>> Handle(ViewServiceByCategoryQuery request, CancellationToken cancellationToken)
    {
        if (request.Category == Category.Consultations)
        {
            var specializations = await _specializationRepository.FindByFilterAsync(
                s => s.Status == Status.Active && s.Category == Category.Consultations,
                s => s.Services
            );

            var specDtos = specializations.Select(s => new ViewSpecializationDTO(
                Id: s.Id,
                Status: s.Status,
                Name: s.Name,
                Services: s.Services
                    .Where(srv => srv.Status == Status.Active)
                    .Select(srv => new ViewSpecializationServiceDTO(
                        Id: srv.Id,
                        Name: srv.Name,
                        Price: srv.Price,
                        Status: srv.Status,
                        Category: srv.Category
                    )).ToList()
            )).ToList();

            var resultData = new ViewCategoryDataDTO(specDtos, new List<ViewSpecializationServiceDTO>());
            return Result<ViewCategoryDataDTO>.Success(resultData);
        }

        else
        {
            var services = await _serviceRepository.FindByFilterAsync(
                s => s.Status == Status.Active && s.Category == request.Category
            );

            var serviceDtos = services.Select(s => new ViewSpecializationServiceDTO(
                Id: s.Id,
                Name: s.Name,
                Price: s.Price,
                Category: s.Category,
                Status: s.Status
            )).ToList();

            var resultData = new ViewCategoryDataDTO(new List<ViewSpecializationDTO>(), serviceDtos);
            return Result<ViewCategoryDataDTO>.Success(resultData);
        }
    }
}