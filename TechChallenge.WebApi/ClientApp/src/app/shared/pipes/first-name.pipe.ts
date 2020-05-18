import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'firstName' })
export class FirstNamePipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return 'no_name';
    var index = value.indexOf(" ")
    var firstName = index == -1 ? value : value.substr(0, index);
    return firstName;
  }
}
