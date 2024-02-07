// http.service.ts

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private _domain = 'http://localhost:5159';

  private readonly _friendlyErrorMessage = 'Oops! Something went wrong. Let\'s try again.';

  constructor(private httpClient: HttpClient) { }

  makeRequestWithoutBody<T>(resource: string, httpMethod: string): Observable<T> {
    return this.makeRequest<T>(resource, httpMethod);
  }

  makeRequestWithBody<T>(resource: string, httpMethod: string, body: any): Observable<T> {
    return this.makeRequest<T>(resource, httpMethod, body);
  }

  private makeRequest<T>(resource: string, httpMethod: string, body?: any): Observable<T> {
    const url = `${this._domain}/${resource}`;
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body
    };

    return this.httpClient.request<T>(httpMethod, url, options)
      .pipe(
        catchError((error: HttpErrorResponse) => this.handleErrorResponse(error))
      );
  }

  private handleErrorResponse(error: HttpErrorResponse): Observable<any> {
    console.error('An error occurred:', error);

    if (error.error && error.error instanceof Object) {
      return throwError(() => ({ errorMessage: error.error.errorMessage, statusCode: error.status }));
    }

    return throwError(() => ({ errorMessage: this._friendlyErrorMessage, statusCode: error.status }));
  }


}
