using AppStudentiCommon.Model;
using AppStudentiCommon.Model.Search;
using AppStudentiCommon.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Interface
{
    public interface IStudentsManager
    {
        ResultData<StudentsViewModel> GetStudentsBySearch(StudentsSearch s);
        List<StudentsViewModel> GetAllStudent();
        StudentsViewModel GetStudentById(int id);
        List<StudentsViewModel> GetStudentByName(string name);
        DtoCommandResult InsertStudent(StudentsViewModel entity);
        DtoCommandResult UpdateStudent(StudentsViewModel entity);
        DtoCommandResult DeleteStudent(int id);
    }
}
