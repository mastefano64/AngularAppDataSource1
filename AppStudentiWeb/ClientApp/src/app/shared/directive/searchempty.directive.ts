import { Directive, Input, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[app-SearchButton]'
})
export class SearchButtonDirective {
  @Input('app-SearchButton') set isempty(value: boolean) {
    this.render(value);
  }

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  private render(value: boolean): void {
    if (value === false)
      this.renderer.addClass(this.el.nativeElement, 'searchActive');
    else this.renderer.removeClass(this.el.nativeElement, 'searchActive');
  }
}
