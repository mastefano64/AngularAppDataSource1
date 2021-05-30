
export class Popup {

  public static confirmDelete(): boolean {
    let valret = confirm("Sei sicuro di voler annullare?"); 
    return valret;
  }

}
