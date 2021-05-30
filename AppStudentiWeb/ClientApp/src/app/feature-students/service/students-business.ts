import { Injectable } from "@angular/core";

import { StudentsViewModel } from "../../app-core/model/viewmodel/students";
import { ValidateMode, ValidateStatus } from "../../app-core/error/errorvalidate";
import { Utility } from "../../app-core/utility";

@Injectable()
export class StudentsBusiness {
  constructor() {}

  validate(mode: ValidateMode, vm: StudentsViewModel): ValidateStatus {
    let status = new ValidateStatus();

    if (vm.name === "qwe1") {
      status.addErrorMessage("Name/1 not valid!");
      return status;
    }

    return status;   
  }
}
