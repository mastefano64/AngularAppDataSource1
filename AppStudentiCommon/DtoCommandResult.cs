using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppStudentiCommon
{
    public class DtoCommandResult
    {
        public DtoCommandResult()
        {
            ErrorsMessage = new List<string>();
        }

        public bool HasError { get; private set; }

        public bool ShowAlertError { get; private set; }

        public List<string> ErrorsMessage { get; private set; }

        public Object Result { get; set; }

        public static DtoCommandResult CreateFromModelState(ModelStateDictionary state)
        {
            DtoCommandResult r = new DtoCommandResult();
            foreach (var modelStateKey in state.Keys)
            {
                var modelStateVal = state[modelStateKey];
                foreach (ModelError error in modelStateVal.Errors)
                {
                    r.ErrorsMessage.Add(error.ErrorMessage);
                }
            }
            r.HasError = true;
            return r;
        }

        public void AddErrorMessage(string message, bool showerror = false)
        {
            ErrorsMessage.Add(message);
            ShowAlertError = showerror;
            HasError = true;
        }
    }
}
