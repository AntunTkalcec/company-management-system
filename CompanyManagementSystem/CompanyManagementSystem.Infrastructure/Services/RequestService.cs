using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Infrastructure.Services;

public class RequestService(IBaseRepository<Request> requestRepository, IMapper mapper, IBaseRepository<User> userRepository) : IRequestService
{
    public async Task<int> CreateAsync(RequestDTO entity)
    {
        await requestRepository.AddAsync(mapper.Map<Request>(entity));

        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        await requestRepository.DeleteAsync(id);
    }

    public async Task<List<RequestDTO>> GetAllAsync()
    {
        List<Request> requests= await requestRepository.GetAllAsync();

        return mapper.Map<List<RequestDTO>>(requests);
    }

    public async Task<RequestDTO> GetByIdAsync(int id)
    {
        Request? request = await requestRepository.GetByIdAsync(id);

        return mapper.Map<RequestDTO>(request);
    }

    public async Task<List<RequestDTO>> GetUnacceptedForCompany(int companyId)
    {
        List<Request>? requests = await requestRepository.Fetch().Include(_ => _.Creator).ThenInclude(_ => _.Company).Where(_ => _.Creator.CompanyId == companyId).ToListAsync();

        return mapper.Map<List<RequestDTO>>(requests);
    }

    public async Task UpdateAsync(int id, RequestDTO entity)
    {
        Request request = mapper.Map<Request>(entity);
        request.UpdatedAt = DateTime.UtcNow;

        await requestRepository.UpdateAsync(request);
    }
}
