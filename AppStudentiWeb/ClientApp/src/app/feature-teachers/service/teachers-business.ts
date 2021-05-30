import { Injectable } from "@angular/core";

import { TeachersViewModel } from "../../app-core/model/viewmodel/teachers";
import { ValidateMode, ValidateStatus } from "../../app-core/error/errorvalidate";
import { Utility } from "../../app-core/utility";

@Injectable()
export class TeachersBusiness {
  constructor() {}

  validate(mode: ValidateMode, vm: TeachersViewModel): ValidateStatus {
    let status = new ValidateStatus();

    if (vm.name === "qwe1") {
      status.addErrorMessage("Name/1 not valid!");
      return status;
    }

    return status;   
  }
}
