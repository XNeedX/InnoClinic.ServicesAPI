namespace Services.Application.DTOs;

public sealed record ViewCategoryDataDTO(
    List<ViewSpecializationDTO> Specializations,
    List<ViewSpecializationServiceDTO> Services
);
