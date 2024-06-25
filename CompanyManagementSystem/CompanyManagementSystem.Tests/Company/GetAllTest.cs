using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Interfaces.Repositories.Base;
using CompanyManagementSystem.Infrastructure.Services;

namespace CompanyManagementSystem.Tests.Company;

[TestFixture]
public class GetAllTest
{
    private IBaseRepository<Core.Entities.Company> _repository;
    private CompanyService _service;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IBaseRepository<Core.Entities.Company>>();
        _mapper = Substitute.For<IMapper>();

        _service = new(_repository, _mapper);
    }

    [Test]
    public async Task GetAll_ShouldReturnCompanies()
    {
        // Arrange
        List<Core.Entities.Company> companies =
        [
            new(),
            new()
        ];
        List<CompanyDTO> companyDtos =
        [
            new(),
            new()
        ];

        _mapper.Map<List<CompanyDTO>>(companies).Returns(companyDtos);
        _repository.GetAllAsync().Returns(Task.FromResult(companies));

        // Act
        List<CompanyDTO> result = await _service.GetAllAsync();


        // Assert
        Assert.That(result, Is.Not.Null, "List should not be null.");
        Assert.That(result, Is.InstanceOf<List<CompanyDTO>>(), "Result should be a list of CompanyDTO objects.");
        Assert.That(result, Is.Not.Empty);
    }
}
