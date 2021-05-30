# AngularAppDataSource1

Quando si realizza un applicazione datacentric contenente: liste, form, etc; è necessario avere delle classi base da cui ereditare che si occupano dell'accesso al ai dati tramite api, onde evitare di reiventare la ruota in ogni componente.

Questo progetto di demo (tra le altre code) contiene un BaseDataSource mediante il quale è possibile inviare al server delle chiamate con funziaonalità di: paginazione, filtro ed ordinamento. Può essere bindato sia in un controllo mat-table / mat-paginator di Angular Material e sia in una tabella HTML custom con relativi bottoni di paginazione.

![example1](/exemple1.png)

![example2](/exemple2.png)

![example3](/exemple3.png)


 loading$: Observable<boolean>;
  
 loadPaggedData(page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): void;
  
 currentPage: number;
  
 pageSize: number;
  
 orderbyColumn: string;
  
 orderbyDirection: string;
  
 hasFirstPage: boolean;
  
 hasPrevPage: boolean;
  
 hasNextPage: boolean;
  
 hasLastPage: boolean;
  
 firstPage: number;
  
 prevPage: number;
  
 nextPage: number;
  
 lastPage: number;
  
 gotoFirstPage(): boolean;
  
 gotoPrevPage(): boolean;
  
 gotoNextPage(): boolean; 
  
 gotoLastPage(): boolean;
  
 refresh(): void; 
