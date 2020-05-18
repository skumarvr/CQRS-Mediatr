import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'firstName' })
export class FirstNamePipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return 'no_name';
    var firstName = value.substr(0, value.indexOf(" "));
    return firstName;
  }
}
