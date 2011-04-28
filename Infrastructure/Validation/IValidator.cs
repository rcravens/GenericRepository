using System.Collections.Generic;

namespace Repository.Infrastructure.Validation
{
    public interface IValidator<T>
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}
