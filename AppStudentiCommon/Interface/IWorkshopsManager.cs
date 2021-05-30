using AppStudentiCommon.Model;
using AppStudentiCommon.Model.Search;
using AppStudentiCommon.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Interface
{
    public interface IWorkshopsManager
    {
        ResultData<WorkshopsViewModel> GetWorkshopsBySearch(WorkshopsSearch s);
        WorkshopsViewModel GetWorkshopById(int id);
        List<WorkshopStudentsViewModel> GetStudentsByWorkshop(int id);
        DtoCommandResult InsertWorkshop(WorkshopsViewModel entity);
        DtoCommandResult UpdateWorkshop(WorkshopsViewModel entity);
        DtoCommandResult DeleteWorkshop(int id);
        DtoCommandResult InsertStudentsWorkshop(int WorkshopId, int StudentId);
        DtoCommandResult DeleteStudentsWorkshop(int WorkshopId, int StudentId);
    }
}
