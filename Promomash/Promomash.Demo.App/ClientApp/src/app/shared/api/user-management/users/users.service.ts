import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, first } from 'rxjs/operators';
import { ApiRoutingService } from '../../api-routing/api-routing.service';
import { CreateUserCommand } from './models/createUserCommand';


@Injectable()
export class UsersService {
  private servicePath: string;
  constructor(private httpClient: HttpClient, private apiRoutingService: ApiRoutingService) {
    this.servicePath = `${apiRoutingService.getBasePath()}/User`;
  }  
  create(command: CreateUserCommand): Observable<any> {
    return this.httpClient
      .post<CreateUserCommand>(`${this.servicePath}`, command)
      .pipe(
        catchError(error => {
            return throwError(error);
        })
      );
  }
}