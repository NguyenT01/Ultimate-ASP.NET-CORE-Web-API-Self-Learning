using AutoMapper;
using Contracts;
using Contracts.Dapper;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly IDapperManager _dapper;
    private readonly IloggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, IDapperManager dapper, IloggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _dapper = dapper;
    }

    public async Task<IEnumerable<CompanyDto1>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await _dapper.Read.GetAll<CompanyDto1>("SELECT * FROM Companies");
        return companies;

        /* var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
        */
    }

    public async Task<CompanyDto1> GetCompanyAsync(Guid id, bool trackChanges)
    {
        var company = await _dapper.Read.FindByConditionSingle<CompanyDto1>(
            "SELECT * FROM Companies WHERE CompanyId = @id", new { id = id });
        if (company is null)
            throw new CompanyNotFoundException(id);
        return company;

        /*
        var company = await GetCompanyAndCheckIfExists(id, trackChanges);

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
        */
    }
    public async Task<CompanyDto1> CreateCompanyAsync(CompanyForCreationDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);

        Guid newCid = Guid.NewGuid();

        var effectedRow = await _dapper.Create.CreateItem<CompanyDto1>(
            "INSERT INTO Companies (CompanyId, Name, Address, Country) VALUES (@guid, @Name, @Address, @Country)",
            new
            {
                guid = newCid,
                Name = companyEntity.Name,
                Address = companyEntity.Address,
                Country = company.Country
            });

        foreach (var employee in company.Employees)
        {
            await _dapper.Create.CreateItem<EmployeeDto1>(
                "INSERT INTO Employees(EmployeeId, Name, Age, Position, CompanyId) VALUES (@eid, @name, @age, @pos, @cid)",
                new
                {
                    eid = Guid.NewGuid(),
                    name = employee.Name,
                    age = employee.Age,
                    pos = employee.Position,
                    cid = newCid
                });
        }

        return await _dapper.Read.FindByConditionSingleNotNull<CompanyDto1>(
            "SELECT * FROM Companies WHERE CompanyId = @cid", new { cid = newCid });

        /*
        var companyEntity = _mapper.Map<Company>(company);

        _repository.Company.CreateCompany(companyEntity);
        await _repository.SaveAsync();

        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
        */
    }
    public async Task<IEnumerable<CompanyDto1>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var companies = await _dapper.Read.FindByConditionList<CompanyDto1>(
            "SELECT * FROM Companies WHERE CompanyId IN @IDs", new { IDs = ids.ToArray() });

        if (companies is null)
            throw new CollectionByIdsBadRequestException();
        return companies;

        /*
        if (ids is null)
            throw new IdParametersBadRequestException();

        var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChanges);

        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
        */
    }
    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();

        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach (var company in companyEntities)
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
        var company = await _dapper.Read.FindByConditionSingle<CompanyDto1>(
            "SELECT * FROM Companies WHERE Companies.CompanyId = @id", new { id = companyId });

        if (company is null)
            throw new CompanyNotFoundException(companyId);

        await _dapper.Delete.DeleteItem("DELETE FROM Companies WHERE CompanyId LIKE @id", new { id = companyId });

        /*
        var company = await GetCompanyAndCheckIfExists(companyId, trackChanges);

        _repository.Company.DeleteCompany(company);
        await _repository.SaveAsync();
        */
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