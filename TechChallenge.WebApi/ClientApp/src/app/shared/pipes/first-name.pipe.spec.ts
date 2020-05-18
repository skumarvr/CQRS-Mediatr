import { FirstNamePipe } from './first-name.pipe';

describe('Testing FirstNamePipe', () => {
  it('create an instance', () => {
    let pipe = new FirstNamePipe();
    expect(pipe).toBeTruthy();
  });

  it('Should return first name (TestUser) given full name (TestUser)', () => {
    let fullName = 'TestUser';
    let pipe = new FirstNamePipe();
    let result = pipe.transform(fullName);
    expect(result).toBe('TestUser');
  });

  it('Should return first name (Test) given full name (Test User)', () => {
    let fullName = 'Test User';
    let pipe = new FirstNamePipe();
    let result = pipe.transform(fullName);
    expect(result).toBe('Test');
  });

  it('Should return first name (Test) given full name (Test User Name)', () => {
    let fullName = 'Test User Name';
    let pipe = new FirstNamePipe();
    let result = pipe.transform(fullName);
    expect(result).toBe('Test');
  });

  it('Should return first name (no_name) given full name is empty string', () => {
    let fullName: string = '';
    let pipe = new FirstNamePipe();
    let result: string = pipe.transform(fullName);
    expect(result).toBe('no_name');
  });

  it('Should return first name (no_name) given full name is null', () => {
    let fullName: string = null;
    let pipe = new FirstNamePipe();
    let result: string = pipe.transform(fullName);
    expect(result).toBe('no_name');
  });

  it('Should return first name (no_name) given full name is undefined', () => {
    let fullName: string = undefined;
    let pipe = new FirstNamePipe();
    let result: string = pipe.transform(fullName);
    expect(result).toBe('no_name');
  });
});
