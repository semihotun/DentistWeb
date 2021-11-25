import { Type } from '@angular/core';

export class DynamicComponentItem {
  constructor(public component: Type<any>, public data: any) {}
}


