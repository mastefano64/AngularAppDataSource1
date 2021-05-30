using AppStudentiCommon.Model;
using AppStudentiCommon.Model.Search;
using AppStudentiCommon.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Interface
{
    public interface ITeachersManager
    {
        ResultData<TeachersViewModel> GetTeachersBySearch(TeachersSearch s);
        List<TeachersViewModel> GetAllTeacher();
        TeachersViewModel GetTeacherById(int id);
        List<TeachersViewModel> GetTeacherByName(string name);
        DtoCommandResult InsertTeacher(TeachersViewModel entity);
        DtoCommandResult UpdateTeacher(TeachersViewModel entity);
        DtoCommandResult DeleteTeacher(int id);
    }
}
