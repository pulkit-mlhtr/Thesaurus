export class Word {
  id: string='';
  primary: string = '';
  synonyms: Map<string, string> = new Map<string,string>().set('','');
}

export class SearchRequestModel {
  currentPage: number=0;
  limit: number=10;
  word: string = '';
  isFirstPage: boolean = this.currentPage == 0 ? true : false;
  isLastPage: boolean = !this.isFirstPage;
  nextPage: string = '';
  previousPage: string = '';
}
