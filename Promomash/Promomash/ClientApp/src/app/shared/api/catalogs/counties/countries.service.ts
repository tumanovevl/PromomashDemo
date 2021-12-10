import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, first } from 'rxjs/operators';
import { ApiRoutingService } from '../../api-routing/api-routing.service';
import { QueryHelper } from '../../models/query/query.helper';

import { CountryListVm } from './models/countryListVm';
import { GetFilteredCountryListQuery } from './models/getFilteredCountryListQuery';

@Injectable()
export class CountriesService {
  private servicePath: string;
  constructor(private httpClient: HttpClient, private apiRoutingService: ApiRoutingService) {
    this.servicePath = `${apiRoutingService.getBasePath()}/Country`;
  }
  GetFilteredCountryList(query: GetFilteredCountryListQuery): Observable<CountryListVm> {    
    return this.httpClient
      .post<CountryListVm>(`${this.servicePath}/GetFilteredCountryList`, query)
      .pipe(first());
  }
}