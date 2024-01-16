using Contracts;

namespace Repository;

public class RepositoryManager: IRepositoryManager
{
    // REPOSITORY CONTEXT
    private readonly RepositoryContext _repositoryContext;
    // REPOSITORY CỦA TỪNG ĐỐI TƯỢNG
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;

    // CONSTRUCTOR chứa REPOSITORY CONTEXT
    public RepositoryManager(RepositoryContext context)
    {
        _repositoryContext = context;
        // KHỞI TẠO TỪNG REPOSITORY CỦA CÁC ĐỐI TƯỢNG KHI ĐÃ CÓ REPOSITORY CONTEXT
        _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
    }

    // HÀM GETTER REPOSITORY CỦA TỪNG ĐỐI TƯỢNG
    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
    
    // IMPLEMENT PHƯƠNG THỨC SAVE() ĐỂ LƯU CÁC THAY ĐỔI
    public async Task SaveAsync()
        => await _repositoryContext.SaveChangesAsync();

}