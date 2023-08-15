import { IfnullPipe } from './ifnull.pipe';

describe('IfnullPipe', () => {
  it('create an instance', () => {
    const pipe = new IfnullPipe();
    expect(pipe).toBeTruthy();
  });
});
