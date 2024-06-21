using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Infrastructure.Services;

public class RequestService(IBaseRepository<Request> requestRepository, IMapper mapper) : IRequestService
{
    public async Task<ErrorOr<int>> CreateAsync(RequestDTO entity)
    {
        await requestRepository.AddAsync(mapper.Map<Request>(entity));

        return entity.Id;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(int id)
    {
        return await requestRepository.DeleteAsync(id);
    }

    public async Task<List<RequestDTO>> GetAllAsync()
    {
        List<Request> requests = await requestRepository.GetAllAsync();

        return mapper.Map<List<RequestDTO>>(requests);
    }

    public async Task<ErrorOr<RequestDTO>> GetByIdAsync(int id)
    {
        Request? request = await requestRepository.GetByIdAsync(id);

        if (request is null)
        {
            return ErrorPartials.Request.RequestNotFound($"Request with id '{id}' not found!");
        }

        return mapper.Map<RequestDTO>(request);
    }

    public async Task<List<RequestDTO>> GetUnacceptedForCompany(int companyId)
    {
        List<Request>? requests = await requestRepository
            .Fetch()
            .Include(_ => _.Creator)
                .ThenInclude(_ => _.Company)
            .Where(_ => _.Creator.CompanyId == companyId)
            .ToListAsync();

        return mapper.Map<List<RequestDTO>>(requests);
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(int id, RequestDTO entity)
    {
        Request request = mapper.Map<Request>(entity);
        request.UpdatedAt = DateTime.UtcNow;

        return await requestRepository.UpdateAsync(request);
    }
}
