import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, first } from 'rxjs/operators';
import { ApiRoutingService } from '../../api-routing/api-routing.service';
import { QueryHelper } from '../../models/query/query.helper';

import { GetFilteredProvinceListQuery } from './models/getFilteredProvinceListQuery';
import { ProvinceListVm } from './models/provinceListVm';

@Injectable()
export class ProvincesService {
  private servicePath: string;
  constructor(private httpClient: HttpClient, private apiRoutingService: ApiRoutingService) {
    this.servicePath = `${apiRoutingService.getBasePath()}/Province`;
  }
  GetFilteredProvinceList(query: GetFilteredProvinceListQuery): Observable<ProvinceListVm> {    
    return this.httpClient
      .post<ProvinceListVm>(`${this.servicePath}/GetFilteredProvinceList`, query)
      .pipe(first());
  }
}