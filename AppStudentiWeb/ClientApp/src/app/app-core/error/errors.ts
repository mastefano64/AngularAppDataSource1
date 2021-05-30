import { ICommandResult } from "./errorvalidate";

export class Errors {

  public static showErrorIfNedded(result: ICommandResult): void {
    if (result.hasError === true && result.showAlertError ===  true) {
      alert(result.errorsMessage[0]);
    }
  }

}
