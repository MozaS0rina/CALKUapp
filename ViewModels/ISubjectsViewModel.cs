using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALKU.ViewModels
{
    public interface ISubjectsViewModel
    {
        public  Task<float> CalculateGPA();
        public Task LoadSubjectsAsync();
        public Task SaveSubjectAsync();
        public Task DeleteSubjectAsync(int id);
    }
}
