import { Component } from '@angular/core';

@Component({
  selector: 'HelloWorld',
  template: '<h1>Page Says: {{text}}</h1>'
})
export class AppComponent { text = 'Hello, World'; }