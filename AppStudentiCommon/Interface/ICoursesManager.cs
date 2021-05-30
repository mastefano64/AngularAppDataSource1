using AppStudentiCommon.Model;
using AppStudentiCommon.Model.Search;
using AppStudentiCommon.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Interface
{
    public interface ICoursesManager
    {
        ResultData<CoursesViewModel> GetCoursesBySearch(CoursesSearch s);
        List<CoursesViewModel> GetAllCourse();
        CoursesViewModel GetCourseById(int id);
        List<CourseTeachersViewModel> GetTeachersByCourse(int id);
        List<CourseWorkshopsViewModel> GetWorkshopsByCourse(int id);
        DtoCommandResult InsertCourse(CoursesViewModel entity);
        DtoCommandResult UpdateCourse(CoursesViewModel entity);
        DtoCommandResult DeleteCourse(int id);
    }
}
