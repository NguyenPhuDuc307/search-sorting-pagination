using CourseManagement.ViewModels;

namespace CourseManagement.Services
{
    public interface ICoursesService
    {
        Task<IEnumerable<CourseViewModel>> GetAll();
        Task<PaginatedList<CourseViewModel>> GetAllFilter(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize);
        Task<CourseViewModel> GetById(int id);
        Task<int> Create(CourseRequest request);
        Task<int> Update(CourseViewModel request);
        Task<int> Delete(int id);
    }
}