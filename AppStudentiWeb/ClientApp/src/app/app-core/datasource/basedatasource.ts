import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";

import { PagingData } from "./pagingdata";
import { EventDispatcher, IEvent } from "../events";
import { QueryBackup } from "./querybackup";

export class BaseDataSource<J, K, Z> implements DataSource<K>, IBaseDataSource {
  private page = 0;
  private minpage = -1;
  private maxpage = -1;
  private pagesize = 25;
  private orderbycolumn = '';
  private orderbydirection = '';
  private responseSubject = new BehaviorSubject<K[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();
  public datetime = new Date();
  public result: any;
  public count = -1;

  private loaded: EventDispatcher<BaseDataSource<J, K, Z>, any> = new EventDispatcher<BaseDataSource<J, K, Z>, any>();

  constructor(protected service: Z, public search?: J) {}

  connect(collectionViewer: CollectionViewer): Observable<K[]> {
    return this.responseSubject.asObservable();
  }

  //

  get currentPage(): number {
    return this.page;
  }

  get pageSize(): number {
    return this.pagesize;
  }

  get orderbyColumn(): string {
    return this.orderbycolumn;
  }

  get orderbyDirection(): string {
    return this.orderbydirection;
  }

  //

  get hasFirstPage(): boolean {
    let value: boolean = false;
    if (this.page !== this.minpage)
      value = true;
    return value;
  }

  get hasPrevPage(): boolean {
    let value: boolean = false;
    if (this.minpage != -1 && this.page > this.minpage)
      value = true;
    return value;
  }

  get hasNextPage(): boolean {
    let value: boolean = false;
    if (this.maxpage != -1 && this.page < this.maxpage)
      value = true;
    return value;
  }

  get hasLastPage(): boolean {
    let value: boolean = false;
    if (this.page !== this.maxpage)
      value = true;
    return value;
  }

  get firstPage(): number {
    return this.minpage;
  }

  get prevPage(): number {
    let value = -1;
    if (this.minpage != -1 && this.page > this.minpage)
      value = this.page - 1;
    return value;
  }

  get nextPage(): number {
    let value = -1;
    if (this.maxpage != -1 && this.page < this.maxpage)
      value = this.page + 1;
    return value;
  }

  get lastPage(): number {
    return this.maxpage;
  }

  //

  get onDataLoaded(): IEvent<BaseDataSource<J, K, Z>, any> {
    return this.loaded;
  }

  queryBackup(): string {
    return QueryBackup.backup(this.search, this.page, this.pagesize, this.orderbycolumn, this.orderbydirection);
  }

  loadPaggedData(page: number, pagesize: number, orderbycolumn: string, orderbydirection = "asc"): void {
    this.loadingSubject.next(true);

    (<any>this.service).fetchData(this.search, page, pagesize, orderbycolumn, orderbydirection).pipe(
      catchError(() => of([])),
      finalize(() => this.loadingSubject.next(false))
    ).subscribe(response => {
      this.page = page;
      this.pagesize = pagesize;
      this.orderbydirection = orderbydirection;
      this.orderbycolumn = orderbycolumn;
      this.setPageCount(response.count);
      this.responseSubject.next(response.items);
      this.result = response;
      this.datetime = new Date();
      this.dispatchDataLoaded();
    });
  }

  gotoFirstPage(): boolean {
    if (this.hasFirstPage === false)
      return false;
    this.loadPaggedData(this.firstPage, this.pagesize, this.orderbycolumn, this.orderbydirection);
    return true;
  }

  gotoPrevPage(): boolean {
    if (this.hasPrevPage === false)
      return false;
    this.loadPaggedData(this.prevPage, this.pagesize, this.orderbycolumn, this.orderbydirection);
    return true;
  }

  gotoNextPage(): boolean {
    if (this.hasNextPage === false)
      return false;
    this.loadPaggedData(this.nextPage, this.pagesize, this.orderbycolumn, this.orderbydirection);
    return true;
  }

  gotoLastPage(): boolean {
    if (this.hasLastPage === false)
      return false;
    this.loadPaggedData(this.lastPage, this.pagesize, this.orderbycolumn, this.orderbydirection);
    return true;
  }

  refresh(): void {
    if (this.minpage === -1 && this.maxpage === -1)
      return;
    this.loadPaggedData(this.page, this.pagesize, this.orderbycolumn, this.orderbydirection);
  }

  private setPageCount(count: number): void {
    if (count === 0) {
      this.count = 0;
      this.minpage = 0;
      this.maxpage = 0;
      return;
    }
    this.count = count;
    let value = this.count / this.pagesize;
    this.minpage = 0;
    this.maxpage = Math.ceil(value) - 1;
  }

  private dispatchDataLoaded(): void {
    let pd = new PagingData();
    pd.page = this.page;
    pd.minPage = this.minpage;
    pd.maxPage = this.maxpage;
    pd.count = this.count;
    pd.hasFirstPage = this.hasFirstPage;
    pd.hasPrevPage = this.hasPrevPage;
    pd.hasNextPage = this.hasNextPage;
    pd.hasLastPage = this.hasLastPage;
    this.loaded.dispatch(this, pd);
  }

  //

  disconnect(collectionViewer: CollectionViewer): void {
    this.responseSubject.complete();
    this.loadingSubject.complete();
  }
}

export interface IBaseDataSource {
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
}
