import { IfemptyPipe } from './ifempty.pipe';

describe('IfemptyPipe', () => {
  it('create an instance', () => {
    const pipe = new IfemptyPipe();
    expect(pipe).toBeTruthy();
  });
});
