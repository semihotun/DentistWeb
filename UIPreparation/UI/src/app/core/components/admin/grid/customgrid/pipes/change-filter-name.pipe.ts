import { Pipe, PipeTransform } from '@angular/core';
import { FilterModel } from '../models/filterModel';
import { FilterOperators } from '../models/filterOperators';

@Pipe({
  name: 'changeFilterName'
})
export class ChangeFilterNamePipe implements PipeTransform {

  filterOperators:FilterOperators;
  
  transform(value: FilterModel, ...args: unknown[]): unknown {
    return value.propertyName+" "+FilterOperators[value.filterType]+" "+value.filter;
  }

}
