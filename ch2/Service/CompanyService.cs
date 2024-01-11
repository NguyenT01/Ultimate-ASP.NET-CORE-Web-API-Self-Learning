﻿using Shared.DataTransferObjects;
using Contracts;
using Service.Contracts;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;

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

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = _repository.Company.GetAllCompanies(trackChanges);

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(id, trackChanges);
        // check if the company is null
        if (company is null)
            throw new CompanyNotFoundException(id);

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);
        _logger.LogWarn(companyEntity.Employees!.ToString()!);

        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();

        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }
}