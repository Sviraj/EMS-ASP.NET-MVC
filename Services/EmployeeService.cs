using WebApplication1.Helpers;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMemoryCacheHelper _memoryCacheHelper;
        public EmployeeService(IRepository<Employee> employeeRepository, IMemoryCacheHelper memoryCacheHelper)
        {
            _employeeRepository = employeeRepository;
            _memoryCacheHelper = memoryCacheHelper;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            // "EmployeeList" is the cache key.
            // The lambda function fetches the employee list from the repository if the cache does not already contain it.
            return _memoryCacheHelper.Cached("EmployeeList", () => _employeeRepository.GetAll().ToList());

        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void AddEmployee(Employee employee)
        {
            // Optionally add business logic checks
            _employeeRepository.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }
    }

}
