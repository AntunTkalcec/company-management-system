using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Core.Interfaces.Services;
using ErrorOr;

namespace CompanyManagementSystem.Infrastructure.Services;

public class CompanyService(IBaseRepository<Company> companyRepository, IMapper mapper) : ICompanyService
{
    public async Task<ErrorOr<int>> CreateAsync(CompanyDTO entity)
    {
        if (!ValidateCompany(entity))
        {
            return ErrorPartials.Company.CompanyValidationFailed("Company data is incorrect.");
        }

        await companyRepository.AddAsync(mapper.Map<Company>(entity));

        return entity.Id;
    }

    public async Task<ErrorOr<Deleted>> DeleteAsync(int id) => await companyRepository.DeleteAsync(id);

    public async Task<List<CompanyDTO>> GetAllAsync()
    {
        List<Company> companies = await companyRepository.GetAllAsync();

        return mapper.Map<List<CompanyDTO>>(companies);
    }

    public async Task<ErrorOr<CompanyDTO>> GetByIdAsync(int id)
    {
        Company? company = await companyRepository.GetByIdAsync(id, _ => _.Staff);

        return company is null
            ? ErrorPartials.Company.CompanyNotFound($"Company with id '{id}' not found!")
            : mapper.Map<CompanyDTO>(company);
    }

    public async Task<ErrorOr<Updated>> UpdateAsync(int id, CompanyDTO entity)
    {
        if (!ValidateCompany(entity))
        {
            return ErrorPartials.Company.CompanyValidationFailed("Company data is incorrect.");
        }

        Company company = mapper.Map<Company>(entity);
        company.UpdatedAt = DateTime.UtcNow;

        return await companyRepository.UpdateAsync(company);
    }

    #region Private methods
    private static bool ValidateCompany(CompanyDTO entity) => !string.IsNullOrEmpty(entity.Name) && entity.PTO > 0;
    #endregion
}
