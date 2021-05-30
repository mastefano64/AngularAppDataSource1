import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { IBaseDataSource } from '../../../app-core/datasource/basedatasource';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-tablesortcol',
  templateUrl: './tablesortcol.component.html',
  styleUrls: ['./tablesortcol.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TableSortColComponent {
  @Input() colname = '';
  @Input() iconsize = '12';
  @Input() datasource: IBaseDataSource;
  @Input() updatedate: Date;
  colvisible = false;
  sub: Subscription;

  constructor() { }

  ngOnInit() {
    this.sub = this.datasource.loading$.subscribe(result => {
      if (result === false) {
        let name1 = this.colname.toLowerCase();
        let name2 = this.datasource.orderbyColumn.toLowerCase();
        if (name1 === name2) {
          this.colvisible = true;
        }
        else {
          this.colvisible = false;
        }
      }
    });
  }

  onClick() {
    let direction = this.datasource.orderbyDirection;
    if (direction) {
      if (direction.toLowerCase() === 'desc') {
        direction = 'asc';
      }
      else {
        direction = 'desc';
      }
    }
    this.datasource.loadPaggedData(0, this.datasource.pageSize, this.colname, direction);
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
