import { Component, Input, Output, EventEmitter, ViewChild, ElementRef, ChangeDetectionStrategy } from '@angular/core';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { merge } from "rxjs";

@Component({
  selector: 'app-searchtextbox',
  templateUrl: './searchtextbox.component.html',
  styleUrls: ['./searchtextbox.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchTextBoxComponent {
  @Input() value = "";
  @Input() placeholder = "";
  @Output() textboxkeyup = new EventEmitter<string>();
  @ViewChild('fastsearch') fastsearch: ElementRef;

  constructor() {}  

  ngAfterViewInit() {
    fromEvent(this.fastsearch.nativeElement, "keyup").pipe(
      debounceTime(150),
      distinctUntilChanged(),
      tap(() => {
        let value = this.fastsearch.nativeElement.value;
        this.textboxkeyup.emit(value);
      })
    ).subscribe();
  }

  clearTextBox() {
    this.fastsearch.nativeElement.value = "";
  }
}
