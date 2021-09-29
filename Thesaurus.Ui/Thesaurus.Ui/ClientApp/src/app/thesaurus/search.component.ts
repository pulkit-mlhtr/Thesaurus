import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SearchApiService } from './shared/search.api.service';

@Component({
  selector: 'thesaurus-search',
  templateUrl: './search.component.html',
})
export class SearchComponent implements OnInit {

  constructor(public service: SearchApiService) { }

    ngOnInit(): void {
        //throw new Error('Method not implemented.');
  }

  onSubmit(form: NgForm) {
    this.service.SearchWord();
  };

  GetWord(word: string) {
    this.service.SearchWord();
  };

  Get(currentPage: number, limit: number) {
    this.service.GetAll(currentPage, limit);
  }
}
