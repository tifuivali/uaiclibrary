import { UaicLibraryWebSpaPage } from './app.po';

describe('uaic-library-web-spa App', () => {
  let page: UaicLibraryWebSpaPage;

  beforeEach(() => {
    page = new UaicLibraryWebSpaPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
