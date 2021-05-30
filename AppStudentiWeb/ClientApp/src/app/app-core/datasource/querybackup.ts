
export class QueryBackup { 

  public static backup(search: any, page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): string {
    const qb = { search: JSON.stringify(search), page: page, pagesize: pagesize, orderbycolumn: orderbycolumn, orderbydirection: orderbydirection };
    const param = window.btoa(JSON.stringify(qb));
    return param;
  }

  public static restore(param: string): any {
    const qb = JSON.parse(window.atob(param));
    const p = { search: JSON.parse(qb.search), page: qb.page, pagesize: qb.pagesize, orderbycolumn: qb.orderbycolumn, orderbydirection: qb.orderbydirection };
    return p;
  }

}
