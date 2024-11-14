import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import ClientDao from '../dao/ClientDao'; // Correct import without .ts extension

@Injectable({
  providedIn: 'root',  // Make sure the service is provided globally
})
export class ClientService {
  private apiUrl = 'https://localhost:44327/api/client'; // Update with your API endpoint

  constructor(private http: HttpClient) {}

  // Method to get all clients
  getClients(): Observable<any> {
    return this.http.get<any>(this.apiUrl).pipe(
      catchError(this.handleError)  // Handle error
    );
  }

  // Method to get a client by id
  getClient(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)  // Handle error
    );
  }

  // Method to save a new client
  saveClient(client: ClientDao): Observable<any> {
    return this.http.post<any>(this.apiUrl, client).pipe(
      catchError(this.handleError)  // Handle error
    );
  }

  // Method to update an existing client
  updateClient(client: ClientDao): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}`, client).pipe(  // Use client.id directly
      catchError(this.handleError)  // Handle error
    );
  }

  // Method to delete a client by id
  deleteClient(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)  // Handle error
    );
  }

  // Error handling
  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.error instanceof ErrorEvent) {
      console.error('Client-side error:', error.error.message);
    } else {
      console.error(`Server-side error: ${error.status}, body was: ${error.error}`);
    }
    return throwError('Something went wrong; please try again later.');
  }
}
