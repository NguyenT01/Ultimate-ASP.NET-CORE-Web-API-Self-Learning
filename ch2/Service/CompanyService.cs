using Shared.DataTransferObjects;
using Contracts;
using Service.Contracts;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using System.ComponentModel.Design;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly IloggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, IloggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;  
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges)
    {
        var company = await GetCompanyAndCheckIfExists(id, trackChanges);

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
    public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        _repository.Company.CreateCompany(companyEntity);
        await _repository.SaveAsync();

        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }
    public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChanges);

        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }
    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();

        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach(var company in companyEntities)
        {
            _repository.Company.CreateCompany(company);
        }
        await _repository.SaveAsync();

        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

        return (companies: companyCollectionToReturn, ids: ids);
    }
    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await GetCompanyAndCheckIfExists(companyId, trackChanges);

        _repository.Company.DeleteCompany(company);
        await _repository.SaveAsync();

    }
    public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
    {
        var company = await GetCompanyAndCheckIfExists(companyId, trackChanges);

        _mapper.Map(companyForUpdate, company);
        await _repository.SaveAsync();
    }

    // PRIVATE METHODS
    private async Task<Company> GetCompanyAndCheckIfExists(Guid companyId, bool trackChanges)
    {
        var companyEntity = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (companyEntity is null)
            throw new CompanyNotFoundException(companyId);
        return companyEntity;
    }
}