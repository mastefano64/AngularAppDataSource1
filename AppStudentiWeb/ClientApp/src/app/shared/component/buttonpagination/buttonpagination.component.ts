import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { IBaseDataSource } from '../../../app-core/datasource/basedatasource';

@Component({
  selector: 'app-buttonpagination',
  templateUrl: './buttonpagination.component.html',
  styleUrls: ['./buttonpagination.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ButtonPaginationComponent {
  @Input() color = "primary";
  @Input() textfirst = "First";
  @Input() textprev = "Prev";
  @Input() textof = "of";
  @Input() textnext = "Next";
  @Input() textlast = "last";
  @Input() datasource: IBaseDataSource;
  @Input() updatedate: Date;

  constructor() { }

  get page(): number {
    if (!this.datasource)
      return 0;
    return this.datasource.currentPage;
  }

  get maxPage(): number {
    if (!this.datasource)
      return 0;
    return this.datasource.lastPage;
  }

  get hasFirstPage(): boolean {
    if (!this.datasource)
      return false;
    return this.datasource.hasFirstPage;
  }

  get hasPrevPage(): boolean {
    if (!this.datasource)
      return false;
    return this.datasource.hasPrevPage;
  }

  get hasNextPage(): boolean {
    if (!this.datasource)
      return false;
    return this.datasource.hasNextPage;
  }

  get hasLastPage(): boolean {
    if (!this.datasource)
      return false;
    return this.datasource.hasLastPage;
  }

  onPagingFirst() {
    this.datasource.gotoFirstPage();
  }

  onPagingPrev() {
    this.datasource.gotoPrevPage();
  }

  onPagingNext() {  
    this.datasource.gotoNextPage();
  }

  onPagingLast() {
    this.datasource.gotoLastPage();
  }
}
