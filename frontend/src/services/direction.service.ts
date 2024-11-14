import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Direction from '../dao/DirectionDao'; // Replace with the correct path to your Direction model

@Injectable({
  providedIn: 'root',  // Provides the service globally
})
export class DirectionService {
  private apiUrl = 'https://localhost:44327/api/direction'; // Update with your API endpoint

  constructor(private http: HttpClient) {}

  // Get all directions
  getDirections(): Observable<any> {
    return this.http.get<any>(this.apiUrl).pipe(
      catchError(this.handleError)  // Handle errors
    );
  }

  // Get directions by client
  getDirectionsByClient(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/byclientid?id=${id}`).pipe(
      catchError(this.handleError)  // Handle errors
    );
  }

  // Get a specific direction by ID
  getDirection(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)  // Handle errors
    );
  }

  // Save a new direction
  saveDirection(direction: Direction): Observable<any> {
    return this.http.post<any>(this.apiUrl, direction).pipe(
      catchError(this.handleError)  // Handle errors
    );
  }

  // Update an existing direction
  updateDirection(direction: Direction): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}`, direction).pipe(
      catchError(this.handleError)  // Handle errors
    );
  }

  // Delete a direction by ID
  deleteDirection(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)  // Handle errors
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
