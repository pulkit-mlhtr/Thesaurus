import { Injectable } from '@angular/core';
import { SearchRequestModel, Word } from './search.request.model';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})

export class SearchApiService {
  constructor(private http: HttpClient) { }

  readonly baseURL = 'http://localhost:56267/Thesaurus/Word/get';
  formData: SearchRequestModel = new SearchRequestModel();
  words: Word[];

  SearchWord() {    

    var response = this.http.get(this.baseURL + '/' + this.formData.word, { responseType: "json" })
      .toPromise()
      .then(res => this.words = (res as Word[]));
  };

  GetAll(currentPage: number, limit: number) {
    var response = this.http.get(this.baseURL + '?currentPage=' + currentPage +'&pageSize='+limit, { responseType: "json" })
      .toPromise()
      .then(res => this.words = (res as Word[]));
  }

  FormatResponse(res:Word[]) {
    var copy = res;    
    for (var i = 0; i < copy.length; i++) {
      if (!res.hasOwnProperty(copy[i].primary)) {
        res.push(copy[i]);
      }
      else {
        res[copy[i].primary].push(copy[i].synonyms);
      }
    }

    return res;
  };
}
